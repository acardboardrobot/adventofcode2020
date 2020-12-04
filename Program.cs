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
            //Day2("day2input.txt");
            //Day3("day3input.txt");
            Day4("day4input.txt");
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

        public static void Day3(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();

            List<int> treesHit = new List<int>();

            foreach (string s in input)
            {
                inputStrings.Add(s);
            }

            treesHit.Add(downhillFunction(1, 1, inputStrings));
            treesHit.Add(downhillFunction(3, 1, inputStrings));
            treesHit.Add(downhillFunction(5, 1, inputStrings));
            treesHit.Add(downhillFunction(7, 1, inputStrings));
            treesHit.Add(downhillFunction(1, 2, inputStrings));

            uint outputNumber = 1;

            foreach(uint i in treesHit)
            {
                Console.WriteLine(outputNumber);
                outputNumber *= i;
            }

            Console.WriteLine(outputNumber);
        }

        public static void Day4(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();
            List<string> passportStrings = new List<string>();

            foreach (string s in input)
            {
                inputStrings.Add(s);
            }

            int lastBlank = 0;

            for (int i = 0; i < inputStrings.Count; i++)
            {
                if (inputStrings[i].Length == 0)
                {
                    string toAdd = "";
                    for (int e = lastBlank; e < i; e++)
                    {
                        toAdd = toAdd + " " + inputStrings[e];
                    }

                    toAdd = toAdd.Trim();

                    passportStrings.Add(toAdd);

                    lastBlank = i;
                }
            }

            string lastEntry = "";

            for (int e = lastBlank; e < inputStrings.Count; e++)
            {
                lastEntry = lastEntry + " " + inputStrings[e];
            }
            lastEntry = lastEntry.Trim();
            passportStrings.Add(lastEntry);

            int validPassports = 0;

            foreach (string s in passportStrings)
            {
                bool byrPresent = false;
                bool iyrPresent = false;
                bool eyrPresent = false;
                bool hgtPresent = false;
                bool hclPresent = false;
                bool eclPresent = false;
                bool pidPresent = false;

                List<string> fields = new List<string>();
                string[] valueArray = s.Split(' ');
                foreach (string value in valueArray)
                {
                    fields.Add(value);
                }

                foreach(string fieldValue in fields)
                {
                    string fieldType = fieldValue.Split(':')[0];
                    if (fieldType == "byr")
                    {
                        byrPresent = true;
                    }
                    else if (fieldType == "iyr")
                    {
                        iyrPresent = true;
                    }
                    else if (fieldType == "eyr")
                    {
                        eyrPresent = true;
                    }
                    else if (fieldType == "hgt")
                    {
                        hgtPresent = true;
                    }
                    else if (fieldType == "hcl")
                    {
                        hclPresent = true;
                    }
                    else if (fieldType == "ecl")
                    {
                        eclPresent = true;
                    }
                    else if (fieldType == "pid")
                    {
                        pidPresent = true;
                    }
                }

                if (byrPresent && iyrPresent && eyrPresent && hgtPresent && hclPresent && eclPresent && pidPresent)
                {
                    validPassports++;
                }
            }

            Console.WriteLine(validPassports);
        }
        public static int downhillFunction(int xInc, int yInc, List<string> map)
        {
            int treeCollisions = 0;

            int xPos = 0;
            int yPos = 0;

            int xIncrement = xInc;
            int yIncrement = yInc;

            while (yPos < map.Count-1)
            {
                xPos += xIncrement;
                yPos += yIncrement;

                if (xPos >= map[0].Length)
                {
                    xPos -= map[0].Length;
                }

                if (map[yPos][xPos] == '#')
                {
                    treeCollisions++;
                }
            }

            Console.WriteLine(treeCollisions);
            return treeCollisions;
        }
    }
}