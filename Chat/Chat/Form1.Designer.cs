namespace Chat
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Online");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Offline");
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.StatusBtn = new System.Windows.Forms.ToolStripSplitButton();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGroup = new System.Windows.Forms.ToolStripButton();
            this.addFriend = new System.Windows.Forms.ToolStripButton();
            this.UsersTree = new System.Windows.Forms.TreeView();
            this.ChatViewer = new System.Windows.Forms.TextBox();
            this.MesBox = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsBtn,
            this.StatusBtn,
            this.AddGroup,
            this.addFriend});
            this.toolStrip1.Location = new System.Drawing.Point(300, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(144, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(23, 22);
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // StatusBtn
            // 
            this.StatusBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StatusBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.offlineToolStripMenuItem});
            this.StatusBtn.Image = ((System.Drawing.Image)(resources.GetObject("StatusBtn.Image")));
            this.StatusBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StatusBtn.Name = "StatusBtn";
            this.StatusBtn.Size = new System.Drawing.Size(32, 22);
            this.StatusBtn.Text = "Status";
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            this.offlineToolStripMenuItem.Click += new System.EventHandler(this.offlineToolStripMenuItem_Click);
            // 
            // AddGroup
            // 
            this.AddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddGroup.Image = ((System.Drawing.Image)(resources.GetObject("AddGroup.Image")));
            this.AddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddGroup.Name = "AddGroup";
            this.AddGroup.Size = new System.Drawing.Size(23, 22);
            this.AddGroup.Text = "Add Group";
            this.AddGroup.Click += new System.EventHandler(this.AddGroup_Click);
            // 
            // addFriend
            // 
            this.addFriend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addFriend.Image = ((System.Drawing.Image)(resources.GetObject("addFriend.Image")));
            this.addFriend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(23, 22);
            this.addFriend.Text = "AddFriend";
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // UsersTree
            // 
            this.UsersTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UsersTree.Location = new System.Drawing.Point(300, 37);
            this.UsersTree.Name = "UsersTree";
            treeNode3.Name = "OnlineUsers";
            treeNode3.Text = "Online";
            treeNode4.Name = "OfflineUsers";
            treeNode4.Text = "Offline";
            this.UsersTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.UsersTree.Size = new System.Drawing.Size(180, 290);
            this.UsersTree.TabIndex = 2;
            // 
            // ChatViewer
            // 
            this.ChatViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChatViewer.Location = new System.Drawing.Point(3, 72);
            this.ChatViewer.Multiline = true;
            this.ChatViewer.Name = "ChatViewer";
            this.ChatViewer.Size = new System.Drawing.Size(291, 187);
            this.ChatViewer.TabIndex = 3;
            // 
            // MesBox
            // 
            this.MesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MesBox.Location = new System.Drawing.Point(3, 265);
            this.MesBox.Name = "MesBox";
            this.MesBox.Size = new System.Drawing.Size(291, 20);
            this.MesBox.TabIndex = 4;
            // 
            // SendBtn
            // 
            this.SendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendBtn.Location = new System.Drawing.Point(219, 291);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 5;
            this.SendBtn.Text = "Отправить";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Location = new System.Drawing.Point(12, 291);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(75, 23);
            this.MinimizeBtn.TabIndex = 6;
            this.MinimizeBtn.Text = "Свернуть";
            this.MinimizeBtn.UseVisualStyleBackColor = true;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 326);
            this.Controls.Add(this.MinimizeBtn);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.MesBox);
            this.Controls.Add(this.ChatViewer);
            this.Controls.Add(this.UsersTree);
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton settingsBtn;
        private System.Windows.Forms.TreeView UsersTree;
        private System.Windows.Forms.TextBox ChatViewer;
        private System.Windows.Forms.TextBox MesBox;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.ToolStripSplitButton StatusBtn;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton AddGroup;
        private System.Windows.Forms.ToolStripButton addFriend;
        private System.Windows.Forms.Button MinimizeBtn;
    }
}

