using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Data;

namespace ConsoleApplication17
{
    class Program
    {
        static bool retry = true;
        static List<string> gathered = new List<string>();
        static string[] stored = new string[] {};
        static string input_ip;
        static bool error = false;
        static int dump_choice;
        static string[] commands = new string[] { "-D", "-h", "-s" };
        static bool ran_command;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" /$$$$$$$                          /$$$$$$$");
            Console.WriteLine("| $$__  $$                        | $$__  $$");
            Console.WriteLine("| $$  \\ $$  /$$$$$$  /$$$$$$/$$$$ | $$  \\ $$ /$$   /$$ /$$$$$$/$$$$   /$$$$$$");
            Console.WriteLine("| $$  | $$ /$$__  $$| $$_  $$_  $$| $$  | $$| $$  | $$| $$_  $$_  $$ /$$__  $$");
            Console.WriteLine("| $$  | $$| $$  \\ $$| $$ \\ $$ \\ $$| $$  | $$| $$  | $$| $$ \\ $$ \\ $$| $$  \\ $$");
            Console.WriteLine("| $$$$$$$/|  $$$$$$/| $$ | $$ | $$| $$$$$$$/|  $$$$$$/| $$ | $$ | $$| $$$$$$$/");
            Console.WriteLine("|_______/  \\______/ |__/ |__/ |__/|_______/  \\______/ |__/ |__/ |__/| $$____/");
            Console.WriteLine("                                                                    | $$");
            Console.WriteLine("                                                                    | $$");
            Console.WriteLine("                                                                    |__/");
            text_colors("y", "Beta V1.0\n");
            Console.ForegroundColor = ConsoleColor.White;
            while (retry == true)
            {
                retry = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please type a destination");
                Console.Write(" > ");
                Console.ForegroundColor = ConsoleColor.White;
                input_ip = Convert.ToString(Console.ReadLine());
                decisions();
                retry = true;
                ran_command = false;
            }
        }

        static string text_colors(string color, string text)
        {
            if (color == "g")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (color == "b")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            if (color == "cy")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            if (color == "y")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            return text;
        }

        static void decisions()
        {
            if (input_ip.StartsWith("-"))
            {
                run_command();
            }

            if (input_ip.Length == 0)
            {
                error = true;
            }

            if (input_ip != commands[0] && error == false && ran_command != true)
            {
                try
                {
                    WebClient client = new WebClient();
                    string read = client.DownloadString(input_ip);
                    gathered.Add(read);
                    text_colors("g", ("There is/are exactly " + gathered.Count + " value(s)"));
                    refresh();
                }
                catch (WebException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error Caught Web Exception\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            error = false;
        }

        static void refresh()
        {
            for (int b = 0; b < gathered.Count; b++)
            {
                if (b + 1 == gathered.Count)
                {
                    Console.WriteLine(gathered[b]);
                }
            }
        }

        static void dump()
        {
            for(int b = 1; b <= gathered.Count; b++)
            {
                text_colors("cy", "\t[" + b + "]. Choose value " + b);
            }
            try
            {
                Console.WriteLine("Select");
                Console.Write(" > ");
                dump_choice = Convert.ToInt32(Console.ReadLine());
                select_method();
            }
            catch
            {

            }
        }

        static void select_method()
        {
            for(int x = 0; x <= dump_choice; x++)
            {
                if (x == dump_choice)
                {
                    try
                    {
                        Console.WriteLine("Found Value");
                        Console.WriteLine(gathered[x - 1]);
                    }
                    catch
                    {
                        Console.WriteLine("Caught Error, Success");
                    }
                }
            }
        }

        static void run_command()
        {
            if (input_ip.Length == 1)
            {
                Console.WriteLine("No Valid Command Entered!");
                ran_command = true;
            }

            if (input_ip == commands[0])
            {
                dump();
                ran_command = true;
            }

            if (input_ip == commands[1])
            {
                Console.WriteLine("\t -D: Dumps the http documents retrieved in current session");
                Console.WriteLine("\t -h: Displays available commands and information");
                Console.WriteLine("\t -s: Writes retrieved http documents from current sessions to file");
                ran_command = true;
            }

            if (input_ip == commands[2])
            {
                string yes_no;
                Console.WriteLine("Would You Like To Log ALL http documents?");
                Console.Write("[y\\n]");
                yes_no = Console.ReadLine();
                ran_command = true;
            }
        }
    }
}
