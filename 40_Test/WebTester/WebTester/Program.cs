using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WebTester
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Form form = new EntranceForm();
            form.FormClosed += bye;
            Application.Run(form);
        }

        static void bye(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("bye");
        }
    }
}
