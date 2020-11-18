using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReedSolomon
{
    class KeyHMatrixHelper
    {
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }

        public KeyHMatrixHelper(int firstValue, int secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
        }
    }
}
