namespace TelegramBotWork
{
    public partial class frmMain
    {

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.lst1 = new System.Windows.Forms.ListBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlAdditional = new System.Windows.Forms.Panel();
            this.pnlTeams = new System.Windows.Forms.Panel();
            this.btnDeletePlayer = new System.Windows.Forms.Button();
            this.lblTeammates = new System.Windows.Forms.Label();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.lblTeams = new System.Windows.Forms.Label();
            this.txtTeamname = new System.Windows.Forms.TextBox();
            this.btnCreateTeam = new System.Windows.Forms.Button();
            this.lstTeams = new System.Windows.Forms.ListBox();
            this.lstTeammates = new System.Windows.Forms.ListBox();
            this.btnDeleteTeam = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlControlUsers = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.btnToDeteacher = new System.Windows.Forms.Button();
            this.btnToDeadmin = new System.Windows.Forms.Button();
            this.btnToTeacher = new System.Windows.Forms.Button();
            this.btnToAdmin = new System.Windows.Forms.Button();
            this.lblUsers = new System.Windows.Forms.Label();
            this.pnlRadio = new System.Windows.Forms.Panel();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.pnlMode = new System.Windows.Forms.Panel();
            this.rbMode3 = new System.Windows.Forms.RadioButton();
            this.rbMode2 = new System.Windows.Forms.RadioButton();
            this.rbMode1 = new System.Windows.Forms.RadioButton();
            this.rbMode0 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSendAll = new System.Windows.Forms.TextBox();
            this.lblSendAll = new System.Windows.Forms.Label();
            this.btnSendAll = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlAdditional.SuspendLayout();
            this.pnlTeams.SuspendLayout();
            this.pnlControlUsers.SuspendLayout();
            this.pnlRadio.SuspendLayout();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(3, 3);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(105, 23);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Запустить";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(133, 25);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOut.Size = new System.Drawing.Size(174, 260);
            this.txtOut.TabIndex = 1;
            // 
            // lst1
            // 
            this.lst1.FormattingEnabled = true;
            this.lst1.Location = new System.Drawing.Point(3, 20);
            this.lst1.Name = "lst1";
            this.lst1.Size = new System.Drawing.Size(317, 238);
            this.lst1.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.btnPause);
            this.pnlMain.Controls.Add(this.btnReload);
            this.pnlMain.Controls.Add(this.btnStop);
            this.pnlMain.Controls.Add(this.btnLaunch);
            this.pnlMain.Location = new System.Drawing.Point(12, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(115, 123);
            this.pnlMain.TabIndex = 3;
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(3, 32);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(105, 23);
            this.btnPause.TabIndex = 3;
            this.btnPause.Text = "Приостановить";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReload
            // 
            this.btnReload.Enabled = false;
            this.btnReload.Location = new System.Drawing.Point(3, 61);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(105, 23);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "Перезагрузить";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(3, 90);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Остановить";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Управление";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Вывод";
            // 
            // pnlAdditional
            // 
            this.pnlAdditional.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdditional.Controls.Add(this.pnlTeams);
            this.pnlAdditional.Controls.Add(this.pnlControlUsers);
            this.pnlAdditional.Controls.Add(this.lblUsers);
            this.pnlAdditional.Controls.Add(this.pnlRadio);
            this.pnlAdditional.Controls.Add(this.lst1);
            this.pnlAdditional.Location = new System.Drawing.Point(313, 25);
            this.pnlAdditional.Name = "pnlAdditional";
            this.pnlAdditional.Size = new System.Drawing.Size(459, 524);
            this.pnlAdditional.TabIndex = 6;
            // 
            // pnlTeams
            // 
            this.pnlTeams.Controls.Add(this.btnDeletePlayer);
            this.pnlTeams.Controls.Add(this.lblTeammates);
            this.pnlTeams.Controls.Add(this.btnDeleteUser);
            this.pnlTeams.Controls.Add(this.lblTeams);
            this.pnlTeams.Controls.Add(this.txtTeamname);
            this.pnlTeams.Controls.Add(this.btnCreateTeam);
            this.pnlTeams.Controls.Add(this.lstTeams);
            this.pnlTeams.Controls.Add(this.lstTeammates);
            this.pnlTeams.Controls.Add(this.btnDeleteTeam);
            this.pnlTeams.Controls.Add(this.btnRefresh);
            this.pnlTeams.Enabled = false;
            this.pnlTeams.Location = new System.Drawing.Point(3, 264);
            this.pnlTeams.Name = "pnlTeams";
            this.pnlTeams.Size = new System.Drawing.Size(449, 258);
            this.pnlTeams.TabIndex = 8;
            // 
            // btnDeletePlayer
            // 
            this.btnDeletePlayer.Location = new System.Drawing.Point(299, 193);
            this.btnDeletePlayer.Name = "btnDeletePlayer";
            this.btnDeletePlayer.Size = new System.Drawing.Size(147, 23);
            this.btnDeletePlayer.TabIndex = 12;
            this.btnDeletePlayer.Text = "Удалить игрока";
            this.btnDeletePlayer.UseVisualStyleBackColor = true;
            this.btnDeletePlayer.Click += new System.EventHandler(this.btnDeletePlayer_Click);
            // 
            // lblTeammates
            // 
            this.lblTeammates.AutoSize = true;
            this.lblTeammates.Location = new System.Drawing.Point(172, 0);
            this.lblTeammates.Name = "lblTeammates";
            this.lblTeammates.Size = new System.Drawing.Size(104, 13);
            this.lblTeammates.TabIndex = 4;
            this.lblTeammates.Text = "Список участников";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(299, 222);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(147, 33);
            this.btnDeleteUser.TabIndex = 1;
            this.btnDeleteUser.Text = "Удалить пользователя";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // lblTeams
            // 
            this.lblTeams.AutoSize = true;
            this.lblTeams.Location = new System.Drawing.Point(3, 0);
            this.lblTeams.Name = "lblTeams";
            this.lblTeams.Size = new System.Drawing.Size(85, 13);
            this.lblTeams.TabIndex = 4;
            this.lblTeams.Text = "Список команд";
            // 
            // txtTeamname
            // 
            this.txtTeamname.Location = new System.Drawing.Point(175, 195);
            this.txtTeamname.Name = "txtTeamname";
            this.txtTeamname.Size = new System.Drawing.Size(118, 20);
            this.txtTeamname.TabIndex = 10;
            // 
            // btnCreateTeam
            // 
            this.btnCreateTeam.Location = new System.Drawing.Point(175, 223);
            this.btnCreateTeam.Name = "btnCreateTeam";
            this.btnCreateTeam.Size = new System.Drawing.Size(118, 33);
            this.btnCreateTeam.TabIndex = 11;
            this.btnCreateTeam.Text = "Добавить команду";
            this.btnCreateTeam.UseVisualStyleBackColor = true;
            this.btnCreateTeam.Click += new System.EventHandler(this.btnCreateTeam_Click);
            // 
            // lstTeams
            // 
            this.lstTeams.FormattingEnabled = true;
            this.lstTeams.Location = new System.Drawing.Point(3, 16);
            this.lstTeams.Name = "lstTeams";
            this.lstTeams.Size = new System.Drawing.Size(166, 173);
            this.lstTeams.TabIndex = 6;
            this.lstTeams.SelectedIndexChanged += new System.EventHandler(this.lstTeams_SelectedIndexChanged);
            // 
            // lstTeammates
            // 
            this.lstTeammates.FormattingEnabled = true;
            this.lstTeammates.Location = new System.Drawing.Point(175, 16);
            this.lstTeammates.Name = "lstTeammates";
            this.lstTeammates.Size = new System.Drawing.Size(271, 173);
            this.lstTeammates.TabIndex = 7;
            // 
            // btnDeleteTeam
            // 
            this.btnDeleteTeam.Location = new System.Drawing.Point(3, 224);
            this.btnDeleteTeam.Name = "btnDeleteTeam";
            this.btnDeleteTeam.Size = new System.Drawing.Size(166, 32);
            this.btnDeleteTeam.TabIndex = 9;
            this.btnDeleteTeam.Text = "Удалить команду";
            this.btnDeleteTeam.UseVisualStyleBackColor = true;
            this.btnDeleteTeam.Click += new System.EventHandler(this.btnDeleteTeam_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 195);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(166, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlControlUsers
            // 
            this.pnlControlUsers.Controls.Add(this.button4);
            this.pnlControlUsers.Controls.Add(this.btnToDeteacher);
            this.pnlControlUsers.Controls.Add(this.btnToDeadmin);
            this.pnlControlUsers.Controls.Add(this.btnToTeacher);
            this.pnlControlUsers.Controls.Add(this.btnToAdmin);
            this.pnlControlUsers.Enabled = false;
            this.pnlControlUsers.Location = new System.Drawing.Point(326, 78);
            this.pnlControlUsers.Name = "pnlControlUsers";
            this.pnlControlUsers.Size = new System.Drawing.Size(126, 180);
            this.pnlControlUsers.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 119);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 36);
            this.button4.TabIndex = 13;
            this.button4.Text = "Добавить игрока в команду";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnToDeteacher
            // 
            this.btnToDeteacher.Location = new System.Drawing.Point(3, 90);
            this.btnToDeteacher.Name = "btnToDeteacher";
            this.btnToDeteacher.Size = new System.Drawing.Size(120, 23);
            this.btnToDeteacher.TabIndex = 1;
            this.btnToDeteacher.Text = "Убрать из учителей";
            this.btnToDeteacher.UseVisualStyleBackColor = true;
            this.btnToDeteacher.Click += new System.EventHandler(this.btnToDeteacher_Click);
            // 
            // btnToDeadmin
            // 
            this.btnToDeadmin.Location = new System.Drawing.Point(3, 61);
            this.btnToDeadmin.Name = "btnToDeadmin";
            this.btnToDeadmin.Size = new System.Drawing.Size(120, 23);
            this.btnToDeadmin.TabIndex = 1;
            this.btnToDeadmin.Text = "Убрать из админов";
            this.btnToDeadmin.UseVisualStyleBackColor = true;
            this.btnToDeadmin.Click += new System.EventHandler(this.btnToDeadmin_Click);
            // 
            // btnToTeacher
            // 
            this.btnToTeacher.Location = new System.Drawing.Point(3, 32);
            this.btnToTeacher.Name = "btnToTeacher";
            this.btnToTeacher.Size = new System.Drawing.Size(120, 23);
            this.btnToTeacher.TabIndex = 1;
            this.btnToTeacher.Text = "Сделать учителем";
            this.btnToTeacher.UseVisualStyleBackColor = true;
            this.btnToTeacher.Click += new System.EventHandler(this.btnToTeacher_Click);
            // 
            // btnToAdmin
            // 
            this.btnToAdmin.Location = new System.Drawing.Point(3, 3);
            this.btnToAdmin.Name = "btnToAdmin";
            this.btnToAdmin.Size = new System.Drawing.Size(120, 23);
            this.btnToAdmin.TabIndex = 0;
            this.btnToAdmin.Text = "Сделать админом";
            this.btnToAdmin.UseVisualStyleBackColor = true;
            this.btnToAdmin.Click += new System.EventHandler(this.btnToAdmin_Click);
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.Location = new System.Drawing.Point(3, 4);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(44, 13);
            this.lblUsers.TabIndex = 4;
            this.lblUsers.Text = "Список";
            // 
            // pnlRadio
            // 
            this.pnlRadio.Controls.Add(this.rb4);
            this.pnlRadio.Controls.Add(this.rb3);
            this.pnlRadio.Controls.Add(this.rb1);
            this.pnlRadio.Enabled = false;
            this.pnlRadio.Location = new System.Drawing.Point(326, 4);
            this.pnlRadio.Name = "pnlRadio";
            this.pnlRadio.Size = new System.Drawing.Size(126, 69);
            this.pnlRadio.TabIndex = 3;
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(3, 50);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(112, 17);
            this.rb4.TabIndex = 3;
            this.rb4.Text = "Администраторы";
            this.rb4.UseVisualStyleBackColor = true;
            this.rb4.CheckedChanged += new System.EventHandler(this.rb4_CheckedChanged);
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(3, 27);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(104, 17);
            this.rb3.TabIndex = 2;
            this.rb3.Text = "Преподователи";
            this.rb3.UseVisualStyleBackColor = true;
            this.rb3.CheckedChanged += new System.EventHandler(this.rb3_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(3, 5);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(118, 17);
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "Все пользователи";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Управление списками";
            // 
            // tmrMain
            // 
            this.tmrMain.Interval = 30000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.rbMode3);
            this.pnlMode.Controls.Add(this.rbMode2);
            this.pnlMode.Controls.Add(this.rbMode1);
            this.pnlMode.Controls.Add(this.rbMode0);
            this.pnlMode.Enabled = false;
            this.pnlMode.Location = new System.Drawing.Point(12, 167);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(115, 118);
            this.pnlMode.TabIndex = 8;
            // 
            // rbMode3
            // 
            this.rbMode3.AutoSize = true;
            this.rbMode3.Location = new System.Drawing.Point(5, 75);
            this.rbMode3.Name = "rbMode3";
            this.rbMode3.Size = new System.Drawing.Size(105, 17);
            this.rbMode3.TabIndex = 3;
            this.rbMode3.TabStop = true;
            this.rbMode3.Text = "Сверхоткрытый";
            this.rbMode3.UseVisualStyleBackColor = true;
            this.rbMode3.CheckedChanged += new System.EventHandler(this.rbMode3_CheckedChanged);
            // 
            // rbMode2
            // 
            this.rbMode2.AutoSize = true;
            this.rbMode2.Location = new System.Drawing.Point(5, 52);
            this.rbMode2.Name = "rbMode2";
            this.rbMode2.Size = new System.Drawing.Size(97, 17);
            this.rbMode2.TabIndex = 2;
            this.rbMode2.TabStop = true;
            this.rbMode2.Text = "Только админ";
            this.rbMode2.UseVisualStyleBackColor = true;
            this.rbMode2.CheckedChanged += new System.EventHandler(this.rbMode2_CheckedChanged);
            // 
            // rbMode1
            // 
            this.rbMode1.AutoSize = true;
            this.rbMode1.Location = new System.Drawing.Point(5, 28);
            this.rbMode1.Name = "rbMode1";
            this.rbMode1.Size = new System.Drawing.Size(77, 17);
            this.rbMode1.TabIndex = 1;
            this.rbMode1.TabStop = true;
            this.rbMode1.Text = "Закрытый";
            this.rbMode1.UseVisualStyleBackColor = true;
            this.rbMode1.CheckedChanged += new System.EventHandler(this.rbMode1_CheckedChanged);
            // 
            // rbMode0
            // 
            this.rbMode0.AutoSize = true;
            this.rbMode0.Location = new System.Drawing.Point(5, 4);
            this.rbMode0.Name = "rbMode0";
            this.rbMode0.Size = new System.Drawing.Size(77, 17);
            this.rbMode0.TabIndex = 0;
            this.rbMode0.Text = "Открытый";
            this.rbMode0.UseVisualStyleBackColor = true;
            this.rbMode0.CheckedChanged += new System.EventHandler(this.rbMode0_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Режим работы";
            // 
            // txtSendAll
            // 
            this.txtSendAll.Enabled = false;
            this.txtSendAll.Location = new System.Drawing.Point(12, 304);
            this.txtSendAll.Multiline = true;
            this.txtSendAll.Name = "txtSendAll";
            this.txtSendAll.Size = new System.Drawing.Size(295, 97);
            this.txtSendAll.TabIndex = 10;
            // 
            // lblSendAll
            // 
            this.lblSendAll.AutoSize = true;
            this.lblSendAll.Location = new System.Drawing.Point(9, 288);
            this.lblSendAll.Name = "lblSendAll";
            this.lblSendAll.Size = new System.Drawing.Size(90, 13);
            this.lblSendAll.TabIndex = 11;
            this.lblSendAll.Text = "Разослать всем";
            // 
            // btnSendAll
            // 
            this.btnSendAll.Enabled = false;
            this.btnSendAll.Location = new System.Drawing.Point(232, 407);
            this.btnSendAll.Name = "btnSendAll";
            this.btnSendAll.Size = new System.Drawing.Size(75, 23);
            this.btnSendAll.TabIndex = 12;
            this.btnSendAll.Text = "Отправить";
            this.btnSendAll.UseVisualStyleBackColor = true;
            this.btnSendAll.Click += new System.EventHandler(this.btnSendAll_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnSendAll);
            this.Controls.Add(this.lblSendAll);
            this.Controls.Add(this.txtSendAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlAdditional);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.txtOut);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "TelegramBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlAdditional.ResumeLayout(false);
            this.pnlAdditional.PerformLayout();
            this.pnlTeams.ResumeLayout(false);
            this.pnlTeams.PerformLayout();
            this.pnlControlUsers.ResumeLayout(false);
            this.pnlRadio.ResumeLayout(false);
            this.pnlRadio.PerformLayout();
            this.pnlMode.ResumeLayout(false);
            this.pnlMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnLaunch;
        public System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.ListBox lst1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlAdditional;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Panel pnlRadio;
        private System.Windows.Forms.RadioButton rb4;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Timer tmrMain;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel pnlControlUsers;
        private System.Windows.Forms.Button btnToTeacher;
        private System.Windows.Forms.Button btnToAdmin;
        private System.Windows.Forms.Button btnToDeteacher;
        private System.Windows.Forms.Button btnToDeadmin;
        private System.Windows.Forms.ListBox lstTeammates;
        private System.Windows.Forms.ListBox lstTeams;
        private System.Windows.Forms.Label lblTeams;
        private System.Windows.Forms.Button btnDeletePlayer;
        private System.Windows.Forms.Button btnCreateTeam;
        private System.Windows.Forms.TextBox txtTeamname;
        private System.Windows.Forms.Button btnDeleteTeam;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel pnlTeams;
        private System.Windows.Forms.Label lblTeammates;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.RadioButton rbMode3;
        private System.Windows.Forms.RadioButton rbMode2;
        private System.Windows.Forms.RadioButton rbMode1;
        private System.Windows.Forms.RadioButton rbMode0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSendAll;
        private System.Windows.Forms.Label lblSendAll;
        private System.Windows.Forms.Button btnSendAll;
    }
}

