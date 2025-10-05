namespace OptiBoot
{
    partial class FormStartupApps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStartupApps));
            listBox1 = new ListBox();
            lblAppName = new Label();
            btnSil = new Button();
            btnDevreDisi = new Button();
            btnEtkinlestir = new Button();
            btnOpenLocation = new Button();
            btnExportToTXT = new Button();
            lblAppLocation = new Label();
            btnBackupREGFile = new Button();
            labelStartupAppsHeader = new Label();
            lblCountApps = new Label();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 95);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(718, 334);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.MouseDown += listBox1_MouseDown;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblAppName.Location = new Point(16, 49);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(0, 17);
            lblAppName.TabIndex = 1;
            // 
            // btnSil
            // 
            btnSil.Enabled = false;
            btnSil.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnSil.FlatStyle = FlatStyle.Flat;
            btnSil.Image = (Image)resources.GetObject("btnSil.Image");
            btnSil.ImageAlign = ContentAlignment.MiddleLeft;
            btnSil.Location = new Point(12, 435);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(98, 37);
            btnSil.TabIndex = 2;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = true;
            btnSil.Click += btnSil_Click;
            // 
            // btnDevreDisi
            // 
            btnDevreDisi.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnDevreDisi.FlatStyle = FlatStyle.Flat;
            btnDevreDisi.Location = new Point(760, 515);
            btnDevreDisi.Name = "btnDevreDisi";
            btnDevreDisi.Size = new Size(97, 23);
            btnDevreDisi.TabIndex = 3;
            btnDevreDisi.Text = "Devredışı Bırak";
            btnDevreDisi.UseVisualStyleBackColor = true;
            btnDevreDisi.Visible = false;
            btnDevreDisi.Click += btnDevreDisi_Click;
            // 
            // btnEtkinlestir
            // 
            btnEtkinlestir.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnEtkinlestir.FlatStyle = FlatStyle.Flat;
            btnEtkinlestir.Location = new Point(863, 515);
            btnEtkinlestir.Name = "btnEtkinlestir";
            btnEtkinlestir.Size = new Size(98, 23);
            btnEtkinlestir.TabIndex = 4;
            btnEtkinlestir.Text = "Etkinleştir";
            btnEtkinlestir.UseVisualStyleBackColor = true;
            btnEtkinlestir.Visible = false;
            btnEtkinlestir.Click += btnEtkinlestir_Click;
            // 
            // btnOpenLocation
            // 
            btnOpenLocation.AutoSize = true;
            btnOpenLocation.Enabled = false;
            btnOpenLocation.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnOpenLocation.FlatStyle = FlatStyle.Flat;
            btnOpenLocation.Image = (Image)resources.GetObject("btnOpenLocation.Image");
            btnOpenLocation.ImageAlign = ContentAlignment.MiddleLeft;
            btnOpenLocation.Location = new Point(603, 435);
            btnOpenLocation.Name = "btnOpenLocation";
            btnOpenLocation.Size = new Size(127, 37);
            btnOpenLocation.TabIndex = 5;
            btnOpenLocation.Text = "Konumu Aç";
            btnOpenLocation.TextAlign = ContentAlignment.MiddleRight;
            btnOpenLocation.UseVisualStyleBackColor = true;
            btnOpenLocation.Click += btnOpenLocation_Click;
            // 
            // btnExportToTXT
            // 
            btnExportToTXT.AutoSize = true;
            btnExportToTXT.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnExportToTXT.FlatStyle = FlatStyle.Flat;
            btnExportToTXT.Image = (Image)resources.GetObject("btnExportToTXT.Image");
            btnExportToTXT.ImageAlign = ContentAlignment.MiddleLeft;
            btnExportToTXT.Location = new Point(499, 435);
            btnExportToTXT.Name = "btnExportToTXT";
            btnExportToTXT.Size = new Size(98, 37);
            btnExportToTXT.TabIndex = 6;
            btnExportToTXT.Text = " Dışa Aktar";
            btnExportToTXT.TextAlign = ContentAlignment.MiddleRight;
            btnExportToTXT.UseVisualStyleBackColor = true;
            btnExportToTXT.Click += btnExportToTXT_Click;
            // 
            // lblAppLocation
            // 
            lblAppLocation.AutoSize = true;
            lblAppLocation.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblAppLocation.Location = new Point(16, 73);
            lblAppLocation.Name = "lblAppLocation";
            lblAppLocation.Size = new Size(0, 17);
            lblAppLocation.TabIndex = 7;
            // 
            // btnBackupREGFile
            // 
            btnBackupREGFile.AutoSize = true;
            btnBackupREGFile.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnBackupREGFile.FlatStyle = FlatStyle.Flat;
            btnBackupREGFile.Image = (Image)resources.GetObject("btnBackupREGFile.Image");
            btnBackupREGFile.ImageAlign = ContentAlignment.MiddleLeft;
            btnBackupREGFile.Location = new Point(395, 435);
            btnBackupREGFile.Name = "btnBackupREGFile";
            btnBackupREGFile.Size = new Size(98, 37);
            btnBackupREGFile.TabIndex = 8;
            btnBackupREGFile.Text = "Yedekle";
            btnBackupREGFile.TextAlign = ContentAlignment.MiddleRight;
            btnBackupREGFile.UseVisualStyleBackColor = true;
            btnBackupREGFile.Click += btnBackupREGFile_Click;
            // 
            // labelStartupAppsHeader
            // 
            labelStartupAppsHeader.AutoSize = true;
            labelStartupAppsHeader.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelStartupAppsHeader.Location = new Point(12, 14);
            labelStartupAppsHeader.Name = "labelStartupAppsHeader";
            labelStartupAppsHeader.Size = new Size(225, 20);
            labelStartupAppsHeader.TabIndex = 9;
            labelStartupAppsHeader.Text = "Başlangıçta Çalışan Programlar";
            // 
            // lblCountApps
            // 
            lblCountApps.AutoSize = true;
            lblCountApps.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            lblCountApps.Location = new Point(7, 480);
            lblCountApps.Name = "lblCountApps";
            lblCountApps.Size = new Size(0, 17);
            lblCountApps.TabIndex = 10;
            // 
            // FormStartupApps
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(746, 506);
            Controls.Add(lblCountApps);
            Controls.Add(labelStartupAppsHeader);
            Controls.Add(btnBackupREGFile);
            Controls.Add(lblAppLocation);
            Controls.Add(btnExportToTXT);
            Controls.Add(btnOpenLocation);
            Controls.Add(btnEtkinlestir);
            Controls.Add(btnDevreDisi);
            Controls.Add(btnSil);
            Controls.Add(lblAppName);
            Controls.Add(listBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(762, 545);
            Name = "FormStartupApps";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Başlangıç Ögeleri";
            FormClosed += FormStartupApps_FormClosed;
            Load += FormStartupApps_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label lblAppName;
        private Button btnSil;
        private Button btnDevreDisi;
        private Button btnEtkinlestir;
        private Button btnOpenLocation;
        private Button btnExportToTXT;
        private Label lblAppLocation;
        private Button btnBackupREGFile;
        private Label labelStartupAppsHeader;
        private Label lblCountApps;
    }
}