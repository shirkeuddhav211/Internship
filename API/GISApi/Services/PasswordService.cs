using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Services
{
    public class PasswordService
    {
        #region static members
        private static int minimumLowerCaseChars = 2;
        private static int minimumUpperCaseChars = 2;
        private static int minimumNumericChars = 2;
        private static int minimumSpecialChars = 2;

        private static string allLowerCaseChars;
        private static string allUpperCaseChars;
        private static string allNumericChars;
        private static string allSpecialChars;
        private static Random rnd;
        #endregion

        static PasswordService()
        {
            // Ranges not using confusing characters
            allLowerCaseChars = GetCharRange('a', 'z', exclusiveChars: "ilo");
            allUpperCaseChars = GetCharRange('A', 'Z', exclusiveChars: "IO");
            allNumericChars = GetCharRange('1', '9');
            allSpecialChars = "!@#%*()$?+-=";

            rnd = new Random();

        }


        public static string GeneratePassword()
        {
            // Get the required number of characters of each catagory and 
            // add random charactes of all catagories
            var result = GetRandomString(allLowerCaseChars, minimumLowerCaseChars) +
                            GetRandomString(allUpperCaseChars, minimumUpperCaseChars) +
                            GetRandomString(allNumericChars, minimumNumericChars) +
                            GetRandomString(allSpecialChars, minimumSpecialChars);

            // Shuffle the result 
            var arr = result.ToCharArray();
            result = new string(Shuffle(arr));
            return result;
        }

        #region private methods
        private static string GetRandomString(string possibleChars, int length)
        {
            var result = string.Empty;
            for (var position = 0; position < length; position++)
            {
                var index = rnd.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }
            return result;
        }

        private static T[] Shuffle<T>(T[] list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Length; i++)
            {
                var temp = list[i];
                int randomIndex = rnd.Next(i + 1);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }
        #endregion
    }
}
