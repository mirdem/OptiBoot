namespace OptiBoot
{
    partial class FormDiskManager
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiskManager));
            comboBoxDisks = new ComboBox();
            buttonReload = new Button();
            labelTotalDiskSize = new Label();
            labelUsedSpace = new Label();
            labelFreeSpace = new Label();
            labelDiskType = new Label();
            labelDiskModel = new Label();
            groupBoxDiskFormat = new GroupBox();
            buttonFormat = new Button();
            labelFormatLabel = new Label();
            labelFormatType = new Label();
            txtDiskName = new TextBox();
            comboBoxFormat = new ComboBox();
            groupBoxDiskInfo = new GroupBox();
            labelSystemDisk = new Label();
            labelDiskFormat = new Label();
            colorProgressBar1 = new ColorProgressBar();
            toolTip1 = new ToolTip(components);
            labelDiskManager = new Label();
            groupBoxDiskFormat.SuspendLayout();
            groupBoxDiskInfo.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxDisks
            // 
            comboBoxDisks.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDisks.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            comboBoxDisks.FormattingEnabled = true;
            comboBoxDisks.Location = new Point(17, 44);
            comboBoxDisks.Name = "comboBoxDisks";
            comboBoxDisks.Size = new Size(285, 25);
            comboBoxDisks.TabIndex = 0;
            comboBoxDisks.SelectedIndexChanged += comboBoxDisks_SelectedIndexChanged;
            // 
            // buttonReload
            // 
            buttonReload.AutoSize = true;
            buttonReload.FlatAppearance.BorderSize = 0;
            buttonReload.FlatStyle = FlatStyle.Flat;
            buttonReload.Image = (Image)resources.GetObject("buttonReload.Image");
            buttonReload.ImageAlign = ContentAlignment.MiddleLeft;
            buttonReload.Location = new Point(318, 35);
            buttonReload.Name = "buttonReload";
            buttonReload.Size = new Size(94, 34);
            buttonReload.TabIndex = 1;
            buttonReload.Text = "Yenile";
            buttonReload.TextAlign = ContentAlignment.MiddleRight;
            buttonReload.UseVisualStyleBackColor = true;
            buttonReload.Click += buttonReload_Click;
            // 
            // labelTotalDiskSize
            // 
            labelTotalDiskSize.AutoSize = true;
            labelTotalDiskSize.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelTotalDiskSize.Location = new Point(16, 75);
            labelTotalDiskSize.Name = "labelTotalDiskSize";
            labelTotalDiskSize.Size = new Size(107, 20);
            labelTotalDiskSize.TabIndex = 2;
            labelTotalDiskSize.Text = "Toplam boyut:";
            // 
            // labelUsedSpace
            // 
            labelUsedSpace.AutoSize = true;
            labelUsedSpace.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelUsedSpace.Location = new Point(16, 108);
            labelUsedSpace.Name = "labelUsedSpace";
            labelUsedSpace.Size = new Size(114, 20);
            labelUsedSpace.TabIndex = 3;
            labelUsedSpace.Text = "Kullanılan alan:";
            // 
            // labelFreeSpace
            // 
            labelFreeSpace.AutoSize = true;
            labelFreeSpace.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelFreeSpace.Location = new Point(16, 141);
            labelFreeSpace.Name = "labelFreeSpace";
            labelFreeSpace.Size = new Size(70, 20);
            labelFreeSpace.TabIndex = 4;
            labelFreeSpace.Text = "Boş alan:";
            // 
            // labelDiskType
            // 
            labelDiskType.AutoSize = true;
            labelDiskType.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDiskType.Location = new Point(16, 179);
            labelDiskType.Name = "labelDiskType";
            labelDiskType.Size = new Size(68, 20);
            labelDiskType.TabIndex = 5;
            labelDiskType.Text = "Disk tipi:";
            // 
            // labelDiskModel
            // 
            labelDiskModel.AutoSize = true;
            labelDiskModel.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDiskModel.Location = new Point(16, 244);
            labelDiskModel.Name = "labelDiskModel";
            labelDiskModel.Size = new Size(57, 20);
            labelDiskModel.TabIndex = 6;
            labelDiskModel.Text = "Model:";
            // 
            // groupBoxDiskFormat
            // 
            groupBoxDiskFormat.Controls.Add(buttonFormat);
            groupBoxDiskFormat.Controls.Add(labelFormatLabel);
            groupBoxDiskFormat.Controls.Add(labelFormatType);
            groupBoxDiskFormat.Controls.Add(txtDiskName);
            groupBoxDiskFormat.Controls.Add(comboBoxFormat);
            groupBoxDiskFormat.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBoxDiskFormat.Location = new Point(12, 411);
            groupBoxDiskFormat.Name = "groupBoxDiskFormat";
            groupBoxDiskFormat.Size = new Size(350, 202);
            groupBoxDiskFormat.TabIndex = 9;
            groupBoxDiskFormat.TabStop = false;
            groupBoxDiskFormat.Text = "Diski Formatla";
            // 
            // buttonFormat
            // 
            buttonFormat.FlatAppearance.BorderSize = 0;
            buttonFormat.FlatStyle = FlatStyle.Flat;
            buttonFormat.ForeColor = Color.Black;
            buttonFormat.Image = (Image)resources.GetObject("buttonFormat.Image");
            buttonFormat.ImageAlign = ContentAlignment.MiddleRight;
            buttonFormat.Location = new Point(239, 168);
            buttonFormat.Name = "buttonFormat";
            buttonFormat.Size = new Size(105, 28);
            buttonFormat.TabIndex = 4;
            buttonFormat.Text = "Biçimlendir";
            buttonFormat.TextAlign = ContentAlignment.MiddleLeft;
            buttonFormat.UseVisualStyleBackColor = true;
            buttonFormat.Click += buttonFormat_Click;
            // 
            // labelFormatLabel
            // 
            labelFormatLabel.AutoSize = true;
            labelFormatLabel.Location = new Point(12, 83);
            labelFormatLabel.Name = "labelFormatLabel";
            labelFormatLabel.Size = new Size(45, 17);
            labelFormatLabel.TabIndex = 3;
            labelFormatLabel.Text = "Etiket:";
            // 
            // labelFormatType
            // 
            labelFormatType.AutoSize = true;
            labelFormatType.Location = new Point(10, 38);
            labelFormatType.Name = "labelFormatType";
            labelFormatType.Size = new Size(78, 17);
            labelFormatType.TabIndex = 2;
            labelFormatType.Text = "Format tipi:";
            // 
            // txtDiskName
            // 
            txtDiskName.Location = new Point(97, 75);
            txtDiskName.Name = "txtDiskName";
            txtDiskName.Size = new Size(122, 25);
            txtDiskName.TabIndex = 1;
            // 
            // comboBoxFormat
            // 
            comboBoxFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFormat.FormattingEnabled = true;
            comboBoxFormat.Location = new Point(98, 35);
            comboBoxFormat.Name = "comboBoxFormat";
            comboBoxFormat.Size = new Size(121, 25);
            comboBoxFormat.TabIndex = 0;
            // 
            // groupBoxDiskInfo
            // 
            groupBoxDiskInfo.Controls.Add(labelSystemDisk);
            groupBoxDiskInfo.Controls.Add(labelDiskFormat);
            groupBoxDiskInfo.Controls.Add(colorProgressBar1);
            groupBoxDiskInfo.Controls.Add(labelTotalDiskSize);
            groupBoxDiskInfo.Controls.Add(labelUsedSpace);
            groupBoxDiskInfo.Controls.Add(labelFreeSpace);
            groupBoxDiskInfo.Controls.Add(labelDiskType);
            groupBoxDiskInfo.Controls.Add(labelDiskModel);
            groupBoxDiskInfo.Location = new Point(12, 78);
            groupBoxDiskInfo.Name = "groupBoxDiskInfo";
            groupBoxDiskInfo.Size = new Size(350, 327);
            groupBoxDiskInfo.TabIndex = 10;
            groupBoxDiskInfo.TabStop = false;
            groupBoxDiskInfo.Text = "Disk Bilgileri";
            // 
            // labelSystemDisk
            // 
            labelSystemDisk.AutoSize = true;
            labelSystemDisk.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelSystemDisk.Location = new Point(16, 277);
            labelSystemDisk.Name = "labelSystemDisk";
            labelSystemDisk.Size = new Size(94, 20);
            labelSystemDisk.TabIndex = 11;
            labelSystemDisk.Text = "Sistem Diski:";
            // 
            // labelDiskFormat
            // 
            labelDiskFormat.AutoSize = true;
            labelDiskFormat.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelDiskFormat.Location = new Point(16, 214);
            labelDiskFormat.Name = "labelDiskFormat";
            labelDiskFormat.Size = new Size(62, 20);
            labelDiskFormat.TabIndex = 10;
            labelDiskFormat.Text = "Format:";
            // 
            // colorProgressBar1
            // 
            colorProgressBar1.ForeColor = SystemColors.Control;
            colorProgressBar1.Location = new Point(16, 25);
            colorProgressBar1.Name = "colorProgressBar1";
            colorProgressBar1.Size = new Size(268, 23);
            colorProgressBar1.TabIndex = 9;
            // 
            // labelDiskManager
            // 
            labelDiskManager.AutoSize = true;
            labelDiskManager.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelDiskManager.Location = new Point(15, 15);
            labelDiskManager.Name = "labelDiskManager";
            labelDiskManager.Size = new Size(106, 20);
            labelDiskManager.TabIndex = 11;
            labelDiskManager.Text = "Disk Yöneticisi";
            // 
            // FormDiskManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 625);
            Controls.Add(labelDiskManager);
            Controls.Add(groupBoxDiskInfo);
            Controls.Add(groupBoxDiskFormat);
            Controls.Add(buttonReload);
            Controls.Add(comboBoxDisks);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormDiskManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Disk Yöneticisi";
            FormClosed += FormDiskManager_FormClosed;
            Load += FormDiskManager_Load;
            groupBoxDiskFormat.ResumeLayout(false);
            groupBoxDiskFormat.PerformLayout();
            groupBoxDiskInfo.ResumeLayout(false);
            groupBoxDiskInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxDisks;
        private Button buttonReload;
        private Label labelTotalDiskSize;
        private Label labelUsedSpace;
        private Label labelFreeSpace;
        private Label labelDiskType;
        private Label labelDiskModel;
        private ProgressBar progressBar1;
        private GroupBox groupBoxDiskFormat;
        private Button buttonFormat;
        private Label labelFormatLabel;
        private Label labelFormatType;
        private TextBox txtDiskName;
        private ComboBox comboBoxFormat;
        private GroupBox groupBoxDiskInfo;
        private ToolTip toolTip1;
        private ColorProgressBar colorProgressBar1;
        private Label labelDiskManager;
        private Label labelDiskFormat;
        private Label labelSystemDisk;
    }
}