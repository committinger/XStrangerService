namespace WebTester
{
    partial class EntranceForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tcTest = new System.Windows.Forms.TabControl();
            this.tbWebGet = new System.Windows.Forms.TabPage();
            this.tbGetResponse = new System.Windows.Forms.TextBox();
            this.tbPost = new System.Windows.Forms.TabPage();
            this.tlpRR = new System.Windows.Forms.TableLayoutPanel();
            this.tbPostResponse = new System.Windows.Forms.TextBox();
            this.tbPostRequest = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tbState = new System.Windows.Forms.TextBox();
            this.tcTest.SuspendLayout();
            this.tbWebGet.SuspendLayout();
            this.tbPost.SuspendLayout();
            this.tlpRR.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTest
            // 
            this.tcTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTest.Controls.Add(this.tbWebGet);
            this.tcTest.Controls.Add(this.tbPost);
            this.tcTest.Location = new System.Drawing.Point(13, 39);
            this.tcTest.Name = "tcTest";
            this.tcTest.SelectedIndex = 0;
            this.tcTest.Size = new System.Drawing.Size(599, 362);
            this.tcTest.TabIndex = 0;
            // 
            // tbWebGet
            // 
            this.tbWebGet.Controls.Add(this.tbGetResponse);
            this.tbWebGet.Location = new System.Drawing.Point(4, 22);
            this.tbWebGet.Name = "tbWebGet";
            this.tbWebGet.Padding = new System.Windows.Forms.Padding(3);
            this.tbWebGet.Size = new System.Drawing.Size(591, 336);
            this.tbWebGet.TabIndex = 0;
            this.tbWebGet.Text = "Get";
            this.tbWebGet.UseVisualStyleBackColor = true;
            // 
            // tbGetResponse
            // 
            this.tbGetResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGetResponse.Location = new System.Drawing.Point(3, 3);
            this.tbGetResponse.Multiline = true;
            this.tbGetResponse.Name = "tbGetResponse";
            this.tbGetResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGetResponse.Size = new System.Drawing.Size(585, 330);
            this.tbGetResponse.TabIndex = 4;
            // 
            // tbPost
            // 
            this.tbPost.Controls.Add(this.tlpRR);
            this.tbPost.Location = new System.Drawing.Point(4, 22);
            this.tbPost.Name = "tbPost";
            this.tbPost.Padding = new System.Windows.Forms.Padding(3);
            this.tbPost.Size = new System.Drawing.Size(591, 336);
            this.tbPost.TabIndex = 1;
            this.tbPost.Text = "Post";
            this.tbPost.UseVisualStyleBackColor = true;
            // 
            // tlpRR
            // 
            this.tlpRR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRR.ColumnCount = 2;
            this.tlpRR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRR.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRR.Controls.Add(this.tbPostResponse, 1, 0);
            this.tlpRR.Controls.Add(this.tbPostRequest, 0, 0);
            this.tlpRR.Location = new System.Drawing.Point(0, 0);
            this.tlpRR.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRR.Name = "tlpRR";
            this.tlpRR.RowCount = 1;
            this.tlpRR.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRR.Size = new System.Drawing.Size(591, 336);
            this.tlpRR.TabIndex = 1;
            // 
            // tbPostResponse
            // 
            this.tbPostResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPostResponse.Location = new System.Drawing.Point(298, 3);
            this.tbPostResponse.Multiline = true;
            this.tbPostResponse.Name = "tbPostResponse";
            this.tbPostResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPostResponse.Size = new System.Drawing.Size(290, 330);
            this.tbPostResponse.TabIndex = 4;
            // 
            // tbPostRequest
            // 
            this.tbPostRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPostRequest.Location = new System.Drawing.Point(3, 3);
            this.tbPostRequest.Multiline = true;
            this.tbPostRequest.Name = "tbPostRequest";
            this.tbPostRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPostRequest.Size = new System.Drawing.Size(289, 330);
            this.tbPostRequest.TabIndex = 3;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(452, 407);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(533, 407);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Location = new System.Drawing.Point(13, 12);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(599, 21);
            this.tbUrl.TabIndex = 6;
            // 
            // tbState
            // 
            this.tbState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbState.Enabled = false;
            this.tbState.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbState.Location = new System.Drawing.Point(12, 409);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(23, 21);
            this.tbState.TabIndex = 7;
            // 
            // EntranceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.tbState);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tcTest);
            this.Name = "EntranceForm";
            this.Text = "WebTester";
            this.tcTest.ResumeLayout(false);
            this.tbWebGet.ResumeLayout(false);
            this.tbWebGet.PerformLayout();
            this.tbPost.ResumeLayout(false);
            this.tlpRR.ResumeLayout(false);
            this.tlpRR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcTest;
        private System.Windows.Forms.TabPage tbWebGet;
        private System.Windows.Forms.TextBox tbGetResponse;
        private System.Windows.Forms.TabPage tbPost;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TableLayoutPanel tlpRR;
        private System.Windows.Forms.TextBox tbPostResponse;
        private System.Windows.Forms.TextBox tbPostRequest;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TextBox tbState;
    }
}

