using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Manage manage = new Manage();
            Console.WriteLine(manage.connectionString);
            Console.WriteLine("Now to the main program\n\n");

            manage.main_menu();
            //Console.ReadLine();
        }
    }
}
