using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Dependencies
{
    public interface IToastDisplay
    {
        void SoftNotify(string Notify);//Show toast
    }
}
