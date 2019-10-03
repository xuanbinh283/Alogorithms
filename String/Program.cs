using System;
using System.Collections.Generic;

namespace String
{
    class Program
    {
        /*
         * allow duplicate charactor
         */
        static void PrintPermutations(string text)
        {
            int size = text.Length;

            // build the count for each char
            var hashMap = new Dictionary<char, int>();
            var result = new char[size];

            foreach (char item in text)
            {
                if (!hashMap.ContainsKey(item))
                {
                    hashMap.Add(item, 1);
                }
                else
                {
                    hashMap[item]++;
                }
            }

            var charArr = new char[hashMap.Count];
            hashMap.Keys.CopyTo(charArr,0);

            // or use list
            //var list = new List<char>(hashMap.Keys);


            PrintPermutationsUtil(size, charArr, 0, hashMap, result);
        }

        private static void PrintPermutationsUtil(int size, char[] charArr, int count, Dictionary<char, int> hashMap, char[] result)
        {
            //AABC

            // base
            if (count == size)
            {
                Console.WriteLine(result);
                return;
            }


            foreach (var key in charArr)
            {
                if (hashMap[key] > 0)
                {
                    result[count] = key;
                    hashMap[key]--;
                    count++;
                    PrintPermutationsUtil(size, charArr, count, hashMap, result);

                    count--;
                    hashMap[key]++;
                }
            }
        }

        static void PrintLongestPalindrome(string text) {
            //forgeeksskeegfor  -> geeksskeeg
            // O(n^2)

            // idea: each palindrome will have multible substring palindmore -> can use dynamic programing
            // how to detech the substring?  (i,j) -> (i+1,j-1) will be the substring
            // we can use bool[n,n] array to store the isSubstring value (1 is substring)
            // -> a substring (i,j) is palindmore if text[i] = text[j] && substring(i+1, j-1) is palindmore

            var size = text.Length;
            var arr = new bool[size, size];
            int countLongest = 1;
            string subString = "";

            // we need some base arr value
            // arr[i,i] always true
            for (int i = 0; i < size; i++)
            {
                arr[i, i] = true;

                //add 2 length substring value
                if (i < size - 1 && text[i] == text[i+1])
                {
                    arr[i, i + 1] = true;
                    subString = text.Substring(i, 2);
                    countLongest = 2;
                }
            }
            
            for (int subLength = 3; subLength < size; subLength++)
            {
                for (int i = 0; i < size; i++)
                {
                    int endIdx = i + subLength - 1;
                    if (endIdx >= size)
                    {
                        break;
                    }

                    arr[i, endIdx] = arr[i + 1, endIdx - 1] && (text[i] == text[endIdx]);
                    if (arr[i, endIdx])
                    {
                        subString = text.Substring(i, subLength);

                    }
                }
            }

            Console.WriteLine(subString);


            
        }

        /*
         *  spencil cases: length1 <> length2 -> return false;
         *      text1 == text 2 => return false
         *     
         *  start with 0 idx and find the match idx
         *      if not found -> return 
         *      find first match idx -> i: use 2 pointers and check the rotate 
         *          if rotate -> return true
         *          else -> find the next idx
         *      
         */
        static bool isStringRotate(string text1, string text2) {

            if (text1.Length != text2.Length || text1 == text2)
            {
                return false;
            }


            int idx2 = text2.IndexOf(text1[0]);

            return isStringRotateUtil(text1, text2, text1.Length, idx2);
            
        }

        private static bool isStringRotateUtil(string text1, string text2, int length, int idx2)
        {
            if (idx2 == -1)
            {
                return false;
            }
            
            int poiter2 = idx2;
            for (int i = 1; i < length; i++)
            {
                if (text1[i] != text2[(poiter2 + i) % length])
                {
                    int newIdx2 = text2.IndexOf(text1[0], idx2 + 1);
                    return isStringRotateUtil(text1, text2, length, newIdx2);
                }
                
            }

            return true;
        }

        static void Main(string[] args)
        {
            var text = "AABC";
            //PrintPermutations(text);
            //text = "forgeeksskeegfor";
            //PrintLongestPalindrome(text);

            Console.Write(isStringRotate("abcdef", "feabcd"));

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
