namespace SAI_Autosaver
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonOpenBackupFolder = new System.Windows.Forms.Button();
            this.buttonManualBackup = new System.Windows.Forms.Button();
            this.buttonHideWindow = new System.Windows.Forms.Button();
            this.groupGeneralSettings = new System.Windows.Forms.GroupBox();
            this.comboBackupDelay = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboSaiVersion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.groupBackupSettings = new System.Windows.Forms.GroupBox();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.textBackupPath = new System.Windows.Forms.TextBox();
            this.checkBackupIntoFolder = new System.Windows.Forms.CheckBox();
            this.checkNotifyProjectNotSaved = new System.Windows.Forms.CheckBox();
            this.checkEnableBackups = new System.Windows.Forms.CheckBox();
            this.checkRunInTray = new System.Windows.Forms.CheckBox();
            this.checkRunWithWindows = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupGeneralSettings.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBackupSettings.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.buttonOpenBackupFolder);
            this.tabPage1.Controls.Add(this.buttonManualBackup);
            this.tabPage1.Controls.Add(this.buttonHideWindow);
            this.tabPage1.Controls.Add(this.groupGeneralSettings);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelStatus);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelStatus, "labelStatus");
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStatus.Name = "labelStatus";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // buttonOpenBackupFolder
            // 
            resources.ApplyResources(this.buttonOpenBackupFolder, "buttonOpenBackupFolder");
            this.buttonOpenBackupFolder.Name = "buttonOpenBackupFolder";
            this.buttonOpenBackupFolder.TabStop = false;
            this.buttonOpenBackupFolder.UseVisualStyleBackColor = true;
            this.buttonOpenBackupFolder.Click += new System.EventHandler(this.buttonOpenBackupFolder_Click);
            // 
            // buttonManualBackup
            // 
            resources.ApplyResources(this.buttonManualBackup, "buttonManualBackup");
            this.buttonManualBackup.Name = "buttonManualBackup";
            this.buttonManualBackup.TabStop = false;
            this.buttonManualBackup.UseVisualStyleBackColor = true;
            this.buttonManualBackup.Click += new System.EventHandler(this.buttonManualBackup_Click);
            // 
            // buttonHideWindow
            // 
            resources.ApplyResources(this.buttonHideWindow, "buttonHideWindow");
            this.buttonHideWindow.Name = "buttonHideWindow";
            this.buttonHideWindow.TabStop = false;
            this.buttonHideWindow.UseVisualStyleBackColor = true;
            this.buttonHideWindow.Click += new System.EventHandler(this.buttonHideWindow_Click);
            // 
            // groupGeneralSettings
            // 
            this.groupGeneralSettings.Controls.Add(this.comboBackupDelay);
            this.groupGeneralSettings.Controls.Add(this.label3);
            this.groupGeneralSettings.Controls.Add(this.comboSaiVersion);
            this.groupGeneralSettings.Controls.Add(this.label4);
            resources.ApplyResources(this.groupGeneralSettings, "groupGeneralSettings");
            this.groupGeneralSettings.Name = "groupGeneralSettings";
            this.groupGeneralSettings.TabStop = false;
            // 
            // comboBackupDelay
            // 
            this.comboBackupDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBackupDelay.FormattingEnabled = true;
            resources.ApplyResources(this.comboBackupDelay, "comboBackupDelay");
            this.comboBackupDelay.Name = "comboBackupDelay";
            this.comboBackupDelay.TabStop = false;
            this.comboBackupDelay.SelectedIndexChanged += new System.EventHandler(this.comboBackupDelay_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboSaiVersion
            // 
            this.comboSaiVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSaiVersion.FormattingEnabled = true;
            resources.ApplyResources(this.comboSaiVersion, "comboSaiVersion");
            this.comboSaiVersion.Name = "comboSaiVersion";
            this.comboSaiVersion.TabStop = false;
            this.comboSaiVersion.SelectedIndexChanged += new System.EventHandler(this.comboSaiVersion_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonSaveSettings);
            this.tabPage2.Controls.Add(this.groupBackupSettings);
            this.tabPage2.Controls.Add(this.checkEnableBackups);
            this.tabPage2.Controls.Add(this.checkRunInTray);
            this.tabPage2.Controls.Add(this.checkRunWithWindows);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonSaveSettings
            // 
            resources.ApplyResources(this.buttonSaveSettings, "buttonSaveSettings");
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.TabStop = false;
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // groupBackupSettings
            // 
            this.groupBackupSettings.Controls.Add(this.buttonSelectFolder);
            this.groupBackupSettings.Controls.Add(this.textBackupPath);
            this.groupBackupSettings.Controls.Add(this.checkBackupIntoFolder);
            this.groupBackupSettings.Controls.Add(this.checkNotifyProjectNotSaved);
            resources.ApplyResources(this.groupBackupSettings, "groupBackupSettings");
            this.groupBackupSettings.Name = "groupBackupSettings";
            this.groupBackupSettings.TabStop = false;
            // 
            // buttonSelectFolder
            // 
            resources.ApplyResources(this.buttonSelectFolder, "buttonSelectFolder");
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.TabStop = false;
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // textBackupPath
            // 
            resources.ApplyResources(this.textBackupPath, "textBackupPath");
            this.textBackupPath.Name = "textBackupPath";
            this.textBackupPath.ReadOnly = true;
            this.textBackupPath.TabStop = false;
            // 
            // checkBackupIntoFolder
            // 
            resources.ApplyResources(this.checkBackupIntoFolder, "checkBackupIntoFolder");
            this.checkBackupIntoFolder.Name = "checkBackupIntoFolder";
            this.checkBackupIntoFolder.TabStop = false;
            this.checkBackupIntoFolder.UseVisualStyleBackColor = true;
            this.checkBackupIntoFolder.CheckedChanged += new System.EventHandler(this.checkBackupIntoFolder_CheckedChanged);
            // 
            // checkNotifyProjectNotSaved
            // 
            resources.ApplyResources(this.checkNotifyProjectNotSaved, "checkNotifyProjectNotSaved");
            this.checkNotifyProjectNotSaved.Name = "checkNotifyProjectNotSaved";
            this.checkNotifyProjectNotSaved.TabStop = false;
            this.checkNotifyProjectNotSaved.UseVisualStyleBackColor = true;
            this.checkNotifyProjectNotSaved.CheckedChanged += new System.EventHandler(this.checkNotifyProjectNotSaved_CheckedChanged);
            // 
            // checkEnableBackups
            // 
            resources.ApplyResources(this.checkEnableBackups, "checkEnableBackups");
            this.checkEnableBackups.Name = "checkEnableBackups";
            this.checkEnableBackups.TabStop = false;
            this.checkEnableBackups.UseVisualStyleBackColor = true;
            this.checkEnableBackups.CheckedChanged += new System.EventHandler(this.checkEnableBackups_CheckedChanged);
            // 
            // checkRunInTray
            // 
            resources.ApplyResources(this.checkRunInTray, "checkRunInTray");
            this.checkRunInTray.Name = "checkRunInTray";
            this.checkRunInTray.TabStop = false;
            this.checkRunInTray.UseVisualStyleBackColor = true;
            this.checkRunInTray.CheckedChanged += new System.EventHandler(this.checkRunInTray_CheckedChanged);
            // 
            // checkRunWithWindows
            // 
            resources.ApplyResources(this.checkRunWithWindows, "checkRunWithWindows");
            this.checkRunWithWindows.Name = "checkRunWithWindows";
            this.checkRunWithWindows.TabStop = false;
            this.checkRunWithWindows.UseVisualStyleBackColor = true;
            this.checkRunWithWindows.CheckedChanged += new System.EventHandler(this.checkRunWithWindows_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label2);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this.exitProgramToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // sToolStripMenuItem
            // 
            resources.ApplyResources(this.sToolStripMenuItem, "sToolStripMenuItem");
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // exitProgramToolStripMenuItem
            // 
            this.exitProgramToolStripMenuItem.Name = "exitProgramToolStripMenuItem";
            resources.ApplyResources(this.exitProgramToolStripMenuItem, "exitProgramToolStripMenuItem");
            this.exitProgramToolStripMenuItem.Click += new System.EventHandler(this.exitProgramToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupGeneralSettings.ResumeLayout(false);
            this.groupGeneralSettings.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBackupSettings.ResumeLayout(false);
            this.groupBackupSettings.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonOpenBackupFolder;
        private System.Windows.Forms.Button buttonManualBackup;
        private System.Windows.Forms.Button buttonHideWindow;
        private System.Windows.Forms.GroupBox groupGeneralSettings;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.GroupBox groupBackupSettings;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.TextBox textBackupPath;
        private System.Windows.Forms.CheckBox checkBackupIntoFolder;
        private System.Windows.Forms.CheckBox checkNotifyProjectNotSaved;
        private System.Windows.Forms.CheckBox checkEnableBackups;
        private System.Windows.Forms.CheckBox checkRunInTray;
        private System.Windows.Forms.CheckBox checkRunWithWindows;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboSaiVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBackupDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
    }
}

