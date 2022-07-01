using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TimeData : IComparable<TimeData>
    {
        public DateTime LastPurchaseOrFirstSupplyDate { get; set; }
        double width;
        internal double Width => width;
        double height;
        internal double Height => height;
        public TimeData(double width, double height, DateTime lastPurchaseOrFirstSupplyDate)
        {
            this.width = width;
            this.height = height;
            LastPurchaseOrFirstSupplyDate = lastPurchaseOrFirstSupplyDate;
        }
        public int CompareTo(TimeData other) => LastPurchaseOrFirstSupplyDate.CompareTo(other.LastPurchaseOrFirstSupplyDate);
    }
}
