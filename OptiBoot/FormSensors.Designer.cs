namespace OptiBoot
{
    partial class FormSensors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSensors));
            labelCPUSicaklik = new Label();
            labelCPUYuzdesi = new Label();
            labelGPUSicaklik = new Label();
            labelRAMSicaklik = new Label();
            labelAnakartSicaklik = new Label();
            labelDiskSicaklik = new Label();
            labelGPUKullanimi = new Label();
            groupBoxCPU = new GroupBox();
            groupBoxGPU = new GroupBox();
            groupBoxRAM = new GroupBox();
            groupBoxMainboard = new GroupBox();
            groupBoxDisk = new GroupBox();
            groupBoxCPU.SuspendLayout();
            groupBoxGPU.SuspendLayout();
            groupBoxRAM.SuspendLayout();
            groupBoxMainboard.SuspendLayout();
            groupBoxDisk.SuspendLayout();
            SuspendLayout();
            // 
            // labelCPUSicaklik
            // 
            labelCPUSicaklik.AutoSize = true;
            labelCPUSicaklik.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelCPUSicaklik.Location = new Point(20, 19);
            labelCPUSicaklik.Name = "labelCPUSicaklik";
            labelCPUSicaklik.Size = new Size(37, 20);
            labelCPUSicaklik.TabIndex = 0;
            labelCPUSicaklik.Text = "N/A";
            // 
            // labelCPUYuzdesi
            // 
            labelCPUYuzdesi.AutoSize = true;
            labelCPUYuzdesi.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelCPUYuzdesi.Location = new Point(20, 53);
            labelCPUYuzdesi.Name = "labelCPUYuzdesi";
            labelCPUYuzdesi.Size = new Size(37, 20);
            labelCPUYuzdesi.TabIndex = 1;
            labelCPUYuzdesi.Text = "N/A";
            // 
            // labelGPUSicaklik
            // 
            labelGPUSicaklik.AutoSize = true;
            labelGPUSicaklik.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelGPUSicaklik.Location = new Point(20, 27);
            labelGPUSicaklik.Name = "labelGPUSicaklik";
            labelGPUSicaklik.Size = new Size(37, 20);
            labelGPUSicaklik.TabIndex = 2;
            labelGPUSicaklik.Text = "N/A";
            // 
            // labelRAMSicaklik
            // 
            labelRAMSicaklik.AutoSize = true;
            labelRAMSicaklik.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelRAMSicaklik.Location = new Point(20, 24);
            labelRAMSicaklik.Name = "labelRAMSicaklik";
            labelRAMSicaklik.Size = new Size(37, 20);
            labelRAMSicaklik.TabIndex = 3;
            labelRAMSicaklik.Text = "N/A";
            // 
            // labelAnakartSicaklik
            // 
            labelAnakartSicaklik.AutoSize = true;
            labelAnakartSicaklik.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            labelAnakartSicaklik.Location = new Point(20, 22);
            labelAnakartSicaklik.Name = "labelAnakartSicaklik";
            labelAnakartSicaklik.Size = new Size(37, 20);
            labelAnakartSicaklik.TabIndex = 4;
            labelAnakartSicaklik.Text = "N/A";
            // 
            // labelDiskSicaklik
            // 
            labelDiskSicaklik.AutoSize = true;
            labelDiskSicaklik.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelDiskSicaklik.Location = new Point(20, 24);
            labelDiskSicaklik.Name = "labelDiskSicaklik";
            labelDiskSicaklik.Size = new Size(37, 20);
            labelDiskSicaklik.TabIndex = 5;
            labelDiskSicaklik.Text = "N/A";
            // 
            // labelGPUKullanimi
            // 
            labelGPUKullanimi.AutoSize = true;
            labelGPUKullanimi.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGPUKullanimi.Location = new Point(20, 57);
            labelGPUKullanimi.Name = "labelGPUKullanimi";
            labelGPUKullanimi.Size = new Size(37, 20);
            labelGPUKullanimi.TabIndex = 6;
            labelGPUKullanimi.Text = "N/A";
            // 
            // groupBoxCPU
            // 
            groupBoxCPU.Controls.Add(labelCPUYuzdesi);
            groupBoxCPU.Controls.Add(labelCPUSicaklik);
            groupBoxCPU.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBoxCPU.Location = new Point(12, 12);
            groupBoxCPU.Name = "groupBoxCPU";
            groupBoxCPU.Size = new Size(247, 89);
            groupBoxCPU.TabIndex = 7;
            groupBoxCPU.TabStop = false;
            groupBoxCPU.Text = "İşlemci";
            // 
            // groupBoxGPU
            // 
            groupBoxGPU.Controls.Add(labelGPUKullanimi);
            groupBoxGPU.Controls.Add(labelGPUSicaklik);
            groupBoxGPU.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBoxGPU.Location = new Point(12, 107);
            groupBoxGPU.Name = "groupBoxGPU";
            groupBoxGPU.Size = new Size(247, 89);
            groupBoxGPU.TabIndex = 8;
            groupBoxGPU.TabStop = false;
            groupBoxGPU.Text = "Ekran Kartı";
            // 
            // groupBoxRAM
            // 
            groupBoxRAM.Controls.Add(labelRAMSicaklik);
            groupBoxRAM.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBoxRAM.Location = new Point(12, 202);
            groupBoxRAM.Name = "groupBoxRAM";
            groupBoxRAM.Size = new Size(247, 56);
            groupBoxRAM.TabIndex = 9;
            groupBoxRAM.TabStop = false;
            groupBoxRAM.Text = "Bellek";
            // 
            // groupBoxMainboard
            // 
            groupBoxMainboard.Controls.Add(labelAnakartSicaklik);
            groupBoxMainboard.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBoxMainboard.Location = new Point(12, 264);
            groupBoxMainboard.Name = "groupBoxMainboard";
            groupBoxMainboard.Size = new Size(247, 56);
            groupBoxMainboard.TabIndex = 10;
            groupBoxMainboard.TabStop = false;
            groupBoxMainboard.Text = "Anakart";
            // 
            // groupBoxDisk
            // 
            groupBoxDisk.Controls.Add(labelDiskSicaklik);
            groupBoxDisk.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBoxDisk.Location = new Point(12, 326);
            groupBoxDisk.Name = "groupBoxDisk";
            groupBoxDisk.Size = new Size(247, 56);
            groupBoxDisk.TabIndex = 11;
            groupBoxDisk.TabStop = false;
            groupBoxDisk.Text = "Disk";
            // 
            // FormSensors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 420);
            Controls.Add(groupBoxDisk);
            Controls.Add(groupBoxMainboard);
            Controls.Add(groupBoxRAM);
            Controls.Add(groupBoxGPU);
            Controls.Add(groupBoxCPU);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(297, 459);
            Name = "FormSensors";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sensörler [BETA]";
            FormClosed += FormSensors_FormClosed;
            Load += FormSensors_Load;
            groupBoxCPU.ResumeLayout(false);
            groupBoxCPU.PerformLayout();
            groupBoxGPU.ResumeLayout(false);
            groupBoxGPU.PerformLayout();
            groupBoxRAM.ResumeLayout(false);
            groupBoxRAM.PerformLayout();
            groupBoxMainboard.ResumeLayout(false);
            groupBoxMainboard.PerformLayout();
            groupBoxDisk.ResumeLayout(false);
            groupBoxDisk.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelCPUSicaklik;
        private Label labelCPUYuzdesi;
        private Label labelGPUSicaklik;
        private Label labelRAMSicaklik;
        private Label labelAnakartSicaklik;
        private Label labelDiskSicaklik;
        private Label labelGPUKullanimi;
        private GroupBox groupBoxCPU;
        private GroupBox groupBoxGPU;
        private GroupBox groupBoxRAM;
        private GroupBox groupBoxMainboard;
        private GroupBox groupBoxDisk;
    }
}