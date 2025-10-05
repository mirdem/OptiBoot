namespace OptiBoot
{
    partial class FormSoundAdapters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSoundAdapters));
            cihazComboBox = new ComboBox();
            labelFormName = new Label();
            labelSelectDevice = new Label();
            labelNameGeneral = new Label();
            labelDeviceIDGeneral = new Label();
            labelManufIDGeneral = new Label();
            labelProductIDGeneral = new Label();
            labelTypeGeneral = new Label();
            labelDefaultDeviceGeneral = new Label();
            groupBoxDriver = new GroupBox();
            labelDriverProvider = new Label();
            labelDriverDate = new Label();
            labelDriverVersion = new Label();
            labelDriverName = new Label();
            groupBoxGeneral = new GroupBox();
            buttonExport = new Button();
            buttonReload = new Button();
            groupBoxDriver.SuspendLayout();
            groupBoxGeneral.SuspendLayout();
            SuspendLayout();
            // 
            // cihazComboBox
            // 
            cihazComboBox.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            cihazComboBox.FormattingEnabled = true;
            cihazComboBox.Location = new Point(12, 84);
            cihazComboBox.Name = "cihazComboBox";
            cihazComboBox.Size = new Size(304, 28);
            cihazComboBox.TabIndex = 0;
            cihazComboBox.SelectedIndexChanged += cihazComboBox_SelectedIndexChanged;
            // 
            // labelFormName
            // 
            labelFormName.AutoSize = true;
            labelFormName.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelFormName.Location = new Point(12, 9);
            labelFormName.Name = "labelFormName";
            labelFormName.Size = new Size(130, 30);
            labelFormName.TabIndex = 1;
            labelFormName.Text = "Ses Cihazları";
            // 
            // labelSelectDevice
            // 
            labelSelectDevice.AutoSize = true;
            labelSelectDevice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSelectDevice.Location = new Point(12, 66);
            labelSelectDevice.Name = "labelSelectDevice";
            labelSelectDevice.Size = new Size(68, 15);
            labelSelectDevice.TabIndex = 2;
            labelSelectDevice.Text = "Cihaz Seçin";
            // 
            // labelNameGeneral
            // 
            labelNameGeneral.AutoSize = true;
            labelNameGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelNameGeneral.Location = new Point(16, 23);
            labelNameGeneral.Name = "labelNameGeneral";
            labelNameGeneral.Size = new Size(36, 20);
            labelNameGeneral.TabIndex = 3;
            labelNameGeneral.Text = "Adı:";
            // 
            // labelDeviceIDGeneral
            // 
            labelDeviceIDGeneral.AutoSize = true;
            labelDeviceIDGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDeviceIDGeneral.Location = new Point(16, 53);
            labelDeviceIDGeneral.Name = "labelDeviceIDGeneral";
            labelDeviceIDGeneral.Size = new Size(127, 20);
            labelDeviceIDGeneral.TabIndex = 4;
            labelDeviceIDGeneral.Text = "Donanım Kimliği:";
            // 
            // labelManufIDGeneral
            // 
            labelManufIDGeneral.AutoSize = true;
            labelManufIDGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelManufIDGeneral.Location = new Point(16, 83);
            labelManufIDGeneral.Name = "labelManufIDGeneral";
            labelManufIDGeneral.Size = new Size(109, 20);
            labelManufIDGeneral.TabIndex = 5;
            labelManufIDGeneral.Text = "Üretici Kimliği:";
            // 
            // labelProductIDGeneral
            // 
            labelProductIDGeneral.AutoSize = true;
            labelProductIDGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelProductIDGeneral.Location = new Point(16, 116);
            labelProductIDGeneral.Name = "labelProductIDGeneral";
            labelProductIDGeneral.Size = new Size(99, 20);
            labelProductIDGeneral.TabIndex = 6;
            labelProductIDGeneral.Text = "Ürün Kimliği:";
            // 
            // labelTypeGeneral
            // 
            labelTypeGeneral.AutoSize = true;
            labelTypeGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelTypeGeneral.Location = new Point(16, 146);
            labelTypeGeneral.Name = "labelTypeGeneral";
            labelTypeGeneral.Size = new Size(36, 20);
            labelTypeGeneral.TabIndex = 7;
            labelTypeGeneral.Text = "Tür:";
            // 
            // labelDefaultDeviceGeneral
            // 
            labelDefaultDeviceGeneral.AutoSize = true;
            labelDefaultDeviceGeneral.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDefaultDeviceGeneral.Location = new Point(16, 176);
            labelDefaultDeviceGeneral.Name = "labelDefaultDeviceGeneral";
            labelDefaultDeviceGeneral.Size = new Size(124, 20);
            labelDefaultDeviceGeneral.TabIndex = 8;
            labelDefaultDeviceGeneral.Text = "Varsayılan Cihaz:";
            // 
            // groupBoxDriver
            // 
            groupBoxDriver.Controls.Add(labelDriverProvider);
            groupBoxDriver.Controls.Add(labelDriverDate);
            groupBoxDriver.Controls.Add(labelDriverVersion);
            groupBoxDriver.Controls.Add(labelDriverName);
            groupBoxDriver.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBoxDriver.Location = new Point(12, 374);
            groupBoxDriver.Name = "groupBoxDriver";
            groupBoxDriver.Size = new Size(585, 225);
            groupBoxDriver.TabIndex = 9;
            groupBoxDriver.TabStop = false;
            groupBoxDriver.Text = "Sürücü Bilgileri";
            // 
            // labelDriverProvider
            // 
            labelDriverProvider.AutoSize = true;
            labelDriverProvider.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDriverProvider.Location = new Point(21, 114);
            labelDriverProvider.Name = "labelDriverProvider";
            labelDriverProvider.Size = new Size(73, 20);
            labelDriverProvider.TabIndex = 3;
            labelDriverProvider.Text = "Sağlayıcı:";
            // 
            // labelDriverDate
            // 
            labelDriverDate.AutoSize = true;
            labelDriverDate.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDriverDate.Location = new Point(21, 84);
            labelDriverDate.Name = "labelDriverDate";
            labelDriverDate.Size = new Size(47, 20);
            labelDriverDate.TabIndex = 2;
            labelDriverDate.Text = "Tarih:";
            // 
            // labelDriverVersion
            // 
            labelDriverVersion.AutoSize = true;
            labelDriverVersion.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDriverVersion.Location = new Point(19, 54);
            labelDriverVersion.Name = "labelDriverVersion";
            labelDriverVersion.Size = new Size(58, 20);
            labelDriverVersion.TabIndex = 1;
            labelDriverVersion.Text = "Sürüm:";
            // 
            // labelDriverName
            // 
            labelDriverName.AutoSize = true;
            labelDriverName.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDriverName.Location = new Point(19, 24);
            labelDriverName.Name = "labelDriverName";
            labelDriverName.Size = new Size(36, 20);
            labelDriverName.TabIndex = 0;
            labelDriverName.Text = "Adı:";
            // 
            // groupBoxGeneral
            // 
            groupBoxGeneral.Controls.Add(labelDeviceIDGeneral);
            groupBoxGeneral.Controls.Add(labelNameGeneral);
            groupBoxGeneral.Controls.Add(labelDefaultDeviceGeneral);
            groupBoxGeneral.Controls.Add(labelManufIDGeneral);
            groupBoxGeneral.Controls.Add(labelTypeGeneral);
            groupBoxGeneral.Controls.Add(labelProductIDGeneral);
            groupBoxGeneral.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBoxGeneral.Location = new Point(12, 129);
            groupBoxGeneral.Name = "groupBoxGeneral";
            groupBoxGeneral.Size = new Size(585, 225);
            groupBoxGeneral.TabIndex = 10;
            groupBoxGeneral.TabStop = false;
            groupBoxGeneral.Text = "Genel Bilgiler";
            // 
            // buttonExport
            // 
            buttonExport.FlatAppearance.BorderSize = 0;
            buttonExport.FlatStyle = FlatStyle.Flat;
            buttonExport.Image = (Image)resources.GetObject("buttonExport.Image");
            buttonExport.ImageAlign = ContentAlignment.TopCenter;
            buttonExport.Location = new Point(506, 613);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(91, 64);
            buttonExport.TabIndex = 11;
            buttonExport.Text = "Dışa Aktar";
            buttonExport.TextAlign = ContentAlignment.BottomCenter;
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // buttonReload
            // 
            buttonReload.FlatAppearance.BorderSize = 0;
            buttonReload.FlatStyle = FlatStyle.Flat;
            buttonReload.Image = (Image)resources.GetObject("buttonReload.Image");
            buttonReload.Location = new Point(423, 613);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(91, 64);
            buttonReload.TabIndex = 12;
            buttonReload.Text = "Yenile";
            buttonReload.TextAlign = ContentAlignment.BottomCenter;
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // FormSoundAdapters
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 689);
            Controls.Add(buttonReload);
            Controls.Add(buttonExport);
            Controls.Add(groupBoxGeneral);
            Controls.Add(groupBoxDriver);
            Controls.Add(labelSelectDevice);
            Controls.Add(labelFormName);
            Controls.Add(cihazComboBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(632, 728);
            Name = "FormSoundAdapters";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ses Cihazları";
            FormClosed += FormSoundAdapters_FormClosed;
            Load += FormSoundAdapters_Load;
            groupBoxDriver.ResumeLayout(false);
            groupBoxDriver.PerformLayout();
            groupBoxGeneral.ResumeLayout(false);
            groupBoxGeneral.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cihazComboBox;
        private Label labelFormName;
        private Label labelSelectDevice;
        private Label labelNameGeneral;
        private Label labelDeviceIDGeneral;
        private Label labelManufIDGeneral;
        private Label labelProductIDGeneral;
        private Label labelTypeGeneral;
        private Label labelDefaultDeviceGeneral;
        private GroupBox groupBoxDriver;
        private GroupBox groupBoxGeneral;
        private Label labelDriverProvider;
        private Label labelDriverDate;
        private Label labelDriverVersion;
        private Label labelDriverName;
        private Button buttonExport;
        private Button buttonReload;
    }
}