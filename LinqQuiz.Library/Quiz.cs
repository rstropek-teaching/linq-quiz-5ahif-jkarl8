using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<int> returnNums = new List<int>();
            for (int i = 1; i < exclusiveUpperLimit; i++)
            {
                if(i%2 == 0)
                {
                    returnNums.Add(i);
                }
            }
            return returnNums.ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit<1) return (new int[] { });
            List<int> returnNums = new List<int>();
            for (int i = exclusiveUpperLimit-1; i > 0; i--)
            {
                if (i % 7 == 0)
                {  
                    if(i*i>System.Int32.MaxValue/2)
                    {
                        throw new OverflowException();
                    }
                    returnNums.Add(i * i);
                }
            }
            return returnNums.ToArray();
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            List<FamilySummary> fs = new List<FamilySummary>();
            if(families == null)
            {
                throw new ArgumentNullException();
            }
            for(int  i = 0; i<families.Count; i++)
            {
                IFamily family = families.ElementAt(i);
                double old = 0.0;
                double cnt = 0.0;
                double temp = 0.0;
                if (family.Persons.Count > 0)
                {
                    for (int j = 0; j < family.Persons.Count; j++)
                    {
                        IPerson person = family.Persons.ElementAt(j);
                        old += (int)person.Age;
                        cnt++;
                    }     
                    temp = old / cnt;
                }
                FamilySummary help = new FamilySummary();
                help.FamilyID = family.ID;
                help.NumberOfFamilyMembers = (int)cnt;
                help.AverageAge = (decimal)temp;
                fs.Add(help);

            }
            return fs.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            char[] help = text.ToUpper().ToCharArray();
            List<int> possibleLetters = Enumerable.Range('A', 'Z').ToList();
            List<(char letter, int numberOfOccurrences)> returnarr = new List<(char letter, int numberOfOccurences)>();

            foreach(var letter in possibleLetters)
            {
                var cnt = help.Count(l => l == letter);
                if(cnt > 0)
                {
                    returnarr.Add(((char)letter, cnt));
                }
            }
            return returnarr.ToArray();
        }
    }
}
