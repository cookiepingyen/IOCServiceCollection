using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorByInjection
{
    public abstract class Operator
    {
        public abstract int Caculate(int Number1, int Number2);
    }
}
