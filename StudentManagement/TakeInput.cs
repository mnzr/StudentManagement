using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    abstract class TakeInput
    {
        // Helper functions
        public int user_choice()
        {
            // Get userchoice
            Console.Write("Your input");
            prompt();
            string input = Console.ReadLine();
            int number;
            Int32.TryParse(input, out number);

            return number;
        }

        public void prompt()
        {
            Console.Write(" > ");
        }

        public String prompt(String message)
        {
            Console.Write(message);
            Console.Write(" > ");

            String information = Console.ReadLine();

            return information;
        }
    }
}
