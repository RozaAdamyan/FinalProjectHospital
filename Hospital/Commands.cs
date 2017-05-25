using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital
{
    class Commands
    {
        private static Hospital hospital;

        public Commands()
        { }

        private static Hospital CreateHospital()
        {
            hospital = new Hospital("Healthy lifestyle", "098282828", "Komitas 28", "HealthyLifestyle@mail.com");
            return hospital;
        }

        private static Command ValidCommand(string line)
        {
            line = line.Trim().Split()[0];
            foreach (string command in Enum.GetNames(typeof(Command)))
            {
                if (command.Equals(line))
                {
                    return (Command)Enum.Parse(typeof(Command), command);

                }
            }
            return Command.nonvalidCommand;
        }

        public static void JustDoIt()
        {
            Medicine pharmacy = new Medicine();

            Hospital hospital = CreateHospital();

            Doctor.ReadDocFile();
            Patient.ReadPatFile();
            Admin.ReadAdminFile();

            Admin admin = Hospital.admin;

            User user = new User();
            bool isactive = false;

            Console.WriteLine("Hi, please enter help, if its your first time here.");

            bool status = true;
            while (status)
            {
                string command = Console.ReadLine().Trim().ToLower();
                Command com = ValidCommand(command);
                if (com == Command.nonvalidCommand)
                {
                    Console.WriteLine("Invalid command ");
                    continue;
                }

                switch (com)
                {
                    case Command.help:
                        HelpCommon();
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Doctor))
                                HelpDoctor();
                            else if (user.GetType() == typeof(Admin))
                                HelpAdmin();
                            else if (user.GetType() == typeof(Patient))
                                HelpPatient();
                        }
                        break;

                    case Command.exit:
                        status = false;
                        break;

                    case Command.about_hospital:
                        Console.WriteLine(hospital.AboutHospital());
                        break;

                    /*start create acc*/
                    case Command.create_an_account:
                        string spec = null;
                        string salary = null;
                        int salaryint = 0;

                        if (user.GetType() == typeof(Admin))
                        {
                            bool specvalid = false;
                            Regex rgx = new Regex("^[a-z]+$");
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Please enter speciality of doctor:");
                                spec = Console.ReadLine().ToLower();
                                if ((rgx.IsMatch(spec)))
                                {
                                    specvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Speciality must contain only letters");
                                    continue;
                                }
                            }
                            if (!specvalid)
                            {
                                Console.WriteLine("You couldn't enter speciality.\nPlease enter command");
                                continue;
                            }


                            bool salaryvalid = false;
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    Console.WriteLine("Enter salary of doctor.");
                                    salary = Console.ReadLine();
                                    salaryint = int.Parse(salary);
                                    if (salaryint <= 200000)
                                    {
                                        Console.WriteLine("Salary of doctor must be more than 200000");
                                        continue;
                                    }
                                    else
                                    {
                                        salaryvalid = true;
                                        break;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Salary must be an integer.");
                                    continue;
                                }
                            }
                            if (!salaryvalid)
                            {
                                Console.WriteLine("You couldn't enter salary.\nPlease enter command");
                                continue;
                            }
                        }
                        else if (user.GetType() == typeof(Doctor))
                        {
                            Console.WriteLine("Log out to create an account of patient.");
                            continue;
                        }

                        if (!isactive || user.GetType() == typeof(Admin))
                        {
                            Console.WriteLine("Please enter name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Please enter last name: ");
                            string lastname = Console.ReadLine();

                            string card_number = "";
                            bool cardvalid = false;
                            Regex rgx1 = new Regex("^[0-9]+$");
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter please card number: ");
                                card_number = Console.ReadLine();
                                if (rgx1.IsMatch(card_number) && card_number.Length == 16)
                                {
                                    cardvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Card number must contain only 16 digits.");
                                    continue;
                                }
                            }
                            if (!cardvalid)
                            {
                                Console.WriteLine("You couldn't enter valid card number.\nPlease enter command.");
                                continue;
                            }

                            string balance = "";
                            bool validbalance = false;
                            int balanceint = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    Console.WriteLine("Enter balance in card:");
                                    balance = Console.ReadLine();
                                    balanceint = int.Parse(balance);
                                    if (balanceint < 0)
                                    {
                                        Console.WriteLine("Balance must be positive");
                                        continue;
                                    }
                                    else
                                    {
                                        validbalance = true;
                                        break;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Enter integer number.");
                                    continue;
                                }
                            }
                            if (validbalance == false)
                            {
                                Console.WriteLine("You couldn't enter valid balance.\nPlease enter command.");
                                continue;
                            }

                            string login = "";
                            bool logvalid = false;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter login: ");
                                login = Console.ReadLine();
                                if (User.IsValidLogin(login))
                                {
                                    logvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("This login is already existing, try again");
                                    continue;
                                }
                            }
                            if (logvalid == false)
                            {
                                Console.WriteLine("Think of unique login.\nPlease enter command");
                                continue;
                            }

                            string password = "";
                            //password length must be 7, it must contain at least one digit, and special symbol
                            Regex rgx2 = new Regex("^(?=.*[!@#$&])(?=.*[0-9]).{7}$");
                            bool pasvalid = false;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter password: ");
                                password = Console.ReadLine();
                                if (rgx2.IsMatch(password))
                                {
                                    pasvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Password length must be 7, and contain at least one digit and one special symbol [!@#$&]");
                                    continue;
                                }
                            }
                            if (pasvalid == false)
                            {
                                Console.WriteLine("Think of correct password.\nPlease enter command");
                                continue;
                            }

                            if (spec != null)
                            {
                                User doc = new Doctor(name, lastname, login, password, card_number, balanceint, salaryint, spec);
                                Console.WriteLine("Account of Dr.{0} have been created.", lastname);
                                Doctor.WriteDocFile();
                            }
                            else
                            {

                                string age_str = "";
                                int age = 0;
                                bool age_valid = false;
                                for (int i = 0; i < 5; i++)
                                {
                                    Console.WriteLine("Please enter age:");
                                    try
                                    {
                                        age_str = Console.ReadLine();
                                        age = int.Parse(age_str);
                                        if (age < 18 && age > 120)
                                        {
                                            Console.WriteLine("Age must be in 18-120 range");
                                            continue;
                                        }
                                        else
                                        {
                                            age_valid = true;
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Age must be an integer");
                                        continue;
                                    }
                                }
                                if (!age_valid)
                                {
                                    Console.WriteLine("You couldn't enter age.\nPlease enter command.");
                                    continue;
                                }

                                User patient = new Patient(name, lastname, age, login, password, card_number, balanceint);
                                Patient.WritePatFile();
                                Console.WriteLine("{0}, your account have been created.\nYou already can log in to your account", name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log out to create an account, you are not admin.");
                        }
                        break;
                    /*end create acc*/


                    case Command.change_password:
                        if (isactive)
                        {
                            bool current = false;
                            for (int i = 0; i < 5; i++)                     // right current password
                            {
                                Console.WriteLine("Enter your current password");
                                string password_old = Console.ReadLine();
                                if (user.Password.Equals(password_old))
                                {
                                    current = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Enter your current password right.\nTry again");
                                    continue;
                                }
                            }
                            if (current == false)
                            {
                                Console.WriteLine("You couldn't enter current password right.\nPlease eneter command.");
                                continue;
                            }

                            bool newpass = false;
                            Regex rgx2 = new Regex("^(?=.*[!@#$&])(?=.*[0-9]).{7}$");
                            for (int j = 0; j < 5; j++)             // new password validation
                            {
                                Console.WriteLine("Enter new password: ");
                                string new_password = Console.ReadLine();
                                if (rgx2.IsMatch(new_password))
                                {
                                    Console.WriteLine("Enter new password one more time: ");
                                    string second_password = Console.ReadLine();
                                    if (second_password.Equals(new_password))
                                    {
                                        newpass = true;
                                        user.ChangePassword(new_password);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("New passwords arn't matching. Try again.");
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("New password length must be 7, it must contain at least one digit and one special symbol: &!@#$");
                                    continue;
                                }
                            }
                            if (newpass == false)
                            {
                                Console.WriteLine("You could not change your password.\nPlease enter command.");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Your password have been changed.");
                                if (user.GetType() == typeof(Patient))
                                    Patient.WritePatFile();
                                if (user.GetType() == typeof(Doctor))
                                    Doctor.WriteDocFile();
                                if (user.GetType() == typeof(Admin))
                                    Admin.WriteAdminFile();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to change password");
                        }
                        break;
                    /*end change password*/

                    case Command.log_in:
                        if (!isactive)
                        {
                            Console.WriteLine("Please enter your login: ");
                            bool logexists = false;
                            string username = "";
                            for (int j = 0; j < 5; j++)
                            {
                                username = Console.ReadLine();
                                for (int i = 0; i < User.users.Count; i++)
                                {
                                    if (User.users[i].Login.Equals(username))
                                    {
                                        logexists = true;
                                        user = User.users[i];
                                        break;
                                    }
                                }
                                if (logexists == false)
                                {
                                    Console.WriteLine("You entered wrong login.\nTry again.");
                                    continue;
                                }
                                else break;
                            }
                            if (logexists == false)
                            {
                                Console.WriteLine("Login is not existing or you entered wrong login.\nPlease enter command");
                                continue;
                            }

                            string user_password = "";
                            bool passvalid = false;
                            Console.WriteLine("Please enter your password: ");
                            for (int i = 0; i < 5; i++)
                            {
                                user_password = "";
                                while (true)
                                {
                                    ConsoleKeyInfo ch = Console.ReadKey(true);
                                    if (ch.Key == ConsoleKey.Enter)
                                        break;
                                    else
                                    {
                                        user_password += ch.KeyChar;
                                        Console.Write("*");
                                    }
                                }
                                int j;
                                for (j = 0; j < User.users.Count; j++)
                                {
                                    if (User.users[j].Login.Equals(username))
                                    {

                                        if ((User.users[j].Password).Equals(user_password))
                                        {
                                            passvalid = true;
                                            break;
                                        }
                                    }
                                }
                                if (passvalid == false)
                                {
                                    Console.WriteLine("\nPlease enter correct password.");
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("\n{0} loged in", User.users[j].Login);
                                    isactive = true;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("You already loged in, if you want to log in to another acc, log out from yours.");
                            continue;
                        }
                        break;
                    /*end log_in*/

                    case Command.log_out:
                        if (isactive == true)
                        {
                            try
                            {
                                isactive = false;
                                Console.WriteLine("{0} loged out.", user.Login);
                                user = new User();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Oops!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You already are loged out.");
                        }
                        break;
                    /*end log_out*/

                    case Command.show_my_balance:
                        if (isactive)
                        {
                            int my_balance = user.ShowBalance();
                            Console.WriteLine("Your balance: " + my_balance);
                        }
                        else
                        {
                            Console.WriteLine("Log in to see balance.");
                        }
                        break;
                    /*end show_my_balance*/

                    case Command.add_money:
                        if (isactive)
                        {
                            Console.WriteLine("Money: ");
                            int money = 0;
                            try
                            {
                                money = int.Parse(Console.ReadLine());
                                if (money > 0)
                                {
                                    user.AddMoney(money);
                                    Console.WriteLine("Money has been added.\nPlease enter command.");
                                    if (user.GetType() == typeof(Patient))
                                        Patient.WritePatFile();
                                    if (user.GetType() == typeof(Doctor))
                                        Doctor.WriteDocFile();
                                    if (user.GetType() == typeof(Admin))
                                        Admin.WriteAdminFile();
                                }
                                else
                                {
                                    Console.WriteLine("Money must be positive.");
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Money must be integer.\nPlease enter command.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to add money.\nPlease enter command.");
                        }
                        break;

                    case Command.show_doctors:
                        if (isactive)
                        {
                            List<Doctor> doctors = hospital.ShowDoctors();
                            foreach (Doctor item in doctors)
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to see stuff of hospital.");
                        }
                        break;


                    case Command.my_patients:
                        if (user.GetType() == typeof(Doctor))
                        {
                            Doctor doc = (Doctor)user;
                            List<Patient> patients = doc.ShowPatients();
                            Console.WriteLine("Your patients.");
                            foreach (Patient pat in patients)
                            {
                                Console.WriteLine(pat.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have not access to such info.");
                        }
                        break;

                    case Command.show_history:
                        if (user.GetType() == typeof(Doctor))
                        {

                            Console.WriteLine("Enter patient name: ");
                            string name_patient = Console.ReadLine();
                            Console.WriteLine("Enter patient lastname: ");
                            string surname_patient = Console.ReadLine();
                            List<Patient> patients = new List<Patient>();       // if there are few patients of the same name
                            for (int i = 0; i < Hospital.patients.Count; i++)
                            {
                                if (Hospital.patients[i].Name.Equals(name_patient) && Hospital.patients[i].Surname.Equals(surname_patient))
                                {
                                    Patient pat = Hospital.patients[i];
                                    patients.Add(pat);
                                }
                            }
                            if (patients.Count != 0)
                            {
                                Doctor doc = (Doctor)user;
                                foreach (Patient patient in patients)
                                {
                                    Dictionary<DateTime, string> history = doc.ShowHistory(patient);
                                    foreach (KeyValuePair<DateTime, string> kvp in history)
                                    {
                                        Console.WriteLine(kvp.Key.ToString("dd.MM.yyyy HH.mm.ss") + " " + kvp.Value);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("We couldn't find patient with such name and surname.\nPlease enter command");
                            }

                        }
                        else
                        {
                            Console.WriteLine("You are patient, an you can see only your history. Enter show_my_history.");
                        }
                        break;

                    case Command.show_my_history:
                        if (user.GetType() == typeof(Patient))
                        {
                            Patient pat = (Patient)user;
                            Console.WriteLine(pat.ToString());
                            Dictionary<DateTime, string> history = pat.ShowHistory(pat);
                            foreach (KeyValuePair<DateTime, string> kvp in history)
                            {
                                Console.WriteLine(kvp.Key.ToString() + " " + kvp.Value);
                            }

                        }
                        break;

                    case Command.all_patients:
                        if (user.GetType() == typeof(Admin))
                        {
                            List<Patient> all_patients = admin.ShowPatients();
                            foreach (Patient pat in all_patients)
                            {
                                Console.WriteLine(pat.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have not access to such info.");
                        }
                        break;

                    case Command.rise_salary:
                        if (user.GetType() == typeof(Admin))
                        {
                            Console.WriteLine("Enter name of doctor:");
                            string doc_name = Console.ReadLine();
                            Console.WriteLine("Enter surname of doctor:");
                            string doc_surname = Console.ReadLine();
                            Console.WriteLine("Enter speciality of doctor:");
                            string spec_doctor = Console.ReadLine();
                            bool rise = false;
                            foreach (Doctor doc in Hospital.doctors)
                            {
                                if (doc.Name.Equals(doc_name) && doc.Surname.Equals(doc_surname) && doc.Speciality.Equals(spec_doctor))
                                {
                                    rise = admin.RiseSalary(doc);
                                    break;
                                }
                            }
                            if (rise)
                            {
                                Console.WriteLine("Dr.{0}'s salary have been rised.", doc_surname);
                                Doctor.WriteDocFile();
                            }
                            else
                            {
                                Console.WriteLine("Salary have not been rised.");
                            }
                        }
                        break;

                    case Command.drop_salary:
                        if (user.GetType() == typeof(Admin))
                        {
                            Console.WriteLine("Enter name of doctor:");
                            string doc_name = Console.ReadLine();
                            Console.WriteLine("Enter surname of doctor:");
                            string doc_surname = Console.ReadLine();
                            Console.WriteLine("Enter speciality of doctor:");
                            string spec_doctor = Console.ReadLine();
                            bool drop = false;
                            foreach (Doctor doc in Hospital.doctors)
                            {
                                if (doc.Name.Equals(doc_name) && doc.Surname.Equals(doc_surname) && doc.Speciality.Equals(spec_doctor))
                                {
                                    drop = admin.DropSalary(doc);
                                    break;
                                }
                            }
                            if (drop)
                            {
                                Console.WriteLine("Dr.{0}'s salary have been droped.", doc_surname);
                                Doctor.WriteDocFile();
                            }
                            else
                            {
                                Console.WriteLine("Salary have not been droped.");
                            }
                        }
                        break;

                    case Command.request_for_consult:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Patient))
                            {
                                Patient pat = (Patient)user;
                                Doctor doc = new Doctor();
                                Console.WriteLine("If you want to find doctor by specility, please enter word <<speciality>>, you also can find doctor by name, enter word <<name>>");
                                string specorname = Console.ReadLine();
                                if (specorname.ToLower().Equals("speciality"))
                                {
                                    Console.WriteLine("Enter speciality.");
                                    string speciality = Console.ReadLine();
                                    List<Doctor> doctors = pat.FindDoctorBySpeciality(speciality);
                                    if (doctors.Count != 0)
                                    {
                                        doc = doctors[0];       // returns first doctor of the list, if you want other doctor search by name
                                        Console.WriteLine(doc.ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("We have not doctor of such speciality.\nPlease enter command.");
                                        continue;
                                    }
                                }
                                else if (specorname.ToLower().Equals("name"))
                                {
                                    Console.WriteLine("Enter name:");
                                    string name_doc = Console.ReadLine();
                                    Console.WriteLine("Enter surname:");
                                    string surname_doc = Console.ReadLine();
                                    Doctor doc_ = pat.FindDoctorByNameSurname(name_doc, surname_doc);
                                    if (doc_ == null)
                                    {
                                        Console.WriteLine("Doctor have not been found.\nPlease enter command.");
                                        continue;
                                    }
                                    doc = doc_;
                                    Console.WriteLine(doc.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("We can find doctor only by name or speciality.\nPlease enter command.");
                                    continue;
                                }

                                bool done = false;
                                string day = "";
                                int first = 0, second = 0;
                                try
                                {

                                    Console.WriteLine("Enter day of week when you want to consult:");
                                    day = Console.ReadLine().ToLower();
                                    if (day.Equals("monday") || day.Equals("thusday") || day.Equals("wednesday") || day.Equals("thusday") || day.Equals("friday"))
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            Console.WriteLine("Enter beginning time(with format 00:00) ");
                                            string format1 = Console.ReadLine();
                                            first = MyCalendar.ReFormatHour(format1);
                                            Console.WriteLine("Enter finish time(with format 00:00) consultation duration must be 20 mins ");
                                            string format2 = Console.ReadLine();
                                            second = MyCalendar.ReFormatHour(format2);
                                            if (((first >= 1000 && first < 1300) || (first >= 1400 && first < 1900)) && (MyCalendar.IsPossible(first, second)))
                                            {
                                                if (doc.calendar.IsBusy(day, first, second))
                                                {
                                                    Console.WriteLine("Sorry, but that interval is already busy.");
                                                    if (i == 0)
                                                        Console.WriteLine("You can try one more time.");
                                                    continue;
                                                }
                                                else
                                                {
                                                    done = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Working hours are 10:00 - 19:00 with break from 13:00 to 14:00\nConsultation lasts 20 mins\nExamples \n12:00 12:20\n12:20 12:40\n12:40 13:00");
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("It is not working day.\nPlease enter command.");
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Oops!\nProblem with calendar. Maybe you entered wrong format of time");
                                    continue;
                                }
                                if (done)
                                {
                                    doc.calendar.AddConsult(pat, day, first, second);
                                    Console.WriteLine("You will be served at your requested time(if there is no warning above).\nPlease enter command.");
                                }
                                else
                                {
                                    if (doc.calendar.Consult(pat, day))
                                    {
                                        int[,] time = doc.calendar.GetInterval(day, pat);
                                        Console.WriteLine("You will be served(if there is no warning) at your asked day at {0} {1}.", MyCalendar.FormatHour(time[0, 0]), MyCalendar.FormatHour(time[0, 1]));
                                        continue;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry, we have not any free time at your asked day.");
                                        continue;
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("Only patients can leave requests for consultations.\nPlease enter command");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to request for consultation.");
                        }
                        break;

                    case Command.show_calendar:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Doctor))
                            {
                                Doctor doc = (Doctor)user;
                                Console.WriteLine("Enter day of week");
                                string day = Console.ReadLine();

                                Console.WriteLine(doc.Name + " " + doc.Surname + " ");
                                Console.WriteLine();
                                doc.calendar.ShowTable(doc, day);

                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Log in to see requests.");
                        }
                        break;


                    case Command.my_salary:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Doctor))
                            {
                                Doctor doc = (Doctor)user;
                                Console.WriteLine(doc.MySalary());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to see salary.\nPlease enter command.");
                        }
                        break;

                    case Command.serve:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Doctor))
                            {
                                Doctor doc = (Doctor)user;
                                string today = DateTime.Now.DayOfWeek.ToString().ToLower();
                                Console.WriteLine("Today you must serve.");         // shows today's all patients
                                doc.calendar.ShowTable(doc, today);
                                Console.WriteLine();
                                TimeSpan now = DateTime.Now.TimeOfDay;
                                Patient pat = doc.calendar.GetPatAtInterval(today, now);    //try to find patient whom doc must serve right now
                                if (pat.Age != 0)                                           // if finds serves him
                                {
                                    Console.WriteLine("Now you must serve: ");
                                    Console.WriteLine("Patient: " + pat.ToString());
                                    Console.WriteLine("Enter please name of disease(with underscore instead of space)");
                                    string disease = Console.ReadLine();
                                    Console.WriteLine("Enter please the name of medicine for treatment(with underscore instead of space)");
                                    string med = Console.ReadLine().ToLower();
                                    string diagnos = "disease:" + disease + " " + "medicine:" + med;
                                    doc.Serve(pat, diagnos, DateTime.Now);
                                    Console.WriteLine("Patient served.");
                                    Doctor.WriteDocFile();
                                }
                                else
                                {
                                    Console.WriteLine("At this moment you have not patient to serve.");     // else don't serve
                                }
                            }
                            else
                            {
                                Console.WriteLine("Only doctors can serve patients.\nPlease enter command.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to serve patient.\nPlease enter command.");
                        }
                        break;

                    case Command.add_drug:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Admin))
                            {
                                Console.WriteLine("Enter name of drug:");
                                string name = Console.ReadLine();
                                int price = 0;
                                bool pricevalid = false;
                                for (int i = 0; i < 5; i++)
                                {
                                    Console.WriteLine("Enter price:");
                                    try
                                    {
                                        price = int.Parse(Console.ReadLine());
                                        pricevalid = true;
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Price must be integer.");
                                        continue;
                                    }
                                }
                                if (pricevalid)
                                {
                                    pharmacy.AddDrug(name, price);
                                    Medicine.DrugWriter();
                                    Console.WriteLine("Drug have been added");
                                }
                                else
                                {
                                    Console.WriteLine("You couldn't add drug.\nPlease enter command");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Only admin can add drug");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to add drug");
                        }
                        break;

                    case Command.change_price:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Admin))
                            {
                                Console.WriteLine("Write the name of drug which price you want to change:");
                                string name = Console.ReadLine();
                                bool isExist = pharmacy.IsExistsDrug(name);

                                if (isExist == false)
                                {
                                    Console.WriteLine("Drug does not exist.\nPlease enter command.");
                                    continue;
                                }

                                int newPrice = 0;
                                bool pricevalid = false;
                                for (int i = 0; i < 5; i++)
                                {
                                    Console.WriteLine("Enter new price for that drug");
                                    try
                                    {
                                        newPrice = int.Parse(Console.ReadLine());
                                        pricevalid = true;
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Price must be integer.");
                                        continue;
                                    }
                                }
                                if (pricevalid)
                                {
                                    pharmacy.ChangePrice(name, newPrice);
                                    Medicine.DrugWriter();
                                    Console.WriteLine("The price was successfully changed. ");
                                }
                                else
                                {
                                    Console.WriteLine("You couldn't change  drug price.\nPlease enter command");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Only admin can change drug price.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to change drug price");
                        }
                        break;

                    case Command.buy_drug:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Patient))
                            {
                                Patient pat = (Patient)user;
                                Console.WriteLine(pharmacy.ShowPharmacy());
                                Console.WriteLine("Enter the name of drug:");
                                string name = Console.ReadLine().ToLower();
                                if (pharmacy.IsExistsDrug(name))
                                {
                                    bool countValid = false;
                                    int drugCount = 0;
                                    try
                                    {
                                        Console.WriteLine("Enter count of pills you want to buy");
                                        int count = int.Parse(Console.ReadLine());
                                        if (count < 1 || count > 1000)
                                        {
                                            Console.WriteLine("Too big order.");
                                            break;
                                        }
                                        else
                                        {
                                            countValid = true;
                                            drugCount = count;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Count must be an integer.\nPlease enter command.");
                                        continue;
                                    }
                                    if (!countValid)
                                    {
                                        Console.WriteLine("You couldn't buy drug.\nPlease enter command");
                                        continue;
                                    }
                                    try
                                    {
                                        int bill = 0;
                                        int age = pat.Age;
                                        if (age > 63)
                                        {
                                            if (Hospital.ReturnOrders() >= 1)
                                            {
                                                Console.WriteLine("You have bought this drug FREE!");
                                                Hospital.DecOrders();
                                                continue;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Special Sales for Olders(15%)");
                                            }
                                        }
                                        else
                                        {
                                            bill = pharmacy.BuyDrug(name, drugCount, age);
                                            Console.WriteLine("Payment: " + bill);
                                            if (pat.Balance >= bill)
                                            {
                                                pat.Pay(bill);
                                                Hospital.Earn(bill);
                                                Console.WriteLine("You have bought this drug.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("You have not enought money.\nCheck your balance.\nPlease enter command.");
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Unexpected exception.\nPlease enter command.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Drug doesn't exist");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Only patients can buy drugs.");
                                break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Log in to buy drug.\nPlease enter command.");
                        }
                        break;

                    case Command.show_report:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Doctor))
                            {
                                Admin.Report();
                            }
                            else
                            {
                                Console.WriteLine("Only doctors can see admin reports.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to see reports.");
                        }
                        break;

                    case Command.add_doctor:
                        if (isactive)
                        {
                            if (user.GetType() == typeof(Admin))
                            {
                                Console.WriteLine("To add doctor you must create his account.\nEnter create_an_account.");
                            }
                            else
                            {
                                Console.WriteLine("Only admin ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to add doctor");
                        }
                        break;

                    default:
                        Console.WriteLine("It is not command, please enter valid commands.");
                        break;
                }
            }
        }

        private static void HelpCommon()
        {
            Console.WriteLine(
                "Valid commands for all users\n" +
                "about_hospital         : Shows main info about hospital\n" +
                "log_in                 : To log in\n" +
                "log_out                : To log out from account\n" +
                "create_an_account      : To create an account\n" +
                "change_password        : To change password of account\n" +
                "show_my_balance        : Shows money at your card\n" +
                "add_money              : To add money\n" +
                "show_doctors           : Shows all doctors of the hospital\n" +
                "exit                   : To exit program\n"

                );
        }

        private static void HelpDoctor()
        {
            Console.WriteLine(
                "Doctor's abilities\n" +
                "my_salary              : Shows salary of doctor\n" +
                "my_patients            : Shows all patients of doctor(patients which have been served by him)\n" +
                "show_history           : To show history of concrete doctor concrete patient\n" +
                "show_calendar          : To see consult list of concrete day\n" +
                "show_report            : Shows report\n" +
                "serve                  : To serve a patient\n"
            );
        }

        private static void HelpAdmin()
        {
            Console.WriteLine(
                "ADMIN's abilities\n" +
                "resumes                : To see resumes of potencial doctors\n" +
                "add_doctor             : To add doctor, with creating account\n" +
                "all_patients           : To show all patients of hospital\n" +
                "add_drug               : To add medicine\n" +
                "change_price           : To change price of medicine\n" +
                "rise_salary            : To rise salary of doctor\n" +
                "drop_salary            : To drop salary of doctor\n"
            );
        }

        private static void HelpPatient()
        {
            Console.WriteLine(
                "Patient's abilities\n" +
                "buy_drug               : To buy drug\n" +
                "request_for_consult    : Request for consultation\n" +
                "show_my_history        : Shows my history\n"
           );
        }
    }

    enum Command
    {
        help,
        log_in,
        log_out,
        create_an_account,
        change_password,
        about_hospital,
        buy_medicine,
        show_calendar,
        show_my_balance,
        add_money,
        add_doctor,
        my_salary,
        show_doctors,
        show_history,
        show_my_history,
        drop_salary,
        rise_salary,
        leave_resume,
        request_for_consult,
        serve,
        my_patients,
        add_drug,
        change_price,
        buy_drug,
        all_patients,
        exit,
        show_report,
        nonvalidCommand,
    }
}
