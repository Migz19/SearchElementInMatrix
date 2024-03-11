using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PROBLEM_CLASS
    {
      
        public static void RequiredFunction(int[,] matrix,
            int item, ref int rowIndex, ref int colIndex)
        {
            

            rowIndex = -1;
            colIndex = -1;
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);
            int low = 0, heigh = numRows - 1;
            if(inValidK(matrix, item, numRows, numColumns) == -1) //k element is out of bounds
            {
                rowIndex = -1;
                colIndex = -1;

                Console.WriteLine(item + " NOT Found at matrix with start "
                    +matrix[0,0]+" and "+matrix[numRows-1,numColumns-1]);
                return;
            }
            while ((low <= heigh))
            {
                int mid = (heigh + low) / 2;
                if (item == matrix[mid, 0]) // checking leftmost element
                {
                    Console.WriteLine("Found at (" + mid + ","
                                      + "0)");
                    rowIndex = mid;
                    colIndex = 0;
                    return;
                }

                else if (item == matrix[mid, numColumns - 1]) // checking rightmost
                                                              // element
                {
                    Console.WriteLine("Found at (" + mid + ","
                                      + (numColumns - 1) + ")");
                    rowIndex = mid;
                    colIndex = numColumns - 1;
                    return;
                }
                else if (item > matrix[mid, 0] && item < matrix[mid, numColumns - 1])

                {
                    int indexColumn = findColumnIndex(matrix, item, mid);
                    rowIndex = mid;
                    colIndex = indexColumn;
                    return;
                }


                if (item < matrix[mid, 0])
                    heigh = mid - 1;
                if (item > matrix[mid, numColumns - 1])
                    low = mid + 1;

            }




        }
        static int inValidK(int[,]matrix,int k, int rows, int cols)
        {
            if(rows < 1 || cols < 1)
            {
                return -1;
            }
            if(k<matrix[0,0]||k>matrix[rows-1,cols-1]){
                return -1;
            }
            return 0;
        }
        static int findColumnIndex(int[,] matrix, int k,
                          int x) // x is the row number
        {
           

            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);
            int l = 0, r = numColumns - 1, mid;
            int[] arr = new int[2];
            while (l <= r)
            {
                mid = (l + r) / 2;

                if (matrix[x, mid] == k)
                {
                    Console.WriteLine("Found at (" + x + ","
                                      + mid + ")");

                    return mid;
                }


                if (matrix[x, mid] > k)
                    r = mid - 1;
                if (matrix[x, mid] < k)
                    l = mid + 1;
            }
            return -1;
            Console.WriteLine("Element not found");
        }

       

    }
}