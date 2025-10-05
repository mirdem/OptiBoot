namespace OptiBoot
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            tabPageGenel = new TabPage();
            checkBoxRunStartsWin = new CheckBox();
            checkBoxAlwaysTop = new CheckBox();
            checkBoxCheckUpdates = new CheckBox();
            checkBoxSystemTray = new CheckBox();
            labelLanguage = new Label();
            comboBoxLanguage = new ComboBox();
            tabPageOther = new TabPage();
            groupBoxTheme = new GroupBox();
            radioButtonThemeDark = new RadioButton();
            radioButtonThemeLight = new RadioButton();
            buttonClearLogs = new Button();
            buttonShowLogs = new Button();
            labelSysLogs = new Label();
            panel1 = new Panel();
            buttonTabOther = new Button();
            buttonTabGeneral = new Button();
            buttonSaveSettings = new Button();
            buttonDefaultSettings = new Button();
            labelAppVersion = new Label();
            toolTip1 = new ToolTip(components);
            materialTabControl1.SuspendLayout();
            tabPageGenel.SuspendLayout();
            tabPageOther.SuspendLayout();
            groupBoxTheme.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // materialTabControl1
            // 
            materialTabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialTabControl1.Controls.Add(tabPageGenel);
            materialTabControl1.Controls.Add(tabPageOther);
            materialTabControl1.Depth = 0;
            materialTabControl1.Location = new Point(157, 3);
            materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(646, 429);
            materialTabControl1.TabIndex = 0;
            // 
            // tabPageGenel
            // 
            tabPageGenel.Controls.Add(checkBoxRunStartsWin);
            tabPageGenel.Controls.Add(checkBoxAlwaysTop);
            tabPageGenel.Controls.Add(checkBoxCheckUpdates);
            tabPageGenel.Controls.Add(checkBoxSystemTray);
            tabPageGenel.Controls.Add(labelLanguage);
            tabPageGenel.Controls.Add(comboBoxLanguage);
            tabPageGenel.Location = new Point(4, 24);
            tabPageGenel.Name = "tabPageGenel";
            tabPageGenel.Size = new Size(638, 401);
            tabPageGenel.TabIndex = 2;
            tabPageGenel.Text = "Genel";
            tabPageGenel.UseVisualStyleBackColor = true;
            // 
            // checkBoxRunStartsWin
            // 
            checkBoxRunStartsWin.AutoSize = true;
            checkBoxRunStartsWin.Location = new Point(17, 200);
            checkBoxRunStartsWin.Name = "checkBoxRunStartsWin";
            checkBoxRunStartsWin.Size = new Size(249, 19);
            checkBoxRunStartsWin.TabIndex = 5;
            checkBoxRunStartsWin.Text = "Windows başlangıcında OptiBoot'u çalıştır";
            checkBoxRunStartsWin.UseVisualStyleBackColor = true;
            checkBoxRunStartsWin.CheckedChanged += checkBoxRunStartsWin_CheckedChanged;
            // 
            // checkBoxAlwaysTop
            // 
            checkBoxAlwaysTop.AutoSize = true;
            checkBoxAlwaysTop.Location = new Point(17, 95);
            checkBoxAlwaysTop.Name = "checkBoxAlwaysTop";
            checkBoxAlwaysTop.Size = new Size(130, 19);
            checkBoxAlwaysTop.TabIndex = 4;
            checkBoxAlwaysTop.Text = "Her zaman üstte tut";
            checkBoxAlwaysTop.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckUpdates
            // 
            checkBoxCheckUpdates.AutoSize = true;
            checkBoxCheckUpdates.Location = new Point(17, 165);
            checkBoxCheckUpdates.Name = "checkBoxCheckUpdates";
            checkBoxCheckUpdates.Size = new Size(198, 19);
            checkBoxCheckUpdates.TabIndex = 3;
            checkBoxCheckUpdates.Text = "Güncellemeleri açılışta kontrol et";
            checkBoxCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // checkBoxSystemTray
            // 
            checkBoxSystemTray.AutoSize = true;
            checkBoxSystemTray.Location = new Point(17, 129);
            checkBoxSystemTray.Name = "checkBoxSystemTray";
            checkBoxSystemTray.Size = new Size(285, 19);
            checkBoxSystemTray.TabIndex = 2;
            checkBoxSystemTray.Text = "Uygulama kapatılınca simge durumuna küçülsün";
            checkBoxSystemTray.UseVisualStyleBackColor = true;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelLanguage.Location = new Point(16, 18);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(23, 17);
            labelLanguage.TabIndex = 1;
            labelLanguage.Text = "Dil";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Items.AddRange(new object[] { "English", "Turkish", "Chinese", "German", "Portuguese" });
            comboBoxLanguage.Location = new Point(16, 38);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(121, 23);
            comboBoxLanguage.TabIndex = 0;
            // 
            // tabPageOther
            // 
            tabPageOther.Controls.Add(groupBoxTheme);
            tabPageOther.Controls.Add(buttonClearLogs);
            tabPageOther.Controls.Add(buttonShowLogs);
            tabPageOther.Controls.Add(labelSysLogs);
            tabPageOther.Location = new Point(4, 24);
            tabPageOther.Name = "tabPageOther";
            tabPageOther.Padding = new Padding(3);
            tabPageOther.Size = new Size(638, 401);
            tabPageOther.TabIndex = 1;
            tabPageOther.Text = "Diğer";
            tabPageOther.UseVisualStyleBackColor = true;
            // 
            // groupBoxTheme
            // 
            groupBoxTheme.Controls.Add(radioButtonThemeDark);
            groupBoxTheme.Controls.Add(radioButtonThemeLight);
            groupBoxTheme.Location = new Point(7, 100);
            groupBoxTheme.Name = "groupBoxTheme";
            groupBoxTheme.Size = new Size(270, 99);
            groupBoxTheme.TabIndex = 3;
            groupBoxTheme.TabStop = false;
            groupBoxTheme.Text = "Tema";
            // 
            // radioButtonThemeDark
            // 
            radioButtonThemeDark.AutoSize = true;
            radioButtonThemeDark.Location = new Point(134, 49);
            radioButtonThemeDark.Name = "radioButtonThemeDark";
            radioButtonThemeDark.Size = new Size(67, 19);
            radioButtonThemeDark.TabIndex = 0;
            radioButtonThemeDark.TabStop = true;
            radioButtonThemeDark.Text = "Karanlık";
            radioButtonThemeDark.UseVisualStyleBackColor = true;
            radioButtonThemeDark.CheckedChanged += radioButtonThemeDark_CheckedChanged;
            // 
            // radioButtonThemeLight
            // 
            radioButtonThemeLight.AutoSize = true;
            radioButtonThemeLight.Location = new Point(17, 49);
            radioButtonThemeLight.Name = "radioButtonThemeLight";
            radioButtonThemeLight.Size = new Size(48, 19);
            radioButtonThemeLight.TabIndex = 0;
            radioButtonThemeLight.TabStop = true;
            radioButtonThemeLight.Text = "Açık";
            radioButtonThemeLight.UseVisualStyleBackColor = true;
            radioButtonThemeLight.CheckedChanged += radioButtonThemeLight_CheckedChanged;
            // 
            // buttonClearLogs
            // 
            buttonClearLogs.FlatAppearance.BorderSize = 0;
            buttonClearLogs.FlatStyle = FlatStyle.Flat;
            buttonClearLogs.Location = new Point(175, 35);
            buttonClearLogs.Name = "buttonClearLogs";
            buttonClearLogs.Size = new Size(75, 23);
            buttonClearLogs.TabIndex = 2;
            buttonClearLogs.Text = "Temizle";
            buttonClearLogs.UseVisualStyleBackColor = true;
            buttonClearLogs.Click += buttonClearLogs_Click;
            // 
            // buttonShowLogs
            // 
            buttonShowLogs.FlatAppearance.BorderSize = 0;
            buttonShowLogs.FlatStyle = FlatStyle.Flat;
            buttonShowLogs.Location = new Point(94, 35);
            buttonShowLogs.Name = "buttonShowLogs";
            buttonShowLogs.Size = new Size(75, 23);
            buttonShowLogs.TabIndex = 1;
            buttonShowLogs.Text = "Göster";
            buttonShowLogs.UseVisualStyleBackColor = true;
            buttonShowLogs.Click += buttonShowLogs_Click;
            // 
            // labelSysLogs
            // 
            labelSysLogs.AutoSize = true;
            labelSysLogs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSysLogs.Location = new Point(7, 39);
            labelSysLogs.Name = "labelSysLogs";
            labelSysLogs.Size = new Size(82, 15);
            labelSysLogs.TabIndex = 0;
            labelSysLogs.Text = "Sistem Logları";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Controls.Add(buttonTabOther);
            panel1.Controls.Add(buttonTabGeneral);
            panel1.Location = new Point(1, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(158, 461);
            panel1.TabIndex = 1;
            // 
            // buttonTabOther
            // 
            buttonTabOther.FlatAppearance.BorderSize = 0;
            buttonTabOther.FlatStyle = FlatStyle.Flat;
            buttonTabOther.Location = new Point(3, 42);
            buttonTabOther.Name = "buttonTabOther";
            buttonTabOther.Size = new Size(153, 36);
            buttonTabOther.TabIndex = 1;
            buttonTabOther.Text = "Diğer";
            buttonTabOther.UseVisualStyleBackColor = true;
            buttonTabOther.Click += buttonTabOther_Click;
            // 
            // buttonTabGeneral
            // 
            buttonTabGeneral.FlatAppearance.BorderSize = 0;
            buttonTabGeneral.FlatStyle = FlatStyle.Flat;
            buttonTabGeneral.Location = new Point(3, 9);
            buttonTabGeneral.Name = "buttonTabGeneral";
            buttonTabGeneral.Size = new Size(153, 36);
            buttonTabGeneral.TabIndex = 0;
            buttonTabGeneral.Text = "Genel";
            buttonTabGeneral.UseVisualStyleBackColor = true;
            buttonTabGeneral.Click += buttonTabGeneral_Click;
            // 
            // buttonSaveSettings
            // 
            buttonSaveSettings.AutoSize = true;
            buttonSaveSettings.FlatAppearance.BorderSize = 0;
            buttonSaveSettings.FlatStyle = FlatStyle.Flat;
            buttonSaveSettings.Image = (Image)resources.GetObject("buttonSaveSettings.Image");
            buttonSaveSettings.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSaveSettings.Location = new Point(722, 433);
            buttonSaveSettings.Name = "buttonSaveSettings";
            buttonSaveSettings.Size = new Size(75, 30);
            buttonSaveSettings.TabIndex = 2;
            buttonSaveSettings.Text = "Kaydet";
            buttonSaveSettings.TextAlign = ContentAlignment.MiddleRight;
            buttonSaveSettings.UseVisualStyleBackColor = true;
            buttonSaveSettings.Click += buttonSaveSettings_Click;
            // 
            // buttonDefaultSettings
            // 
            buttonDefaultSettings.AutoSize = true;
            buttonDefaultSettings.FlatAppearance.BorderSize = 0;
            buttonDefaultSettings.FlatStyle = FlatStyle.Flat;
            buttonDefaultSettings.Image = (Image)resources.GetObject("buttonDefaultSettings.Image");
            buttonDefaultSettings.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDefaultSettings.Location = new Point(602, 433);
            buttonDefaultSettings.Name = "buttonDefaultSettings";
            buttonDefaultSettings.Size = new Size(114, 30);
            buttonDefaultSettings.TabIndex = 3;
            buttonDefaultSettings.Text = "Varsayılan";
            buttonDefaultSettings.TextAlign = ContentAlignment.MiddleRight;
            buttonDefaultSettings.UseVisualStyleBackColor = true;
            buttonDefaultSettings.Click += buttonDefaultSettings_Click;
            // 
            // labelAppVersion
            // 
            labelAppVersion.AutoSize = true;
            labelAppVersion.Location = new Point(168, 440);
            labelAppVersion.Name = "labelAppVersion";
            labelAppVersion.Size = new Size(40, 15);
            labelAppVersion.TabIndex = 4;
            labelAppVersion.Text = "v 1.0.0";
            // 
            // FormSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 464);
            Controls.Add(labelAppVersion);
            Controls.Add(buttonDefaultSettings);
            Controls.Add(buttonSaveSettings);
            Controls.Add(panel1);
            Controls.Add(materialTabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(815, 503);
            Name = "FormSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ayarlar";
            FormClosed += FormSettings_FormClosed;
            Load += FormSettings_Load;
            materialTabControl1.ResumeLayout(false);
            tabPageGenel.ResumeLayout(false);
            tabPageGenel.PerformLayout();
            tabPageOther.ResumeLayout(false);
            tabPageOther.PerformLayout();
            groupBoxTheme.ResumeLayout(false);
            groupBoxTheme.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private TabPage tabPageOther;
        private Panel panel1;
        private Label labelSysLogs;
        private Button buttonTabOther;
        private Button buttonTabGeneral;
        private TabPage tabPageGenel;
        private Label labelLanguage;
        private ComboBox comboBoxLanguage;
        private Button buttonSaveSettings;
        private Button buttonDefaultSettings;
        private Label labelAppVersion;
        private CheckBox checkBoxSystemTray;
        private ToolTip toolTip1;
        private CheckBox checkBoxCheckUpdates;
        private CheckBox checkBoxAlwaysTop;
        private CheckBox checkBoxRunStartsWin;
        private Button buttonShowLogs;
        private Button buttonClearLogs;
        private GroupBox groupBoxTheme;
        private RadioButton radioButtonThemeDark;
        private RadioButton radioButtonThemeLight;
    }
}