using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReedSolomon
{
    public static class MatrixExt
    {
        // метод расширения для получения количества строк матрицы
        public static int RowsCount(this int[,] matrix)
        {
            return matrix.GetUpperBound(0) + 1;
        }

        // метод расширения для получения количества столбцов матрицы
        public static int ColumnsCount(this int[,] matrix)
        {
            return matrix.GetUpperBound(1) + 1;
        }
    }
}
