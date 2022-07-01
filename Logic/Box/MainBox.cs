using DataStracture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Logic
{
    public class MainBox : IComparable<MainBox>
    {
        int amountOfBoxAfterBuy;
        internal double Width { get; private set; }
        LinkedList1060<SpecificBox> listOfRemoveBoxes;
        LinkedList1060<SpecificBox> listOfUpdateBoxes;
        internal BST<SpecificBox> specipicTree { get; set; }
        public MainBox(double width)
        {
            this.Width = width;
            specipicTree = new BST<SpecificBox>();
            listOfRemoveBoxes = new LinkedList1060<SpecificBox>();
            listOfUpdateBoxes = new LinkedList1060<SpecificBox>();
        }
        internal void ChangeIsChaceklInSpecificTree()
        {
            foreach (SpecificBox box in listOfRemoveBoxes) specipicTree.ChangeIsChecked(box);
            foreach (SpecificBox box in listOfUpdateBoxes) specipicTree.ChangeIsChecked(box);
        }  // set true on the isCheck after the customer refuse to the offer
        internal (SpecificBox, bool) SupplySpecipicTree(double height, int amount) // function supply to the specific tree
        {
            var tmp = new SpecificBox(height, Width, amount);
            SpecificBox foundbBox;
            bool isExised;
            isExised = specipicTree.Search(tmp, out foundbBox);
            if (isExised)
            {
                foundbBox.UpdateExisedBox(amount);
                return (foundbBox, true);
            }
            specipicTree.Add(tmp);
            return (tmp, false);
        }
        internal (bool, int) BuyHeightBox(double height, int amount, out double updateHeight, out SpecificBox specificBox)
        {
            var tmp = new SpecificBox(height, Width, amount);
            SpecificBox foundBox;
            bool isExists;
            isExists = specipicTree.FindBestMatch(tmp, out foundBox);
            if (!isExists)
            {
                specificBox = null;
                updateHeight = default;
                return (false, amount);
            }
            if (!CheckFoundItem(foundBox, height * 1.3, amount, height))
            {
                specificBox = null;
                updateHeight = default;
                return (false, amount);
            }
            foundBox.UpdateBoxAfterBuy(amount, out amountOfBoxAfterBuy);
            if (amountOfBoxAfterBuy == 0)
            {
                specificBox = foundBox;
                updateHeight = foundBox.Height;
                listOfUpdateBoxes.AddFirst(new SpecificBox(foundBox.Height, Width, foundBox.Amount - amount));
                return (true, 0);
            }
            specificBox = foundBox;
            updateHeight = foundBox.Height;
            listOfRemoveBoxes.AddFirst(foundBox);
            return (true, (amountOfBoxAfterBuy));
        }
        internal void ResetSpecificBoxAfterPurchase()
        {
            foreach (var box in listOfRemoveBoxes)
            {
                specipicTree.RemoveSingleNode(box);
            }

            SpecificBox foundBox;
            foreach (var box in listOfUpdateBoxes)
            {
                specipicTree.Search(box, out foundBox);
                foundBox.Amount = box.Amount;
                box.UpdateTimeDataAfterBuy();
            }
        }  // update the boxes after buy (remove and update)
        private bool CheckFoundItem(SpecificBox foundBox, double currentlyHeightBox, int amount, double height)
        {
            var currentlyBox = new SpecificBox(currentlyHeightBox, Width, amount);
            if (foundBox.CompareTo(currentlyBox) > 0 || height > foundBox.Height) return false;
            return true;
        }// the finction check if the box that found are valid
        internal bool CheckIfSpecipicTreeExised(double height, out SpecificBox foundBoxDetails)
        {
            var tmp = new SpecificBox(height, Width, 0);
            SpecificBox foundBox;
            bool isExised;
            isExised = specipicTree.Search(tmp, out foundBox);
            if (isExised)
            {
                foundBoxDetails = foundBox;
                return true;
            }
            foundBoxDetails = default;
            return false;
        } // return to the show function the height in order to show him
        internal void RemoveUnExpiredBoxes(SpecificBox specificBox)
        {
            specipicTree.RemoveSingleNode(specificBox);
            specipicTree.Counter--;
        }
        // ffunction that remove the expired box , The TIMMER start him each 24 hours.
        internal string ShowAllBoxes()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SpecificBox box in specipicTree)
            {
                sb.AppendLine($"the width is:{Width} \n    height is:{box.Height} \n    amount is:{box.Amount} ");
            }
            return sb.ToString();
        }
        public int CompareTo(MainBox obj) => Width.CompareTo(obj.Width);
        public override string ToString() => Width.ToString();
    }
}