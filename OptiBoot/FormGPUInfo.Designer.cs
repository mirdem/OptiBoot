namespace OptiBoot
{
    partial class FormGPUInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGPUInfo));
            lblGPUName = new Label();
            lblGPUDescription = new Label();
            lblGPUMemory = new Label();
            lblGPUDriver = new Label();
            lblGPUProcessor = new Label();
            lblGPUArchitecture = new Label();
            lblGPUManufacturer = new Label();
            lblGPUType = new Label();
            lblGPUMemoryType = new Label();
            lblGPUDriverDate = new Label();
            lblGPUAvailability = new Label();
            lblGPUStatus = new Label();
            labelGPUInfoCaption = new Label();
            picLogo = new PictureBox();
            lblGPUTemperature = new Label();
            lblGPUCoreClock = new Label();
            lblGPUMemoryClock = new Label();
            lblGPUBIOSVersion = new Label();
            lblGPUMemoryBandwidth = new Label();
            lblGPUUtilization = new Label();
            lblGPUPowerUsage = new Label();
            lblGPUOpenCL = new Label();
            lblGPUCUDA = new Label();
            lblGPUDirectCompute = new Label();
            lblGPUDirectML = new Label();
            lblGPUVulkan = new Label();
            lblGPURayTracing = new Label();
            lblGPUPhysX = new Label();
            lblGPUOpenGL = new Label();
            btnRefresh = new Button();
            btnPreviousGPU2 = new Button();
            btnNextGPU2 = new Button();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // lblGPUName
            // 
            lblGPUName.AutoSize = true;
            lblGPUName.Font = new Font("Segoe UI", 11.25F);
            lblGPUName.Location = new Point(12, 50);
            lblGPUName.Name = "lblGPUName";
            lblGPUName.Size = new Size(102, 20);
            lblGPUName.TabIndex = 0;
            lblGPUName.Text = "Marka/Model:";
            // 
            // lblGPUDescription
            // 
            lblGPUDescription.AutoSize = true;
            lblGPUDescription.Font = new Font("Segoe UI", 11.25F);
            lblGPUDescription.Location = new Point(12, 78);
            lblGPUDescription.Name = "lblGPUDescription";
            lblGPUDescription.Size = new Size(73, 20);
            lblGPUDescription.TabIndex = 1;
            lblGPUDescription.Text = "Açıklama:";
            // 
            // lblGPUMemory
            // 
            lblGPUMemory.AutoSize = true;
            lblGPUMemory.Font = new Font("Segoe UI", 11.25F);
            lblGPUMemory.Location = new Point(12, 103);
            lblGPUMemory.Name = "lblGPUMemory";
            lblGPUMemory.Size = new Size(102, 20);
            lblGPUMemory.TabIndex = 2;
            lblGPUMemory.Text = "Bellek miktarı:";
            // 
            // lblGPUDriver
            // 
            lblGPUDriver.AutoSize = true;
            lblGPUDriver.Font = new Font("Segoe UI", 11.25F);
            lblGPUDriver.Location = new Point(12, 129);
            lblGPUDriver.Name = "lblGPUDriver";
            lblGPUDriver.Size = new Size(122, 20);
            lblGPUDriver.TabIndex = 3;
            lblGPUDriver.Text = "Sürücü versiyonu:";
            // 
            // lblGPUProcessor
            // 
            lblGPUProcessor.AutoSize = true;
            lblGPUProcessor.Font = new Font("Segoe UI", 11.25F);
            lblGPUProcessor.Location = new Point(12, 154);
            lblGPUProcessor.Name = "lblGPUProcessor";
            lblGPUProcessor.Size = new Size(101, 20);
            lblGPUProcessor.TabIndex = 4;
            lblGPUProcessor.Text = "Video işlemci:";
            // 
            // lblGPUArchitecture
            // 
            lblGPUArchitecture.AutoSize = true;
            lblGPUArchitecture.Font = new Font("Segoe UI", 11.25F);
            lblGPUArchitecture.Location = new Point(12, 178);
            lblGPUArchitecture.Name = "lblGPUArchitecture";
            lblGPUArchitecture.Size = new Size(85, 20);
            lblGPUArchitecture.TabIndex = 5;
            lblGPUArchitecture.Text = "Mimari tipi:";
            // 
            // lblGPUManufacturer
            // 
            lblGPUManufacturer.AutoSize = true;
            lblGPUManufacturer.Font = new Font("Segoe UI", 11.25F);
            lblGPUManufacturer.Location = new Point(349, 50);
            lblGPUManufacturer.Name = "lblGPUManufacturer";
            lblGPUManufacturer.Size = new Size(94, 20);
            lblGPUManufacturer.TabIndex = 6;
            lblGPUManufacturer.Text = "Üretici firma:";
            // 
            // lblGPUType
            // 
            lblGPUType.AutoSize = true;
            lblGPUType.Font = new Font("Segoe UI", 11.25F);
            lblGPUType.Location = new Point(349, 78);
            lblGPUType.Name = "lblGPUType";
            lblGPUType.Size = new Size(119, 20);
            lblGPUType.TabIndex = 7;
            lblGPUType.Text = "Dahili/Harici tipi";
            // 
            // lblGPUMemoryType
            // 
            lblGPUMemoryType.AutoSize = true;
            lblGPUMemoryType.Font = new Font("Segoe UI", 11.25F);
            lblGPUMemoryType.Location = new Point(349, 103);
            lblGPUMemoryType.Name = "lblGPUMemoryType";
            lblGPUMemoryType.Size = new Size(78, 20);
            lblGPUMemoryType.TabIndex = 8;
            lblGPUMemoryType.Text = "Bellek tipi:";
            // 
            // lblGPUDriverDate
            // 
            lblGPUDriverDate.AutoSize = true;
            lblGPUDriverDate.Font = new Font("Segoe UI", 11.25F);
            lblGPUDriverDate.Location = new Point(349, 129);
            lblGPUDriverDate.Name = "lblGPUDriverDate";
            lblGPUDriverDate.Size = new Size(91, 20);
            lblGPUDriverDate.TabIndex = 9;
            lblGPUDriverDate.Text = "Sürücü tarihi";
            // 
            // lblGPUAvailability
            // 
            lblGPUAvailability.AutoSize = true;
            lblGPUAvailability.Font = new Font("Segoe UI", 11.25F);
            lblGPUAvailability.Location = new Point(349, 154);
            lblGPUAvailability.Name = "lblGPUAvailability";
            lblGPUAvailability.Size = new Size(165, 20);
            lblGPUAvailability.TabIndex = 10;
            lblGPUAvailability.Text = "Kullanılabilirlik durumu:";
            // 
            // lblGPUStatus
            // 
            lblGPUStatus.AutoSize = true;
            lblGPUStatus.Font = new Font("Segoe UI", 11.25F);
            lblGPUStatus.Location = new Point(349, 178);
            lblGPUStatus.Name = "lblGPUStatus";
            lblGPUStatus.Size = new Size(101, 20);
            lblGPUStatus.TabIndex = 11;
            lblGPUStatus.Text = "Durum bilgisi:";
            // 
            // labelGPUInfoCaption
            // 
            labelGPUInfoCaption.AutoSize = true;
            labelGPUInfoCaption.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelGPUInfoCaption.Location = new Point(12, 9);
            labelGPUInfoCaption.Name = "labelGPUInfoCaption";
            labelGPUInfoCaption.Size = new Size(118, 25);
            labelGPUInfoCaption.TabIndex = 12;
            labelGPUInfoCaption.Text = "GPU Bilgileri";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = Properties.Resources.DefaultGPU;
            picLogo.Location = new Point(731, 34);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(154, 155);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 13;
            picLogo.TabStop = false;
            // 
            // lblGPUTemperature
            // 
            lblGPUTemperature.AutoSize = true;
            lblGPUTemperature.Font = new Font("Segoe UI", 11.25F);
            lblGPUTemperature.Location = new Point(12, 224);
            lblGPUTemperature.Name = "lblGPUTemperature";
            lblGPUTemperature.Size = new Size(61, 20);
            lblGPUTemperature.TabIndex = 17;
            lblGPUTemperature.Text = "Sıcaklık:";
            // 
            // lblGPUCoreClock
            // 
            lblGPUCoreClock.AutoSize = true;
            lblGPUCoreClock.Font = new Font("Segoe UI", 11.25F);
            lblGPUCoreClock.Location = new Point(12, 254);
            lblGPUCoreClock.Name = "lblGPUCoreClock";
            lblGPUCoreClock.Size = new Size(132, 20);
            lblGPUCoreClock.TabIndex = 18;
            lblGPUCoreClock.Text = "Çekirdek Saat Hızı:";
            // 
            // lblGPUMemoryClock
            // 
            lblGPUMemoryClock.AutoSize = true;
            lblGPUMemoryClock.Font = new Font("Segoe UI", 11.25F);
            lblGPUMemoryClock.Location = new Point(12, 284);
            lblGPUMemoryClock.Name = "lblGPUMemoryClock";
            lblGPUMemoryClock.Size = new Size(115, 20);
            lblGPUMemoryClock.TabIndex = 19;
            lblGPUMemoryClock.Text = "Bellek Saat Hızı:";
            // 
            // lblGPUBIOSVersion
            // 
            lblGPUBIOSVersion.AutoSize = true;
            lblGPUBIOSVersion.Font = new Font("Segoe UI", 11.25F);
            lblGPUBIOSVersion.Location = new Point(12, 314);
            lblGPUBIOSVersion.Name = "lblGPUBIOSVersion";
            lblGPUBIOSVersion.Size = new Size(111, 20);
            lblGPUBIOSVersion.TabIndex = 20;
            lblGPUBIOSVersion.Text = "BIOS Versiyonu:";
            // 
            // lblGPUMemoryBandwidth
            // 
            lblGPUMemoryBandwidth.AutoSize = true;
            lblGPUMemoryBandwidth.Font = new Font("Segoe UI", 11.25F);
            lblGPUMemoryBandwidth.Location = new Point(12, 344);
            lblGPUMemoryBandwidth.Name = "lblGPUMemoryBandwidth";
            lblGPUMemoryBandwidth.Size = new Size(147, 20);
            lblGPUMemoryBandwidth.TabIndex = 21;
            lblGPUMemoryBandwidth.Text = "Bellek Bant Genişliği:";
            // 
            // lblGPUUtilization
            // 
            lblGPUUtilization.AutoSize = true;
            lblGPUUtilization.Font = new Font("Segoe UI", 11.25F);
            lblGPUUtilization.Location = new Point(12, 374);
            lblGPUUtilization.Name = "lblGPUUtilization";
            lblGPUUtilization.Size = new Size(106, 20);
            lblGPUUtilization.TabIndex = 22;
            lblGPUUtilization.Text = "GPU Kullanımı:";
            // 
            // lblGPUPowerUsage
            // 
            lblGPUPowerUsage.AutoSize = true;
            lblGPUPowerUsage.Font = new Font("Segoe UI", 11.25F);
            lblGPUPowerUsage.Location = new Point(349, 224);
            lblGPUPowerUsage.Name = "lblGPUPowerUsage";
            lblGPUPowerUsage.Size = new Size(98, 20);
            lblGPUPowerUsage.TabIndex = 23;
            lblGPUPowerUsage.Text = "Güç Tüketimi:";
            // 
            // lblGPUOpenCL
            // 
            lblGPUOpenCL.AutoSize = true;
            lblGPUOpenCL.Font = new Font("Segoe UI", 11.25F);
            lblGPUOpenCL.Location = new Point(349, 254);
            lblGPUOpenCL.Name = "lblGPUOpenCL";
            lblGPUOpenCL.Size = new Size(64, 20);
            lblGPUOpenCL.TabIndex = 24;
            lblGPUOpenCL.Text = "OpenCL:";
            // 
            // lblGPUCUDA
            // 
            lblGPUCUDA.AutoSize = true;
            lblGPUCUDA.Font = new Font("Segoe UI", 11.25F);
            lblGPUCUDA.Location = new Point(349, 284);
            lblGPUCUDA.Name = "lblGPUCUDA";
            lblGPUCUDA.Size = new Size(52, 20);
            lblGPUCUDA.TabIndex = 25;
            lblGPUCUDA.Text = "CUDA:";
            // 
            // lblGPUDirectCompute
            // 
            lblGPUDirectCompute.AutoSize = true;
            lblGPUDirectCompute.Font = new Font("Segoe UI", 11.25F);
            lblGPUDirectCompute.Location = new Point(349, 314);
            lblGPUDirectCompute.Name = "lblGPUDirectCompute";
            lblGPUDirectCompute.Size = new Size(113, 20);
            lblGPUDirectCompute.TabIndex = 26;
            lblGPUDirectCompute.Text = "DirectCompute:";
            // 
            // lblGPUDirectML
            // 
            lblGPUDirectML.AutoSize = true;
            lblGPUDirectML.Font = new Font("Segoe UI", 11.25F);
            lblGPUDirectML.Location = new Point(349, 344);
            lblGPUDirectML.Name = "lblGPUDirectML";
            lblGPUDirectML.Size = new Size(69, 20);
            lblGPUDirectML.TabIndex = 27;
            lblGPUDirectML.Text = "DirectML";
            // 
            // lblGPUVulkan
            // 
            lblGPUVulkan.AutoSize = true;
            lblGPUVulkan.Font = new Font("Segoe UI", 11.25F);
            lblGPUVulkan.Location = new Point(349, 374);
            lblGPUVulkan.Name = "lblGPUVulkan";
            lblGPUVulkan.Size = new Size(53, 20);
            lblGPUVulkan.TabIndex = 28;
            lblGPUVulkan.Text = "Vulkan";
            // 
            // lblGPURayTracing
            // 
            lblGPURayTracing.AutoSize = true;
            lblGPURayTracing.Font = new Font("Segoe UI", 11.25F);
            lblGPURayTracing.Location = new Point(349, 404);
            lblGPURayTracing.Name = "lblGPURayTracing";
            lblGPURayTracing.Size = new Size(84, 20);
            lblGPURayTracing.TabIndex = 29;
            lblGPURayTracing.Text = "RayTracing:";
            // 
            // lblGPUPhysX
            // 
            lblGPUPhysX.AutoSize = true;
            lblGPUPhysX.Font = new Font("Segoe UI", 11.25F);
            lblGPUPhysX.Location = new Point(12, 404);
            lblGPUPhysX.Name = "lblGPUPhysX";
            lblGPUPhysX.Size = new Size(50, 20);
            lblGPUPhysX.TabIndex = 30;
            lblGPUPhysX.Text = "PhysX:";
            // 
            // lblGPUOpenGL
            // 
            lblGPUOpenGL.AutoSize = true;
            lblGPUOpenGL.Font = new Font("Segoe UI", 11.25F);
            lblGPUOpenGL.Location = new Point(349, 434);
            lblGPUOpenGL.Name = "lblGPUOpenGL";
            lblGPUOpenGL.Size = new Size(65, 20);
            lblGPUOpenGL.TabIndex = 31;
            lblGPUOpenGL.Text = "OpenGL:";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(655, 437);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(114, 32);
            btnRefresh.TabIndex = 35;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnPreviousGPU2
            // 
            btnPreviousGPU2.Location = new Point(775, 437);
            btnPreviousGPU2.Name = "btnPreviousGPU2";
            btnPreviousGPU2.Size = new Size(114, 32);
            btnPreviousGPU2.TabIndex = 36;
            btnPreviousGPU2.Text = "Previous GPU";
            btnPreviousGPU2.UseVisualStyleBackColor = true;
            // 
            // btnNextGPU2
            // 
            btnNextGPU2.Location = new Point(775, 404);
            btnNextGPU2.Name = "btnNextGPU2";
            btnNextGPU2.Size = new Size(114, 32);
            btnNextGPU2.TabIndex = 37;
            btnNextGPU2.Text = "Next GPU";
            btnNextGPU2.UseVisualStyleBackColor = true;
            // 
            // FormGPUInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 479);
            Controls.Add(btnNextGPU2);
            Controls.Add(btnPreviousGPU2);
            Controls.Add(btnRefresh);
            Controls.Add(lblGPUOpenGL);
            Controls.Add(lblGPUPhysX);
            Controls.Add(lblGPURayTracing);
            Controls.Add(lblGPUVulkan);
            Controls.Add(lblGPUDirectML);
            Controls.Add(lblGPUDirectCompute);
            Controls.Add(lblGPUCUDA);
            Controls.Add(lblGPUOpenCL);
            Controls.Add(lblGPUPowerUsage);
            Controls.Add(lblGPUUtilization);
            Controls.Add(lblGPUMemoryBandwidth);
            Controls.Add(lblGPUBIOSVersion);
            Controls.Add(lblGPUMemoryClock);
            Controls.Add(lblGPUCoreClock);
            Controls.Add(lblGPUTemperature);
            Controls.Add(picLogo);
            Controls.Add(labelGPUInfoCaption);
            Controls.Add(lblGPUStatus);
            Controls.Add(lblGPUAvailability);
            Controls.Add(lblGPUDriverDate);
            Controls.Add(lblGPUMemoryType);
            Controls.Add(lblGPUType);
            Controls.Add(lblGPUManufacturer);
            Controls.Add(lblGPUArchitecture);
            Controls.Add(lblGPUProcessor);
            Controls.Add(lblGPUDriver);
            Controls.Add(lblGPUMemory);
            Controls.Add(lblGPUDescription);
            Controls.Add(lblGPUName);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormGPUInfo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GPU Bilgileri";
            FormClosed += FormGPUInfo_FormClosed;
            Load += FormGPUInfo_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGPUName;
        private Label lblGPUDescription;
        private Label lblGPUMemory;
        private Label lblGPUDriver;
        private Label lblGPUProcessor;
        private Label lblGPUArchitecture;
        private Label lblGPUManufacturer;
        private Label lblGPUType;
        private Label lblGPUMemoryType;
        private Label lblGPUDriverDate;
        private Label lblGPUAvailability;
        private Label lblGPUStatus;
        private Label labelGPUInfoCaption;
        private PictureBox picLogo;
        private Label lblGPUTemperature;
        private Label lblGPUCoreClock;
        private Label lblGPUMemoryClock;
        private Label lblGPUBIOSVersion;
        private Label lblGPUMemoryBandwidth;
        private Label lblGPUUtilization;
        private Label lblGPUPowerUsage;
        private Label lblGPUOpenCL;
        private Label lblGPUCUDA;
        private Label lblGPUDirectCompute;
        private Label lblGPUDirectML;
        private Label lblGPUVulkan;
        private Label lblGPURayTracing;
        private Label lblGPUPhysX;
        private Label lblGPUOpenGL;
        private Button btnRefresh;
        private Button btnPreviousGPU2;
        private Button btnNextGPU2;
    }
}