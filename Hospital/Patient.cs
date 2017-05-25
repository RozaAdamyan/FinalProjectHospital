using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Patient : User, IHistory
    {
        public Dictionary<DateTime, string> my_history = new Dictionary<DateTime, string>();
        public int Age { get; set; }

        public Patient(string name, string surname, int age, string login, string password, string cardNumber, int balance)
            : base(name, surname, login, password, cardNumber, balance)
        {
            Age = age;
            Hospital.patients.Add(this);
        }

        public Patient()
        { }

        public List<Doctor> FindDoctorBySpeciality(string speciality)
        {
            List<Doctor> docs = new List<Doctor>();

            foreach (var doc in Hospital.doctors)
            {
                if ((doc.Speciality).Equals(speciality))
                {
                    docs.Add(doc);
                }
            }
            return docs;
        }

        public Doctor FindDoctorByNameSurname(string name, string surname)
        {
            Doctor doctor = null;
            foreach (var doc in Hospital.doctors)
            {
                if ((doc.Name).Equals(name))
                {
                    if ((doc.Surname).Equals(surname))
                    {
                        doctor = doc;
                        break;
                    }
                }
            }
            return doctor;
        }

        public void Pay(int money)
        {
            Balance -= money;
        }

        public Dictionary<DateTime, string> ShowHistory(Patient pat)
        {
            return my_history;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}    Age: {2}", Name, Surname, Age);
        }

        /*****************Reading and Writing*********************************/

        public static void ReadPatFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Patients.txt"))
                {
                    string s = "";
                    Patient pat = null;

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "%")
                        {                                       //reading patients
                            s = sr.ReadLine();
                            string[] str = s.Split();
                            string name = str[0];
                            string surname = str[1];
                            string login = str[2];
                            string password = str[3];
                            string cardNumber = str[4];
                            int balance = int.Parse(str[5]);
                            int age = int.Parse(str[6]);
                            pat = new Patient(name, surname, age, login, password, cardNumber, balance);
                        }
                        if (sr.ReadLine() == "*")
                        {
                            while ((s = sr.ReadLine()) != "*")      //reading their histories
                            {
                                DateTime date = DateTime.ParseExact(s, "dd.MM.yyyy HH.mm.ss", CultureInfo.InvariantCulture);
                                string diagnos = sr.ReadLine();
                                pat.my_history.Add(date, diagnos);
                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Patients.txt:");
                Console.WriteLine(e.Message);
            }
        }

        public static void WritePatFile()
        {
            try
            {
                File.WriteAllText("Patients.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Patients.txt"))
                {
                    foreach (var member in Hospital.patients)
                    {
                        sw.WriteLine("%");
                        sw.WriteLine(member.Name + " " + member.Surname + " " + member.Login + " " + member.Password + " "
                            + member.CardNumber + " " + member.Balance + " " + member.Age);
                        sw.WriteLine("*");
                        foreach (var elem in member.my_history)
                        {
                            sw.WriteLine(elem.Key.ToString("dd.MM.yyyy HH.mm.ss"));
                            sw.WriteLine(elem.Value);
                        }
                        sw.WriteLine("*");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file Doctors.txt: ");
                Console.WriteLine(e.Message);
            }
        }

    }
}
