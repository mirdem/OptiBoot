namespace OptiBoot
{
    partial class FormNewTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTask));
            textBoxTaskName = new TextBox();
            textBoxTaskDescription = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            comboBoxStatus = new ComboBox();
            comboBoxRepeat = new ComboBox();
            buttonSaveTask = new Button();
            buttonCancel = new Button();
            labelName = new Label();
            labelDescription = new Label();
            labelTime = new Label();
            labelStatus = new Label();
            labelRepeat = new Label();
            labelHeader = new Label();
            labelSonuc = new Label();
            grpEylemAyarlari = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            comboBoxEylemTuru = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btnProgramSec = new Button();
            txtBaslangicYolu = new TextBox();
            txtArgumanlar = new TextBox();
            txtProgramYolu = new TextBox();
            grpEylemAyarlari.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxTaskName
            // 
            textBoxTaskName.Location = new Point(118, 73);
            textBoxTaskName.Name = "textBoxTaskName";
            textBoxTaskName.Size = new Size(136, 23);
            textBoxTaskName.TabIndex = 0;
            // 
            // textBoxTaskDescription
            // 
            textBoxTaskDescription.Location = new Point(118, 102);
            textBoxTaskDescription.Multiline = true;
            textBoxTaskDescription.Name = "textBoxTaskDescription";
            textBoxTaskDescription.Size = new Size(265, 117);
            textBoxTaskDescription.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(118, 225);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.FormattingEnabled = true;
            comboBoxStatus.Location = new Point(118, 295);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(149, 23);
            comboBoxStatus.TabIndex = 3;
            // 
            // comboBoxRepeat
            // 
            comboBoxRepeat.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRepeat.FormattingEnabled = true;
            comboBoxRepeat.Location = new Point(118, 324);
            comboBoxRepeat.Name = "comboBoxRepeat";
            comboBoxRepeat.Size = new Size(149, 23);
            comboBoxRepeat.TabIndex = 4;
            // 
            // buttonSaveTask
            // 
            buttonSaveTask.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSaveTask.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonSaveTask.FlatStyle = FlatStyle.Flat;
            buttonSaveTask.Location = new Point(448, 738);
            buttonSaveTask.Name = "buttonSaveTask";
            buttonSaveTask.Size = new Size(92, 36);
            buttonSaveTask.TabIndex = 5;
            buttonSaveTask.Text = "Kaydet";
            buttonSaveTask.UseVisualStyleBackColor = true;
            buttonSaveTask.Click += buttonSaveTask_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Location = new Point(448, 780);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(92, 36);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "İptal";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            labelName.Location = new Point(16, 73);
            labelName.Name = "labelName";
            labelName.Size = new Size(33, 17);
            labelName.TabIndex = 7;
            labelName.Text = "Adı:";
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            labelDescription.Location = new Point(16, 107);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(68, 17);
            labelDescription.TabIndex = 8;
            labelDescription.Text = "Açıklama:";
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            labelTime.Location = new Point(16, 229);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(50, 17);
            labelTime.TabIndex = 9;
            labelTime.Text = "Zaman";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            labelStatus.Location = new Point(16, 301);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(50, 17);
            labelStatus.TabIndex = 10;
            labelStatus.Text = "Durum";
            // 
            // labelRepeat
            // 
            labelRepeat.AutoSize = true;
            labelRepeat.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            labelRepeat.Location = new Point(16, 330);
            labelRepeat.Name = "labelRepeat";
            labelRepeat.Size = new Size(45, 17);
            labelRepeat.TabIndex = 11;
            labelRepeat.Text = "Tekrar";
            // 
            // labelHeader
            // 
            labelHeader.AutoSize = true;
            labelHeader.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            labelHeader.Location = new Point(16, 27);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(135, 18);
            labelHeader.TabIndex = 12;
            labelHeader.Text = "Yeni Görev Oluştur";
            // 
            // labelSonuc
            // 
            labelSonuc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelSonuc.AutoSize = true;
            labelSonuc.Font = new Font("Arial Narrow", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSonuc.Location = new Point(12, 796);
            labelSonuc.Name = "labelSonuc";
            labelSonuc.Size = new Size(159, 20);
            labelSonuc.TabIndex = 13;
            labelSonuc.Text = "Durum: İşlem Bekleniyor";
            // 
            // grpEylemAyarlari
            // 
            grpEylemAyarlari.Controls.Add(label4);
            grpEylemAyarlari.Controls.Add(label3);
            grpEylemAyarlari.Controls.Add(comboBoxEylemTuru);
            grpEylemAyarlari.Controls.Add(label2);
            grpEylemAyarlari.Controls.Add(label1);
            grpEylemAyarlari.Controls.Add(btnProgramSec);
            grpEylemAyarlari.Controls.Add(txtBaslangicYolu);
            grpEylemAyarlari.Controls.Add(txtArgumanlar);
            grpEylemAyarlari.Controls.Add(txtProgramYolu);
            grpEylemAyarlari.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            grpEylemAyarlari.Location = new Point(16, 417);
            grpEylemAyarlari.Name = "grpEylemAyarlari";
            grpEylemAyarlari.Size = new Size(494, 297);
            grpEylemAyarlari.TabIndex = 14;
            grpEylemAyarlari.TabStop = false;
            grpEylemAyarlari.Text = "Eylem Ayarları";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label4.Location = new Point(26, 164);
            label4.Name = "label4";
            label4.Size = new Size(78, 17);
            label4.TabIndex = 16;
            label4.Text = "Eylem Türü:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label3.Location = new Point(26, 126);
            label3.Name = "label3";
            label3.Size = new Size(97, 17);
            label3.TabIndex = 21;
            label3.Text = "Başlangıç Yolu:";
            // 
            // comboBoxEylemTuru
            // 
            comboBoxEylemTuru.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEylemTuru.Font = new Font("Segoe UI", 11.25F);
            comboBoxEylemTuru.FormattingEnabled = true;
            comboBoxEylemTuru.Location = new Point(142, 158);
            comboBoxEylemTuru.Name = "comboBoxEylemTuru";
            comboBoxEylemTuru.Size = new Size(257, 28);
            comboBoxEylemTuru.TabIndex = 15;
            comboBoxEylemTuru.SelectedIndexChanged += comboBoxEylemTuru_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label2.Location = new Point(26, 82);
            label2.Name = "label2";
            label2.Size = new Size(110, 35);
            label2.TabIndex = 20;
            label2.Text = "Argümanlar (İsteğe bağlı):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label1.Location = new Point(25, 42);
            label1.Name = "label1";
            label1.Size = new Size(94, 17);
            label1.TabIndex = 19;
            label1.Text = "Program Yolu:";
            // 
            // btnProgramSec
            // 
            btnProgramSec.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnProgramSec.FlatStyle = FlatStyle.Flat;
            btnProgramSec.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnProgramSec.Location = new Point(405, 42);
            btnProgramSec.Name = "btnProgramSec";
            btnProgramSec.Size = new Size(75, 27);
            btnProgramSec.TabIndex = 18;
            btnProgramSec.Text = "Gözat";
            btnProgramSec.UseVisualStyleBackColor = true;
            btnProgramSec.Click += btnProgramSec_Click;
            // 
            // txtBaslangicYolu
            // 
            txtBaslangicYolu.Font = new Font("Segoe UI", 11.25F);
            txtBaslangicYolu.Location = new Point(142, 119);
            txtBaslangicYolu.Name = "txtBaslangicYolu";
            txtBaslangicYolu.Size = new Size(257, 27);
            txtBaslangicYolu.TabIndex = 17;
            // 
            // txtArgumanlar
            // 
            txtArgumanlar.Font = new Font("Segoe UI", 11.25F);
            txtArgumanlar.Location = new Point(142, 81);
            txtArgumanlar.Name = "txtArgumanlar";
            txtArgumanlar.Size = new Size(257, 27);
            txtArgumanlar.TabIndex = 16;
            // 
            // txtProgramYolu
            // 
            txtProgramYolu.Font = new Font("Segoe UI", 11.25F);
            txtProgramYolu.Location = new Point(142, 42);
            txtProgramYolu.Name = "txtProgramYolu";
            txtProgramYolu.Size = new Size(257, 27);
            txtProgramYolu.TabIndex = 15;
            // 
            // FormNewTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(552, 828);
            Controls.Add(grpEylemAyarlari);
            Controls.Add(labelSonuc);
            Controls.Add(labelHeader);
            Controls.Add(labelRepeat);
            Controls.Add(labelStatus);
            Controls.Add(labelTime);
            Controls.Add(labelDescription);
            Controls.Add(labelName);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSaveTask);
            Controls.Add(comboBoxRepeat);
            Controls.Add(comboBoxStatus);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBoxTaskDescription);
            Controls.Add(textBoxTaskName);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormNewTask";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yeni Görev Oluştur";
            Load += FormNewTask_Load;
            grpEylemAyarlari.ResumeLayout(false);
            grpEylemAyarlari.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxTaskName;
        private TextBox textBoxTaskDescription;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBoxStatus;
        private ComboBox comboBoxRepeat;
        private Button buttonSaveTask;
        private Button buttonCancel;
        private Label labelName;
        private Label labelDescription;
        private Label labelTime;
        private Label labelStatus;
        private Label labelRepeat;
        private Label labelHeader;
        private Label labelSonuc;
        private GroupBox grpEylemAyarlari;
        private TextBox txtBaslangicYolu;
        private TextBox txtArgumanlar;
        private TextBox txtProgramYolu;
        private Button btnProgramSec;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private ComboBox comboBoxEylemTuru;
    }
}