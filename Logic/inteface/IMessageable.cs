using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IMessageable
    {
        void isError(string message);
        void isSuccess(string message);
        void isShow(string message);
        bool isWant();

    }
}
