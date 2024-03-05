using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Assessment
{
    public static class Assessment
    {
        public static string FindLongestCommonPrefix(List<string> input)
        {
            if(input == null || input.Count == 0)
            {
                return string.Empty;
            }

            //Taking the first string as reference
            var stringOfReference = input[0];


            for (int i = 0; i < stringOfReference.Length; i++)
            {
                char currentChar = stringOfReference[i];

                
                for (int j = 1; j < input.Count; j++)
                {
                    // If the index exceeds the length of the current string or the character is different, return the prefix
                    if (i >= input[j].Length || input[j][i] != currentChar)
                        return stringOfReference.Substring(0, i);
                }
            }

            return stringOfReference;
        }
    }
}
