using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_1_Server.ViewModel
{
    public class NumberVM
    {
        private int Number;
        public NumberVM(int number)
        {
            
            if (number == 0)
            {
                this.Number = 1;
            }
            this.Number = 0;
        }
    }
}
