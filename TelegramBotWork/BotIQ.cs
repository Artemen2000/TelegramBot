using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace TelegramBotWork
{
    
    /// <summary>
    /// Класс бота, отвечающий за мышление и анализ.
    /// </summary>
    public class BotIQ
    {
        /// <summary>
        /// Память бота.
        /// </summary>
        public BotData data;
        public frmMain form;
        public TelegramBotClient client;
        public BotsAnswers strings;

        public BotIQ(frmMain frm, TelegramBotClient clien)
        {
            form = frm;
            data = new BotData(this);
            strings = new BotsAnswers();
            client = clien;
        }

        /// <summary>
        /// Анализировать апдейт.
        /// </summary>
        /// <param name="client">Клиент.</param>
        /// <param name="update">Апдейт для анализа.</param>
        public void analyzeUpdate(Update[] update, frmMain frm)
        {
            Telegram.Bot.Types.Message message = update[update.Length - 1].Message;
            

            User talker = message.From;
            bool exists = false;
            foreach (User usr in data.users)
            {
                if (usr.Id == talker.Id)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                Array.Resize(ref data.users, data.users.Length + 1);
                data.users[data.users.Length - 1] = talker;
            }

            string text;
            PhotoSize[] photo;
            if (message.Photo != null)
            {
                photo = message.Photo;
                frm.pnl.writeLine("Получена фотография от пользователя " + talker.FirstName + " " + talker.LastName + ".");
            }
            if (message.Text != null)
            {
                text = message.Text;
                frm.pnl.writeLine("Получено сообщение от пользователя " + talker.FirstName + " " + talker.LastName + ": \"" + text + "\".");

                string[] words = stringAnalyzer.stringToWords(text, this);
                string answer = stringAnalyzer.analyzeWords(text, words, talker, this);

                client.SendTextMessageAsync(message.Chat.Id, answer);
                frm.pnl.writeLine("Отправлено сообщение пользователю " + talker.FirstName + " " + talker.LastName + ": \"" + answer + "\".");
            }

        }
        #region "Вывод информации в лог"

        public void writeLine(string str)
        {
            form.pnl.writeLine(str);
        }
        #endregion

        public void checkEvent()
        {
            DateTime dt = DateTime.Now;
            foreach (Day day in data.timetable)
            {
                if (dt.Date.Day == day.day && dt.Date.Month == day.month && dt.Year == day.year)
                {
                    foreach (Event eve in day.data)
                    {
                        if (eve.hour == dt.Hour && eve.minute == dt.Minute)
                        {
                            massSend(data.users, "Сейчас " + eve.dayEvent + ".");
                        }
                        if ((eve.hour * 60 + eve.minute) - (dt.Hour * 60 + dt.Minute) == 10)
                        {
                            massSend(data.users, "Через 10 минут " + eve.dayEvent + ".");
                        }
                        if ((eve.hour * 60 + eve.minute) - (dt.Hour * 60 + dt.Minute) == 5)
                        {
                            massSend(data.users, "Через 5 минут " + eve.dayEvent + ".");
                        }
                        if ((eve.hour * 60 + eve.minute) - (dt.Hour * 60 + dt.Minute) == 15)
                        {
                            massSend(data.users, "Через 15 минут " + eve.dayEvent + ".");
                        }
                    }
                    break;
                }
            }
        }

        #region "Управление достижениями"

        #region "Выдача достижения"

        /// <summary>
        /// Выдача достижения.
        /// </summary>
        /// <param name="usr">Пользователь, которому выдается достижение.</param>
        /// <param name="text">Название достижения.</param>
        /// <returns></returns>
        public string giveAchievement(User usr, string text)
        {
            Array.Resize<Achievement>(ref data.achievements, data.achievements.Length + 1);
            data.achievements[data.achievements.Length - 1] = new Achievement(usr, text, DateTime.Now);
            data.updateAchievements();
            massSend(data.users, "Пользователь " + usr.FirstName + " " + usr.LastName + " получил достижение \"" + text + "\".");
            writeLine("Добавлено достижение пользователю " + usr.FirstName + " " + usr.LastName + ": " + text);
            return "Выполено.";
        }
        #endregion

        #region "Отъем достижения"

        /// <summary>
        /// Отъем достижения.
        /// </summary>
        /// <param name="usr">Пользователь, у которого отнимается достижение.</param>
        /// <param name="text">Название достижения.</param>
        /// <returns></returns>
        public string removeAchievement(User usr, string text)
        {
            for (int i = 0; i < data.achievements.Length; i++)
            {
                if (data.achievements[i].owner == usr && data.achievements[i].achievement == text)
                {
                    for (int j = i; j < data.achievements.Length - 1; j++)
                    {
                        data.achievements[j] = data.achievements[j + 1];
                    }
                    Array.Resize<Achievement>(ref data.achievements, data.achievements.Length - 1);
                    return "Выполнено.";
                }
            }
            return "Выполено.";
        }
        #endregion

        #endregion

        #region "Управление пользователями"

        #region "Добавление администратора"

        /// <summary>
        /// Добавление администратора из списка пользователей.
        /// </summary>
        /// <param name="usr">Пользователь.</param>
        /// <returns>Отчет о работе.</returns>
        public string addAdmin(User usr)
        {
            foreach (User user in data.admins)
            {
                if (user.Id == usr.Id)
                {
                    return "Данный пользователь уже администратор.";
                }
            }
            Array.Resize<User>(ref data.admins, data.admins.Length + 1);
            data.admins[data.admins.Length - 1] = usr;
            data.updateData();
            writeLine("Добавлен администратор " + usr.FirstName + " " + usr.LastName + ".");
            return "Выполнено.";
        }
        #endregion

        #region "Удаление администратора"

        /// <summary>
        /// Удаление администратора.
        /// </summary>
        /// <param name="usr">Пользователь.</param>
        /// <returns>Отчет о работе.</returns>
        public string removeAdmin(User usr)
        {
            for (int i = 0; i < data.admins.Length; i++)
            {
                if (data.admins[i].Id == usr.Id)
                {
                    for (int j = i; j < data.admins.Length - 1; j++)
                    {
                        data.admins[j] = data.admins[j + 1];
                    }
                    Array.Resize<User>(ref data.admins, data.admins.Length - 1);
                    data.updateData();
                    writeLine("Удален администратор " + usr.FirstName + " " + usr.LastName + ".");
                    return "Выполнено.";
                }
            }
            return "Данный пользователь не является администратором.";
        }
        #endregion

        #region "Добавление учителя."
        public string addTeacher(User usr)
        {
            bool exists = false;
            foreach (User user in data.teachers)
            {
                if (user.Id == usr.Id)
                {
                    exists = true;
                    break;
                }
            }
            if (exists)
            {
                return "Данный пользователь уже учитель.";
            }
            else
            {
                Array.Resize<User>(ref data.teachers, data.teachers.Length + 1);
                data.teachers[data.teachers.Length - 1] = usr;
                data.updateData();
                writeLine("Добавлен учитель " + usr.FirstName + " " + usr.LastName + ".");
                return "Выполнено.";
            }
        }
        #endregion

        #region "Удаление учителя."
        public string removeTeacher(User usr)
        {
            for (int i = 0; i < data.teachers.Length; i++)
            {
                if (data.teachers[i].Id == usr.Id)
                {
                    for (int j = i; j < data.teachers.Length - 1; j++)
                    {
                        data.teachers[j] = data.teachers[j + 1];
                    }
                    Array.Resize<User>(ref data.teachers, data.teachers.Length - 1);
                    data.updateData();
                    writeLine("Удален учитель " + usr.FirstName + " " + usr.LastName + ".");
                    return "Выполнено.";
                }
            }
            return "Данный пользователь не является учителем.";
        }
        #endregion

        #region "Добавление команды"
        public string createTeam(string name)
        {
            foreach (Team team in data.teams)
            {
                if (team.name == name)
                {
                    return "Такая команда уже существует.";
                }
            }
            Array.Resize(ref data.teams, data.teams.Length + 1);
            data.teams[data.teams.Length - 1] = new Team(name);
            data.updateTeams();
            writeLine("Создана команда " + name + ".");
            return "Выполнено.";
        }
        #endregion

        #region "Удаление команды"
        public string removeTeam(string name)
        {
            for (int i = 0; i < data.teams.Length; i++)
            {
                if (data.teams[i].name == name)
                {
                    for (int j = i; j < data.teams.Length - 1; j++)
                    {
                        data.teams[j] = data.teams[j + 1];
                    }
                    Array.Resize(ref data.teams, data.teams.Length - 1);
                    data.updateTeams();
                    writeLine("Удалена команда " + name + ".");
                    return "Выполнено.";
                }
            }
            return "Данная команда не была найдена.";
        }
        #endregion

        #region "Добавление участника команды"
        public string addTeamMate (User usr, string name)
        {
            foreach (Team team in data.teams)
            {
                if (team.name == name)
                {
                    team.addMate(usr);
                    data.updateTeams();
                    writeLine("Пользователь " + usr.FirstName + " " + usr.LastName + " добавлен в команду " + name + ".");
                    return "Выполнено.";
                }
            }
            return "Данная команда не была найдена.";
        }
        #endregion

        #region "Удаление участника команды"
        public string removeTeamMate(User usr, string name)
        {
            foreach (Team team in data.teams)
            {
                if (team.name == name)
                {
                    for (int i = 0; i < team.teammates.Length; i++)
                    {
                        if (team.teammates[i].Id == usr.Id)
                        {
                            for (int j = i; j < team.teammates.Length - 1; j++)
                            {
                                team.teammates[j] = team.teammates[j + 1];
                            }
                            Array.Resize(ref team.teammates, team.teammates.Length - 1);
                            data.updateTeams();
                            return "Выполнено.";
                        }
                    }
                }
            }
            return "Данная команда не была найдена.";
        }
        #endregion
        
        #region "Добавление события"

        public string addEvent(Event eve, int d, int m, int y)
        {
            bool exists = false;
            foreach (Day day in data.timetable)
            {
                if (day.day == d && day.month == m && day.year == y)
                {
                    Array.Resize(ref day.data, day.data.Length + 1);
                    day.data[day.data.Length - 1] = eve;
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                Event[] events = new Event[1];
                events[0] = eve;
                int n = 0;
                if (data.timetable.Length > 0)
                {
                    n = d - data.timetable[0].num;
                }
                Day day = new Day(n, d, m, y, events);
                Array.Resize(ref data.timetable, data.timetable.Length + 1);
                data.timetable[data.timetable.Length - 1] = day;
            }
            data.updateTimetable();
            massSend(data.users, "Добавлено событие " + eve.dayEvent + " в " + eve.time + ".");
            return "Выполнено.";
        }

        #endregion

        #region "Удаление события"
        public string removeEvent(Event eve, int d, int m, int y)
        {
            for (int i = 0; i < data.timetable.Length; i++)
            {
                if (data.timetable[i].day == d && data.timetable[i].month == m && data.timetable[i].year == y)
                {
                    for (int j = 0; j < data.timetable[i].data.Length; j++)
                    {
                        if (data.timetable[i].data[j].dayEvent == eve.dayEvent && data.timetable[i].data[j].hour == eve.hour && data.timetable[i].data[j].minute == eve.minute)
                        {
                            for (int k = j; k < data.timetable[i].data.Length - 1; k++)
                            {
                                data.timetable[i].data[k] = data.timetable[i].data[k + 1];
                            }
                            Array.Resize(ref data.timetable[i].data, data.timetable[i].data.Length - 1);
                            writeLine("Удалено событие " + eve.dayEvent + " в " + eve.hour + ":" + eve.minute);
                            data.updateTimetable();
                            return "Выполнено.";
                        }
                    }
                }
            }
            return "Данное событие не было найдено.";
        }

        #endregion

        #region "Удаление пользователя."

        public string removeUser(User usr)
        {
            for(int i = 0; i < data.users.Length; i++)
            {
                if (data.users[i].Id == usr.Id)
                {
                    for (int j = i; j < data.users.Length - 1; j++)
                    {
                        data.users[j] = data.users[j + 1];
                    }
                    Array.Resize(ref data.users, data.users.Length - 1);
                }
            }
            for (int i = 0; i < data.teachers.Length; i++)
            {
                if (data.teachers[i].Id == usr.Id)
                {
                    for (int j = i; j < data.teachers.Length - 1; j++)
                    {
                        data.teachers[j] = data.teachers[j + 1];
                    }
                    Array.Resize(ref data.teachers, data.teachers.Length - 1);
                }
            }
            for (int i = 0; i < data.admins.Length; i++)
            {
                if (data.admins[i].Id == usr.Id)
                {
                    for (int j = i; j < data.admins.Length - 1; j++)
                    {
                        data.admins[j] = data.admins[j + 1];
                    }
                    Array.Resize(ref data.admins, data.admins.Length - 1);
                }
            }
            return "Данный пользователь не был найден.";
        }

        #endregion



        #endregion
        

        #region "Информационные запросы"

        public string giveUserList()
        {
            string str = "";
            foreach (User usr in data.users)
            {
                str += usr.FirstName + " " + usr.LastName + " " + usr.Username + "\r\n";
            }

            return str;
        }

        public string giveTeacherList()
        {
            string str = "";
            foreach (User usr in data.teachers)
            {
                str += usr.FirstName + " " + usr.LastName + " " + usr.Username + "\r\n";
            }

            return str;
        }

        public string giveAdminList()
        {
            string str = "";
            foreach (User usr in data.admins)
            {
                str += usr.FirstName + " " + usr.LastName + " " + usr.Username + "\r\n";
            }

            return str;
        }

        public string giveAchievement(User usr)
        {
            string str = "Достижения:\r\n";
            foreach (Achievement achievement in data.achievements)
            {
                if (achievement.owner.Id == usr.Id && achievement.actual)
                {
                    str += achievement.owned.Day + "/" + achievement.owned.Month + "/" + achievement.owned.Year + " " + achievement.owned.Hour + ":" + achievement.owned.Minute + " " + achievement.achievement + "\r\n";
                }
            }
            return str;
        }

        public string giveTimetable()
        {
            string str = "";
            DateTime dt = DateTime.Now;
            foreach (Day day in data.timetable)
            {
                if (day.day == dt.Day && day.month == dt.Month && day.year == dt.Year)
                {
                    foreach (Event eve in day.data)
                    {
                        str += eve.time + " - " + eve.dayEvent + ".\r\n";
                    }
                    break;
                }
            }

            return str;
        }

        public string giveTimetable(int dayNum)
        {
            string str = "";
            DateTime dt = DateTime.Now;
            foreach (Day day in data.timetable)
            {
                if (day.num == dayNum)
                {
                    foreach (Event eve in day.data)
                    {
                        str += eve.time + " - " + eve.dayEvent + ".\r\n";
                    }
                    break;
                }
            }

            return str;
        }

        public string giveTeams()
        {
            string str = "Список команд:\r\n";
            if (data.teams.Length == 0)
            {
                return "на данный момент нет никаких команд.";
            }
            else
            {
                foreach (Team team in data.teams)
                {
                    str += team.name + "\r\n";
                }
            }
            return str;
        }

        public string givePlayers(string name)
        {
            string str = "";
            foreach (Team team in data.teams)
            {
                if (team.name == name)
                {
                    foreach (User usr in team.teammates)
                    {
                        str += usr.FirstName + " " + usr.LastName + " " + usr.Username + "\r\n";
                    }
                }
            }
            return "Такая команда не найдена.";
        }

        #endregion

        #region "Изменение режима работы"
        /// <summary>
        /// Изменение режима работы бота.
        /// </summary>
        /// <param name="mod">Номер режима работы.</param>
        /// <returns>Отчет о работе.</returns>
        public string changeMode(int mod)
        {
            if (data.mode == mod)
            {
                return "Я уже работаю в этом режиме.";
            }
            else
            {
                data.mode = mod;
                data.updateData();
                writeLine("Изменен режим работы на " + mod.ToString());
                return "Режим работы успешно изменен.";
            }
        }
        #endregion

        #region "Массовая рассылка"
        public string massSend(User[] mass, string text)
        {
            string returnStr = "Отправлено " + mass.Length + " пользователям " + "\"" + text + "\".";
            foreach (User usr in mass)
            {
                client.SendTextMessageAsync(usr.Id, text);
            }
            writeLine(returnStr);
            return returnStr;
        }
        #endregion
    }

    /// <summary>
    /// Класс анализа буквенного сообщения.
    /// </summary>
    public class stringAnalyzer
    {
        public static string[] stringToWords(string str, BotIQ bot)
        {
            char[] chars = { ' ', '\\', '|', '/', ';', ':', '.', ',' };
            str = str.Trim(chars);
            str += " ";
            int n = str.Length;
            int i = 0;
            string[] words = new string[0];
            foreach (string glag in bot.strings.toadd)
            {
                if (str.IndexOf(glag) == i && str[str.IndexOf(glag) + glag.Length] == ' ')
                {
                    i += glag.Length + 1;
                    Array.Resize(ref words, words.Length + 1);
                    words[words.Length - 1] = glag;
                }
            }
            foreach (string glag in bot.strings.toremove)
            {
                if (str.IndexOf(glag) == i && str[str.IndexOf(glag) + glag.Length] == ' ')
                {
                    i += glag.Length + 1;
                    Array.Resize(ref words, words.Length + 1);
                    words[words.Length - 1] = glag;
                }
            }
            foreach (string glag in bot.strings.togive)
            {
                if (str.IndexOf(glag) == i && str[str.IndexOf(glag) + glag.Length] == ' ')
                {
                    i += glag.Length + 1;
                    Array.Resize(ref words, words.Length + 1);
                    words[words.Length - 1] = glag;
                }
            }
            foreach (string[] glag in bot.strings.time)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.users)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.teachers)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.admins)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.achievements)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.timetable)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string[] glag in bot.strings.teams)
            {
                foreach (string glag1 in glag)
                {
                    if (str.IndexOf(glag1) == i && str[str.IndexOf(glag1) + glag1.Length] == ' ')
                    {
                        i += glag1.Length + 1;
                        Array.Resize(ref words, words.Length + 1);
                        words[words.Length - 1] = glag1;
                    }
                }
            }
            foreach (string glag in bot.strings.events)
            {
                if (str.IndexOf(glag) == i && str[str.IndexOf(glag) + glag.Length] == ' ')
                {
                    i += glag.Length + 1;
                    Array.Resize(ref words, words.Length + 1);
                    words[words.Length - 1] = glag;
                }

            }


            string sas = "";
            for (int j = i; j < str.Length; j++)
            {
                sas += str[j];
            }
            if (sas != "")
            {
                string[] words1 = sas.Split();

                if (words.Length > 0)
                {
                    if (words1[words1.Length - 1] == "")
                    {
                        Array.Resize(ref words1, words1.Length - 1);
                    }
                    Array.Resize(ref words, words.Length + words1.Length);
                    for (int j = words.Length - words1.Length; j < words.Length; j++)
                    {
                        words[j] = words1[j + words1.Length - words.Length];
                    }
                }
                else
                {
                    return words1;
                }
            }

            return words;
        }

        /// <summary>
        /// Очче сложная функция для анализа слов
        /// </summary>
        /// <param name="words">Массив полученных слов.</param>
        /// <param name="user">Пользователь, от которого пришло сообщение.</param>
        /// <param name="bot">Бот, отвечающий на сообщения.</param>
        /// <returns></returns>
        public static string analyzeWords(string str, string[] words, User user, BotIQ bot)
        {
            string answer = "";

            #region "Проверка прав собеседника."
            bool isKnown = false;
            foreach (User usr in bot.data.users)
                if (usr.Id == user.Id)
                {
                    isKnown = true;
                    break;
                }
            bool teacher = false;
            foreach (User usr in bot.data.teachers)
                if (usr.Id == user.Id)
                {
                    teacher = true;
                    break;
                }

            bool admin = false;
            foreach (User usr in bot.data.admins)
                if (usr.Id == user.Id)
                {
                    admin = true;
                    break;
                }

            #endregion

            #region "Допиливание системы распознавания команд."
            if (isKnown == true || bot.data.mode == 0)
            {
                #region "Добавить..."
                if (bot.strings.toadd.Contains(words[0]))
                {
                    #region "...учителя..."
                    if (bot.strings.teachers[3].Contains(words[1]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Добавить учителя %параметры пользователя%\".";
                            }
                            if (words.Length == 3)
                            {
                                int count = findUserCount(bot.data.users, words[2]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addTeacher(findUser(bot.data.users, words[2]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 4)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addTeacher(findUser(bot.data.users, words[2], words[3]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 5)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addTeacher(findUser(bot.data.users, words[2], words[3], words[4]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                        }
                        else
                        {
                            answer = "У Вас недостаточно прав.";
                        }
                    }
                    #endregion

                    #region "...админа..."
                    if (bot.strings.admins[3].Contains(words[1]))
                    {
                        if (admin || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Добавить администратора %параметры пользователя%\".";
                            }
                            if (words.Length == 3)
                            {
                                int count = findUserCount(bot.data.users, words[2]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addAdmin(findUser(bot.data.users, words[2]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 4)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addAdmin(findUser(bot.data.users, words[2], words[3]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 5)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.addAdmin(findUser(bot.data.users, words[2], words[3], words[4]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                        }
                        else
                        {
                            answer = "У Вас недостаточно прав.";
                        }
                    }
                    #endregion

                    #region "...событие..."
                    if (bot.strings.events.Contains(words[1]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 8)
                            {
                                answer = "Использование: \"Добавить событие %название_в_одно_слово% %час% %минута% %день% %месяц% %год%\"";
                            }
                            else
                            {
                                try
                                {
                                    Event eve = new Event(words[2], int.Parse(words[3]), int.Parse(words[4]));
                                    int day = int.Parse(words[5]);
                                    int month = int.Parse(words[6]);
                                    int year = int.Parse(words[7]);
                                    answer = bot.addEvent(eve, day, month, year);
                                }
                                catch (ArgumentException)
                                {
                                    answer = "Введите допустимые значения.";
                                }
                            }
                        }
                    }
                    #endregion

                    #region "...достижение..."
                    if (bot.strings.achievements[3].Contains(words[1]))
                    {
                        if ((teacher && bot.data.mode != 2) || admin || bot.data.mode == 3)
                        {
                            if (words.Length < 4)
                            {
                                answer = "Использование: \"Дать достижение %название_достижения% %параметры пользователя%\"";
                            }
                            if (admin == true || teacher == true)
                            {
                                if (words.Length == 4)
                                {
                                    int count = findUserCount(bot.data.users, words[3]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.giveAchievement(findUser(bot.data.users, words[3]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                                if (words.Length == 5)
                                {
                                    int count = findUserCount(bot.data.users, words[3], words[4]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.giveAchievement(findUser(bot.data.users, words[3], words[4]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                                if (words.Length == 6)
                                {
                                    int count = findUserCount(bot.data.users, words[3], words[4], words[5]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.giveAchievement(findUser(bot.data.users, words[3], words[4], words[5]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                            }
                            else
                            {
                                answer = "У Вас недостаточно прав.";
                            }
                        }
                    }

                    #endregion

                    #region "...команду..."
                    if (bot.strings.teams[3].Contains(words[1]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Добавить команду %название%\"";
                            }
                            else
                            {
                                answer = bot.createTeam(words[2]);
                            }
                        }
                    }
                    #endregion

                    #region "...пользователя в команду..."
                    if (bot.strings.users[3].Contains(words[1]) && words[2] == "в" && bot.strings.teams[3].Contains(words[3]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 6)
                            {
                                answer = "Использование: \"Добавить пользователя в команду %команда% %имя пользователя% %второе имя пользователя% %третье имя пользователя%\"";
                            }
                            else
                            {
                                if (words.Length == 6)
                                {
                                    int count = findUserCount(bot.data.users, words[5]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.addTeamMate(findUser(bot.data.users, words[5]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                                if (words.Length == 7)
                                {
                                    int count = findUserCount(bot.data.users, words[5], words[6]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.addTeamMate(findUser(bot.data.users, words[5], words[6]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                                if (words.Length >= 8)
                                {
                                    int count = findUserCount(bot.data.users, words[5], words[6], words[7]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.addTeamMate(findUser(bot.data.users, words[5], words[6], words[7]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion

                #region "Удалить..."

                if (bot.strings.toremove.Contains(words[0]))
                {
                    #region "...учителя..."
                    if (bot.strings.teachers[3].Contains(words[1]))
                    {
                        if (admin || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Удалить учителя %параметры пользователя%\".";
                            }
                            if (words.Length == 3)
                            {
                                int count = findUserCount(bot.data.users, words[2]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeTeacher(findUser(bot.data.users, words[2]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 4)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeTeacher(findUser(bot.data.users, words[2], words[3]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 5)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeTeacher(findUser(bot.data.users, words[2], words[3], words[4]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                        }
                        else
                        {
                            answer = "У Вас недостаточно прав.";
                        }
                    }
                    #endregion

                    #region "...админа..."
                    if (bot.strings.admins[3].Contains(words[1]))
                    {
                        if (admin || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Удалить администратора %параметры пользователя%\".";
                            }
                            if (words.Length == 3)
                            {
                                int count = findUserCount(bot.data.users, words[2]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeAdmin(findUser(bot.data.users, words[2]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 4)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeAdmin(findUser(bot.data.users, words[2], words[3]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 5)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeAdmin(findUser(bot.data.users, words[2], words[3], words[4]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                        }
                        else
                        {
                            answer = "У Вас недостаточно прав.";
                        }
                    }
                    #endregion

                    #region "...пользователя..."

                    if (bot.strings.users[3].Contains(words[1]))
                    {
                        if (admin || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Удалить пользователя %параметры пользователя%\".";
                            }
                            if (words.Length == 3)
                            {
                                int count = findUserCount(bot.data.users, words[2]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeUser(findUser(bot.data.users, words[2]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 4)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeUser(findUser(bot.data.users, words[2], words[3]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                            if (words.Length == 5)
                            {
                                int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                if (count == 0)
                                {
                                    answer = "Такой пользователь не найден.";
                                }
                                else if (count == 1)
                                {
                                    answer = bot.removeUser(findUser(bot.data.users, words[2], words[3], words[4]));
                                }
                                else
                                {
                                    answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                }
                            }
                        }
                        else
                        {
                            answer = "У Вас недостаточно прав.";
                        }
                    }
                    #endregion

                    #region "...событие..."
                    if (bot.strings.events.Contains(words[1]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 8)
                            {
                                answer = "Использование: \"Удалить событие %название% %час% %минута% %день% %месяц% %год%\"";
                            }
                            else
                            {
                                try
                                {
                                    Event eve = new Event(words[2], int.Parse(words[3]), int.Parse(words[4]));
                                    int day = int.Parse(words[5]);
                                    int month = int.Parse(words[6]);
                                    int year = int.Parse(words[7]);
                                    answer = bot.removeEvent(eve, day, month, year);
                                }
                                catch (ArgumentException)
                                {
                                    answer = "Введите допустимые значения.";
                                }
                            }
                        }
                    }
                    #endregion

                    #region "...достижение..."
                    if (bot.strings.achievements[3].Contains(words[1]))
                    {
                        if ((teacher && bot.data.mode != 2) || admin || bot.data.mode == 3)
                        {
                            if (words.Length < 4)
                            {
                                answer = "Использование: \"Отнять достижение %название достижения% %параметры пользователя%\"";
                            }
                            if (admin == true || teacher == true)
                            {
                                if (words.Length == 4)
                                {
                                    int count = findUserCount(bot.data.users, words[3]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.removeAchievement(findUser(bot.data.users, words[3]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                                if (words.Length == 5)
                                {
                                    int count = findUserCount(bot.data.users, words[3], words[4]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.removeAchievement(findUser(bot.data.users, words[3], words[4]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                                if (words.Length == 6)
                                {
                                    int count = findUserCount(bot.data.users, words[3], words[4], words[5]);
                                    if (count == 0)
                                    {
                                        answer = "Такой пользователь не найден.";
                                    }
                                    else if (count == 1)
                                    {
                                        answer = bot.removeAchievement(findUser(bot.data.users, words[3], words[4], words[5]), words[2]);
                                    }
                                    else
                                    {
                                        answer = "Найдено слишком много пользователей. Пожалуйста, уточните параметры поиска.";
                                    }
                                }
                            }
                            else
                            {
                                answer = "У Вас недостаточно прав.";
                            }
                        }
                    }

                    #endregion

                    #region "...команду..."
                    if (bot.strings.teams[3].Contains(words[1]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 3)
                            {
                                answer = "Использование: \"Удалить команду %название%\"";
                            }
                            else
                            {
                                bot.removeTeam(words[2]);
                            }
                        }
                    }
                    #endregion

                    #region "...пользователя из команды..."
                    if (bot.strings.users[3].Contains(words[1]) && words[2] == "из" && bot.strings.teams[1].Contains(words[3]))
                    {
                        if (admin || (teacher && bot.data.mode != 2) || bot.data.mode == 3)
                        {
                            if (words.Length < 6)
                            {
                                answer = "Использование: \"Удалить пользователя в команду %команда% %имя пользователя% %второе имя пользователя% %третье имя пользователя%\"";
                            }
                            else
                            {
                                if (words.Length == 6)
                                {
                                    int count = findUserCount(bot.data.users, words[5]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.removeTeamMate(findUser(bot.data.users, words[5]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                                if (words.Length == 7)
                                {
                                    int count = findUserCount(bot.data.users, words[5], words[6]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.removeTeamMate(findUser(bot.data.users, words[5], words[6]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                                if (words.Length >= 8)
                                {
                                    int count = findUserCount(bot.data.users, words[5], words[6], words[7]);
                                    if (count == 0)
                                    {
                                        answer = "Такого пользователя нет.";
                                    }
                                    if (count == 1)
                                    {
                                        answer = bot.removeTeamMate(findUser(bot.data.users, words[5], words[6], words[7]), words[4]);
                                    }
                                    if (count > 1)
                                    {
                                        answer = "Найдено несколько таких пользователей. Пожалуйста, уточните параметры.";
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion

                #region "Сообщить..."

                if (bot.strings.togive.Contains(words[0]))
                {
                    if (words.Length > 1)
                    {
                        #region "...расписание..."
                        if (bot.strings.timetable[3].Contains(words[1]))
                        {
                            if ((isKnown && bot.data.mode == 0) || (isKnown && bot.data.mode == 1) || (bot.data.mode == 2 && admin) || bot.data.mode == 3)
                            {
                                if (words.Length == 2)
                                {
                                    answer = bot.giveTimetable();
                                }
                            }
                        }
                        #endregion

                        #region "...список пользователей..."
                        if (bot.strings.users[3].Contains(words[1]))
                        {
                            if ((isKnown && bot.data.mode == 0) || (isKnown && bot.data.mode == 1) || (bot.data.mode == 2 && admin) || bot.data.mode == 3)
                            {
                                answer = bot.giveUserList();
                            }
                        }
                        #endregion

                        #region "...список учителей..."
                        if (bot.strings.teachers[3].Contains(words[1]))
                        {
                            if ((teacher && bot.data.mode != 2) || admin || bot.data.mode == 3) answer = bot.giveTeacherList();
                            else answer = "У Вас недостаточно прав.";
                        }
                        #endregion

                        #region "...список администраторов..."
                        if (bot.strings.admins[3].Contains(words[1]))
                        {
                            if (admin || bot.data.mode == 3) answer = bot.giveAdminList();
                            else answer = "У Вас недостаточно прав.";
                        }
                        #endregion

                        #region "...список достижений"
                        if (bot.strings.achievements[3].Contains(words[1]))
                        {
                            if ((isKnown && bot.data.mode == 0) || (isKnown && bot.data.mode == 1) || (bot.data.mode == 2 && admin) || bot.data.mode == 3)
                            {
                                if (words.Length == 2)
                                {
                                    answer = bot.giveAchievement(user);
                                }
                                if (words.Length == 3)
                                {
                                    int count = findUserCount(bot.data.users, words[2]);
                                    if (count == 0) answer = "Такого пользователя нет.";
                                    if (count == 1) answer = bot.giveAchievement(findUser(bot.data.users, words[2]));
                                    if (count > 1) answer = "Найдено несколько пользователей. Уточните параметры.";
                                }
                                if (words.Length == 4)
                                {
                                    int count = findUserCount(bot.data.users, words[2], words[3]);
                                    if (count == 0) answer = "Такого пользователя нет.";
                                    if (count == 1) answer = bot.giveAchievement(findUser(bot.data.users, words[2], words[3]));
                                    if (count > 1) answer = "Найдено несколько пользователей. Уточните параметры.";
                                }
                                if (words.Length >= 5)
                                {
                                    int count = findUserCount(bot.data.users, words[2], words[3], words[4]);
                                    if (count == 0) answer = "Такого пользователя нет.";
                                    if (count == 1) answer = bot.giveAchievement(findUser(bot.data.users, words[2], words[3], words[4]));
                                    if (count > 1) answer = "Найдено несколько пользователей. Уточните параметры.";
                                }
                            }
                        }
                        #endregion

                        #region "...список команд..."
                        if (bot.strings.teams[3].Contains(words[1]))
                        {
                            if ((isKnown && bot.data.mode == 0) || (isKnown && bot.data.mode == 1) || (bot.data.mode == 2 && admin) || bot.data.mode == 3)
                            {
                                answer = bot.giveTeams();
                            }
                        }
                        #endregion

                        #region "...список игроков команды..."
                        if (bot.strings.users[3].Contains(words[1]) && words[2] == "в" && bot.strings.teams[4].Contains(words[3]))
                        {
                            if ((isKnown && bot.data.mode == 0) || (isKnown && bot.data.mode == 1) || (bot.data.mode == 2 && admin) || bot.data.mode == 3)
                            {
                                if (words.Length < 5)
                                {
                                    answer = "Использование: \"Дать пользователей в команде %название команды%\"";
                                }
                                else
                                {
                                    answer = bot.givePlayers(words[4]);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        answer = "Использование: \"Дать расписание/пользователей/учителей/администраторов/достижения/команды/пользователей в команде\"";
                    }
                }

                #endregion

                #region "Разослать..."

                if (bot.strings.tosend.Contains(words[0]))
                {
                    if (admin || teacher || bot.data.mode == 3)
                    {
                        if (words.Length < 2)
                        {
                            answer = "Использование: \"Разослать %текст%\".";
                        }
                        string st = "";
                        for (int i = 1; i < words.Length; i++)
                        {
                            st += words[i] = " ";
                        }
                        answer = bot.massSend(bot.data.users, st);
                    }
                    else
                    {
                        answer = "У Вас недостаточно прав.";
                    }
                }

                #endregion

                #region "Помощь"

                if (words[0] == "помощь" || words[0] == "Помощь" || words[0] == "Help" || words[0] == "help")
                {
                    string help = "Существуют команды \"Добавить\",\"Удалить\", \"Разослать\" и \"Дать\" команду, пользователей, события, команды, расписание.";
                    if (bot.data.mode == 0)
                    {
                        answer = "Режим: Открытый. " + help;
                    }
                    if (bot.data.mode == 1)
                    {
                        answer = "Режим: Закрытый. " + help;
                    }
                    if (bot.data.mode == 2)
                    {
                        answer = "Режим: Только Админ. " + help;
                    }
                    if (bot.data.mode == 3)
                    {
                        answer = "Режим: Сверхоткрытый. " + help;
                    }
                }

                #endregion


            }
            #endregion

            return answer;
        }

        #region "Поиск пользователей"
        public static int findUserCount(User[] mass, string name)
        {
            int count = 0;
            foreach (User usr in mass)
            {
                if (usr.FirstName == name || usr.LastName == name || usr.Username == name)
                {
                    count++;
                }
            }
            return count;
        }
        public static int findUserCount(User[] mass, string name, string secondName)
        {
            int count = 0;
            foreach (User usr in mass)
            {
                if ((usr.FirstName == name && usr.LastName == secondName)
                    || (usr.FirstName == name && usr.Username == secondName)
                    || (usr.LastName == name && usr.FirstName == secondName) 
                    || (usr.LastName == name && usr.Username == secondName)
                    || (usr.Username == name && usr.FirstName == secondName)
                    || (usr.Username == name && usr.LastName == secondName))
                {
                    count++;
                }
            }
            return count;
        }
        public static int findUserCount(User[] mass, string name, string secondName, string thirdName)
        {
            int count = 0;
            foreach (User usr in mass)
            {
                if ((usr.FirstName == name && usr.LastName == secondName && usr.Username == thirdName)
                    || (usr.FirstName == name && usr.Username == secondName && usr.LastName == thirdName)
                    || (usr.LastName == name && usr.FirstName == secondName && usr.Username == thirdName)
                    || (usr.LastName == name && usr.Username == secondName && usr.FirstName == thirdName)
                    || (usr.Username == name && usr.FirstName == secondName && usr.LastName == thirdName)
                    || (usr.Username == name && usr.LastName == secondName && usr.FirstName == thirdName))
                {
                    count++;
                }
            }
            return count;
        }
        public static User findUser(User[] mass, string name)
        {
            User user = new User();
            foreach (User usr in mass)
            {
                if (usr.FirstName == name || usr.LastName == name || usr.Username == name)
                {
                    return usr;
                }
            }
            return user;
        }
        public static User findUser(User[] mass, string name, string secondName)
        {
            User user = new User();
            foreach (User usr in mass)
            {
                if ((usr.FirstName == name && usr.LastName == secondName)
                    || (usr.FirstName == name && usr.Username == secondName)
                    || (usr.LastName == name && usr.FirstName == secondName)
                    || (usr.LastName == name && usr.Username == secondName)
                    || (usr.Username == name && usr.FirstName == secondName)
                    || (usr.Username == name && usr.LastName == secondName))
                {
                    return usr;
                }
            }
            return user;
        }
        public static User findUser(User[] mass, string name, string secondName, string thirdName)
        {
            User user = new User();
            foreach (User usr in mass)
            {
                if ((usr.FirstName == name && usr.LastName == secondName && usr.Username == thirdName)
                    || (usr.FirstName == name && usr.Username == secondName && usr.LastName == thirdName)
                    || (usr.LastName == name && usr.FirstName == secondName && usr.Username == thirdName)
                    || (usr.LastName == name && usr.Username == secondName && usr.FirstName == thirdName)
                    || (usr.Username == name && usr.FirstName == secondName && usr.LastName == thirdName)
                    || (usr.Username == name && usr.LastName == secondName && usr.FirstName == thirdName))
                {
                    return usr;
                }
            }
            return user;
        }
        #endregion
    }
}
