using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirsyAttemptOdBoxProject
{
    internal class Program
    {
        static ImplementInterface implementInterface = new ImplementInterface();
        static Manager manager = new Manager(implementInterface);
        static List<GeneralBox> boxes = new List<GeneralBox>();
        static void Main(string[] args)
        {
            //manager.Supply(10, 10, 3);
            //manager.Supply(10, 10, 3);
            //manager.Supply(10, 12, 5);
            //manager.Supply(11, 10, 10);
            SupplyOrCustomer();
        }
        static void BuyBox()
        {
            Console.Clear();
            Console.WriteLine("please inaert width height and amount");
            Console.WriteLine("width:");
            double.TryParse(Console.ReadLine(), out double width);
            Console.WriteLine("height:");
            double.TryParse(Console.ReadLine(), out double height);
            Console.WriteLine("amount");
            int.TryParse(Console.ReadLine(), out int amount);
            List<GeneralBox> boxes = manager.BuyBox(width, height, amount);
            
        }
        static void SupplyOrCustomer()
        {
            int num = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" 1.customer \n 2.suplly");
                int.TryParse(Console.ReadLine(), out num);
            } while (num != 1 && num != 2);
            switch (num)
            {
                case 1:
                    {
                        HomeScreenCustomer();
                        break;
                    }
                case 2:
                    {
                        HomeScreenManager();
                        break;
                    }
            }
        }
        static void HomeScreenManager()
        {
            int num;
            do
            {
                Console.WriteLine(" 1.restock \n 2.display all boxes \n 3.Exit \n 4.return to the maim menu");
                int.TryParse(Console.ReadLine(), out num);
                Console.Clear();
            } while (num > 4 && num < 0);
            switch (num)
            {
                case 1:
                    {
                        SupplyBox();
                        Console.WriteLine("succees");
                        break;
                    }
                case 2:
                    {
                        manager.ShowAllBoxes();
                        break;
                    }
                case 3:
                    {
                        return;
                    }
                case 4:
                    {
                        SupplyOrCustomer();
                        break;
                    }
            }
            HomeScreenManager();
        }
        static void HomeScreenCustomer()
        {
            int num;
            do
            {
                Console.WriteLine(" 1.show box \n 2.display all boxes \n 3.buy box \n 4.Exit \n 5.return to the main menu");
                int.TryParse(Console.ReadLine(), out num);
                Console.Clear();
            } while (num > 5 && num < 0);
            switch (num)
            {
                case 1:
                    {
                        ShowOneBox();
                        break;
                    }
                case 2:
                    {
                        manager.ShowAllBoxes();
                        Console.WriteLine("");
                        break;
                    }
                case 3:
                    {
                        BuyBox();
                        break;
                    }
                case 4:
                    {
                        return;
                    }
                case 5:
                    {
                        SupplyOrCustomer();
                        break;
                    }
            }
            HomeScreenCustomer();

        }
        private static void SupplyBox()
        {
            Console.Clear();
            Console.WriteLine("please inaert width height and amount");
            Console.WriteLine("width:");
            double.TryParse(Console.ReadLine(), out double width);
            Console.WriteLine("height:");
            double.TryParse(Console.ReadLine(), out double height);
            Console.WriteLine("amount");
            int.TryParse(Console.ReadLine(), out int amount);
            manager.Supply(width, height, amount);
        }
        static void ShowOneBox()
        {
            Console.Clear();
            double width;
            double height;
            Console.WriteLine("insert width:");
            double.TryParse(Console.ReadLine(), out width);
            Console.WriteLine("insert height:");
            double.TryParse(Console.ReadLine(), out height);
            manager.ShowBoxByDetails(width, height);
        }
    }

}
