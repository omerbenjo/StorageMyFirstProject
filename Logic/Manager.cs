using DataStracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Manager
    {
        BST<MainBox> MainTree;
        readonly double Standarddeviation = Properties.Settings.Default.Deaviation;
        int MAXAMOUNT = Properties.Settings.Default.MaxAmount;
        int expiredDate = Properties.Settings.Default.ExpiredDateDays;

        DoublyLinkedList<TimeData> timeData = new DoublyLinkedList<TimeData>();
        List<GeneralBox> generalBoxes = new List<GeneralBox>();
        LinkedList1060<MainBox> listOfUpdateMainBox = new LinkedList1060<MainBox>();
        LinkedList1060<SpecificBox> listOfRemoveExpiredSpecificBox = new LinkedList1060<SpecificBox>();
        LinkedList1060<SpecificBox> listOfUpdateExpiredSpecificBox = new LinkedList1060<SpecificBox>();

        IMessageable messageAble;
        Timer timer;
        public Manager(IMessageable massegeable)
        {
            TimeSpan checkPeriod = new TimeSpan(24, 0, 0);
            TimeSpan checkPeriodFirstTime = new TimeSpan(30, 0, 0, 0);
            timer = new Timer(CheckExpiraionBoxes, null, checkPeriodFirstTime, checkPeriod);
            this.messageAble = massegeable;
            MainTree = new BST<MainBox>();
        }
        public List<GeneralBox> BuyBox(double width, double height, int amount)
        {
            generalBoxes.Clear();
            int counterBoxType = 0;
            if (width <= 0 || height <= 0 || amount <= 0)
            {
                messageAble.isError("you insert unvalid size");
                return generalBoxes;
            }

            amount = CheckAmountValidition(amount);
            int restOfAmount = amount;
            var tmp = new MainBox(width);
            MainBox foundMainBox;
            SpecificBox FoundSpecificBox;
            double updateHeight = 0;
            bool isExists;

            while (restOfAmount != 0 && counterBoxType < 3)
            {
                
                int counterTypeOfSpecificBox = 0;
                isExists = MainTree.FindBestMatch(tmp, out foundMainBox); // check if have a suitable box
                if (!isExists) break;

                if (!CheckFoundItem(foundMainBox, width * Standarddeviation, width)) break;
                while (foundMainBox.specipicTree.Counter != counterTypeOfSpecificBox && restOfAmount != 0 && counterBoxType < 3)
                {
                    
                    counterTypeOfSpecificBox++;
                    (isExists, restOfAmount) = foundMainBox.BuyHeightBox(height, restOfAmount, out updateHeight, out FoundSpecificBox);
                    if (restOfAmount != 0) listOfRemoveExpiredSpecificBox.AddLast(FoundSpecificBox);
                    else listOfUpdateExpiredSpecificBox.AddLast(FoundSpecificBox);
                    if (isExists)
                    {
                        listOfUpdateMainBox.AddFirst(foundMainBox);
                        generalBoxes.Add(new GeneralBox(foundMainBox.Width, updateHeight, amount - restOfAmount));
                        amount = restOfAmount;
                        counterBoxType++;
                    }
                }
            }
            if (counterBoxType >= 3 && restOfAmount != 0) messageAble.isError("we sell until 3 diffrent types of boxes");
            return UpdateStorageAfterBuy(generalBoxes);

        }
        private List<GeneralBox> UpdateStorageAfterBuy(List<GeneralBox> generalBoxes)
        {
            if (generalBoxes.Count == 0)
            {
                messageAble.isError("we didnt found any suitible box ");
                return default;
            }
            if (CheckIfCustomerWantToBuy(generalBoxes))
            {
                messageAble.isSuccess("thank you :)");
                ResetLists();
            }
            else
            {
                messageAble.isSuccess("thank you :),maybe next time");
                foreach (MainBox box in listOfUpdateMainBox)
                {
                    MainTree.ChangeIsChecked(box);
                    box.ChangeIsChaceklInSpecificTree();
                }
            }
            return generalBoxes;
        }
        private void ResetLists()
        {
            if (listOfRemoveExpiredSpecificBox != null) foreach (var box in listOfRemoveExpiredSpecificBox) timeData.RemoveExpiredList(box.MyBox);
            if (listOfUpdateExpiredSpecificBox != null) foreach (var box in listOfUpdateExpiredSpecificBox) timeData.UpdateExpiredList(box.MyBox);
            if (listOfUpdateMainBox != null)
                foreach (MainBox box in listOfUpdateMainBox)
                {
                    box.ResetSpecificBoxAfterPurchase();
                    box.specipicTree.Counter--;
                    if (box.specipicTree.Counter == 0) MainTree.RemoveSingleNode(box);
                }
        }
        public void Supply(double width, double height, int amount)
        {
            SpecificBox tmpSpecificBox;
            if (width <= 0 || height <= 0 || amount <= 0)
            {
                messageAble.isError("you insert unvalid size");
                return;
            }
            MainBox tmp = new MainBox(width);
            MainBox foundBox;
            bool isFound;
            isFound = MainTree.Search(tmp, out foundBox);
            if (!isFound)
            {
                MainTree.Add(tmp);
                (tmpSpecificBox, _) = tmp.SupplySpecipicTree(height, amount);
                timeData.AddLast(tmpSpecificBox.MyBox);
            }
            else
            {
                (tmpSpecificBox, isFound) = foundBox.SupplySpecipicTree(height, amount);
                if (!isFound) timeData.AddLast(tmpSpecificBox.MyBox);
            }


        }
        public void ShowAllBoxes()
        {
            if (MainTree.Counter != 0)
            {
                foreach (var box in MainTree)
                {
                    string str = box.ShowAllBoxes();
                    messageAble.isShow(str);
                }
                return;
            }
            messageAble.isError("no boxes in the storage");
        }
        public void ShowBoxByDetails(double width, double height)
        {
            var tmp = new MainBox(width);
            MainBox foundBox;
            SpecificBox foundSpecificBox;
            bool isExised;
            isExised = MainTree.Search(tmp, out foundBox);
            if (isExised)
            {
                if (foundBox.CheckIfSpecipicTreeExised(height, out foundSpecificBox)) messageAble.isShow($"the height is:{width} \n    height is:{foundSpecificBox.Height} \n    amount is:{foundSpecificBox.Amount} ");
            }
            messageAble.isShow("the box are not found");
        }
        private bool CheckFoundItem(MainBox foundBox, double currentlyWidthBox, double width)
        {
            var currentlyBox = new MainBox(currentlyWidthBox);
            if (foundBox.CompareTo(currentlyBox) > 0 || foundBox.Width < width) return false;
            return true;
        } // the finction check if the box that found are valid
        private void CheckExpiraionBoxes(object state)
        {
            TimeData firstData;
            timeData.GetAt(0, out firstData);
            while (firstData != null && DateTime.Now - firstData.LastPurchaseOrFirstSupplyDate > TimeSpan.FromDays(expiredDate))
            {
                MainBox dummyBox = new MainBox(firstData.Width);
                MainBox foundBox;
                MainTree.Search(dummyBox, out foundBox);
                timeData.RemoveFirst();
                foundBox.RemoveUnExpiredBoxes(new SpecificBox(firstData.Height, firstData.Width, 20));
                if (foundBox.specipicTree.Counter == 0)
                {
                    MainTree.RemoveSingleNode(dummyBox);
                    MainTree.Counter--;
                }
                if (timeData.Counter == 0) break;

                timeData.GetAt(0, out firstData);
            }


        } // function that connect to the timer and check their experation
        private int CheckAmountValidition(int amount)
        {
            if (amount > MAXAMOUNT / 2)
            {
                messageAble.isError("max value for each purchase is 125");
                amount = MAXAMOUNT / 2;
                return amount;
            }
            else return amount;
        } // check if the amout that the customer inssert valid
        private bool CheckIfCustomerWantToBuy(List<GeneralBox> generalBoxes)
        {
            messageAble.isSuccess("the boxes that we found for you is:");
            foreach (GeneralBox generalBox in generalBoxes) messageAble.isShow(generalBox.ToString());

            messageAble.isShow("for purchase the boxes press 1 any buttom finish the purchase ");
            bool isWant = messageAble.isWant();
            if (isWant) return true;
            return false;
        } // send to the customer message and get answer if the customer want to buy
    }
}
