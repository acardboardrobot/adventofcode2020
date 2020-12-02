using System;
using System.Collections.Generic;
using System.IO;

namespace Advent_of_Code_2020
{
	class Program
	{
		static void Main(string[] args)
		{
			//Day1("day1input.txt");
            Day2("day2input.txt");
		}

		public static void Day1(string inputFile)
		{
			var input = File.ReadAllLines(inputFile);

			List < int > inputNumbers = new List < int > ();

			foreach(string s in input)
			{
				inputNumbers.Add(int.Parse(s));
			}

			int firstNumber = 0;
			int secondNumber = 0;
            int thirdNumber = 0;
			bool foundNumbers = false;

			while (!foundNumbers)
			{
				for (int i = 0; i < inputNumbers.Count; i++)
				{
					for (int e = 0; e < inputNumbers.Count; e++)
					{
						for (int f = 0; f < inputNumbers.Count; f++)
						{
							if (i != e && i != f && e != f)
							{
								if (inputNumbers[i] + inputNumbers[e] + inputNumbers[f] == 2020)
								{
									firstNumber = inputNumbers[i];
									secondNumber = inputNumbers[e];
                                    thirdNumber = inputNumbers[f];
									foundNumbers = true;
								}
							}
						}

					}
				}
			}

			Console.WriteLine(firstNumber);
			Console.WriteLine(secondNumber);
            Console.WriteLine(thirdNumber);

			Console.WriteLine(firstNumber * secondNumber * thirdNumber);

		}
	
        public static void Day2(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();

            foreach (string s in input)
            {
                inputStrings.Add(s);
            }

            int validCount = 0;

            foreach (string currentLine in inputStrings)
            {
                string[] elements = currentLine.Split(' ');
                int firstPosition = int.Parse(elements[0].Split('-')[0]);
                int secondPosition = int.Parse(elements[0].Split('-')[1]);

                firstPosition--;
                secondPosition--;
                
                char desChar = elements[1][0];

                int currentCountofChar = 0;
                bool firstPos = false;
                bool secondPos = false;

                if (elements[2][firstPosition] == desChar)
                {
                    firstPos = true;
                }

                if (elements[2].Length > secondPosition)
                {
                    if (elements[2][secondPosition] == desChar)
                    {
                        secondPos = true;
                    }
                }

                if (firstPos != secondPos)
                {
                    validCount++;
                }
            }

            Console.WriteLine(validCount);
        }

    }
}