using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
	class Program
	{
		static void Main(string[] args)
		{
			//Day1("day1input.txt");
            //Day2("day2input.txt");
            //Day3("day3input.txt");
            //Day4("day4input.txt");
            Day5("day5input.txt");
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

                foreach(string fieldTuple in fields)
                {
                    string fieldType = fieldTuple.Split(':')[0];
                    string fieldValue = fieldTuple.Split(':')[1];

                    if (fieldType == "byr")
                    {
                        if (fieldValue.Length == 4)
                        {
                            int birthYear = int.Parse(fieldValue);
                            if (birthYear >= 1920 && birthYear <= 2002)
                            {
                                byrPresent = true;
                            }
                        }
                    }
                    else if (fieldType == "iyr")
                    {
                        if (fieldValue.Length == 4)
                        {
                            int issueYear = int.Parse(fieldValue);
                            if (issueYear >= 2010 && issueYear <= 2020)
                            {
                                iyrPresent = true;
                            }
                        }
                    }
                    else if (fieldType == "eyr")
                    {
                        if (fieldValue.Length == 4)
                        {
                            int expiryYear = int.Parse(fieldValue);
                            if (expiryYear >= 2020 && expiryYear <= 2030)
                            {
                                eyrPresent = true;
                            }
                        }
                    }
                    else if (fieldType == "hgt")
                    {
                        string endUnit = fieldValue.Substring(fieldValue.Length-2);
                        int heightNumber = 0;

                        if (endUnit == "in")
                        {
                            heightNumber = int.Parse(fieldValue.Substring(0, fieldValue.Length-2));
                            if (heightNumber >= 59 && heightNumber <= 76)
                            {
                                hgtPresent = true;
                            }
                        }
                        else if (endUnit == "cm")
                        {
                            heightNumber = int.Parse(fieldValue.Substring(0, fieldValue.Length-2));
                            if (heightNumber >= 150 && heightNumber <= 193)
                            {
                                hgtPresent = true;
                            }
                        }
                    }
                    else if (fieldType == "hcl")
                    {
                        if (fieldValue.Length == 7 && fieldValue[0] == '#')
                        {
                            string hairColour = fieldValue.Substring(1);

                            hclPresent = Regex.Match(hairColour, "[0-9a-f]").Success;
                        }
                    }
                    else if (fieldType == "ecl")
                    {
                        if (fieldValue == "amb" || fieldValue == "blu" || fieldValue == "brn" || fieldValue == "gry" || fieldValue == "grn" || fieldValue == "hzl" || fieldValue == "oth" )
                        {
                            eclPresent = true;
                        }
                    }
                    else if (fieldType == "pid")
                    {
                        if (fieldValue.Length == 9)
                        {
                            pidPresent = Regex.Match(fieldValue, "[0-9]").Success;
                        }
                    }
                }

                if (byrPresent && iyrPresent && eyrPresent && hgtPresent && hclPresent && eclPresent && pidPresent)
                {
                    validPassports++;
                }
            }

            Console.WriteLine(validPassports);
        }

        public static void Day5(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();

            foreach (string s in input)
            {
                inputStrings.Add(s);
            }

            int highestSeatId = 0;
            int lowestSeatId = 500;
            List<int> boardingPassIds = new List<int>();

            foreach (string boardingPass in inputStrings)
            {
                int rowNumber = 0;
                int rowMax = 127;
                int rowMin = 0;
                int columnNumber = 0;
                int columnMin = 0;
                int columnMax = 7;
                int currentRowIncrement = 64;
                int currentColumnIncrement = 4;

                for (int i = 0; i < 7; i++)
                {
                    if (boardingPass[i] == 'B')
                    {  
                        rowMin += currentRowIncrement;
                    }
                    else
                    {
                        rowMax -= currentRowIncrement;
                    }
                    currentRowIncrement /= 2;
                }

                rowNumber = rowMin;

                for (int i = 7; i < 10; i++)
                {
                    if (boardingPass[i] == 'R')
                    {  
                        columnMin += currentColumnIncrement;
                    }
                    else
                    {
                        columnMax -= currentColumnIncrement;
                    }
                    currentColumnIncrement /= 2;
                }

                columnNumber = columnMin;

                int currentSeatId = (rowNumber * 8) + columnNumber;

                if (currentSeatId > highestSeatId)
                {
                    highestSeatId = currentSeatId;
                }

                if (currentSeatId < lowestSeatId)
                {
                    lowestSeatId = currentSeatId;
                }

                boardingPassIds.Add(currentSeatId);
            }

            Console.WriteLine(highestSeatId);

            boardingPassIds.Sort();

            List<int> adjacentSeats = new List<int>();

            for(int i = 0; i < boardingPassIds.Count; i++)
            {
                if (i > 0 && i < boardingPassIds.Count - 1)
                {
                    if (boardingPassIds[i] - boardingPassIds[i-1] == 1 && boardingPassIds[i+1] - boardingPassIds[i] == 1)
                    {
                        //Console.WriteLine(boardingPassIds[i]);
                    }
                    else
                    {
                        adjacentSeats.Add(boardingPassIds[i]);
                    }
                }
            }

            int mySeatId = adjacentSeats[1] - 1;
            Console.WriteLine(mySeatId);

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