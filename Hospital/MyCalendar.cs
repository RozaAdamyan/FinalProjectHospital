using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    struct WeekDays
    {
        public string Name { get; set; }
        public Dictionary<Patient, int[,]> patients { get; set; }
        public int[,] BusyOrFreeTimes;
        public static int[,] hours = WorkingHours();


        public WeekDays(string name)
        {
            Name = name;
            this.patients = new Dictionary<Patient, int[,]>();
            BusyOrFreeTimes = new int[27, 2];

        }

        public static int[,] WorkingHours()     // 20 mins for each consultation, 
        {
            int[,] hours = new int[27, 2];
            hours[0, 0] = 1000;
            hours[0, 1] = 1020;
            for (int i = 1; i < 27; i++)
            {
                hours[i, 0] = hours[i - 1, 0] + 20;
                int n = hours[i, 0] / 10;
                if ((n % 10) == 6)
                {
                    hours[i, 0] += 40;
                }

                hours[i, 1] = hours[i - 1, 1] + 20;
                int b = hours[i, 1] / 10;
                if ((b) % 10 == 6)
                {
                    hours[i, 1] += 40;
                }

            }
            return hours;

        }
    }



    class MyCalendar
    {
        public List<WeekDays> weekDays = new List<WeekDays>();

        public MyCalendar()
        {
            this.weekDays = InitializeWeekDays();

        }

        private List<WeekDays> InitializeWeekDays()
        {
            WeekDays Monday = new WeekDays("monday");
            WeekDays Thuesday = new WeekDays("thusday");
            WeekDays Wednesday = new WeekDays("wednesday");
            WeekDays Thusday = new WeekDays("thusday");
            WeekDays Friday = new WeekDays("friday");
            weekDays.Add(Monday);
            weekDays.Add(Thuesday);
            weekDays.Add(Wednesday);
            weekDays.Add(Thusday);
            weekDays.Add(Friday);

            return weekDays;
        }

        public bool IsBusy(string day, int num1, int num2)
        {
            WeekDays dayofweek = new WeekDays();
            bool answer = false;

            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    dayofweek = weekDays[i];
                    break;
                }
            }

            for (int i = 0; i < 27; i++)
            {
                if (dayofweek.BusyOrFreeTimes[i, 0] == num1 && dayofweek.BusyOrFreeTimes[i, 1] == num2)
                {
                    answer = true;
                    break;
                }
            }

            return answer;
        }

        public void AddConsult(Patient pat, string day, int num1, int num2)
        {
            WeekDays dayofweek = new WeekDays();
            int[,] interval = new int[1, 2];
            interval[0, 0] = num1;
            interval[0, 1] = num2;

            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    dayofweek = weekDays[i];
                    break;
                }
            }

            for (int i = 0; i < 27; i++)
            {
                if (WeekDays.hours[i, 0] == interval[0, 0] && WeekDays.hours[i, 1] == interval[0, 1])
                {
                    dayofweek.BusyOrFreeTimes[i, 0] = WeekDays.hours[i, 0];
                    dayofweek.BusyOrFreeTimes[i, 1] = WeekDays.hours[i, 1];
                    break;
                }
            }

            try
            {
                dayofweek.patients.Add(pat, interval);
            }
            catch (Exception)
            {
                Console.WriteLine("You can not consult more than one time at day.");
            }

        }

        public void MakeFree(Patient patient, string day)
        {
            WeekDays dayofweek = new WeekDays();
            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    dayofweek = weekDays[i];
                    break;
                }
            }

            int[,] value = dayofweek.patients[patient];
            dayofweek.patients.Remove(patient);

            for (int j = 0; j < 27; j++)
            {
                if (dayofweek.BusyOrFreeTimes[j, 0] == value[0, 0] && dayofweek.BusyOrFreeTimes[j, 1] == value[0, 1])
                {
                    dayofweek.BusyOrFreeTimes[j, 0] = 0;
                    dayofweek.BusyOrFreeTimes[j, 1] = 0;
                    break;
                }
            }
        }

        public bool Consult(Patient pat, string day)
        {
            bool answer = false;
            int[,] interval = new int[1, 2];
            WeekDays dayofweek;
            for (int i = 0; i < this.weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    dayofweek = weekDays[i];

                    for (int j = 0; j < 27; j++)
                    {
                        if (dayofweek.BusyOrFreeTimes[j, 0] == 0 && dayofweek.BusyOrFreeTimes[j, 1] == 0)
                        {
                            dayofweek.BusyOrFreeTimes[j, 0] = WeekDays.hours[j, 0];
                            dayofweek.BusyOrFreeTimes[j, 1] = WeekDays.hours[j, 1];

                            interval[0, 0] = dayofweek.BusyOrFreeTimes[j, 0];
                            interval[0, 1] = dayofweek.BusyOrFreeTimes[j, 1];
                            try
                            {
                                dayofweek.patients.Add(pat, interval);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("You can not consult more than one time at day");
                            }
                            answer = true;

                            break;
                        }
                    }

                }
            }
            return answer;
        }

        public static bool IsPossible(int first, int second)
        {
            bool answer = false;
            for (int i = 0; i < 27; i++)
            {
                if (WeekDays.hours[i, 0] == first && WeekDays.hours[i, 1] == second)
                {
                    answer = true;
                    break;
                }
            }
            return answer;
        }

        public void ShowTable(Doctor doc, string day)
        {
            int index = 0;
            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    index = i;
                    break;
                }
            }

            foreach (var member in doc.calendar.weekDays[index].patients)
            {
                int[,] a = member.Value;
                Console.WriteLine(member.Key.ToString() + "\n" + MyCalendar.FormatHour(a[0, 0]) + " " + MyCalendar.FormatHour(a[0, 1]));
            }
        }

        public int[,] GetInterval(string day, Patient pat)
        {
            int index = 0;
            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    index = i;
                    break;
                }
            }

            int[,] interval = new int[1, 2];
            foreach (var patient in weekDays[index].patients)
            {
                if (patient.Key.Equals(pat))
                {
                    interval = patient.Value;
                }
            }
            return interval;
        }

        public Patient GetPatAtInterval(string day, TimeSpan now)
        {
            Patient pat = new Patient();
            int[,] interval = new int[1, 2];

            string time = "" + now;
            string nowTime = time.Substring(0, 5);
            int index = nowTime.IndexOf(":");
            nowTime = nowTime.Remove(index, 1);
            int final = int.Parse(nowTime);

            for (int i = 0; i < weekDays.Count; i++)
            {
                if (weekDays[i].Name.Equals(day))
                {
                    index = i;
                    for (int j = 0; j < 27; j++)
                    {
                        if (final > weekDays[i].BusyOrFreeTimes[j, 0] && final < weekDays[i].BusyOrFreeTimes[j, 1])
                        {
                            interval[0, 0] = weekDays[i].BusyOrFreeTimes[j, 0];
                            interval[0, 1] = weekDays[i].BusyOrFreeTimes[j, 1];
                            break;
                        }
                    }
                }
            }

            foreach (var elem in weekDays[index].patients)
            {
                if (elem.Value[0, 0] == interval[0, 0] && elem.Value[0, 1] == interval[0, 1])
                {
                    pat = elem.Key;
                    break;
                }
            }
            return pat;
        }

        public static string FormatHour(int time)
        {
            string output = time / 100 + ":" + time % 100;
            if (output.Length == 4)
                output += "0";
            return output;
        }

        public static int ReFormatHour(string str)
        {
            int index = str.IndexOf(":");
            str = str.Remove(index, 1);
            return int.Parse(str);
        }

    }
}






