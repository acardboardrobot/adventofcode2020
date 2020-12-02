using System;
using System.Collections.Generic;
using System.IO;

namespace Advent_of_Code_2020
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Day1("day1input.txt");
		}

		public static string Day1(string inputFile)
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

			return "done";
		}
	}
}