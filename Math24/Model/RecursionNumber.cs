using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math24.Model
{
    /// <summary>
    /// 以下程式碼來源： http://www.codeproject.com/KB/recipes/Premutations.aspx
    /// </summary>
    internal class RecursionNumber
    {
        List<List<string>> obj;
        public RecursionNumber(List<List<string>> obj)
        {
            this.obj = obj;
        }
        private int elementLevel = -1;
        private int numberOfElements;
        private int[] permutationValue = new int[0];

        private char[] inputSet;
        public char[] InputSet
        {
            get { return inputSet; }
            set { inputSet = value; }
        }

        private int permutationCount = 0;
        public int PermutationCount
        {
            get { return permutationCount; }
            set { permutationCount = value; }
        }

        int dataCount = 0;
        public char[] MakeCharArray(string InputString)
        {
            char[] charString = InputString.ToCharArray();
            dataCount = charString.Count();
            Array.Resize(ref permutationValue, charString.Length);
            numberOfElements = charString.Length;
            return charString;
        }

        public void CalcPermutation(int k)
        {
            elementLevel++;
            permutationValue.SetValue(elementLevel, k);

            if (elementLevel == numberOfElements)
            {
                OutputPermutation(permutationValue);
            }
            else
            {
                for (int i = 0; i < numberOfElements; i++)
                {
                    if (permutationValue[i] == 0)
                    {
                        CalcPermutation(i);
                    }

                }
            }

            elementLevel--;
            permutationValue.SetValue(0, k);

        }

        List<string> obj2 = new List<string>();
        private void OutputPermutation(int[] value)
        {
            foreach (int j in value)
            {
                Console.Write(inputSet.GetValue(j - 1));
                obj2.Add(inputSet.GetValue(j - 1).ToString());

                if (obj2.Count == dataCount)
                {
                    obj.Add(obj2);
                    obj2 = new List<string>();
                }
            }
            Console.WriteLine();
            PermutationCount++;
        }
    }
}
