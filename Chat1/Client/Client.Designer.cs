namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Send = new System.Windows.Forms.Button();
            this.File = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.Friend = new System.Windows.Forms.TextBox();
            this.tbMess = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 63);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(527, 375);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(464, 454);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 45);
            this.Send.TabIndex = 3;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // File
            // 
            this.File.Location = new System.Drawing.Point(386, 454);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(75, 45);
            this.File.TabIndex = 4;
            this.File.Text = "File";
            this.File.UseVisualStyleBackColor = true;
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(451, 12);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(90, 45);
            this.Connect.TabIndex = 5;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "User name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(240, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Friend";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(118, 22);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 26);
            this.Username.TabIndex = 8;
            // 
            // Friend
            // 
            this.Friend.Location = new System.Drawing.Point(321, 22);
            this.Friend.Name = "Friend";
            this.Friend.Size = new System.Drawing.Size(100, 26);
            this.Friend.TabIndex = 9;
            // 
            // tbMess
            // 
            this.tbMess.Location = new System.Drawing.Point(12, 454);
            this.tbMess.Multiline = true;
            this.tbMess.Name = "tbMess";
            this.tbMess.Size = new System.Drawing.Size(368, 45);
            this.tbMess.TabIndex = 10;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 513);
            this.Controls.Add(this.tbMess);
            this.Controls.Add(this.Friend);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.File);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Button File;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Friend;
        private System.Windows.Forms.TextBox tbMess;
    }
}

