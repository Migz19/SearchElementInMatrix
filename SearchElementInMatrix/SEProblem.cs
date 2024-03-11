using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "SearchElementInMatrix"; } }

        public override void TryMyCode()
        {
            /* WRITE 4~6 DIFFERENT CASES FOR TRACE, EACH WITH
             * 1) SMALL INPUT SIZE
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT FROM THE FUNCTION
             * 4) PRINT THE CASE 
             */
            // Case 1: Element in the middle of the matrix
            int[,] matrix1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int item1 = 0;
            int expectedRow1 = -1;
            int expectedCol1 = -1;
            int outputRow1 = -1;
            int outputCol1 = -1;
            PROBLEM_CLASS.RequiredFunction(matrix1, item1, ref outputRow1, ref outputCol1);
            PrintCase(matrix1, item1, outputRow1, outputCol1, expectedRow1, expectedCol1);

            // Case 2: Element is the last in the matrix
            int[,] matrix2 = { { -20, 20 }, { 30, 40 } };
            int item2 = 50;
            int expectedRow2 = -1;
            int expectedCol2 = -1;
            int outputRow2 = -1;
            int outputCol2 = -1;
            PROBLEM_CLASS.RequiredFunction(matrix2, item2, ref outputRow2, ref outputCol2);
            PrintCase(matrix2, item2, outputRow2, outputCol2, expectedRow2, expectedCol2);

            // Case 3: Element is the first in the matrix
            int[,] matrix3 = { { 5, 10, 15 }, { 20, 25, 30 } };
            int item3 = 5;
            int expectedRow3 = 0;
            int expectedCol3 = 0;
            int outputRow3 = -1;
            int outputCol3 = -1;
            PROBLEM_CLASS.RequiredFunction(matrix3, item3, ref outputRow3, ref outputCol3);
            PrintCase(matrix3, item3, outputRow3, outputCol3, expectedRow3, expectedCol3);

            // Case 4: Element does not exist in the matrix
            int[,] matrix4 = { { 2, 4, 6 }, { 8, 10, 12 } };
            int item4 = 7;
            int expectedRow4 = -1;
            int expectedCol4 = -1;
            int outputRow4 = -1;
            int outputCol4 = -1;
            PROBLEM_CLASS.RequiredFunction(matrix4, item4, ref outputRow4, ref outputCol4);
            PrintCase(matrix4, item4, outputRow4, outputCol4, expectedRow4, expectedCol4);

            // Case 5: Large matrix, element in the bottom row
            int[,] matrix5 = { { 1, 3, 5, 7 }, { 9, 11, 13, 15 }, { 17, 19, 21, 23 }, { 25, 27, 29, 31 } };
            int item5 = 29;
            int expectedRow5 = 3;
            int expectedCol5 = 2;
            int outputRow5 = -1;
            int outputCol5 = -1;
            PROBLEM_CLASS.RequiredFunction(matrix5, item5, ref outputRow5, ref outputCol5);
            PrintCase(matrix5, item5, outputRow5, outputCol5, expectedRow5, expectedCol5);

        }



        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int[,] matrix = null;
            int item, expectedRow, expectedCol;
            int outputRow = -1, outputCol = -1;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
            bool readTimeFromFile = timeOutInMillisec == -1;

            for (int i = 0; i < totalCases; i++)
            {
                int rows = br.ReadInt32();
                int cols = br.ReadInt32();
                matrix = new int[rows, cols];
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        matrix[row, col] = br.ReadInt32();
                    }
                }
                item = br.ReadInt32();
                expectedRow = br.ReadInt32();
                expectedCol = br.ReadInt32();

                int testCaseTimeOut = timeOutInMillisec;
                if (readTimeFromFile)
                {
                    testCaseTimeOut = br.ReadInt32();
                }
                /*LARGE TIMEOUT FOR SAMPLE CASES TO ENSURE CORRECTNESS ONLY*/
                if (level == HardniessLevel.Easy)
                {
                    testCaseTimeOut = 1000; // Adjust according to your need for Easy level
                }
                bool caseTimedOut = false;
                bool caseException = false;

                Thread tstCaseThr = new Thread(() =>
                {
                    try
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        PROBLEM_CLASS.RequiredFunction(matrix, item, ref outputRow, ref outputCol);
                        sw.Stop();
                        //Console.WriteLine($"actual time = {sw.ElapsedMilliseconds}, testCaseTimeOut = {testCaseTimeOut}");

                        if (sw.ElapsedMilliseconds > testCaseTimeOut)
                        {
                            caseTimedOut = true;
                        }
                    }
                    catch
                    {
                        caseException = true;
                    }
                });

                tstCaseThr.Start();
                if (!tstCaseThr.Join(testCaseTimeOut))
                {
                    caseTimedOut = true;
                    tstCaseThr.Abort();
                }
                //Console.WriteLine($"Answer in Case {i + 1}. Your answer = ({outputRow}, {outputCol}), Correct answer = ({expectedRow}, {expectedCol})");

                if (caseTimedOut)
                {
                    Console.WriteLine($"Time Limit Exceeded in Case {i + 1}.");
                    timeLimitCases++;
                }
                else if (caseException)
                {
                    Console.WriteLine($"Exception in Case {i + 1}.");
                    wrongCases++;
                }
                else if (outputRow == expectedRow && outputCol == expectedCol)
                {
                    Console.WriteLine($"Test Case {i + 1} Passed!");
                    correctCases++;
                }
                else
                {
                    Console.WriteLine($"Wrong Answer in Case {i + 1}. Your answer = ({outputRow}, {outputCol}), Correct answer = ({expectedRow}, {expectedCol})");
                    wrongCases++;
                }

                // Reset outputRow and outputCol for the next test case
                outputRow = -1;
                outputCol = -1;
            }

            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine($"# correct = {correctCases}");
            Console.WriteLine($"# time limit = {timeLimitCases}");
            Console.WriteLine($"# wrong = {wrongCases}");
            Console.WriteLine($"\nFINAL EVALUATION (%) = {Math.Round((float)correctCases / totalCases * 100, 0)}");
        }



        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        /// <summary>
        /// Generate a file of test cases according to the specified params
        /// </summary>
        /// <param name="level">Easy or Hard</param>
        /// <param name="numOfCases">Required number of cases</param>
        /// <param name="includeTimeInFile">specify whether to include the expected time for each case in the file or not</param>
        /// <param name="timeFactor">factor to be multiplied by the actual time</param>
        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Helper Methods
        private static void PrintCase(int[,] matrix, int item, int outputRow, int outputCol, int expectedRow, int expectedCol)
        {
            Console.WriteLine("Input Matrix:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Search Item: {item}");
            Console.WriteLine("Expected Output: Row " + expectedRow + ", Col " + expectedCol);
            Console.WriteLine("Returned Output: Row " + outputRow + ", Col " + outputCol);

            // Determine correctness
            string result = (outputRow == expectedRow && outputCol == expectedCol) ? "CORRECT" : "WRONG";
            Console.WriteLine(result);
            Console.WriteLine("-----------------------------------------");
        }

        #endregion

    }
}
