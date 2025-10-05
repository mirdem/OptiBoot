namespace OptiBoot
{
    partial class FormServices
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStripServices;
        private System.Windows.Forms.ToolStripMenuItem menuStart;
        private System.Windows.Forms.ToolStripMenuItem menuStop;
        private System.Windows.Forms.ToolStripMenuItem menuPause;
        private System.Windows.Forms.ToolStripMenuItem menuContinue;
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServices));
            lblServiceName = new Label();
            groupBoxProcess = new GroupBox();
            btnStart = new Button();
            btnStop = new Button();
            btnPause = new Button();
            btnContinue = new Button();
            groupBoxStartupSettings = new GroupBox();
            btnSetAutomatic = new Button();
            btnSetManual = new Button();
            btnSetDisabled = new Button();
            listBoxServices = new ListBox();
            label1 = new Label();
            lblStatus = new Label();
            btnRefresh = new Button();
            txtFilter = new TextBox();
            btnExportTxt = new Button();
            lblDisplayName = new Label();
            lblDescription = new Label();
            lblStatusService = new Label();
            lblStartupType = new Label();
            lblServiceType = new Label();
            lblCanPause = new Label();
            lblCanStop = new Label();
            lblMachineName = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            groupBoxDetails = new GroupBox();
            groupBoxProcess.SuspendLayout();
            groupBoxStartupSettings.SuspendLayout();
            groupBoxDetails.SuspendLayout();
            SuspendLayout();
            // 
            // lblServiceName
            // 
            lblServiceName.AutoSize = true;
            lblServiceName.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblServiceName.Location = new Point(23, 38);
            lblServiceName.Name = "lblServiceName";
            lblServiceName.Size = new Size(13, 20);
            lblServiceName.TabIndex = 1;
            lblServiceName.Text = " ";
            // 
            // groupBoxProcess
            // 
            groupBoxProcess.Controls.Add(btnStart);
            groupBoxProcess.Controls.Add(btnStop);
            groupBoxProcess.Controls.Add(btnPause);
            groupBoxProcess.Controls.Add(btnContinue);
            groupBoxProcess.Location = new Point(462, 582);
            groupBoxProcess.Name = "groupBoxProcess";
            groupBoxProcess.Size = new Size(356, 73);
            groupBoxProcess.TabIndex = 14;
            groupBoxProcess.TabStop = false;
            groupBoxProcess.Text = "İşlemler";
            // 
            // btnStart
            // 
            btnStart.FlatAppearance.BorderColor = Color.Silver;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Location = new Point(15, 32);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 26);
            btnStart.TabIndex = 7;
            btnStart.Text = "Başlat";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.FlatAppearance.BorderColor = Color.Silver;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Location = new Point(96, 32);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 26);
            btnStop.TabIndex = 8;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnPause
            // 
            btnPause.FlatAppearance.BorderColor = Color.Silver;
            btnPause.FlatStyle = FlatStyle.Flat;
            btnPause.Location = new Point(177, 32);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(75, 26);
            btnPause.TabIndex = 9;
            btnPause.Text = "Duraklat";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            // 
            // btnContinue
            // 
            btnContinue.FlatAppearance.BorderColor = Color.Silver;
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.Location = new Point(258, 32);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(75, 26);
            btnContinue.TabIndex = 10;
            btnContinue.Text = "Devam Et";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // groupBoxStartupSettings
            // 
            groupBoxStartupSettings.Controls.Add(btnSetAutomatic);
            groupBoxStartupSettings.Controls.Add(btnSetManual);
            groupBoxStartupSettings.Controls.Add(btnSetDisabled);
            groupBoxStartupSettings.Location = new Point(462, 661);
            groupBoxStartupSettings.Name = "groupBoxStartupSettings";
            groupBoxStartupSettings.Size = new Size(356, 73);
            groupBoxStartupSettings.TabIndex = 15;
            groupBoxStartupSettings.TabStop = false;
            groupBoxStartupSettings.Text = "Başlatma Ayarları";
            // 
            // btnSetAutomatic
            // 
            btnSetAutomatic.FlatAppearance.BorderColor = Color.Silver;
            btnSetAutomatic.FlatStyle = FlatStyle.Flat;
            btnSetAutomatic.Location = new Point(15, 33);
            btnSetAutomatic.Name = "btnSetAutomatic";
            btnSetAutomatic.Size = new Size(75, 23);
            btnSetAutomatic.TabIndex = 11;
            btnSetAutomatic.Text = "Otomatik";
            btnSetAutomatic.UseVisualStyleBackColor = true;
            btnSetAutomatic.Click += btnSetAutomatic_Click;
            // 
            // btnSetManual
            // 
            btnSetManual.FlatAppearance.BorderColor = Color.Silver;
            btnSetManual.FlatStyle = FlatStyle.Flat;
            btnSetManual.Location = new Point(186, 33);
            btnSetManual.Name = "btnSetManual";
            btnSetManual.Size = new Size(75, 26);
            btnSetManual.TabIndex = 12;
            btnSetManual.Text = "Elle";
            btnSetManual.UseVisualStyleBackColor = true;
            btnSetManual.Click += btnSetManual_Click;
            // 
            // btnSetDisabled
            // 
            btnSetDisabled.FlatAppearance.BorderColor = Color.Silver;
            btnSetDisabled.FlatStyle = FlatStyle.Flat;
            btnSetDisabled.Location = new Point(105, 33);
            btnSetDisabled.Name = "btnSetDisabled";
            btnSetDisabled.Size = new Size(75, 26);
            btnSetDisabled.TabIndex = 13;
            btnSetDisabled.Text = "Devredışı";
            btnSetDisabled.UseVisualStyleBackColor = true;
            btnSetDisabled.Click += btnSetDisabled_Click;
            // 
            // listBoxServices
            // 
            listBoxServices.BackColor = Color.White;
            listBoxServices.ForeColor = Color.Black;
            listBoxServices.FormattingEnabled = true;
            listBoxServices.ItemHeight = 15;
            listBoxServices.Location = new Point(12, 85);
            listBoxServices.Name = "listBoxServices";
            listBoxServices.Size = new Size(380, 589);
            listBoxServices.TabIndex = 23;
            listBoxServices.SelectedIndexChanged += listBoxServices_SelectedIndexChanged;
            listBoxServices.MouseDown += listBoxServices_MouseDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(16, 32);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 24;
            label1.Text = "Filtre";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            lblStatus.Location = new Point(12, 680);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(53, 15);
            lblStatus.TabIndex = 19;
            lblStatus.Text = "lblStatus";
            // 
            // btnRefresh
            // 
            btnRefresh.FlatAppearance.BorderColor = Color.Silver;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Location = new Point(318, 694);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 26);
            btnRefresh.TabIndex = 20;
            btnRefresh.Text = "Yenile";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // txtFilter
            // 
            txtFilter.Location = new Point(14, 56);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(378, 23);
            txtFilter.TabIndex = 22;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // btnExportTxt
            // 
            btnExportTxt.FlatAppearance.BorderColor = Color.Silver;
            btnExportTxt.FlatStyle = FlatStyle.Flat;
            btnExportTxt.Location = new Point(237, 694);
            btnExportTxt.Name = "btnExportTxt";
            btnExportTxt.Size = new Size(75, 26);
            btnExportTxt.TabIndex = 21;
            btnExportTxt.Text = "Dışa Aktar";
            btnExportTxt.UseVisualStyleBackColor = true;
            btnExportTxt.Click += btnExportTxt_Click;
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblDisplayName.Location = new Point(23, 63);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(13, 20);
            lblDisplayName.TabIndex = 25;
            lblDisplayName.Text = " ";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblDescription.Location = new Point(24, 93);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(411, 71);
            lblDescription.TabIndex = 26;
            lblDescription.Text = " ";
            // 
            // lblStatusService
            // 
            lblStatusService.AutoSize = true;
            lblStatusService.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblStatusService.Location = new Point(23, 175);
            lblStatusService.Name = "lblStatusService";
            lblStatusService.Size = new Size(13, 20);
            lblStatusService.TabIndex = 27;
            lblStatusService.Text = " ";
            // 
            // lblStartupType
            // 
            lblStartupType.AutoSize = true;
            lblStartupType.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblStartupType.Location = new Point(23, 205);
            lblStartupType.Name = "lblStartupType";
            lblStartupType.Size = new Size(13, 20);
            lblStartupType.TabIndex = 28;
            lblStartupType.Text = " ";
            // 
            // lblServiceType
            // 
            lblServiceType.AutoSize = true;
            lblServiceType.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblServiceType.Location = new Point(23, 235);
            lblServiceType.Name = "lblServiceType";
            lblServiceType.Size = new Size(13, 20);
            lblServiceType.TabIndex = 29;
            lblServiceType.Text = " ";
            // 
            // lblCanPause
            // 
            lblCanPause.AutoSize = true;
            lblCanPause.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCanPause.Location = new Point(23, 265);
            lblCanPause.Name = "lblCanPause";
            lblCanPause.Size = new Size(13, 20);
            lblCanPause.TabIndex = 30;
            lblCanPause.Text = " ";
            // 
            // lblCanStop
            // 
            lblCanStop.AutoSize = true;
            lblCanStop.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCanStop.Location = new Point(23, 295);
            lblCanStop.Name = "lblCanStop";
            lblCanStop.Size = new Size(13, 20);
            lblCanStop.TabIndex = 31;
            lblCanStop.Text = " ";
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblMachineName.Location = new Point(24, 325);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(13, 20);
            lblMachineName.TabIndex = 32;
            lblMachineName.Text = " ";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // groupBoxDetails
            // 
            groupBoxDetails.Controls.Add(lblDisplayName);
            groupBoxDetails.Controls.Add(lblServiceName);
            groupBoxDetails.Controls.Add(lblMachineName);
            groupBoxDetails.Controls.Add(lblDescription);
            groupBoxDetails.Controls.Add(lblCanStop);
            groupBoxDetails.Controls.Add(lblStatusService);
            groupBoxDetails.Controls.Add(lblCanPause);
            groupBoxDetails.Controls.Add(lblStartupType);
            groupBoxDetails.Controls.Add(lblServiceType);
            groupBoxDetails.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBoxDetails.Location = new Point(422, 85);
            groupBoxDetails.Name = "groupBoxDetails";
            groupBoxDetails.Size = new Size(441, 400);
            groupBoxDetails.TabIndex = 33;
            groupBoxDetails.TabStop = false;
            groupBoxDetails.Text = "Service Details";
            // 
            // FormServices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 822);
            Controls.Add(groupBoxDetails);
            Controls.Add(listBoxServices);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(btnRefresh);
            Controls.Add(txtFilter);
            Controls.Add(btnExportTxt);
            Controls.Add(groupBoxProcess);
            Controls.Add(groupBoxStartupSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormServices";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Servisler";
            FormClosed += FormServices_FormClosed;
            Load += FormServices_Load;
            groupBoxProcess.ResumeLayout(false);
            groupBoxStartupSettings.ResumeLayout(false);
            groupBoxDetails.ResumeLayout(false);
            groupBoxDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblServiceName;
        private Button btnStart;
        private Button btnStop;
        private Button btnPause;
        private Button btnContinue;
        private Button btnSetAutomatic;
        private Button btnSetManual;
        private Button btnSetDisabled;
        private GroupBox groupBoxProcess;
        private GroupBox groupBoxStartupSettings;
        private ListBox listBoxServices;
        private Label label1;
        private Label lblStatus;
        private Button btnRefresh;
        private TextBox txtFilter;
        private Button btnExportTxt;
        private Label lblDisplayName;
        private Label lblDescription;
        private Label lblStatusService;
        private Label lblStartupType;
        private Label lblServiceType;
        private Label lblCanPause;
        private Label lblCanStop;
        private Label lblMachineName;
        private ContextMenuStrip contextMenuStrip1;
        private GroupBox groupBoxDetails;
    }
}