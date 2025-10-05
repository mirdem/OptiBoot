namespace OptiBoot
{
    partial class FormRAMInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRAMInfo));
            refreshTimer = new System.Windows.Forms.Timer(components);
            totalMemoryLabel = new Label();
            usedMemoryLabel = new Label();
            availableMemoryLabel = new Label();
            label4 = new Label();
            ramUsageProgressBar = new ProgressBar();
            usagePercentageLabel = new Label();
            manufacturerLabel = new Label();
            memoryTypeLabel = new Label();
            speedLabel = new Label();
            capacityLabel = new Label();
            modulesLabel = new Label();
            ramBrandPictureBox = new PictureBox();
            labelRAMDetails = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)ramBrandPictureBox).BeginInit();
            labelRAMDetails.SuspendLayout();
            SuspendLayout();
            // 
            // refreshTimer
            // 
            refreshTimer.Tick += RefreshTimer_Tick;
            // 
            // totalMemoryLabel
            // 
            totalMemoryLabel.AutoSize = true;
            totalMemoryLabel.Font = new Font("Segoe UI", 14.25F);
            totalMemoryLabel.Location = new Point(14, 58);
            totalMemoryLabel.Margin = new Padding(4, 0, 4, 0);
            totalMemoryLabel.Name = "totalMemoryLabel";
            totalMemoryLabel.Size = new Size(172, 25);
            totalMemoryLabel.TabIndex = 1;
            totalMemoryLabel.Text = "Toplam RAM: ... MB";
            // 
            // usedMemoryLabel
            // 
            usedMemoryLabel.AutoSize = true;
            usedMemoryLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            usedMemoryLabel.Location = new Point(14, 99);
            usedMemoryLabel.Margin = new Padding(4, 0, 4, 0);
            usedMemoryLabel.Name = "usedMemoryLabel";
            usedMemoryLabel.Size = new Size(124, 15);
            usedMemoryLabel.TabIndex = 2;
            usedMemoryLabel.Text = "Kullanılan RAM: ... MB";
            // 
            // availableMemoryLabel
            // 
            availableMemoryLabel.AutoSize = true;
            availableMemoryLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            availableMemoryLabel.Location = new Point(14, 127);
            availableMemoryLabel.Margin = new Padding(4, 0, 4, 0);
            availableMemoryLabel.Name = "availableMemoryLabel";
            availableMemoryLabel.Size = new Size(137, 15);
            availableMemoryLabel.TabIndex = 3;
            availableMemoryLabel.Text = "Kullanılabilir RAM: ... MB";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(38, 147);
            label4.Name = "label4";
            label4.Size = new Size(63, 25);
            label4.TabIndex = 3;
            label4.Text = "label4";
            // 
            // ramUsageProgressBar
            // 
            ramUsageProgressBar.Location = new Point(14, 14);
            ramUsageProgressBar.Margin = new Padding(4, 3, 4, 3);
            ramUsageProgressBar.Name = "ramUsageProgressBar";
            ramUsageProgressBar.Size = new Size(420, 27);
            ramUsageProgressBar.TabIndex = 0;
            // 
            // usagePercentageLabel
            // 
            usagePercentageLabel.AutoSize = true;
            usagePercentageLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            usagePercentageLabel.Location = new Point(351, 44);
            usagePercentageLabel.Margin = new Padding(4, 0, 4, 0);
            usagePercentageLabel.Name = "usagePercentageLabel";
            usagePercentageLabel.Size = new Size(83, 13);
            usagePercentageLabel.TabIndex = 4;
            usagePercentageLabel.Text = "Kullanım: ...%";
            // 
            // manufacturerLabel
            // 
            manufacturerLabel.AutoSize = true;
            manufacturerLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            manufacturerLabel.Location = new Point(15, 42);
            manufacturerLabel.Name = "manufacturerLabel";
            manufacturerLabel.Size = new Size(113, 17);
            manufacturerLabel.TabIndex = 6;
            manufacturerLabel.Text = "Üretici: Bilinmiyor";
            // 
            // memoryTypeLabel
            // 
            memoryTypeLabel.AutoSize = true;
            memoryTypeLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            memoryTypeLabel.Location = new Point(15, 67);
            memoryTypeLabel.Name = "memoryTypeLabel";
            memoryTypeLabel.Size = new Size(141, 17);
            memoryTypeLabel.TabIndex = 7;
            memoryTypeLabel.Text = "Bellek Türü: Bilinmiyor";
            // 
            // speedLabel
            // 
            speedLabel.AutoSize = true;
            speedLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            speedLabel.Location = new Point(15, 92);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(94, 17);
            speedLabel.TabIndex = 8;
            speedLabel.Text = "Hız: Bilinmiyor";
            // 
            // capacityLabel
            // 
            capacityLabel.AutoSize = true;
            capacityLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            capacityLabel.Location = new Point(15, 117);
            capacityLabel.Name = "capacityLabel";
            capacityLabel.Size = new Size(178, 17);
            capacityLabel.TabIndex = 9;
            capacityLabel.Text = "Modül Kapasitesi: Bilinmiyor";
            // 
            // modulesLabel
            // 
            modulesLabel.AutoSize = true;
            modulesLabel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            modulesLabel.Location = new Point(15, 142);
            modulesLabel.Name = "modulesLabel";
            modulesLabel.Size = new Size(165, 17);
            modulesLabel.TabIndex = 10;
            modulesLabel.Text = "RAM Modülleri: Bilinmiyor";
            // 
            // ramBrandPictureBox
            // 
            ramBrandPictureBox.Location = new Point(329, 262);
            ramBrandPictureBox.Name = "ramBrandPictureBox";
            ramBrandPictureBox.Size = new Size(128, 128);
            ramBrandPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            ramBrandPictureBox.TabIndex = 11;
            ramBrandPictureBox.TabStop = false;
            // 
            // labelRAMDetails
            // 
            labelRAMDetails.Controls.Add(memoryTypeLabel);
            labelRAMDetails.Controls.Add(manufacturerLabel);
            labelRAMDetails.Controls.Add(modulesLabel);
            labelRAMDetails.Controls.Add(speedLabel);
            labelRAMDetails.Controls.Add(capacityLabel);
            labelRAMDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelRAMDetails.Location = new Point(12, 219);
            labelRAMDetails.Name = "labelRAMDetails";
            labelRAMDetails.Size = new Size(309, 199);
            labelRAMDetails.TabIndex = 12;
            labelRAMDetails.TabStop = false;
            labelRAMDetails.Text = "RAM Detayları";
            // 
            // FormRAMInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(489, 430);
            Controls.Add(labelRAMDetails);
            Controls.Add(ramBrandPictureBox);
            Controls.Add(usagePercentageLabel);
            Controls.Add(availableMemoryLabel);
            Controls.Add(usedMemoryLabel);
            Controls.Add(totalMemoryLabel);
            Controls.Add(ramUsageProgressBar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(505, 469);
            Name = "FormRAMInfo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RAM Kullanım İzleyici";
            FormClosed += FormRAMInfo_FormClosed;
            Load += FormRAMInfo_Load;
            ((System.ComponentModel.ISupportInitialize)ramBrandPictureBox).EndInit();
            labelRAMDetails.ResumeLayout(false);
            labelRAMDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private Label totalMemoryLabel;
        private Label usedMemoryLabel;
        private Label availableMemoryLabel;
        private Label label4;
        private ProgressBar ramUsageProgressBar;
        private Label usagePercentageLabel;
        private Label manufacturerLabel;
        private Label memoryTypeLabel;
        private Label speedLabel;
        private Label capacityLabel;
        private Label modulesLabel;
        private PictureBox ramBrandPictureBox;
        private GroupBox labelRAMDetails;
    }
}