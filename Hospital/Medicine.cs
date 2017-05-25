using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    struct Medicine
    {
        public static IDictionary<string, int> medicine = new Dictionary<string, int>();

        public static void InitializeMedicine()
        {
            DrugReader();
        }

        public void AddDrug(string name, int price)
        {
            medicine.Add(name, price);
        }

        public void ChangePrice(string name, int new_price)
        {
            medicine[name] = new_price;
        }

        public double GetPrice(string name)
        {
            double price = medicine[name];
            return price;
        }

        public int BuyDrug(string name, int count, int age)
        {
            bool isExist = IsExistsDrug(name);
            int bill = 0;
            if (isExist)
            {
                bill += count * medicine[name];
            }
            if (age > 63)
            {
                bill = SaleForOlders(bill, 15);
            }
            return bill;
        }

        public int SaleForOlders(int price, double percent)
        {
            price = (int)(price - price * percent / 100);
            return price;
        }

        public bool IsExistsDrug(string name)
        {
            bool exists = false;
            foreach (string key in medicine.Keys)
            {
                if (key.Equals(name))
                {
                    exists = true;
                }

            }
            if (exists)
                return true;
            else
                return false;
        }

        public string ShowPharmacy()
        {
            string result = "";
            foreach (var item in medicine)
            {
                result += '>' + item.Key + " " + item.Value + '\n';
            }
            return result;
        }

        /*****************Reading and Writing*********************************/

        private static void DrugReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Drugs.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] keyvalue = s.Split();
                        medicine.Add(keyvalue[0], int.Parse(keyvalue[1]));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Drugs:");
                Console.WriteLine(e.Message);
            }

        }

        public static void DrugWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\Drugs.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Drugs.txt"))
                {
                    foreach (KeyValuePair<string, int> item in medicine)
                        sw.WriteLine(item.Key + " " + item.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file Drugs.txt:");
                Console.WriteLine(e.Message);
            }
        }


    }
}
