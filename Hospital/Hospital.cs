using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    class Hospital
    {
        public static Admin admin;
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Adress { get; private set; }
        public string Website { get; private set; }
        public static List<Doctor> doctors = new List<Doctor>();
        public static List<Patient> patients = new List<Patient>();
        private static int state_order = 2;
        private static long earning = 500000;

        public static long GetEarning()
        {
            return earning;
        }

        public static int ReturnOrders()
        {
            return state_order;
        }

        public static void DecOrders()
        {
            state_order--;
        }

        public Hospital(string name, string phone_number, string adress, string website)
        {
            Name = name;
            PhoneNumber = phone_number;
            Adress = adress;
            Website = website;
            Medicine.InitializeMedicine();
        }

        public static void Earn(int money)
        {
            earning += money;
        }

        public string AboutHospital()
        {
            return String.Format("Hospital: {0}\nPhone number: {1}\nAdress: {2}\nWebsite: {3}", Name, PhoneNumber, Adress, Website);
        }

        public List<Doctor> ShowDoctors()
        {
            doctors.Sort();
            return doctors;
        }


    }
}

