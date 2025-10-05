namespace OptiBoot
{
    partial class FormWindowsProcesses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWindowsProcesses));
            listBoxProcess = new ListBox();
            textBoxSearch = new TextBox();
            buttonKillProcess = new Button();
            labelTime = new Label();
            dateTimePicker1 = new DateTimePicker();
            buttonSetTimer = new Button();
            contextMenu = new ContextMenuStrip(components);
            groupBoxCountdown = new GroupBox();
            buttonCancel = new Button();
            labelCountdown = new Label();
            labelCountdownInfo = new Label();
            pictureBox1 = new PictureBox();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            buttonOpenLocation = new Button();
            labelPriority = new Label();
            labelFilePath = new Label();
            labelStartTime = new Label();
            labelRunningTime = new Label();
            labelMemory = new Label();
            labelCPU = new Label();
            labelTotalProcess = new Label();
            checkBoxHideSystemProcesses = new CheckBox();
            groupBoxCountdown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxProcess
            // 
            listBoxProcess.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxProcess.FormattingEnabled = true;
            listBoxProcess.ItemHeight = 15;
            listBoxProcess.Location = new Point(12, 338);
            listBoxProcess.Name = "listBoxProcess";
            listBoxProcess.Size = new Size(537, 424);
            listBoxProcess.TabIndex = 0;
            listBoxProcess.SelectedIndexChanged += listBoxProcess_SelectedIndexChanged;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(41, 308);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(172, 23);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // buttonKillProcess
            // 
            buttonKillProcess.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonKillProcess.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonKillProcess.FlatStyle = FlatStyle.Flat;
            buttonKillProcess.Image = (Image)resources.GetObject("buttonKillProcess.Image");
            buttonKillProcess.ImageAlign = ContentAlignment.MiddleLeft;
            buttonKillProcess.Location = new Point(444, 303);
            buttonKillProcess.Name = "buttonKillProcess";
            buttonKillProcess.Size = new Size(105, 30);
            buttonKillProcess.TabIndex = 3;
            buttonKillProcess.Text = "Sonlandır";
            buttonKillProcess.TextAlign = ContentAlignment.MiddleRight;
            buttonKillProcess.UseVisualStyleBackColor = true;
            buttonKillProcess.Click += buttonKillProcess_Click;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            labelTime.Location = new Point(6, 32);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(37, 17);
            labelTime.TabIndex = 4;
            labelTime.Text = "Saat:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(63, 32);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(109, 22);
            dateTimePicker1.TabIndex = 5;
            // 
            // buttonSetTimer
            // 
            buttonSetTimer.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonSetTimer.FlatStyle = FlatStyle.Flat;
            buttonSetTimer.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
            buttonSetTimer.Location = new Point(178, 27);
            buttonSetTimer.Name = "buttonSetTimer";
            buttonSetTimer.Size = new Size(75, 30);
            buttonSetTimer.TabIndex = 6;
            buttonSetTimer.Text = "Ayarla";
            buttonSetTimer.UseVisualStyleBackColor = true;
            buttonSetTimer.Click += buttonSetTimer_Click;
            // 
            // contextMenu
            // 
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(61, 4);
            // 
            // groupBoxCountdown
            // 
            groupBoxCountdown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxCountdown.Controls.Add(buttonCancel);
            groupBoxCountdown.Controls.Add(labelCountdown);
            groupBoxCountdown.Controls.Add(labelCountdownInfo);
            groupBoxCountdown.Controls.Add(dateTimePicker1);
            groupBoxCountdown.Controls.Add(labelTime);
            groupBoxCountdown.Controls.Add(buttonSetTimer);
            groupBoxCountdown.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBoxCountdown.Location = new Point(12, 12);
            groupBoxCountdown.Name = "groupBoxCountdown";
            groupBoxCountdown.Size = new Size(537, 112);
            groupBoxCountdown.TabIndex = 8;
            groupBoxCountdown.TabStop = false;
            groupBoxCountdown.Text = "Geri sayım";
            // 
            // buttonCancel
            // 
            buttonCancel.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Arial Black", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            buttonCancel.ForeColor = Color.Red;
            buttonCancel.Location = new Point(261, 28);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(30, 29);
            buttonCancel.TabIndex = 9;
            buttonCancel.Text = "X";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // labelCountdown
            // 
            labelCountdown.ForeColor = Color.IndianRed;
            labelCountdown.Location = new Point(334, 18);
            labelCountdown.Name = "labelCountdown";
            labelCountdown.Size = new Size(197, 59);
            labelCountdown.TabIndex = 8;
            // 
            // labelCountdownInfo
            // 
            labelCountdownInfo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            labelCountdownInfo.Location = new Point(8, 66);
            labelCountdownInfo.Name = "labelCountdownInfo";
            labelCountdownInfo.Size = new Size(283, 43);
            labelCountdownInfo.TabIndex = 7;
            labelCountdownInfo.Text = "Süre sonunda seçtiğniz işlem sonlandırılır.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.search;
            pictureBox1.Location = new Point(12, 308);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 24);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(buttonOpenLocation);
            groupBox1.Controls.Add(labelPriority);
            groupBox1.Controls.Add(labelFilePath);
            groupBox1.Controls.Add(labelStartTime);
            groupBox1.Controls.Add(labelRunningTime);
            groupBox1.Controls.Add(labelMemory);
            groupBox1.Controls.Add(labelCPU);
            groupBox1.Location = new Point(12, 133);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(537, 143);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "İşlem Detayları";
            // 
            // buttonOpenLocation
            // 
            buttonOpenLocation.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonOpenLocation.FlatStyle = FlatStyle.Flat;
            buttonOpenLocation.Location = new Point(434, 105);
            buttonOpenLocation.Name = "buttonOpenLocation";
            buttonOpenLocation.Size = new Size(97, 32);
            buttonOpenLocation.TabIndex = 6;
            buttonOpenLocation.Text = "Çalıştır";
            buttonOpenLocation.UseVisualStyleBackColor = true;
            buttonOpenLocation.Click += buttonOpenLocation_Click;
            // 
            // labelPriority
            // 
            labelPriority.AutoSize = true;
            labelPriority.Location = new Point(234, 88);
            labelPriority.Name = "labelPriority";
            labelPriority.Size = new Size(50, 15);
            labelPriority.TabIndex = 5;
            labelPriority.Text = "Öncelik:";
            // 
            // labelFilePath
            // 
            labelFilePath.Location = new Point(234, 46);
            labelFilePath.Name = "labelFilePath";
            labelFilePath.Size = new Size(297, 42);
            labelFilePath.TabIndex = 4;
            labelFilePath.Text = "Konum:";
            // 
            // labelStartTime
            // 
            labelStartTime.AutoSize = true;
            labelStartTime.Location = new Point(234, 24);
            labelStartTime.Name = "labelStartTime";
            labelStartTime.Size = new Size(103, 15);
            labelStartTime.TabIndex = 3;
            labelStartTime.Text = "Başlangıç Zamanı:";
            // 
            // labelRunningTime
            // 
            labelRunningTime.AutoSize = true;
            labelRunningTime.Location = new Point(29, 88);
            labelRunningTime.Name = "labelRunningTime";
            labelRunningTime.Size = new Size(95, 15);
            labelRunningTime.TabIndex = 2;
            labelRunningTime.Text = "Çalışma Zamanı:";
            // 
            // labelMemory
            // 
            labelMemory.AutoSize = true;
            labelMemory.Location = new Point(29, 46);
            labelMemory.Name = "labelMemory";
            labelMemory.Size = new Size(41, 15);
            labelMemory.TabIndex = 1;
            labelMemory.Text = "Bellek:";
            // 
            // labelCPU
            // 
            labelCPU.AutoSize = true;
            labelCPU.Location = new Point(29, 24);
            labelCPU.Name = "labelCPU";
            labelCPU.Size = new Size(33, 15);
            labelCPU.TabIndex = 0;
            labelCPU.Text = "CPU:";
            // 
            // labelTotalProcess
            // 
            labelTotalProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelTotalProcess.AutoSize = true;
            labelTotalProcess.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            labelTotalProcess.Location = new Point(12, 765);
            labelTotalProcess.Name = "labelTotalProcess";
            labelTotalProcess.Size = new Size(0, 15);
            labelTotalProcess.TabIndex = 18;
            // 
            // checkBoxHideSystemProcesses
            // 
            checkBoxHideSystemProcesses.AutoSize = true;
            checkBoxHideSystemProcesses.Location = new Point(221, 313);
            checkBoxHideSystemProcesses.Name = "checkBoxHideSystemProcesses";
            checkBoxHideSystemProcesses.Size = new Size(156, 19);
            checkBoxHideSystemProcesses.TabIndex = 19;
            checkBoxHideSystemProcesses.Text = "Sistem Hizmetlerini Gizle";
            checkBoxHideSystemProcesses.UseVisualStyleBackColor = true;
            checkBoxHideSystemProcesses.CheckedChanged += checkBoxHideSystemProcesses_CheckedChanged;
            // 
            // FormWindowsProcesses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 789);
            Controls.Add(checkBoxHideSystemProcesses);
            Controls.Add(labelTotalProcess);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(groupBoxCountdown);
            Controls.Add(buttonKillProcess);
            Controls.Add(textBoxSearch);
            Controls.Add(listBoxProcess);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormWindowsProcesses";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Windows Processes";
            FormClosed += FormWindowsProcesses_FormClosed;
            Load += FormWindowsProcesses_Load;
            groupBoxCountdown.ResumeLayout(false);
            groupBoxCountdown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxProcess;
        private TextBox textBoxSearch;
        private Button buttonKillProcess;
        private Label labelTime;
        private DateTimePicker dateTimePicker1;
        private Button buttonSetTimer;
        private ContextMenuStrip contextMenu;
        private GroupBox groupBoxCountdown;
        private Label labelCountdownInfo;
        private Label labelCountdown;
        private Button buttonCancel;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer countdownTimer;
        private ToolTip toolTip1;
        private GroupBox groupBox1;
        private Label labelPriority;
        private Label labelFilePath;
        private Label labelStartTime;
        private Label labelRunningTime;
        private Label labelMemory;
        private Label labelCPU;
        private Button buttonOpenLocation;
        private Label labelTotalProcess;
        private CheckBox checkBoxHideSystemProcesses;
    }
}