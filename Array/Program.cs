using System;

namespace Array
{
    class Program
    {
        public class ArraySum
        {


            public void subArraySum(int[] arr, int n, int sum)
            {

                for (int i = 0; i < n; i++)
                {
                    var cur_sum = arr[i];

                    if (cur_sum > sum)
                        continue;

                    if (cur_sum == sum)
                    {
                        Console.WriteLine("Sum found at index: " + i);
                        continue;
                    }

                    int j = i;
                    while (cur_sum < sum && j < n)
                    {
                        cur_sum += arr[++j];
                        if (cur_sum > sum)
                            break;

                        if (cur_sum == sum)
                        {
                            Console.WriteLine($"Sum found between indexes: {i} and {j}");
                            break;
                        }
                    }


                }
            }
        }



        // merge two sorted arrays with O(1) extra space -> print
        // { 1, 5, 9, 10, 15, 20 }      //j
        // { 2, 3, 8, 13 }              //i
        // 
        //  loop arr2 from the last
        //  compare to arr1 from the last-1 
        //      if arr2[i] < arr1[j]  -> move arr1[j] to right
        //      else arr1[j+1] = arr2[i]
        //  arr2[i] = last;
        static void Merge2SortedArrays(int[] arr1, int[] arr2, int l1, int l2)
        {
            for (int i = l2 - 1; i >= 0; i--)
            {
                var lastArr1 = arr1[l1 - 1];

                // insert into arr1: correct order
                for (int j = l1 - 2; j >= 0; j--)
                {
                    if (arr2[i] < arr1[j])
                        arr1[j + 1] = arr1[j];
                    else
                    {
                        arr1[j + 1] = arr2[i];
                        break;
                    }
                }
                arr2[i] = lastArr1;
            }

            //print array1
            Console.Write("Arr1: ");
            foreach (var item in arr1)
            {
                Console.Write(item + " ");
            }
            //print array2
            Console.Write("\nArr2: ");
            foreach (var item in arr2)
            {
                Console.Write(item + " ");
            }


        }

        // Prints max at first position, min at second 
        // position, second max at third position, second 
        // min at fourth position and so on. 
        public static void rearrange(int[] arr, int n)
        {
            // initialize index of first minimum 
            // and first maximum element 
            int max_idx = n - 1, min_idx = 0;

            // store maximum element of array 
            int max_elem = arr[n - 1] + 1;

            // traverse array elements 
            for (int i = 0; i < n; i++)
            {

                // at even index : we have to put 
                // maximum element 
                if (i % 2 == 0)
                {
                    arr[i] += (arr[max_idx] % max_elem) * max_elem;
                    max_idx--;
                }

                // at odd index : we have to 
                // put minimum element 
                else
                {
                    arr[i] += (arr[min_idx] % max_elem) * max_elem;
                    min_idx++;
                }
            }

            // array elements back to it's original form 
            for (int i = 0; i < n; i++)
                arr[i] = arr[i] / max_elem;
        }

        static void TestMerge2SortedArrays()
        {
            int[] arr1 = new int[] { 1, 5, 9, 10, 15, 20 }; //j
            int[] arr2 = new int[] { 2, 3, 8, 13 };         //i

            var l1 = arr1.Length;
            var l2 = arr2.Length;

            // check which arr has the maximum
            if (arr1[l1 - 1] > arr2[l2 - 1])
            {
                Merge2SortedArrays(arr1, arr2, l1, l2);
            }
            else
                Merge2SortedArrays(arr2, arr1, l2, l1);
        }

        static void TestReArrange()
        {
            int[] arr = { 1, 2, 3, 4, 5, 12, 17, 19, 30 };
            int n = arr.Length;
            Console.WriteLine("Original Array");
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();

            rearrange(arr, n);

            Console.WriteLine("Modified Array");
            for (int i = 0; i < n; i++)
                Console.Write(arr[i] + " ");
        }

        /*
         * x^y > y^x
         X[] = {2, 1, 6}, Y = {1, 5}
         -> out: 3
         Pairs are (2, 1), (2, 5) and (6, 1)
         *
         * 
         * 
         */
        static void NumberOfPairs(int[] x, int[] y)
        {

        }
        private static double FindMedian(int[] a, int n, int[] b, int m)
        {
            int minIndex = 0, maxIndex = n;
            int i = 0;
            int j = 0;
            while (minIndex <= maxIndex)
            {
                i = (maxIndex + minIndex) / 2;
                j = (m + n + 1) / 2 - i;


                if (a[i + 1] < b[j])
                {
                    minIndex = i;
                }
                else if (a[i] > b[j + 1])
                {
                    maxIndex = i + 1;
                }
                else
                {
                    break;
                }

                // check i == 0 or n, j == 0 or m 
            }

            Console.Write($"i: {i}, j: {j} ");
            return 1d;
        }



        static void Main(string[] args)
        {
            //ArraySum arraysum = new ArraySum();
            //int[] arr = new int[] {15, 2, 4, 8,
            //                  9, 5, 10, 23};
            //int n = arr.Length;
            //int sum = 23;
            //arraysum.subArraySum(arr, n, sum);

            //TestMerge2SortedArrays();

            //TestReArrange();

            int[] a = new int[] { 2, 4, 6, 7, 12, 19, 40 };
            int[] b = new int[] { 10, 13, 14, 17, 33, 56, 67, 70 };
            int n = a.Length;
            int m = b.Length;

            double median = FindMedian(a, n, b, m);
            Console.WriteLine("median: " + median);

            Console.ReadKey();
        }

        
    }
}
