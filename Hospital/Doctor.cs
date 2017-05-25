using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Doctor : User, IHistory, IShowPatient, IComparable<Doctor>
    {
        public string Speciality { get; private set; }
        public int Salary { get; set; }
        private List<Patient> my_patients = new List<Patient>();
        public MyCalendar calendar = new MyCalendar();
        private List<Doctor> BySpeciality = new List<Doctor>();     //temporary list


        public Doctor()
        { }

        public Doctor(string name, string surname, string login, string password, string cardNumber, int balance, int salary, string speciality) : base(name, surname, login, password, cardNumber, balance)
        {
            Salary = salary;
            Speciality = speciality;
            Hospital.doctors.Add(this);
        }

        public List<Patient> ShowPatients()
        {
            return my_patients;
        }

        public void Serve(Patient patient, string diagnos, DateTime date)
        {
            patient.my_history.Add(date, diagnos);
            string day = DateTime.Now.DayOfWeek.ToString().ToLower();
            calendar.MakeFree(patient, day);
            my_patients.Add(patient);
            Patient.WritePatFile();
        }

        public Dictionary<DateTime, string> ShowHistory(Patient pat)
        {
            return pat.my_history;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} {1}     Speciality: {2}", Name, Surname, Speciality);
        }

        public string MySalary()
        {
            return string.Format("Salary: " + Salary);
        }

        public int CompareTo(Doctor other)
        {
            if (other.my_patients.Count > this.my_patients.Count)
            {
                return 1;
            }
            else if (other.my_patients.Count < this.my_patients.Count)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int DoctorsBySpeciality(string speciality)
        {
            BySpeciality.Clear();
            speciality.ToLower();
            foreach (var item in Hospital.doctors)
            {
                if (item.Speciality.ToLower() == speciality)
                {
                    BySpeciality.Add(item);
                }
            }
            return BySpeciality.Count;
        }

        /*****************Reading and Writing*********************************/

        public static void ReadDocFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Doctors.txt"))
                {
                    string s = "";
                    Doctor doc = null;

                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "%")
                        {                                       //reading doctors
                            s = sr.ReadLine();
                            string[] str = s.Split();
                            string name = str[0];
                            string surname = str[1];
                            string login = str[2];
                            string password = str[3];
                            string cardNumber = str[4];
                            int balance = int.Parse(str[5]);
                            int salary = int.Parse(str[6]);
                            string speciality = str[7];
                            doc = new Doctor(name, surname, login, password, cardNumber, balance, salary, speciality);
                        }
                        if (sr.ReadLine() == "*")
                        {
                            while ((s = sr.ReadLine()) != "*")
                            {
                                Patient pat = new Patient();
                                string[] patient = s.Split();           // reading their patients
                                pat.Name = patient[0];
                                pat.Surname = patient[1];
                                pat.Login = patient[2];
                                pat.Password = patient[3];
                                pat.CardNumber = patient[4];
                                pat.Balance = int.Parse(patient[5]);
                                pat.Age = int.Parse(patient[6]);
                                doc.my_patients.Add(pat);
                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Doctors.txt:");
                Console.WriteLine(e.Message);
            }
        }

        public static void WriteDocFile()
        {
            try
            {
                File.WriteAllText("Doctors.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Doctors.txt"))
                {
                    foreach (var member in Hospital.doctors)
                    {
                        sw.WriteLine("%");
                        sw.WriteLine(member.Name + " " + member.Surname + " " + member.Login + " " + member.Password + " "
                            + member.CardNumber + " " + member.Balance + " " + member.Salary + " " + member.Speciality);
                        sw.WriteLine("*");
                        foreach (var pat in member.my_patients)
                        {
                            sw.WriteLine(pat.Name + " " + pat.Surname + " " + pat.Login + " " + pat.Password + " " + pat.CardNumber + " "
                                          + pat.Balance + " " + pat.Age);
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


