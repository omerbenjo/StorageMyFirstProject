using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GeneralBox
    {
        double width;
        double height;
        int amount;
        public GeneralBox(double width, double height, int amount)
        {
            this.width = width;
            this.height = height;
            this.amount = amount;
        }
        public override string ToString() => $"the width is {width} the height is {height} and {amount} pieces";
    }
}
