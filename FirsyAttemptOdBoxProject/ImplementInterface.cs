using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirsyAttemptOdBoxProject
{
    internal class ImplementInterface : Logic.IMessageable
    {
        public void isError(string message) => Console.WriteLine(message);
        public void isShow(string message) => Console.WriteLine(message);
        public void isSucceed(string message) => Console.WriteLine(message);
        public void isSuccess(string message) => Console.WriteLine(message);
        public bool isWant()
        {
            string s = Console.ReadLine();
            if (s == "1") return true;
            return false;
        }
    }
}
