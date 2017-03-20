using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Newtonsoft.Json;

namespace TelegramBotWork
{
    public class BotsAnswers
    {
        //I R D V T P
        public string[][] users;
        public string[][] teachers;
        public string[][] admins;
        public string[][] timetable;
        public string[][] time;
        public string[] toadd;
        public string[] toremove;
        public string[] events;
        public string[] togive;
        public string[][] teams;
        public string[][] achievements;
        public string[] tosend;

        public BotsAnswers()
        {
            #region "Если файла нет."
            if (!System.IO.File.Exists("data/strings.txt"))
            {
                #region "Падежные."
                users = new string[6][];
                users[0] = new string[4];
                users[1] = new string[4];
                users[2] = new string[4];
                users[3] = new string[4];
                users[4] = new string[4];
                users[5] = new string[4];
                users[0][0] = "пользователи";
                users[0][1] = "юзеры";
                users[0][2] = "все";
                users[0][3] = "пользователь";
                users[1][0] = "пользователей";
                users[1][1] = "юзеров";
                users[1][2] = "всех";
                users[1][3] = "пользователя";
                users[2][0] = "пользователям";
                users[2][1] = "юзерам";
                users[2][2] = "всем";
                users[2][3] = "пользователю";
                users[3][0] = "пользователей";
                users[3][1] = "юзеров";
                users[3][2] = "всех";
                users[3][3] = "пользователя";
                users[4][0] = "пользователями";
                users[4][1] = "юзерами";
                users[4][2] = "всеми";
                users[4][3] = "пользователем";
                users[5][0] = "пользователях";
                users[5][1] = "юзерах";
                users[5][2] = "всех";
                users[5][3] = "пользователе";

                teachers = new string[6][];
                teachers[0] = new string[6];
                teachers[1] = new string[6];
                teachers[2] = new string[6];
                teachers[3] = new string[6];
                teachers[4] = new string[6];
                teachers[5] = new string[6];
                teachers[0][0] = "учителя";
                teachers[0][1] = "преподаватели";
                teachers[0][2] = "тичеры";
                teachers[1][0] = "учителей";
                teachers[1][1] = "преподавателей";
                teachers[1][2] = "тичеров";
                teachers[2][0] = "учителям";
                teachers[2][1] = "преподавателям";
                teachers[2][2] = "тичерам";
                teachers[3][0] = "учителей";
                teachers[3][1] = "преподавателей";
                teachers[3][2] = "тичеров";
                teachers[4][0] = "учителями";
                teachers[4][1] = "преподавателями";
                teachers[4][2] = "тичерами";
                teachers[5][0] = "учителях";
                teachers[5][1] = "преподавателях";
                teachers[5][2] = "тичерах";

                teachers[0][3] = "учитель";
                teachers[0][4] = "преподаватель";
                teachers[0][5] = "тичер";
                teachers[1][3] = "учителя";
                teachers[1][4] = "преподавателя";
                teachers[1][5] = "тичера";
                teachers[2][3] = "учителю";
                teachers[2][4] = "преподавателю";
                teachers[2][5] = "тичеру";
                teachers[3][3] = "учителя";
                teachers[3][4] = "преподавателя";
                teachers[3][5] = "тичера";
                teachers[4][3] = "учителем";
                teachers[4][4] = "преподавателем";
                teachers[4][5] = "тичером";
                teachers[5][3] = "учителе";
                teachers[5][4] = "преподавателе";
                teachers[5][5] = "тичере";


                admins = new string[6][];
                admins[0] = new string[3];
                admins[1] = new string[3];
                admins[2] = new string[3];
                admins[3] = new string[3];
                admins[4] = new string[3];
                admins[5] = new string[3];
                admins[0][0] = "админы";
                admins[0][1] = "администраторы";
                admins[0][2] = "боги";
                admins[1][0] = "админов";
                admins[1][1] = "администраторов";
                admins[1][2] = "богов";
                admins[2][0] = "админам";
                admins[2][1] = "администраторам";
                admins[2][2] = "богам";
                admins[3][0] = "админов";
                admins[3][1] = "администраторов";
                admins[3][2] = "богов";
                admins[4][0] = "админами";
                admins[4][1] = "администраторами";
                admins[4][2] = "богами";
                admins[5][0] = "админах";
                admins[5][1] = "администраторах";
                admins[5][2] = "богах";

                timetable = new string[6][];
                timetable[0] = new string[1];
                timetable[1] = new string[1];
                timetable[2] = new string[1];
                timetable[3] = new string[1];
                timetable[4] = new string[1];
                timetable[5] = new string[1];
                timetable[0][0] = "расписание";
                timetable[1][0] = "расписания";
                timetable[2][0] = "расписанию";
                timetable[3][0] = "расписание";
                timetable[4][0] = "расписанием";
                timetable[5][0] = "расписании";


                teams = new string[6][];
                teams[0] = new string[2];
                teams[1] = new string[2];
                teams[2] = new string[2];
                teams[3] = new string[2];
                teams[4] = new string[2];
                teams[5] = new string[2];
                teams[0][0] = "команда";
                teams[0][1] = "команды";
                teams[1][0] = "команды";
                teams[1][1] = "команд";
                teams[2][0] = "команде";
                teams[2][1] = "командам";
                teams[3][0] = "команду";
                teams[3][1] = "команды";
                teams[4][0] = "командой";
                teams[4][1] = "командами";
                teams[5][0] = "команде";
                teams[5][1] = "командах";
                achievements = new string[6][];
                achievements[0] = new string[2];
                achievements[1] = new string[2];
                achievements[2] = new string[2];
                achievements[3] = new string[2];
                achievements[4] = new string[2];
                achievements[5] = new string[2];
                achievements[0][0] = "достижение";
                achievements[0][1] = "ачивка";
                achievements[1][0] = "достижения";
                achievements[1][1] = "ачивки";
                achievements[2][0] = "достижению";
                achievements[2][1] = "ачивке";
                achievements[3][0] = "достижение";
                achievements[3][1] = "ачивку";
                achievements[4][0] = "достижением";
                achievements[4][1] = "ачивкой";
                achievements[5][0] = "достижении";
                achievements[5][1] = "ачивке";
                #endregion

                #region "Время."
                time = new string[5][];
                time[0] = new string[1];
                time[1] = new string[1];
                time[2] = new string[1];
                time[3] = new string[1];
                time[4] = new string[1];
                time[0][0] = "позавчера";
                time[1][0] = "вчера";
                time[2][0] = "сегодня";
                time[3][0] = "завтра";
                time[4][0] = "послезавтра";
                #endregion

                #region "Глаголы."
                toadd = new string[2];
                toadd[0] = "Добавить";
                toadd[1] = "Добавь";
                toremove = new string[3];
                toremove[0] = "Убери";
                toremove[1] = "Удалить";
                toremove[2] = "Удали";
                togive = new string[4];
                togive[0] = "Дай";
                togive[1] = "Скажи";
                togive[2] = "Сообщи";
                togive[3] = "Дать";
                tosend = new string[1];
                tosend[0] = "Разослать";
                #endregion

                #region "События."
                events = new string[1];
                events[0] = "событие";
                #endregion
                updateStrings();
            }
            #endregion
            else
            {
                string[] lines = System.IO.File.ReadAllLines("data/strings.txt");
                users = JsonConvert.DeserializeObject<string[][]>(lines[0]);
                teachers = JsonConvert.DeserializeObject<string[][]>(lines[1]);
                admins = JsonConvert.DeserializeObject<string[][]>(lines[2]);
                timetable = JsonConvert.DeserializeObject<string[][]>(lines[3]);
                time = JsonConvert.DeserializeObject<string[][]>(lines[4]);
                toadd = JsonConvert.DeserializeObject<string[]>(lines[5]);
                toremove = JsonConvert.DeserializeObject<string[]>(lines[6]);
                togive = JsonConvert.DeserializeObject<string[]>(lines[7]);
                events = JsonConvert.DeserializeObject<string[]>(lines[8]);
                teams = JsonConvert.DeserializeObject<string[][]>(lines[9]);
                achievements = JsonConvert.DeserializeObject<string[][]>(lines[10]);
                tosend = JsonConvert.DeserializeObject<string[]>(lines[11]);
            }
        }

        void updateStrings()
        {
            string[] lines = new string[12];
            lines[0] = JsonConvert.SerializeObject(users);
            lines[1] = JsonConvert.SerializeObject(teachers);
            lines[2] = JsonConvert.SerializeObject(admins);
            lines[3] = JsonConvert.SerializeObject(timetable);
            lines[4] = JsonConvert.SerializeObject(time);
            lines[5] = JsonConvert.SerializeObject(toadd);
            lines[6] = JsonConvert.SerializeObject(toremove);
            lines[7] = JsonConvert.SerializeObject(togive);
            lines[8] = JsonConvert.SerializeObject(events);
            lines[9] = JsonConvert.SerializeObject(teams);
            lines[10] = JsonConvert.SerializeObject(achievements);
            lines[11] = JsonConvert.SerializeObject(tosend);
            System.IO.File.WriteAllLines("data/strings.txt", lines);
        }



        public string giveHelp(BotIQ bot, User talker)
        {
            string str = "";
            if (bot.data.mode == 0)
            {
                str = "Я - бот. Меня создал павлов Артём. На данный момент я работаю в открытом режиме, в этом режиме любой может мне писать. Рад познакомиться.";
            }
            if (bot.data.mode == 1)
            {
                str = "Я - бот. На данный момент я работаю в закрытом режиме, мне могут писать только люди, с которыми я общался ранее.";
            }
            if (bot.data.mode == 2)
            {
                str = "Я - бот. На данный момент я работаю в сверхоткрытом режиме.";
            }
            if (bot.data.mode == 3)
            {
                str = "Я - бот. На данный момент я работаю с сверхзакрытом режиме. Работать со мной и писать мне могут только админы.";
            }
            if (bot.data.admins.Contains<User>(talker))
            {
                str = "Служу тебе, мой господин.";
            }

            return str;
        }
    }
}
