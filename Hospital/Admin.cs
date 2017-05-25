using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Admin : User, IShowPatient
    {

        public Admin(string name, string surname, string login, string password, string cardNumber, int balance)
            : base(name, surname, login, password, cardNumber, balance)
        {
            Hospital.admin = this;
        }

        public List<Doctor> ShowAllDoctors()
        {
            return Hospital.doctors;
        }

        public List<Patient> ShowPatients()
        {
            return Hospital.patients;
        }

        public bool RiseSalary(Doctor doc)    // by count of patients
        {
            List<Patient> patients = doc.ShowPatients();
            bool rise = false;
            if (patients.Count >= 4)
            {
                doc.Salary += 25000;
                rise = true;
            }
            return rise;
        }

        public bool DropSalary(Doctor doc)    // by count of patients
        {
            List<Patient> patients = doc.ShowPatients();
            bool drop = false;
            if (patients.Count <= 2)
            {
                doc.Salary -= 10000;
                drop = true;
            }
            return drop;
        }

        public static void Report()
        {
            Console.WriteLine("At this time Hospital's balance is {0}", Hospital.GetEarning());
            foreach (var doc in Hospital.doctors)
            {
                Console.WriteLine("Doctor {0} {1} - Served patients count  {2}", doc.Name, doc.Surname, doc.ShowPatients().Count);
            }
        }

        /*****************Reading and Writing*********************************/

        public static void ReadAdminFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Admin.txt"))
                {
                    string s = "";
                    Admin admin;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] str = s.Split();
                        string name = str[0];
                        string surname = str[1];
                        string login = str[2];
                        string password = str[3];
                        string cardNumber = str[4];
                        int balance = int.Parse(str[5]);

                        admin = new Admin(name, surname, login, password, cardNumber, balance);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Admin.txt:");
                Console.WriteLine(e.Message);
            }
        }
        public static void WriteAdminFile()
        {
            try
            {
                File.WriteAllText("Admin.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Admin.txt"))
                {

                    sw.WriteLine(Hospital.admin.Name + " " + Hospital.admin.Surname + " " + Hospital.admin.Login + " " +
                                 Hospital.admin.Password + " " + Hospital.admin.CardNumber + " " + Hospital.admin.Balance);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Admin:");
                Console.WriteLine(e.Message);
            }
        }

    }
}
