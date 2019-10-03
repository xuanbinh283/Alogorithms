using System;
using System.Collections.Generic;
using System.Text;

namespace Sort
{
    public class MergeSort
    {
        public void Test() {
            Console.WriteLine("test");
        }

        internal List<int> Sort(List<int> unsorted)
        {

            if (unsorted.Count == 1)
                return unsorted;

            var left = new List<int>();
            var right = new List<int>();
            int middle = unsorted.Count / 2;



            var sort = new List<int>();



            return sort;
        }
    }
}
