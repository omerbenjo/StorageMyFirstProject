using System;
using System.Collections;
using System.Collections.Generic;
using DataStracture;

namespace Logic
{
    internal class SpecificBox : IComparable<SpecificBox>
    {
        internal double Height { get; private set; }
        double width;
        private DoublyLinkedList<TimeData>.Node myBox;
        public DoublyLinkedList<TimeData>.Node MyBox
        {
            get { return myBox; }
            set { myBox = value; }
        }
        internal int Amount { get; set; }
        const int MAXAMOUNT = 250;
        public SpecificBox(double height, double width, int amount)
        {
            Height = height;
            this.width = width;
            Amount = amount;
            MyBox = new DoublyLinkedList<TimeData>.Node(new TimeData(width, height, DateTime.Now));
        }
        internal void UpdateExisedBox(int amount)
        {
            if (Amount + amount <= MAXAMOUNT)
            {
                Amount += amount;
                return;
            }
            int restAmount = amount - (MAXAMOUNT - Amount);
            string restBoxes = restAmount.ToString();
            Amount = MAXAMOUNT;
        }
        internal void UpdateBoxAfterBuy(int amount, out int restBox)
        {
            int tmpAmount = Amount;
            if (tmpAmount <= amount)
            {
                restBox = amount - tmpAmount;
                return;
            }
            restBox = 0;
        }
        internal void UpdateTimeDataAfterBuy() => myBox.data.LastPurchaseOrFirstSupplyDate = DateTime.Now;
        public int CompareTo(SpecificBox other) => Height.CompareTo(other.Height);


    }
}