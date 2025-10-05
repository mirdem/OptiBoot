namespace OptiBoot
{
    partial class FormSelectScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectScreen));
            buttonStartupApps = new Button();
            buttonServices = new Button();
            buttonPerformance = new Button();
            buttonRAM = new Button();
            buttonGPU = new Button();
            labelVersion = new Label();
            toolTip1 = new ToolTip(components);
            buttonSoundAdapter = new Button();
            buttonDiskManager = new Button();
            buttonSensors = new Button();
            buttonAbout = new Button();
            buttonUpdates = new Button();
            buttonSettings = new Button();
            buttonTasks = new Button();
            buttonWinProcess = new Button();
            SuspendLayout();
            // 
            // buttonStartupApps
            // 
            buttonStartupApps.FlatAppearance.BorderSize = 0;
            buttonStartupApps.FlatStyle = FlatStyle.Flat;
            buttonStartupApps.Image = (Image)resources.GetObject("buttonStartupApps.Image");
            buttonStartupApps.ImageAlign = ContentAlignment.TopCenter;
            buttonStartupApps.Location = new Point(57, 30);
            buttonStartupApps.Name = "buttonStartupApps";
            buttonStartupApps.Size = new Size(76, 106);
            buttonStartupApps.TabIndex = 0;
            buttonStartupApps.Text = "Başlangıç";
            buttonStartupApps.TextAlign = ContentAlignment.BottomCenter;
            buttonStartupApps.UseVisualStyleBackColor = true;
            buttonStartupApps.Click += buttonStartupApps_Click;
            // 
            // buttonServices
            // 
            buttonServices.FlatAppearance.BorderSize = 0;
            buttonServices.FlatStyle = FlatStyle.Flat;
            buttonServices.Image = (Image)resources.GetObject("buttonServices.Image");
            buttonServices.ImageAlign = ContentAlignment.TopCenter;
            buttonServices.Location = new Point(165, 30);
            buttonServices.Name = "buttonServices";
            buttonServices.Size = new Size(76, 106);
            buttonServices.TabIndex = 1;
            buttonServices.Text = "Servisler";
            buttonServices.TextAlign = ContentAlignment.BottomCenter;
            buttonServices.UseVisualStyleBackColor = true;
            buttonServices.Click += buttonServices_Click;
            // 
            // buttonPerformance
            // 
            buttonPerformance.FlatAppearance.BorderSize = 0;
            buttonPerformance.FlatStyle = FlatStyle.Flat;
            buttonPerformance.Image = (Image)resources.GetObject("buttonPerformance.Image");
            buttonPerformance.ImageAlign = ContentAlignment.TopCenter;
            buttonPerformance.Location = new Point(275, 30);
            buttonPerformance.Name = "buttonPerformance";
            buttonPerformance.Size = new Size(78, 106);
            buttonPerformance.TabIndex = 2;
            buttonPerformance.Text = "CPU İzleyici";
            buttonPerformance.TextAlign = ContentAlignment.BottomCenter;
            buttonPerformance.UseVisualStyleBackColor = true;
            buttonPerformance.Click += buttonPerformance_Click;
            // 
            // buttonRAM
            // 
            buttonRAM.FlatAppearance.BorderSize = 0;
            buttonRAM.FlatStyle = FlatStyle.Flat;
            buttonRAM.Image = (Image)resources.GetObject("buttonRAM.Image");
            buttonRAM.ImageAlign = ContentAlignment.TopCenter;
            buttonRAM.Location = new Point(57, 162);
            buttonRAM.Name = "buttonRAM";
            buttonRAM.Size = new Size(99, 106);
            buttonRAM.TabIndex = 3;
            buttonRAM.Text = " RAM Monitörü";
            buttonRAM.TextAlign = ContentAlignment.BottomCenter;
            buttonRAM.UseVisualStyleBackColor = true;
            buttonRAM.Click += buttonRAM_Click;
            // 
            // buttonGPU
            // 
            buttonGPU.FlatAppearance.BorderSize = 0;
            buttonGPU.FlatStyle = FlatStyle.Flat;
            buttonGPU.Image = (Image)resources.GetObject("buttonGPU.Image");
            buttonGPU.ImageAlign = ContentAlignment.TopCenter;
            buttonGPU.Location = new Point(165, 162);
            buttonGPU.Name = "buttonGPU";
            buttonGPU.Size = new Size(89, 106);
            buttonGPU.TabIndex = 4;
            buttonGPU.Text = "GPU Detayları";
            buttonGPU.TextAlign = ContentAlignment.BottomCenter;
            buttonGPU.UseVisualStyleBackColor = true;
            buttonGPU.Click += buttonGPU_Click;
            // 
            // labelVersion
            // 
            labelVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelVersion.AutoSize = true;
            labelVersion.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 162);
            labelVersion.Location = new Point(460, 530);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(38, 13);
            labelVersion.TabIndex = 5;
            labelVersion.Text = "label1";
            // 
            // buttonSoundAdapter
            // 
            buttonSoundAdapter.FlatAppearance.BorderSize = 0;
            buttonSoundAdapter.FlatStyle = FlatStyle.Flat;
            buttonSoundAdapter.Image = (Image)resources.GetObject("buttonSoundAdapter.Image");
            buttonSoundAdapter.ImageAlign = ContentAlignment.TopCenter;
            buttonSoundAdapter.Location = new Point(275, 162);
            buttonSoundAdapter.Name = "buttonSoundAdapter";
            buttonSoundAdapter.Size = new Size(82, 106);
            buttonSoundAdapter.TabIndex = 7;
            buttonSoundAdapter.Text = "Ses Cihazları";
            buttonSoundAdapter.TextAlign = ContentAlignment.BottomCenter;
            buttonSoundAdapter.UseVisualStyleBackColor = true;
            buttonSoundAdapter.Click += buttonSoundAdapter_Click;
            // 
            // buttonDiskManager
            // 
            buttonDiskManager.FlatAppearance.BorderSize = 0;
            buttonDiskManager.FlatStyle = FlatStyle.Flat;
            buttonDiskManager.Image = (Image)resources.GetObject("buttonDiskManager.Image");
            buttonDiskManager.ImageAlign = ContentAlignment.TopCenter;
            buttonDiskManager.Location = new Point(385, 162);
            buttonDiskManager.Name = "buttonDiskManager";
            buttonDiskManager.Size = new Size(89, 106);
            buttonDiskManager.TabIndex = 8;
            buttonDiskManager.Text = "Disk Yönetimi";
            buttonDiskManager.TextAlign = ContentAlignment.BottomCenter;
            buttonDiskManager.UseVisualStyleBackColor = true;
            buttonDiskManager.Click += buttonDiskManager_Click;
            // 
            // buttonSensors
            // 
            buttonSensors.FlatAppearance.BorderSize = 0;
            buttonSensors.FlatStyle = FlatStyle.Flat;
            buttonSensors.Image = (Image)resources.GetObject("buttonSensors.Image");
            buttonSensors.ImageAlign = ContentAlignment.TopCenter;
            buttonSensors.Location = new Point(162, 297);
            buttonSensors.Name = "buttonSensors";
            buttonSensors.Size = new Size(103, 106);
            buttonSensors.TabIndex = 9;
            buttonSensors.Text = "Sensörler {BETA]";
            buttonSensors.TextAlign = ContentAlignment.BottomCenter;
            buttonSensors.UseVisualStyleBackColor = true;
            buttonSensors.Click += buttonSensors_Click;
            // 
            // buttonAbout
            // 
            buttonAbout.Anchor = AnchorStyles.Bottom;
            buttonAbout.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonAbout.FlatStyle = FlatStyle.Flat;
            buttonAbout.Image = (Image)resources.GetObject("buttonAbout.Image");
            buttonAbout.ImageAlign = ContentAlignment.MiddleLeft;
            buttonAbout.Location = new Point(38, 496);
            buttonAbout.Name = "buttonAbout";
            buttonAbout.Size = new Size(133, 36);
            buttonAbout.TabIndex = 10;
            buttonAbout.Text = "About";
            buttonAbout.TextAlign = ContentAlignment.MiddleRight;
            buttonAbout.UseVisualStyleBackColor = true;
            buttonAbout.Click += buttonAbout_Click;
            // 
            // buttonUpdates
            // 
            buttonUpdates.Anchor = AnchorStyles.Bottom;
            buttonUpdates.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonUpdates.FlatStyle = FlatStyle.Flat;
            buttonUpdates.Image = (Image)resources.GetObject("buttonUpdates.Image");
            buttonUpdates.ImageAlign = ContentAlignment.MiddleLeft;
            buttonUpdates.Location = new Point(180, 496);
            buttonUpdates.Name = "buttonUpdates";
            buttonUpdates.Size = new Size(133, 36);
            buttonUpdates.TabIndex = 11;
            buttonUpdates.Text = "Updates";
            buttonUpdates.TextAlign = ContentAlignment.MiddleRight;
            buttonUpdates.UseVisualStyleBackColor = true;
            buttonUpdates.Click += buttonUpdates_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Anchor = AnchorStyles.Bottom;
            buttonSettings.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonSettings.FlatStyle = FlatStyle.Flat;
            buttonSettings.Image = (Image)resources.GetObject("buttonSettings.Image");
            buttonSettings.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSettings.Location = new Point(327, 496);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(133, 36);
            buttonSettings.TabIndex = 12;
            buttonSettings.Text = "Settings";
            buttonSettings.TextAlign = ContentAlignment.MiddleRight;
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // buttonTasks
            // 
            buttonTasks.FlatAppearance.BorderSize = 0;
            buttonTasks.FlatStyle = FlatStyle.Flat;
            buttonTasks.Image = (Image)resources.GetObject("buttonTasks.Image");
            buttonTasks.ImageAlign = ContentAlignment.TopCenter;
            buttonTasks.Location = new Point(53, 297);
            buttonTasks.Name = "buttonTasks";
            buttonTasks.Size = new Size(103, 106);
            buttonTasks.TabIndex = 13;
            buttonTasks.Text = "Görevler";
            buttonTasks.TextAlign = ContentAlignment.BottomCenter;
            buttonTasks.UseVisualStyleBackColor = true;
            buttonTasks.Click += buttonTasks_Click;
            // 
            // buttonWinProcess
            // 
            buttonWinProcess.FlatAppearance.BorderSize = 0;
            buttonWinProcess.FlatStyle = FlatStyle.Flat;
            buttonWinProcess.Image = (Image)resources.GetObject("buttonWinProcess.Image");
            buttonWinProcess.ImageAlign = ContentAlignment.TopCenter;
            buttonWinProcess.Location = new Point(371, 30);
            buttonWinProcess.Name = "buttonWinProcess";
            buttonWinProcess.Size = new Size(103, 106);
            buttonWinProcess.TabIndex = 14;
            buttonWinProcess.Text = "Windows İşlemleri";
            buttonWinProcess.TextAlign = ContentAlignment.BottomCenter;
            buttonWinProcess.UseVisualStyleBackColor = true;
            buttonWinProcess.Click += buttonWinProcess_Click;
            // 
            // FormSelectScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 544);
            Controls.Add(buttonWinProcess);
            Controls.Add(buttonTasks);
            Controls.Add(buttonSettings);
            Controls.Add(buttonUpdates);
            Controls.Add(buttonAbout);
            Controls.Add(buttonSensors);
            Controls.Add(buttonDiskManager);
            Controls.Add(buttonSoundAdapter);
            Controls.Add(labelVersion);
            Controls.Add(buttonGPU);
            Controls.Add(buttonRAM);
            Controls.Add(buttonPerformance);
            Controls.Add(buttonServices);
            Controls.Add(buttonStartupApps);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(434, 583);
            Name = "FormSelectScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OptiBoot";
            FormClosed += FormSelectScreen_FormClosed;
            Load += FormSelectScreen_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStartupApps;
        private Button buttonServices;
        private Button buttonPerformance;
        private Button buttonRAM;
        private Button buttonGPU;
        private Label labelVersion;
        private ToolTip toolTip1;
        private Button buttonSoundAdapter;
        private Button buttonDiskManager;
        private Button buttonSensors;
        private Button buttonAbout;
        private Button buttonUpdates;
        private Button buttonSettings;
        private Button buttonTasks;
        private Button buttonWinProcess;
    }
}