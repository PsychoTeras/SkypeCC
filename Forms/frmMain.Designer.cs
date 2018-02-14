using SkypeCC.Controls;

namespace SkypeCC.Forms
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.cbProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbUsers = new SkypeCC.Controls.ListBoxCC();
            this.tsMessages = new System.Windows.Forms.ToolStrip();
            this.btnMessagesDeleteSelected = new System.Windows.Forms.ToolStripButton();
            this.btnMessageEdit = new System.Windows.Forms.ToolStripButton();
            this.btnMessagesClearAll = new System.Windows.Forms.ToolStripButton();
            this.lvMessages = new SkypeCC.Controls.ListViewCC();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilMessages = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tsMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(12, 12);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.cbProfile);
            this.scMain.Panel1.Controls.Add(this.label1);
            this.scMain.Panel1.Controls.Add(this.lbUsers);
            this.scMain.Panel1MinSize = 270;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tsMessages);
            this.scMain.Panel2.Controls.Add(this.lvMessages);
            this.scMain.Size = new System.Drawing.Size(1013, 738);
            this.scMain.SplitterDistance = 270;
            this.scMain.SplitterWidth = 6;
            this.scMain.TabIndex = 2;
            // 
            // cbProfile
            // 
            this.cbProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProfile.FormattingEnabled = true;
            this.cbProfile.Location = new System.Drawing.Point(51, 0);
            this.cbProfile.Name = "cbProfile";
            this.cbProfile.Size = new System.Drawing.Size(219, 21);
            this.cbProfile.TabIndex = 3;
            this.cbProfile.SelectedIndexChanged += new System.EventHandler(this.CbProfileSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profile:";
            // 
            // lbUsers
            // 
            this.lbUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUsers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.IntegralHeight = false;
            this.lbUsers.Location = new System.Drawing.Point(0, 27);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(270, 711);
            this.lbUsers.TabIndex = 0;
            this.lbUsers.SelectedIndexChanged += new System.EventHandler(this.lbUsers_SelectedIndexChanged);
            // 
            // tsMessages
            // 
            this.tsMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMessages.AutoSize = false;
            this.tsMessages.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMessages.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMessagesDeleteSelected,
            this.btnMessageEdit,
            this.btnMessagesClearAll});
            this.tsMessages.Location = new System.Drawing.Point(0, -1);
            this.tsMessages.Name = "tsMessages";
            this.tsMessages.Size = new System.Drawing.Size(733, 25);
            this.tsMessages.TabIndex = 1;
            this.tsMessages.Text = "toolStrip1";
            // 
            // btnMessagesDeleteSelected
            // 
            this.btnMessagesDeleteSelected.Image = ((System.Drawing.Image)(resources.GetObject("btnMessagesDeleteSelected.Image")));
            this.btnMessagesDeleteSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMessagesDeleteSelected.Name = "btnMessagesDeleteSelected";
            this.btnMessagesDeleteSelected.Size = new System.Drawing.Size(106, 22);
            this.btnMessagesDeleteSelected.Text = "Delete selected";
            this.btnMessagesDeleteSelected.Click += new System.EventHandler(this.btnMessagesDeleteSelected_Click);
            // 
            // btnMessageEdit
            // 
            this.btnMessageEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnMessageEdit.Image")));
            this.btnMessageEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMessageEdit.Name = "btnMessageEdit";
            this.btnMessageEdit.Size = new System.Drawing.Size(93, 22);
            this.btnMessageEdit.Text = "Edit selected";
            this.btnMessageEdit.Click += new System.EventHandler(this.btnMessageEdit_Click);
            // 
            // btnMessagesClearAll
            // 
            this.btnMessagesClearAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnMessagesClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnMessagesClearAll.Image")));
            this.btnMessagesClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMessagesClearAll.Name = "btnMessagesClearAll";
            this.btnMessagesClearAll.Size = new System.Drawing.Size(75, 22);
            this.btnMessagesClearAll.Text = "Delete all";
            this.btnMessagesClearAll.Click += new System.EventHandler(this.btnMessagesClearAll_Click);
            // 
            // lvMessages
            // 
            this.lvMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMessages.CheckBoxes = true;
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvMessages.FullRowSelect = true;
            this.lvMessages.GridLines = true;
            this.lvMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMessages.LabelWrap = false;
            this.lvMessages.Location = new System.Drawing.Point(0, 27);
            this.lvMessages.MultiSelect = false;
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.OwnerDraw = true;
            this.lvMessages.Size = new System.Drawing.Size(733, 711);
            this.lvMessages.SmallImageList = this.ilMessages;
            this.lvMessages.TabIndex = 0;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            this.lvMessages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMessages_KeyDown);
            this.lvMessages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMessages_MouseDoubleClick);
            this.lvMessages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMessages_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 36;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Message";
            this.columnHeader3.Width = 553;
            // 
            // ilMessages
            // 
            this.ilMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMessages.ImageStream")));
            this.ilMessages.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMessages.Images.SetKeyName(0, "bullet_blue.png");
            this.ilMessages.Images.SetKeyName(1, "bullet_black.png");
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 762);
            this.Controls.Add(this.scMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SkypeCC";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel1.PerformLayout();
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.tsMessages.ResumeLayout(false);
            this.tsMessages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer scMain;
        private ListBoxCC lbUsers;
        private System.Windows.Forms.ComboBox cbProfile;
        private System.Windows.Forms.Label label1;
        private ListViewCC lvMessages;
        private System.Windows.Forms.ToolStrip tsMessages;
        private System.Windows.Forms.ToolStripButton btnMessagesDeleteSelected;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList ilMessages;
        private System.Windows.Forms.ToolStripButton btnMessagesClearAll;
        private System.Windows.Forms.ToolStripButton btnMessageEdit;
    }
}

