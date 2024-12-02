using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorByInjection
{
    public class Division : Operator
    {
        public override int Caculate(int Number1, int Number2)
        {
            return Number1 / Number2;
        }
    }
}
