namespace Chat
{
    partial class SearchFriend
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
            this.name = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.ip = new System.Windows.Forms.Label();
            this.ipText = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.userList = new System.Windows.Forms.DataGridView();
            this.Nickname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.userList)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(44, 33);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(38, 13);
            this.name.TabIndex = 0;
            this.name.Text = "Name:";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(88, 30);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(100, 20);
            this.nameText.TabIndex = 1;
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Location = new System.Drawing.Point(62, 63);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(20, 13);
            this.ip.TabIndex = 2;
            this.ip.Text = "IP:";
            // 
            // ipText
            // 
            this.ipText.Location = new System.Drawing.Point(88, 60);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(100, 20);
            this.ipText.TabIndex = 3;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(97, 86);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(97, 227);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 5;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            // 
            // userList
            // 
            this.userList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nickname,
            this.IPAddress});
            this.userList.Location = new System.Drawing.Point(12, 115);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(245, 106);
            this.userList.TabIndex = 6;
            // 
            // Nickname
            // 
            this.Nickname.HeaderText = "Nickname";
            this.Nickname.Name = "Nickname";
            // 
            // IPAddress
            // 
            this.IPAddress.HeaderText = "IP Address";
            this.IPAddress.Name = "IPAddress";
            // 
            // SearchFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.name);
            this.Name = "SearchFriend";
            this.Text = "SearchFriend";
            ((System.ComponentModel.ISupportInitialize)(this.userList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.DataGridView userList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nickname;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAddress;
    }
}