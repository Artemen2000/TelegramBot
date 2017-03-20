using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.IO;

namespace TelegramBotWork
{
    public class BotData
    {
        public BotData(BotIQ botIq)
        {
            iq = botIq;
            if (!System.IO.File.Exists("data/Data.txt"))
            {
                timetable = new Day[0];
                users = new User[0];
                admins = new User[0];
                teachers = new User[0];
                
                mode = 0;
                updateData();
            }
            else loadData();
            if (!System.IO.File.Exists("data/Achievements.txt"))
            {
                achievements = new Achievement[0];
                updateAchievements();
            }
            else loadAchievements();
            if (!System.IO.File.Exists("data/Timetable.txt"))
            {
                timetable = new Day[2];
                Event[] events = new Event[2];
                events[0] = new Event("Обед", 14, 20);
                events[1] = new Event("Ужжиж", 13, 25);
                timetable[0] = new Day(1, 19, 3, 2017, events);
                events[0] = new Event("Обед", 14, 20);
                events[1] = new Event("Ужжиж", 13, 25);
                timetable[1] = new Day(1, 20, 3, 2017, events);
                updateTimetable();
            }
            else loadTimetable();
            if (!System.IO.File.Exists("data/teams.txt"))
            {
                teams = new Team[0];
                updateTeams();
            }
            else loadTeams();
            dayNum = 0;
            DateTime dt = DateTime.Now;
            foreach (Day day in timetable)
            {
                if (day.day == dt.Day && dt.Month == day.month && dt.Year == day.year)
                {
                    dayNum = day.num;
                }
            }
        }

        public int dayNum;
        BotIQ iq;
        public Achievement[] achievements;
        public Day[] timetable;
        public int mode;
        public User[] users;
        public User[] teachers;
        public User[] admins;
        public Team[] teams;

        #region "Сохранить"

        public void updateTeams()
        {
            string[] lines = new string[teams.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = JsonConvert.SerializeObject(teams[i]);
            }
            System.IO.File.WriteAllLines("data/teams.txt", lines);
        }

        public void updateData()
        {
            string[] str = new string[5];
            str[0] = "Содержимое этого файла лучше не менять своими руками - хуже будет!";
            str[1] = "mode " + mode.ToString();
            str[2] = JsonConvert.SerializeObject(users);
            str[3] = JsonConvert.SerializeObject(teachers);
            str[4] = JsonConvert.SerializeObject(admins);
            System.IO.File.WriteAllLines("data/Data.txt", str);
        }

        public void updateAchievements()
        {
            string[] lines = new string[achievements.Length];
            for (int i = 0; i < achievements.Length; i++)
            {
                lines[i] = JsonConvert.SerializeObject(achievements[i]);
            }
            System.IO.File.WriteAllLines("data/Achievements.txt", lines);
        }

        public void updateTimetable()
        {
            string[] str = new string[2];
            str[0] = "Содержимое этого файла лучше не менять своими руками - хуже будет!";
            str[1] = JsonConvert.SerializeObject(timetable);
            System.IO.File.WriteAllLines("data/Timetable.txt", str);
        }


        #endregion

        #region "Загрузить"

        public void loadTeams()
        {
            writeLine("Загрузка команд...");
            string[] lines = System.IO.File.ReadAllLines("data/teams.txt");
            teams = new Team[lines.Length];
            for (int i = 0; i < teams.Length; i++)
            {
                teams[i] = JsonConvert.DeserializeObject<Team>(lines[i]);
            }
            writeLine("Команды загружены.");
        }

        public void loadData()
        {
            writeLine("Загрузка данных...");
            string[] lines = System.IO.File.ReadAllLines("data/Data.txt");
            string line = "";
            for (int i = 5; i < lines[1].Length; i++)
            {
                line += lines[1][i];
            }
            mode = int.Parse(line);
            users = JsonConvert.DeserializeObject<User[]>(lines[2]);
            teachers = JsonConvert.DeserializeObject<User[]>(lines[3]);
            admins = JsonConvert.DeserializeObject<User[]>(lines[4]);
            writeLine("Данные загружены.");
        }

        public void loadAchievements()
        {
            writeLine("Загрузка достижений...");
            string[] lines = System.IO.File.ReadAllLines("data/Achievements.txt");
            achievements = new Achievement[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                achievements[i] = JsonConvert.DeserializeObject<Achievement>(lines[i]);
            }
            writeLine("Достижения загружены.");
        }

        public void loadTimetable()
        {
            writeLine("Загрузка расписания...");
            string[] lines = System.IO.File.ReadAllLines("data/Timetable.txt");
            timetable = JsonConvert.DeserializeObject<Day[]>(lines[1]);
            writeLine("Расписание загружено.");
        }

        #endregion

        void writeLine(string str)
        {
            iq.form.pnl.writeLine(str);
        }
    }

    public class Event
    {
        public string time, dayEvent;
        public int hour, minute;
        public Event(string str, int h, int m)
        {
            hour = h;
            minute = m;
            dayEvent = str;
            time = "[" + h / 10 + h % 10 + ":" + m / 10 + m % 10 + "]";
        }
    }

    public class Team
    {
        public string name;
        public User[] teammates;

        public Team(string str)
        {
            name = str;
            teammates = new User[0];
        }
        public string addMate(User usr)
        {
            foreach(User user in teammates)
            {
                if (user.Id == usr.Id)
                {
                    return "Данный игрок уже в этой команде.";
                }
            }
            Array.Resize<User>(ref teammates, teammates.Length + 1);
            teammates[teammates.Length - 1] = usr;
            return "Успешно";
        }
    }
    
    public class Day
    {
        public int num;
        public int day;
        public int month;
        public int year;
        public Event[] data;
        public Day(int n, int d, int m, int y, Event[] da)
        {
            num = n;
            day = d;
            month = m;
            year = y;
            data = da;
        }
    }

    public class Achievement
    {
        public User owner;
        public string achievement;
        public DateTime owned;
        public DateTime deowned;
        public bool actual;
        public Achievement(User usr, string text, DateTime own)
        {
            owner = usr;
            achievement = text;
            owned = own;
            actual = true;
        }
        public void deachieve()
        {
            deowned = DateTime.Now;
            actual = false;
        }
    }
}
