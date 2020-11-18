using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReedSolomon
{
    class Program
    {
        public static int[,] AdditionTable { get; set; } =
            new int[,] { { 0, 4, 7, 2, 6, 5, 3},
                         { 4, 0, 5, 1, 3, 7, 6},
                         { 7, 5, 0, 6, 2, 4, 1},
                         { 2, 1, 6, 0, 7, 3, 5},
                         { 6, 3, 2, 7, 0, 1, 4},
                         { 5, 7, 4, 3, 1, 0, 2},
                         { 3, 6, 1, 5, 4, 2, 0} };

        public static int[,] MultiplicationTable { get; set; } =
            new int[,] { { 1, 2, 3, 4, 5, 6, 7},
                         { 2, 3, 4, 5, 6, 7, 1},
                         { 3, 4, 5, 6, 7, 1, 2},
                         { 4, 5, 6, 7, 1, 2, 3},
                         { 5, 6, 7, 1, 2, 3, 4},
                         { 6, 7, 1, 2, 3, 4, 5},
                         { 7, 1, 2, 3, 4, 5, 6} };

        public static int[,] ReverseF { get; set; } =
            new int[,] { { 1, 1, 1, 1, 1, 1, 1},
                         { 1, 7, 6, 5, 4, 3, 2},
                         { 1, 6, 4, 2, 7, 5, 3},
                         { 1, 5, 2, 6, 3, 7, 4},
                         { 1, 4, 7, 3, 6, 2, 5},
                         { 1, 3, 5, 7, 2, 4, 6},
                         { 1, 2, 3, 4, 5, 6, 7} };

        public static int[,] HTranspose { get; set; } =
            new int[,] { { 1, 1, 1, 1 },
                         { 5, 4, 3, 2},
                         { 2, 7, 5, 3},
                         { 6, 3, 7, 4},
                         { 3, 6, 2, 5},
                         { 7, 2, 4, 6},
                         { 4, 5, 6, 7} };

        public static List<int> UsersString { get; set; } = new List<int>();
        public static int[,] SPolynomial { get; set; } = new int[1, 7];
        public static int[,] VPolynomial { get; set; } = new int[1, 7];
        public static int[,] EPolynomial { get; set; } = new int[1, 7] { { 0, 0, 0, 0, 0, 0, 0 } };

        public static int[,] DeltaValues { get; set; } =
            new int[,] { { 0, 0 },
                         { -1, 6 },
                         { -2, 5 },
                         { -3, 4 },
                         { -4, 3 },
                         { -5, 2 },
                         { -6, 1 } };

        public static int[,] SyndromeFirstElement { get; set; } = new int[3, 2];
        public static int[,] SyndromeSecondElement { get; set; } = new int[3, 2];
        public static int[,] SyndromeThirdElement { get; set; } = new int[3, 2];
        public static int[,] SyndromeFourthElement { get; set; } = new int[3, 2];


        public static List<int> ElementsOfMatrix { get; set; } = new List<int>();

        public static List<int[,]> ListOfSyndromes { get; set; }  = new List<int[,]>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter information vector: ");
                var str = Console.ReadLine();
                if(str.Length != 3)
                {
                    Console.WriteLine("Wrong vector!");
                }
                else
                {
                    SPolynomialCreation(str);
                    break;
                }
            }

            while (true)
            {

                Console.WriteLine("Enter 1 or 2 to set 1/2 errors: ");
                var numOfErrors = Convert.ToInt32(Console.ReadLine());
                if(numOfErrors != 1 && numOfErrors != 2)
                {
                    Console.WriteLine("Wrong choise!");
                }
                else
                {
                    if(numOfErrors == 1)
                    {
                        Console.WriteLine("Enter error position: ");
                        var pos = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter error: ");
                        var error = Convert.ToInt32(Console.ReadLine());

                        FixOneError(pos, error);
                    }
                    if(numOfErrors == 2)
                    {
                        Console.WriteLine("Enter error1 position: ");
                        var pos1 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter error1: ");
                        var error1 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter error2 position: ");
                        var pos2 = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter error2: ");
                        var error2 = Convert.ToInt32(Console.ReadLine());

                        FixTwoErrors(pos1, error1, pos2, error2);
                    }
                }
            }

        }


        public static int[,] SPolynomialCreation(string userStr)
        {
            StringConverter(userStr);

            for (int i = 1; i < 8; i++)
            {
                var a_1x = ProductOfNumbers(UsersString[1], i);
                var a_2x = ProductOfNumbers(UsersString[2], ProductOfNumbers(i, i));
                var sumA_0AndA_1x = SumOfNumbers(UsersString[0], a_1x);
                var resSum = SumOfNumbers(sumA_0AndA_1x, a_2x);
                SPolynomial[0, i-1] = resSum;
                //SPolynomial.Add(resSum);
            }


            return null;
        }

        public static int SumOfNumbers(int a, int b)
        {
            if(a == 0)
            {
                return b;
            }
            if( b == 0)
            {
                return a;
            }

            return AdditionTable[a - 1, b - 1];

        }

        public static int ProductOfNumbers(int a, int b)
        {
            if(a == 0 || b == 0)
            {
                return 0;
            }

            return MultiplicationTable[a - 1, b - 1];
        }

        public static void FixOneError(int position, int error)
        {
            Console.Write("S = ");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{SPolynomial[0, i]},");
            }
            Console.WriteLine("");


            VPolynomial = SPolynomial;
            var numberInPolynomialPlusError = SumOfNumbers(VPolynomial[0, position], error);
            VPolynomial[0, position] = numberInPolynomialPlusError;
            Console.Write("V = S + E\n");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{VPolynomial[0,i]},");
            }
            Console.WriteLine("");

            var vectorAEstimate = MatrixMultiplication(VPolynomial, ReverseF);
            Console.WriteLine("a` = V*F^-1");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{vectorAEstimate[0, i]},");
            }
            Console.WriteLine("");

            List<int> Syndrome = new List<int>();
            for (int i = 3; i < 7; i++)
            {
                Syndrome.Add(vectorAEstimate[0, i]);
            }

            var firstDelta = FindDelta(Syndrome)[0];//по идее все дельты для 1 ошибки одинаковые
            var errorPosition = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (DeltaValues[i, j] == firstDelta)
                    {
                        errorPosition = i + 1;
                    }
                }
            }

            var degree = 0;
            var wrongValue = 0;

            if (Syndrome[0] > HTranspose[errorPosition - 1, 0])
            {
                degree = Syndrome[0] - HTranspose[errorPosition - 1, 0];
                wrongValue = degree + 1;
            }
            else if(Syndrome[0] == HTranspose[errorPosition - 1, 0])
            {
                degree = 0;
                wrongValue = degree + 1;
            }
            else if(Syndrome[0] < HTranspose[errorPosition - 1, 0])
            {
                for (int i = 0; i < 7; i++)
                {
                    if(MultiplicationTable[HTranspose[errorPosition - 1, 0], i] == Syndrome[0])
                    {
                        degree = i;//?? может быть ошибка
                        wrongValue = degree + 1;
                    }
                }
            }

            EPolynomial[0, errorPosition - 1] = wrongValue;//ставим ошибку в вектор ошибок


            Console.Write("corrector = ");
            var corr = MatrixMultiplication(EPolynomial, ReverseF);
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{corr[0, i]},");
            }
            Console.WriteLine("");
            Console.Write("a = corrector + a` = ");
            var AResult = SumOfVectors(vectorAEstimate, corr);
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{AResult[0, i]},");
            }
            Console.WriteLine("");
        }

        public static List<int> FindDelta(List<int> syndrome)
        {
            List<int> DeltaList = new List<int>();
            List<int> TempListForFindingErrorPosition = new List<int>();
            for (int i = 0; i < syndrome.Count - 1; i++)
            {
                if(syndrome[i] > syndrome[i + 1])
                {
                    DeltaList.Add(-(syndrome[i] - syndrome[i + 1]));
                }
                else//разница может быть равна 0
                {
                    DeltaList.Add(syndrome[i + 1] - syndrome[i]);
                }
            }

            for (int a = 0; a < 3; a++)
            {

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (DeltaValues[i, j] == DeltaList[a])
                        {
                            TempListForFindingErrorPosition.Add(i + 1);
                        }
                    }
                }
            }

            
            return TempListForFindingErrorPosition;
        }

        public static void FixTwoErrors(int pos1, int error1, int pos2, int error2)
        {
            Console.Write("S = ");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{SPolynomial[0, i]},");
            }
            Console.WriteLine("");
            VPolynomial = SPolynomial;
            var firstNumberInPolynomialPlusError = SumOfNumbers(VPolynomial[0, pos1], error1);
            var secondNumberInPolynomialPlusError = SumOfNumbers(VPolynomial[0, pos2], error2);
            VPolynomial[0, pos1] = firstNumberInPolynomialPlusError;//от 0 до 6
            VPolynomial[0, pos2] = secondNumberInPolynomialPlusError;//от 0 до 6


            Console.WriteLine("V = S + E");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{VPolynomial[0, i]},");
            }
            Console.WriteLine("");

            var vectorAEstimate = MatrixMultiplication(VPolynomial, ReverseF);
            Console.WriteLine("a` = V*F^-1");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{vectorAEstimate[0, i]},");
            }
            Console.WriteLine("");


            List<int> Syndrome = new List<int>();
            for (int i = 3; i < 7; i++)
            {
                Syndrome.Add(vectorAEstimate[0, i]);
            }
            var firstDelta = FindDelta(Syndrome)[0];//строка матрица h, а не значение дельты

            SyndromeFirstElement = FindingSyndromVectors(Syndrome[0]);
            SyndromeSecondElement = FindingSyndromVectors(Syndrome[1]);
            SyndromeThirdElement = FindingSyndromVectors(Syndrome[2]);
            SyndromeFourthElement = FindingSyndromVectors(Syndrome[3]);

            ListOfSyndromes.Add(SyndromeSecondElement);
            ListOfSyndromes.Add(SyndromeThirdElement);
            ListOfSyndromes.Add(SyndromeFourthElement);

            FindingTwoErrors(vectorAEstimate);

        }

        public static int[,] FindingTwoErrors(int[,] vectorAEstimate)
        {
            var delta1 = 0;
            var delta2 = 0;
            var element1Matrix1 = 0;
            var element2Matrix1 = 0;
            var element1Matrix2 = 0;
            var element2Matrix2 = 0;
            var element1Matrix3 = 0;
            var element2Matrix3 = 0;
            var element1Matrix4 = 0;
            var element2Matrix4 = 0;

            while (ElementsOfMatrix.Count != 8)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if(j == 0)
                        {
                            if (ElementsOfMatrix.Count != 8)
                            {
                                element1Matrix1 = SyndromeFirstElement[i, j];
                                element2Matrix1 = SyndromeFirstElement[i, j + 1];

                                ElementsOfMatrix = new List<int>();

                                ElementsOfMatrix.Add(element1Matrix1);
                                ElementsOfMatrix.Add(element2Matrix1);

                                MatrixRecursion(0, delta1, delta2, 0);
                            }

                            #region
                            //for (int m = 0; m < 3; m++)//цикл по 2й матрице
                            //{
                            //    for (int n = 0; n < 2; n++)
                            //    {
                            //        var degree1 = ReverseDelta(SyndromeSecondElement[m, n] - element1Matrix1);

                            //        if (degree1 == delta1)
                            //        {
                            //            element1Matrix2 = SyndromeSecondElement[m, n];
                            //            if (n == 0)
                            //            {
                            //                var degree2 = ReverseDelta(SyndromeSecondElement[m, 1] - element2Matrix1);
                            //                if (degree2 == delta2)
                            //                {
                            //                    element2Matrix2 = SyndromeSecondElement[m, 1];
                            //                    for (int k = 0; k < 3; k++)//цикл по 3 матрице(нужна рекурсия)
                            //                    {
                            //                        for (int l = 0; l < 2; l++)
                            //                        {
                            //                            var degree12 = ReverseDelta(SyndromeThirdElement[k, l] - element1Matrix2);
                            //                            if (degree12 == delta1)
                            //                            {
                            //                                element1Matrix3 = SyndromeThirdElement[k, l];
                            //                                if(l == 0)
                            //                                {
                            //                                    var degree22 = ReverseDelta(SyndromeThirdElement[k, 1] - element2Matrix2);
                            //                                    if(degree22 == delta2)
                            //                                    {
                            //                                        element2Matrix3 = SyndromeThirdElement[k, 1];
                            //                                    }
                            //                                }
                            //                                else
                            //                                {

                            //                                }
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //            else
                            //            {
                            //                var degree2 = ReverseDelta(SyndromeSecondElement[m, 0] - element2Matrix1);
                            //                if(degree2 == delta2)
                            //                {
                            //                    element2Matrix2 = SyndromeSecondElement[m, 0];
                            //                    for (int k = 0; k < 3; k++)//цикл по 3 матрице(нужна рекурсия)
                            //                    {
                            //                        for (int l = 0; l < 2; l++)
                            //                        {
                            //                            var degree12 = ReverseDelta(SyndromeThirdElement[k, l] - element1Matrix2);
                            //                            if (degree12 == delta1)
                            //                            {
                            //                                element1Matrix3 = SyndromeThirdElement[k, l];
                            //                                if (l == 0)
                            //                                {
                            //                                    var degree22 = ReverseDelta(SyndromeThirdElement[k, 1] - element2Matrix2);
                            //                                    if (degree22 == delta2)
                            //                                    {
                            //                                        element2Matrix3 = SyndromeThirdElement[k, 1];
                            //                                    }
                            //                                }
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }


                            //    }
                            //}
                            #endregion
                        }
                        else
                        {
                            if(ElementsOfMatrix.Count != 8)
                            {
                                element1Matrix1 = SyndromeFirstElement[i, j];
                                element2Matrix1 = SyndromeFirstElement[i, j - 1];

                                ElementsOfMatrix = new List<int>();

                                ElementsOfMatrix.Add(element1Matrix1);
                                ElementsOfMatrix.Add(element2Matrix1);

                                MatrixRecursion(0, delta1, delta2, 0);
                            }
                            

                        }


                        #region Comment
                        //if (j == 0)
                        //{
                        //    element1Matrix1 = SyndromeFirstElement[i, j];
                        //    element2Matrix1 = SyndromeFirstElement[i, j + 1];

                        //    for (int m = 0; m < 3; m++)//цикл по 2й матрице
                        //    {
                        //        var degree1 = SumOfNumbers(SyndromeSecondElement[m, 0], element1Matrix1);
                        //        if(degree1 == delta1)//если степени между первыми элементами 1 и 2 матрицы совпадают
                        //        {
                        //            element1Matrix2 = SyndromeSecondElement[m, 0];
                        //            var degree2 = SumOfNumbers(SyndromeSecondElement[m, 1], element2Matrix1);
                        //            if(degree2 == delta2)//если степени между вторыми элементами 1 и 2 матрицы совпадают
                        //            {
                        //                element2Matrix2 = SyndromeSecondElement[m, 1];
                        //                for (int n = 0; n < 3; n++)
                        //                {
                        //                    var degree12 = SumOfNumbers(SyndromeThirdElement[n, 0], element1Matrix2);//3 матрица
                        //                    if(degree12 == delta1)
                        //                    {
                        //                        element1Matrix3 = SyndromeThirdElement[n, 0];
                        //                        var degree22 = SumOfNumbers(SyndromeThirdElement[n, 1], element2Matrix2);
                        //                        if(degree22 -1 == delta2)///////////////////////////////////////////////
                        //                        {
                        //                            element2Matrix3 = SyndromeThirdElement[n, 1];
                        //                            for (int k = 0; k < 3; k++)
                        //                            {
                        //                                var degree13 = SumOfNumbers(SyndromeFourthElement[k, 0], element1Matrix3);
                        //                                if(degree13 == delta1)
                        //                                {
                        //                                    element1Matrix4 = SyndromeFourthElement[k, 0];
                        //                                    var degree23 = SumOfNumbers(SyndromeFourthElement[k, 1], element2Matrix3);
                        //                                    if(degree23 == delta2)
                        //                                    {
                        //                                        element2Matrix4 = SyndromeFourthElement[k, 1];
                        //                                        break;
                        //                                    }
                        //                                }
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    element1Matrix1 = SyndromeFirstElement[i, j];
                        //    element2Matrix1 = SyndromeFirstElement[i, j - 1];

                        //    for (int m = 0; m < 3; m++)
                        //    {
                        //        var degree1 = SumOfNumbers(SyndromeSecondElement[m, 1], element1Matrix1);
                        //        if (degree1 == delta1)//если степени между первыми элементами 1 и 2 матрицы совпадают
                        //        {
                        //            element1Matrix2 = SyndromeSecondElement[m, 1];
                        //            var degree2 = SumOfNumbers(SyndromeSecondElement[m, 0], element2Matrix1);
                        //            if (degree2 == delta2)//если степени между вторыми элементами 1 и 2 матрицы совпадают
                        //            {
                        //                element2Matrix2 = SyndromeSecondElement[m, 0];
                        //                for (int n = 0; n < 3; n++)
                        //                {
                        //                    var degree12 = SumOfNumbers(SyndromeThirdElement[n, 1], element1Matrix2);//3 матрица
                        //                    if (degree12 == delta1)
                        //                    {
                        //                        element1Matrix3 = SyndromeThirdElement[n, 1];
                        //                        var degree22 = SumOfNumbers(SyndromeThirdElement[n, 0], element2Matrix2);
                        //                        if (degree22 == delta2)
                        //                        {
                        //                            element2Matrix3 = SyndromeThirdElement[n, 0];
                        //                            for (int k = 0; k < 3; k++)
                        //                            {
                        //                                var degree13 = SumOfNumbers(SyndromeFourthElement[k, 1], element1Matrix3);
                        //                                if (degree13 == delta1)
                        //                                {
                        //                                    element1Matrix4 = SyndromeFourthElement[k, 1];
                        //                                    var degree23 = SumOfNumbers(SyndromeFourthElement[k, 0], element2Matrix3);
                        //                                    if (degree23 == delta2)
                        //                                    {
                        //                                        element2Matrix4 = SyndromeFourthElement[k, 0];
                        //                                        break;
                        //                                    }
                        //                                }
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion


                    }
                }
                if (delta2 < 7)
                {
                    delta2++;
                }
                else
                {
                    delta1++;
                    delta2 = 0;
                }
            }

            int[,] EVektor1 = new[,] { { ElementsOfMatrix[0], ElementsOfMatrix[2], ElementsOfMatrix[4], ElementsOfMatrix[6] } };
            int[,] EVektor2 = new[,] { { ElementsOfMatrix[1], ElementsOfMatrix[3], ElementsOfMatrix[5], ElementsOfMatrix[7] } };

            var differenceOf1Degrees = ElementsOfMatrix[2] - ElementsOfMatrix[0];//разница между найденными значениями степеней
            var differenceOf2Degrees = ElementsOfMatrix[3] - ElementsOfMatrix[1];

            var error1Position = 0;//позиция в проверочной матрице, то есть номер строки от единицы, не от нуля
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (DeltaValues[i, j] == differenceOf1Degrees)
                    {
                        error1Position = i + 1;
                    }
                }
            }
            var error2Position = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (DeltaValues[i, j] == differenceOf2Degrees)
                    {
                        error2Position = i + 1;
                    }
                }
            }

            var degree1 = 0;
            var wrongValue1 = 0;

            if (ElementsOfMatrix[0] > HTranspose[error1Position - 1, 0])
            {
                degree1 = ElementsOfMatrix[0] - HTranspose[error1Position - 1, 0];
                wrongValue1 = degree1 + 1;
            }
            else if (ElementsOfMatrix[0] == HTranspose[error1Position - 1, 0])
            {
                degree1 = 0;
                wrongValue1 = degree1 + 1;
            }
            else if (ElementsOfMatrix[0] < HTranspose[error1Position - 1, 0])
            {
                for (int i = 0; i < 7; i++)
                {
                    if (MultiplicationTable[HTranspose[error1Position - 1, 0], i] == ElementsOfMatrix[0])
                    {
                        degree1 = i;//?? может быть ошибка
                        wrongValue1 = degree1 + 1;
                    }
                }
            }

            var degree2 = 0;
            var wrongValue2 = 0;

            if (ElementsOfMatrix[1] > HTranspose[error2Position - 1, 0])
            {
                degree2 = ElementsOfMatrix[1] - HTranspose[error2Position - 1, 0];
                wrongValue2 = degree2 + 1;
            }
            else if (ElementsOfMatrix[1] == HTranspose[error2Position - 1, 0])
            {
                degree2 = 0;
                wrongValue2 = degree2 + 1;
            }
            else if (ElementsOfMatrix[1] < HTranspose[error2Position - 1, 0])
            {
                for (int i = 0; i < 7; i++)
                {
                    if (MultiplicationTable[HTranspose[error2Position - 1, 0], i] == ElementsOfMatrix[1])
                    {
                        degree2 = i;//?? может быть ошибка
                        wrongValue2 = degree2 + 1;
                    }
                }
            }

            EPolynomial[0, error1Position - 1] = wrongValue1;//ставим ошибку в вектор ошибок
            EPolynomial[0, error2Position - 1] = wrongValue2;

            var corr = MatrixMultiplication(EPolynomial, ReverseF);


            Console.Write("corrector = E*F^-1");
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{corr[0, i]},");
            }
            Console.WriteLine("");

            Console.Write("a = corr + a` = ");
            var AResult = SumOfVectors(vectorAEstimate, corr);
            for (int i = 0; i < 7; i++)
            {
                Console.Write($"{AResult[0, i]},");
            }
            Console.WriteLine("");




            return null;

        }


        public static void MatrixRecursion(int posOfFirstElInElOfMatrix, int delta1, int delta2, int numberOfSyndromsList)
        {
            for (int m = 0; m < 3; m++)//цикл по 2й матрице
            {
                for (int n = 0; n < 2; n++)
                {
                    var degree1 = ReverseDelta(ListOfSyndromes[numberOfSyndromsList][m,n] - ElementsOfMatrix[posOfFirstElInElOfMatrix]);

                    if (degree1 == delta1)
                    {
                        ElementsOfMatrix.Add(ListOfSyndromes[numberOfSyndromsList][m, n]); //el1matr2
                        if (n == 0)
                        {
                            var degree2 = ReverseDelta(ListOfSyndromes[numberOfSyndromsList][m, 1] - ElementsOfMatrix[posOfFirstElInElOfMatrix + 1]);
                            if (degree2 == delta2)
                            {
                                ElementsOfMatrix.Add(ListOfSyndromes[numberOfSyndromsList][m, 1]);
                                if(ElementsOfMatrix.Count != 8)
                                {
                                    MatrixRecursion((ElementsOfMatrix.Count() - 2), delta1, delta2, numberOfSyndromsList + 1);
                                }
                            }
                            else
                            {
                                ElementsOfMatrix.RemoveAt(ElementsOfMatrix.Count() - 1);
                            }
                        }
                        else
                        {
                            var degree2 = ReverseDelta(ListOfSyndromes[numberOfSyndromsList][m, 0] - ElementsOfMatrix[posOfFirstElInElOfMatrix + 1]);
                            if (degree2 == delta2)
                            {
                                ElementsOfMatrix.Add(ListOfSyndromes[numberOfSyndromsList][m, 0]);
                                if (ElementsOfMatrix.Count != 8)
                                {
                                    MatrixRecursion((ElementsOfMatrix.Count() - 2), delta1, delta2, numberOfSyndromsList + 1);
                                }
                            }
                            else
                            {
                                ElementsOfMatrix.RemoveAt(ElementsOfMatrix.Count() - 1);
                            }
                        }
                    }


                }
            }
        }

        public static int ReverseDelta(int delta)
        {
            var reverseDelta = 0;
            if (delta < 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if(DeltaValues[i,j] == delta)
                        {
                            reverseDelta = DeltaValues[i, j + 1];
                        }
                    }
                }
                return reverseDelta;
            }
            else
            {
                return delta;
            }
        }

        public static int[,] FindingSyndromVectors(int syndromeElement)
        {
            List<int> syndromeElementsList = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    if(AdditionTable[i,j] == syndromeElement)
                    {
                        syndromeElementsList.Add(i + 1);
                        syndromeElementsList.Add(j + 1);
                    }
                }
            }

            int[,] syndromeArray = new int[3, 2];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    syndromeArray[i, j] = syndromeElementsList[0];
                    syndromeElementsList.RemoveAt(0);
                }
            }
            return syndromeArray;

        }

        public static int[,] SumOfVectors(int[,] vectorA, int[,] vectorB)
        {
            var A = new int[1, 7];
            for (int i = 0; i < 7; i++)
            {
                A[0, i] = SumOfNumbers(vectorA[0, i], vectorB[0, i]);
            }
            return A;
        }

        public static void StringConverter(string str)
        {
            var strArray = str.ToCharArray();
            foreach (var item in strArray)
            {
                UsersString.Add(Convert.ToInt32(item.ToString()));
            }
        }

        public static int[,] MatrixMultiplication(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
            {
                throw new Exception("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }

            var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < matrixA.ColumnsCount(); k++)
                    {
                        var resOfMultipl = ProductOfNumbers(matrixA[i, k], matrixB[k, j]);
                        matrixC[i, j] = SumOfNumbers(matrixC[i, j], resOfMultipl);
                    }
                }
            }

            return matrixC;
        }
    }
}



//while (ElementsOfMatrix.Count != 8)
//{
//    for (int i = 0; i < 3; i++)
//    {
//        for (int j = 0; j < 2; j++)
//        {
//            if (j == 0)
//            {
//                element1Matrix1 = SyndromeFirstElement[i, j];
//                element2Matrix1 = SyndromeFirstElement[i, j + 1];

//                ElementsOfMatrix = new List<int>();

//                ElementsOfMatrix.Add(element1Matrix1);
//                ElementsOfMatrix.Add(element2Matrix1);

//                MatrixRecursion(0, delta1, delta2);

//                //for (int m = 0; m < 3; m++)//цикл по 2й матрице
//                //{
//                //    for (int n = 0; n < 2; n++)
//                //    {
//                //        var degree1 = ReverseDelta(SyndromeSecondElement[m, n] - element1Matrix1);

//                //        if (degree1 == delta1)
//                //        {
//                //            element1Matrix2 = SyndromeSecondElement[m, n];
//                //            if (n == 0)
//                //            {
//                //                var degree2 = ReverseDelta(SyndromeSecondElement[m, 1] - element2Matrix1);
//                //                if (degree2 == delta2)
//                //                {
//                //                    element2Matrix2 = SyndromeSecondElement[m, 1];
//                //                    for (int k = 0; k < 3; k++)//цикл по 3 матрице(нужна рекурсия)
//                //                    {
//                //                        for (int l = 0; l < 2; l++)
//                //                        {
//                //                            var degree12 = ReverseDelta(SyndromeThirdElement[k, l] - element1Matrix2);
//                //                            if (degree12 == delta1)
//                //                            {
//                //                                element1Matrix3 = SyndromeThirdElement[k, l];
//                //                                if(l == 0)
//                //                                {
//                //                                    var degree22 = ReverseDelta(SyndromeThirdElement[k, 1] - element2Matrix2);
//                //                                    if(degree22 == delta2)
//                //                                    {
//                //                                        element2Matrix3 = SyndromeThirdElement[k, 1];
//                //                                    }
//                //                                }
//                //                                else
//                //                                {

//                //                                }
//                //                            }
//                //                        }
//                //                    }
//                //                }
//                //            }
//                //            else
//                //            {
//                //                var degree2 = ReverseDelta(SyndromeSecondElement[m, 0] - element2Matrix1);
//                //                if(degree2 == delta2)
//                //                {
//                //                    element2Matrix2 = SyndromeSecondElement[m, 0];
//                //                    for (int k = 0; k < 3; k++)//цикл по 3 матрице(нужна рекурсия)
//                //                    {
//                //                        for (int l = 0; l < 2; l++)
//                //                        {
//                //                            var degree12 = ReverseDelta(SyndromeThirdElement[k, l] - element1Matrix2);
//                //                            if (degree12 == delta1)
//                //                            {
//                //                                element1Matrix3 = SyndromeThirdElement[k, l];
//                //                                if (l == 0)
//                //                                {
//                //                                    var degree22 = ReverseDelta(SyndromeThirdElement[k, 1] - element2Matrix2);
//                //                                    if (degree22 == delta2)
//                //                                    {
//                //                                        element2Matrix3 = SyndromeThirdElement[k, 1];
//                //                                    }
//                //                                }
//                //                            }
//                //                        }
//                //                    }
//                //                }
//                //            }
//                //        }


//                //    }
//                //}
//            }
//            else
//            {
//                element1Matrix1 = SyndromeFirstElement[i, j];
//                element2Matrix1 = SyndromeFirstElement[i, j - 1];

//                ElementsOfMatrix = new List<int>();

//                ElementsOfMatrix.Add(element1Matrix1);
//                ElementsOfMatrix.Add(element2Matrix1);

//                MatrixRecursion(0, delta1, delta2);

//            }


//            #region Comment
//            //if (j == 0)
//            //{
//            //    element1Matrix1 = SyndromeFirstElement[i, j];
//            //    element2Matrix1 = SyndromeFirstElement[i, j + 1];

//            //    for (int m = 0; m < 3; m++)//цикл по 2й матрице
//            //    {
//            //        var degree1 = SumOfNumbers(SyndromeSecondElement[m, 0], element1Matrix1);
//            //        if(degree1 == delta1)//если степени между первыми элементами 1 и 2 матрицы совпадают
//            //        {
//            //            element1Matrix2 = SyndromeSecondElement[m, 0];
//            //            var degree2 = SumOfNumbers(SyndromeSecondElement[m, 1], element2Matrix1);
//            //            if(degree2 == delta2)//если степени между вторыми элементами 1 и 2 матрицы совпадают
//            //            {
//            //                element2Matrix2 = SyndromeSecondElement[m, 1];
//            //                for (int n = 0; n < 3; n++)
//            //                {
//            //                    var degree12 = SumOfNumbers(SyndromeThirdElement[n, 0], element1Matrix2);//3 матрица
//            //                    if(degree12 == delta1)
//            //                    {
//            //                        element1Matrix3 = SyndromeThirdElement[n, 0];
//            //                        var degree22 = SumOfNumbers(SyndromeThirdElement[n, 1], element2Matrix2);
//            //                        if(degree22 -1 == delta2)///////////////////////////////////////////////
//            //                        {
//            //                            element2Matrix3 = SyndromeThirdElement[n, 1];
//            //                            for (int k = 0; k < 3; k++)
//            //                            {
//            //                                var degree13 = SumOfNumbers(SyndromeFourthElement[k, 0], element1Matrix3);
//            //                                if(degree13 == delta1)
//            //                                {
//            //                                    element1Matrix4 = SyndromeFourthElement[k, 0];
//            //                                    var degree23 = SumOfNumbers(SyndromeFourthElement[k, 1], element2Matrix3);
//            //                                    if(degree23 == delta2)
//            //                                    {
//            //                                        element2Matrix4 = SyndromeFourthElement[k, 1];
//            //                                        break;
//            //                                    }
//            //                                }
//            //                            }
//            //                        }
//            //                    }
//            //                }
//            //            }
//            //        }
//            //    }
//            //}
//            //else
//            //{
//            //    element1Matrix1 = SyndromeFirstElement[i, j];
//            //    element2Matrix1 = SyndromeFirstElement[i, j - 1];

//            //    for (int m = 0; m < 3; m++)
//            //    {
//            //        var degree1 = SumOfNumbers(SyndromeSecondElement[m, 1], element1Matrix1);
//            //        if (degree1 == delta1)//если степени между первыми элементами 1 и 2 матрицы совпадают
//            //        {
//            //            element1Matrix2 = SyndromeSecondElement[m, 1];
//            //            var degree2 = SumOfNumbers(SyndromeSecondElement[m, 0], element2Matrix1);
//            //            if (degree2 == delta2)//если степени между вторыми элементами 1 и 2 матрицы совпадают
//            //            {
//            //                element2Matrix2 = SyndromeSecondElement[m, 0];
//            //                for (int n = 0; n < 3; n++)
//            //                {
//            //                    var degree12 = SumOfNumbers(SyndromeThirdElement[n, 1], element1Matrix2);//3 матрица
//            //                    if (degree12 == delta1)
//            //                    {
//            //                        element1Matrix3 = SyndromeThirdElement[n, 1];
//            //                        var degree22 = SumOfNumbers(SyndromeThirdElement[n, 0], element2Matrix2);
//            //                        if (degree22 == delta2)
//            //                        {
//            //                            element2Matrix3 = SyndromeThirdElement[n, 0];
//            //                            for (int k = 0; k < 3; k++)
//            //                            {
//            //                                var degree13 = SumOfNumbers(SyndromeFourthElement[k, 1], element1Matrix3);
//            //                                if (degree13 == delta1)
//            //                                {
//            //                                    element1Matrix4 = SyndromeFourthElement[k, 1];
//            //                                    var degree23 = SumOfNumbers(SyndromeFourthElement[k, 0], element2Matrix3);
//            //                                    if (degree23 == delta2)
//            //                                    {
//            //                                        element2Matrix4 = SyndromeFourthElement[k, 0];
//            //                                        break;
//            //                                    }
//            //                                }
//            //                            }
//            //                        }
//            //                    }
//            //                }
//            //            }
//            //        }
//            //    }
//            //}
//            #endregion


//        }
//    }
//    if (delta2 < 7)
//    {
//        delta2++;
//    }
//    else
//    {
//        delta1++;
//        delta2 = 0;
//    }
//}

//int[,] EVektor1 = new[,] { { element1Matrix1, element1Matrix2, element1Matrix3, element1Matrix4 } };
//int[,] EVektor2 = new[,] { { element2Matrix1, element2Matrix2, element2Matrix3, element2Matrix4 } };


//return null;

//        }


//        public static void MatrixRecursion(int posOfFirstElInElOfMatrix, int delta1, int delta2)
//{
//    for (int m = 0; m < 3; m++)//цикл по 2й матрице
//    {
//        for (int n = 0; n < 2; n++)
//        {
//            var degree1 = ReverseDelta(SyndromeSecondElement[m, n] - ElementsOfMatrix[posOfFirstElInElOfMatrix]);

//            if (degree1 == delta1)
//            {
//                ElementsOfMatrix.Add(SyndromeSecondElement[m, n]); //el1matr2
//                if (n == 0)
//                {
//                    var degree2 = ReverseDelta(SyndromeSecondElement[m, 1] - ElementsOfMatrix[posOfFirstElInElOfMatrix + 1]);
//                    if (degree2 == delta2)
//                    {
//                        ElementsOfMatrix.Add(SyndromeSecondElement[m, 1]);
//                        if (ElementsOfMatrix.Count != 8)
//                        {
//                            MatrixRecursion((ElementsOfMatrix.Count() - 2), delta1, delta2);
//                        }
//                    }
//                }
//                else
//                {
//                    var degree2 = ReverseDelta(SyndromeSecondElement[m, 0] - ElementsOfMatrix[posOfFirstElInElOfMatrix + 1]);
//                    if (degree2 == delta2)
//                    {
//                        ElementsOfMatrix.Add(SyndromeSecondElement[m, 0]);
//                        if (ElementsOfMatrix.Count != 8)
//                        {
//                            MatrixRecursion((ElementsOfMatrix.Count() - 2), delta1, delta2);
//                        }
//                    }
//                }
//            }


//        }
//    }
//}