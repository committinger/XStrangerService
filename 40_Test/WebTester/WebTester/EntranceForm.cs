using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace WebTester
{
    public partial class EntranceForm : Form
    {
        #region 常量
        private const string constantConfigFileName = "config.txt";
        private const string constantConfigSetSign = "=";
        private const string constantConfigUrl = "URL";
        #endregion

        #region 私有成员
        WebClient client;
        #endregion

        #region 构造方法
        public EntranceForm()
        {
            InitializeComponent();
            load();
        }
        #endregion

        #region 初始化

        private void load()
        {
            setBinding();
            setDefaultValue();
            setStateFree();
            loadCfg();
        }

        private void setBinding()
        {
            this.FormClosing += EntranceForm_FormClosing;
        }

        private void setDefaultValue()
        {
            this.tcTest.SelectTab(0);
        }

        private void loadCfg()
        {
            try
            {
                string[] cfgStrings = File.ReadAllLines(constantConfigFileName, Encoding.UTF8);
                this.tbUrl.Text = findCfg(constantConfigUrl, constantConfigSetSign, cfgStrings);
            }
            catch
            {
                MessageBox.Show("Config获取失败");
            }
        }

        private string findCfg(string cfgName, string setSign, string[] cfgs)
        {
            if (string.IsNullOrEmpty(cfgName) || string.IsNullOrEmpty(setSign) || cfgs == null) return null;
            int index = Array.FindIndex(cfgs, t => t != null && t.Trim().StartsWith(constantConfigUrl));
            if (index == -1)
                return null;
            return cfgs[index].Trim().Substring(cfgName.Length).TrimStart().Substring(setSign.Length).TrimStart();
        }

        #endregion

        #region 界面Handler

        void EntranceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= EntranceForm_FormClosing;
            StringBuilder configBuilder = new StringBuilder();
            configBuilder.AppendLine().Append(constantConfigUrl).Append(constantConfigSetSign).Append(tbUrl.Text);
            try
            {
                File.WriteAllText(constantConfigFileName, configBuilder.ToString(), Encoding.UTF8);
            }
            catch { }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (client != null)
                return;
            try
            {
                clientInvoke();
            }
            catch (Exception ex)
            {
                client = null;
                setStateFree();
                noticeException(ex);
            }
        }

        #endregion

        #region 回调Handler

        void client_PostCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    this.tbPostResponse.Text = e.Result;
                }
            }
            catch (Exception ex)
            {
                noticeException(ex);
            }
            client.UploadStringCompleted -= client_PostCompleted;
            client.Dispose();
            client = null;
            setStateFree();
        }

        void client_GetCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e != null)
                {

                    this.tbGetResponse.Text = e.Result.ToString();
                }
            }
            catch (Exception ex)
            {
                noticeException(ex);
            }
            client.DownloadStringCompleted -= client_GetCompleted;
            client.Dispose();
            client = null;
            setStateFree();
        }

        #endregion

        #region 界面公共方法

        private void noticeException(Exception ex)
        {
            MessageBox.Show(new StringBuilder(ex.Message).AppendLine(ex.StackTrace).ToString());
        }

        private void setStateBusy()
        {
            this.tbState.BackColor = Color.Red;
        }

        private void setStateFree()
        {
            this.tbState.BackColor = Color.Lime;
        }

        #endregion

        #region 后台方法

        private void clientInvoke()
        {
            switch (tcTest.SelectedIndex)
            {
                case 0:
                    webGet();
                    break;
                case 1:
                    webPost();
                    break;
            }
        }

        private void webPost()
        {
            try
            {
                setStateBusy();
                client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string url = tbUrl.Text;
                string data = tbPostRequest.Text;
                client.UploadStringCompleted += client_PostCompleted;
                client.UploadStringAsync(new Uri(url), data);
            }
            catch (Exception ex)
            {
                client.UploadStringCompleted -= client_PostCompleted;
                client.Dispose();
                client = null;
                setStateFree();
                noticeException(ex);
            }
        }

        private void webGet()
        {
            try
            {
                setStateBusy();
                client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.DownloadStringCompleted += client_GetCompleted;
                string url = tbUrl.Text;
                client.DownloadStringAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                client.DownloadStringCompleted -= client_GetCompleted;
                client.Dispose();
                client = null;
                setStateFree();
                noticeException(ex);
            }
        }

        #endregion
    }
}
