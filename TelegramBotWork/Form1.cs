using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Telegram.Bot;

using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

using File = Telegram.Bot.Types.File;
using Telegram.Bot.Converters;

namespace TelegramBotWork
{
    public partial class frmMain : Form
    {
        TelegramBotClient client;
        BotIQ iq;
        public BotPanel pnl;
        bool launched;
        string token;

        public frmMain()
        {
            InitializeComponent();
            pnl = new BotPanel(this);
            launched = false;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (launched == true) return;
            pnl.writeLine("Запуск...");
            pnl.writeLine("Загрузка токена...");

            if (!System.IO.File.Exists("data/token.txt"))
            {
                pnl.writeLine("Токен не обнаружен!");
                MessageBox.Show("Не обнаружен токен! Пожалуйста, введите его в файл \"token.txt\"", "Внимание!");
                if (!System.IO.Directory.Exists("data")) System.IO.Directory.CreateDirectory("data");
                System.IO.File.WriteAllText("data/token.txt", "");
                Process.Start("data");
                Process.GetCurrentProcess().Kill();
                return;
            }
            token = System.IO.File.ReadAllText("data/token.txt");
            try
            {
                client = new TelegramBotClient(token);
            }
            catch (ArgumentException)
            {
                pnl.writeLine("Неправильный токен!");
                MessageBox.Show("Неправильный токен! Пожалуйста, введите правильный токен в файл \"token.txt\"", "Внимание!");
                if (!System.IO.Directory.Exists("data")) System.IO.Directory.CreateDirectory("data");
                Process.Start("data");
                Process.GetCurrentProcess().Kill();
                return;
            }
            

            pnl.writeLine("Загрузка искусственного интеллекта...");

            iq = new BotIQ(this, client);

            pnl.writeLine("Запуск системы...");
            
            client.StartReceiving();
            client.OnMessage += Client_OnMessage;
            pnl.writeLine("Готово.");

            tmrMain.Start();
            pnlControlUsers.Enabled = true;
            pnlRadio.Enabled = true;
            btnReload.Enabled = true;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            pnlTeams.Enabled = true;
            pnlMode.Enabled = true;
            txtSendAll.Enabled = true;
            btnSendAll.Enabled = true;
            launched = true;
            refresh();
            
            if (iq.data.mode == 0)
            {
                rbMode0.Checked = true;
            }
            if (iq.data.mode == 1)
            {
                rbMode1.Checked = true;
            }
            if (iq.data.mode == 2)
            {
                rbMode2.Checked = true;
            }
            if (iq.data.mode == 3)
            {
                rbMode3.Checked = true;
            }
        }

        private void Client_OnMessage(object sender, MessageEventArgs e)
        {
            //txtOut.Text += "sas";
            Task<Telegram.Bot.Types.Update[]> tskupd = client.GetUpdatesAsync();

            iq.analyzeUpdate(tskupd.Result, this);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            pnl.writeLine("Отключение...");
            
            client.StopReceiving();
            pnl.writeLine("Успешно отключено.");

            pnlControlUsers.Enabled = false;
            btnReload.Enabled = false;
            btnPause.Enabled = false;
            pnlTeams.Enabled = false;

            pnlMode.Enabled = false;
            txtSendAll.Enabled = false;
            btnSendAll.Enabled = false;
            tmrMain.Stop();
            launched = false;
            refresh();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (launched == true)
            {
                client.StopReceiving();
                pnl.writeLine("Приостановлено.");
                btnPause.Text = "Продолжить";
                tmrMain.Stop();
                launched = false;
            }
            else
            {
                client.StartReceiving();
                pnl.writeLine("Продолжено.");
                btnPause.Text = "Приостановить";
                tmrMain.Start();
                launched = true;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            pnl.writeLine("Перезагрузка...");
            client.StopReceiving();
            tmrMain.Stop();
            pnl.writeLine("Запуск...");
            pnl.writeLine("Загрузка токена...");

            if (!System.IO.File.Exists("data/token.txt"))
            {
                pnl.writeLine("Токен не обнаружен!");
                MessageBox.Show("Не обнаружен токен! Пожалуйста, введите его в файл \"token.txt\"", "Внимание!");
                if (!System.IO.Directory.Exists("data")) System.IO.Directory.CreateDirectory("data");
                System.IO.File.WriteAllText("data/token.txt", "");
                Process.Start("data");
                Process.GetCurrentProcess().Kill();
                return;
            }
            token = System.IO.File.ReadAllText("data/token.txt");
            try
            {
                client = new TelegramBotClient(token);
            }
            catch (ArgumentException)
            {
                pnl.writeLine("Неправильный токен!");
                MessageBox.Show("Неправильный токен! Пожалуйста, введите правильный токен в файл \"token.txt\"", "Внимание!");
                if (!System.IO.Directory.Exists("data")) System.IO.Directory.CreateDirectory("data");
                Process.Start("data");
                Process.GetCurrentProcess().Kill();
                return;
            }

            pnl.writeLine("Загрузка искусственного интеллекта...");
            iq = new BotIQ(this, client);
            tmrMain.Start();
            pnl.writeLine("Запуск системы...");
            client.OnMessage += Client_OnMessage;
            client.StartReceiving();
            refresh();
            pnl.writeLine("Готово.");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!System.IO.File.Exists("logs/logOption.txt"))
            {
                System.IO.File.WriteAllText("logs/logOption.txt", pnl.logNum.ToString());
            }
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            lst1.Items.Clear();
            foreach (User usr in iq.data.users)
            {
                lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
            }
            if (lst1.Items.Count != 0)
            {
                lst1.SelectedIndex = 0;
            }
        }
        

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            lst1.Items.Clear();
            foreach (User usr in iq.data.teachers)
            {
                lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
            }
            if (lst1.Items.Count != 0)
            {
                lst1.SelectedIndex = 0;
            }
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            lst1.Items.Clear();
            foreach (User usr in iq.data.admins)
            {
                lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
            }
            if (lst1.Items.Count != 0)
            {
                lst1.SelectedIndex = 0;
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            iq.checkEvent();
            refresh();
        }

        private void btnToAdmin_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1)
            {
                string[] str = ((string)lst1.SelectedItem).Split();
                iq.addAdmin(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]));
                refresh();
            }
        }

        private void btnToTeacher_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1)
            {
                string[] str = ((string)lst1.SelectedItem).Split();
                iq.addTeacher(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]));
                refresh();
            }
        }

        private void btnToDeadmin_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1)
            {
                string[] str = ((string)lst1.SelectedItem).Split();
                iq.removeAdmin(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]));
                refresh();
            }
        }

        private void btnToDeteacher_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1)
            {
                string[] str = ((string)lst1.SelectedItem).Split();
                iq.removeTeacher(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]));
                refresh();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1 && lstTeams.SelectedIndex != -1)
            {
                string[] str = ((string)lst1.SelectedItem).Split();
                iq.addTeamMate(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]), (string)lstTeams.SelectedItem);
                refresh();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void lstTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstTeammates.Items.Clear();
            foreach (Team team in iq.data.teams)
            {
                if (team.name == (string)lstTeams.SelectedItem)
                {
                    foreach (User usr in team.teammates)
                    {
                        lstTeammates.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
                    }
                    break;
                }
            }
        }

        private void btnDeleteTeam_Click(object sender, EventArgs e)
        {
            if (lstTeams.SelectedIndex != -1)
            iq.removeTeam((string)lstTeams.SelectedItem);
            refresh();
        }

        private void btnCreateTeam_Click(object sender, EventArgs e)
        {
            iq.createTeam(txtTeamname.Text);
            refresh();
        }

        void refresh()
        {
            int i = lstTeams.SelectedIndex;
            lstTeams.Items.Clear();
            lst1.Items.Clear();
            lstTeammates.Items.Clear();
            foreach (Team team in iq.data.teams)
            {
                lstTeams.Items.Add(team.name);
            }
            if (lstTeams.Items.Count >= i + 1)
            {
                lstTeams.SelectedIndex = i;
            }

            if (rb1.Checked)
            {
                foreach (User usr in iq.data.users)
                {
                    lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
                }
            }
            if (rb3.Checked)
            {
                foreach (User usr in iq.data.teachers)
                {
                    lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
                }
            }
            if (rb4.Checked)
            {
                foreach (User usr in iq.data.admins)
                {
                    lst1.Items.Add(usr.Id + " " + usr.FirstName + " " + usr.LastName + " " + usr.Username);
                }
            }
        }

        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            if (lstTeammates.SelectedIndex != -1 && lstTeams.SelectedIndex != -1)
            {
                string[] str = ((string)lstTeammates.SelectedItem).Split();
                iq.removeTeamMate(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]), (string)lstTeams.SelectedItem);
                refresh();
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lst1.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы точно хотите удалить этого пользователя? Вы не сможете самостоятельно добавить пользователя в список!", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string[] str = ((string)lst1.SelectedItem).Split();
                    iq.removeUser(stringAnalyzer.findUser(iq.data.users, str[1], str[2], str[3]));
                    refresh();
                }
            }
        }

        private void rbMode0_CheckedChanged(object sender, EventArgs e)
        {
            iq.changeMode(0);
        }

        private void rbMode1_CheckedChanged(object sender, EventArgs e)
        {
            iq.changeMode(1);
        }

        private void rbMode2_CheckedChanged(object sender, EventArgs e)
        {
            iq.changeMode(2);
        }

        private void rbMode3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMode3.Checked == true)
            {
                if (MessageBox.Show("Вы точно хотите поставить сверхоткрытый режим? В этом режиме любой пользователь имеет те же права, что и администратор.", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    iq.changeMode(3);
                }
                else
                {
                    if (iq.data.mode == 0)
                    {
                        rbMode0.Checked = true;
                    }
                    if (iq.data.mode == 1)
                    {
                        rbMode1.Checked = true;
                    }
                    if (iq.data.mode == 2)
                    {
                        rbMode2.Checked = true;
                    }
                    if (iq.data.mode == 3)
                    {
                        rbMode3.Checked = true;
                    }
                }
            }
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            iq.massSend(iq.data.users, txtSendAll.Text);
        }
    }

    public class BotPanel
    {
        delegate void SetTextCallback(string text);

        frmMain frm;
        public int logNum;
        string log;
        public BotPanel(frmMain form)
        {
            frm = form;
            if (!System.IO.File.Exists("logs/logOption.txt"))
            {
                if (System.IO.Directory.GetFiles("logs/").Length > 0)
                {
                    if (MessageBox.Show("Внимание! Произошла ошибка, связанная с логгированием данных. Чтобы сохранить Ваши логи, пройдите в директорию файлов, сделайте их резервное копирование и перезапустите программу.", "Внимание!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Process.Start("logs");
                        Process.GetCurrentProcess().Kill();
                        return;
                    }
                }
                logNum = 0;
                if (!System.IO.Directory.Exists("logs/"))
                {
                    System.IO.Directory.CreateDirectory("logs/");
                }
                System.IO.File.WriteAllText("logs/logOption.txt", "0");
            }
            string str = System.IO.File.ReadAllText("logs/logOption.txt");
            logNum = int.Parse(str);

            logNum++;
            System.IO.File.WriteAllText("logs/logOption.txt", logNum.ToString());
            writeLine(System.DateTime.Now.ToString());
            updateLog();
        }

        /// <summary>
        /// Дополняет лог и консоль.
        /// </summary>
        /// <param name="str">Добавляемый текст.</param>
        public void writeLine(string str)
        {
            DateTime time = DateTime.Now;
            string s = "[" + time.Hour / 10 + time.Hour % 10 + ":" + time.Minute / 10 + time.Minute % 10 + ":" + time.Second / 10 + time.Second % 10 + "] " + str + "\r\n";
            log += s;
            updateLog();
            SetText(s);
            
            
        }

        void SetText(string text)
        {
            if (frm.txtOut.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                frm.Invoke(d, new object[] { text });
            }
            else
            {
                frm.txtOut.Text += text;
                frm.txtOut.SelectionStart = frm.txtOut.Text.Length;
                frm.txtOut.ScrollToCaret();
            }
        }

        /// <summary>
        /// Обновляет файл лога.
        /// </summary>
        public void updateLog()
        {
            if (!System.IO.Directory.Exists("logs/"))
            {
                System.IO.Directory.CreateDirectory("logs/");
            }
            System.IO.File.WriteAllText("logs/log" + logNum.ToString() + ".txt", log);
        }

    }
}
