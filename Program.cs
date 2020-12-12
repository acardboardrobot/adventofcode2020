using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //Day5("day5input.txt");
            //Day6("day6input.txt");
            //Day7("day7input.txt");
            //Day8("day8input.txt");
            //Day9("day9input.txt");
            //Day10("day10input.txt");
            //Day11("day11input.txt");
            Day12("day12input.txt");
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

        public static void Day6(string inputFile)
        {

            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();
            List<string> groupStrings = new List<string>();

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
                        toAdd = toAdd + '\n' + inputStrings[e];
                    }

                    toAdd = toAdd.Trim();

                    groupStrings.Add(toAdd);

                    lastBlank = i;
                }
            }

            List<int> groupCounts = new List<int>();

            for (int i = 0; i < groupStrings.Count; i++)
            {
                List<string> eachLine = new List<string>();
                eachLine = groupStrings[i].Split('\n').ToList();

                List<char> charsInString = new List<char>();
                for (int e = 0; e < groupStrings[i].Length; e++)
                {
                    charsInString.Add(groupStrings[i][e]);
                }
                
                List<char> charsInAllStrings = new List<char>();

                foreach (char c in charsInString)
                {
                    bool presentInAll = true;

                    foreach (string compareString in eachLine)
                    {
                        if (!compareString.Contains(c))
                        {
                            presentInAll = false;
                        }
                    }

                    if (presentInAll)
                    {
                        charsInAllStrings.Add(c);
                    }
                }

                groupCounts.Add(charsInAllStrings.Distinct().ToList().Count);
            }

            int sumCount = 0;

            foreach (int i in groupCounts)
            {
                sumCount += i;
            }

            Console.WriteLine(sumCount);
        }

        public static void Day7(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> inputStrings = new List<string>();

            foreach (string s in input)
            {
                string cleaned = s.Replace("bags", "bag");
                inputStrings.Add(cleaned);
            }

            List<string> bagTypesCanContainGold = new List<string>();

            /*
            foreach (string s in inputStrings)
            {
                List<string> kids = getChildren(s);
                List<string> visited = new List<string>();

                while (kids.Count > 0)
                {
                    string currentChild = kids[0];
                    string currentType = kids[0].Substring(2);
                    string currentFullString = getBagString(currentType, inputStrings);
                    

                    if (currentChild.Contains("shiny gold"))
                    {
                        bagTypesCanContainGold.Add(s);
                        kids = new List<string>();
                    }
                    else
                    {
                        kids.AddRange(getChildren(currentFullString));
                        visited.Add(kids[0]);
                        kids.RemoveAt(0);
                        kids = kids.Distinct().ToList();
                    }
                }
            }
            */

            string startingString = getBagString("shiny gold", inputStrings);

            int bagsHeldInGold = getHeldBagCount(startingString, inputStrings);

            //Console.WriteLine(bagTypesCanContainGold.Count);

            Console.WriteLine("{0} bags in your shiny gold one.", bagsHeldInGold);
            //foreach type of bag
            //add the types it links to to a list
            //Check these types, if they're a gold bag, then bool is true and exit. We just need to know that. If not, recurse
        }

        public static void Day8(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            List<string> instructionSet = new List<string>();

            foreach (string s in input)
            {
                instructionSet.Add(s);
            }

            int currentLine = 0;
            int accumulator = 0;

            List<int> visitedInstructions = new List<int>();

            bool keepRunning = false;

            for (int i = 0; i < instructionSet.Count; i++)
            {
                string currentInstruction = instructionSet[i].Split(" ")[0];
                int currentInstructionValue = int.Parse(instructionSet[i].Split(" ")[1]);
                List<string> newInstructions = new  List<string>();

                for (int e = 0; e < instructionSet.Count; e++)
                {
                    if (i == e)
                    {
                        string alteredInstruction = instructionSet[i];
                        if (currentInstruction == "nop")
                        {
                            alteredInstruction = instructionSet[i].Replace("nop", "jmp");
                        }
                        else if (currentInstruction == "jmp")
                        {
                            alteredInstruction = instructionSet[i].Replace("jmp", "nop");
                        }
                        newInstructions.Add(alteredInstruction);
                    }
                    else
                    {
                        newInstructions.Add(instructionSet[e]);
                    }
                }

                if (getsToEnd(newInstructions))
                {
                    Console.WriteLine(i);
                }

            }
        }

        public static void Day9(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            int priorCount = 25;
            int lowerBound = 0;
            int highBound = priorCount;
            int currentPointer = priorCount;
            long valueToGet = 0;

            List<long> numberSet = new List<long>();
            List<long> subSet = new List<long>();

            foreach (string s in input)
            {
                numberSet.Add(long.Parse(s));
            }

            for (int i = 0; i < priorCount; i++)
            {
                subSet.Add(numberSet[i]);
            }

            for (lowerBound = 0; lowerBound < numberSet.Count - priorCount; lowerBound++)
            {
                long targetValue = numberSet[currentPointer];
                if (lowerBound > 0)
                {
                    subSet.RemoveAt(0);
                }

                bool correct = false;
                foreach (int firstValue in subSet)
                {
                    foreach (int secondValue in subSet)
                    {
                        if (firstValue + secondValue == targetValue)
                        {
                            correct = true;
                        }
                    }
                }

                if (!correct)
                {
                    Console.WriteLine(targetValue);
                    valueToGet = targetValue;
                    break;
                }

                subSet.Add(numberSet[currentPointer]);
                currentPointer++;
            }

            for (lowerBound = 0; lowerBound < numberSet.Count; lowerBound++)
            {
                long subSetTotal = numberSet[lowerBound];

                for (currentPointer = lowerBound + 1; currentPointer < numberSet.Count && subSetTotal < valueToGet; currentPointer++)
                {
                    subSetTotal += numberSet[currentPointer];

                    if (subSetTotal > valueToGet)
                    {
                        break;
                    }
                    else if (subSetTotal == valueToGet)
                    {
                        List<long> answerSet = numberSet.GetRange(lowerBound, currentPointer - lowerBound);
                        Console.WriteLine(answerSet.Min() + answerSet.Max());
                        break;
                    }
                }
            }
        }

        public static void Day10(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

            int oneJumpCount = 0;
            int threeJumpCount = 1;
            int highestVolt = 3;

            List<int> numberSet = new List<int>();

            foreach (string s in input)
            {
                numberSet.Add(int.Parse(s));
            }

            numberSet.Sort();

            int currentVolt = 0;
            int previousVolt = 0;

            foreach (int i in numberSet)
            {
                currentVolt = i;

                int difference = currentVolt - previousVolt;

                if (difference > 3)
                {
                    highestVolt = currentVolt + 3;
                    break;
                }
                else if (difference == 1)
                {
                    oneJumpCount++;
                }
                else if (difference > 1)
                {
                    threeJumpCount++;
                }

                highestVolt = currentVolt + 3;
                previousVolt = currentVolt;

                List<int> countPassList = new List<int>();

                for (int e = i; e < numberSet.Count-i; e++)
                {
                    countPassList.Add(numberSet[i]);
                }
            }

            numberSet.Add(0);
            numberSet.Add(highestVolt);
            numberSet.Sort();
            long outTotal = getAdapterCount(numberSet.Count, numberSet);

            Console.WriteLine(oneJumpCount * threeJumpCount);

            Console.WriteLine(outTotal);
        }

        public static void Day11(string inputFile)
        {

            var input = File.ReadAllLines(inputFile);

            int currentX = 0;
            int currentY = 0;
            int currentOccupiedCount = 0;

            int iteration = 0;

            List<SeatingLocation> seatSet = new List<SeatingLocation>();
            List<SeatingLocation> oldSeatSet = new List<SeatingLocation>();

            foreach (string s in input)
            {
                for (currentX = 0; currentX < s.Length; currentX++)
                {
                    bool thisIsSeat = false;
                    if (s[currentX] == 'L')
                    {
                        thisIsSeat = true;
                    }
                    seatSet.Add(new SeatingLocation(currentX, currentY, thisIsSeat, false));
                }
                currentY++;
            }

            foreach (SeatingLocation s in seatSet)
            {
                s.buildAdjacencies(seatSet);
            }

            Console.WriteLine("Adjacencies built");

            bool keepRunning = true;

            while (keepRunning)
            {
                oldSeatSet = seatSet;
                //visualiseWaitingArea(seatSet);
                List<SeatingLocation> newSeatSet = tickGamePart2(seatSet);

                int countIt = 0;

                seatSet = newSeatSet;

                for (int i = 0; i < oldSeatSet.Count; i++)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", oldSeatSet[i].xPos, oldSeatSet[i].yPos, oldSeatSet[i].isSeat, oldSeatSet[i].occupied);
                    if (oldSeatSet[i].occupied != newSeatSet[i].occupied && oldSeatSet[i].isSeat)
                    {
                        countIt++;
                    }
                    Console.WriteLine("-----------");
                }

                if (countIt == 0)
                {
                    keepRunning = false;
                }

            }

            int outNumber = 0;

            for (int i = 0; i < seatSet.Count; i++)
            {
                if (seatSet[i].isSeat && seatSet[i].occupied)
                {
                    outNumber++;
                }
            }

            Console.WriteLine(outNumber);

        }

        public static void Day12(string inputFile)
        {
            var input = File.ReadAllLines(inputFile);

			List < string > commandLines = new List < string > ();

            char currentFacing = 'E';
            Point currentPosition = new Point(0, 0);

			foreach(string s in input)
			{
				commandLines.Add(s);
			}

            foreach (string commandTuple in commandLines)
            {
                char direction = commandTuple[0];
                int magnitude = int.Parse(commandTuple.Substring(1));

                Console.WriteLine(commandTuple);
                Console.WriteLine("{0}, {1}", direction, magnitude);

                if (direction == 'F')
                {
                    //Forward
                    currentPosition = moveBy(currentPosition, currentFacing, magnitude);
                }
                else if (direction == 'L')
                {
                    //Turn Left
                    currentFacing = turnBy(currentFacing, direction, magnitude);
                }
                else if (direction == 'R')
                {
                    currentFacing = turnBy(currentFacing, direction, magnitude);
                }
                else if (direction == 'N')
                {
                    currentPosition = moveBy(currentPosition, direction, magnitude);
                }
                else if (direction == 'S')
                {
                    currentPosition = moveBy(currentPosition, direction, magnitude);
                }
                else if (direction == 'E')
                {
                    currentPosition = moveBy(currentPosition, direction, magnitude);
                }
                else if (direction == 'W')
                {
                    currentPosition = moveBy(currentPosition, direction, magnitude);
                }

                Console.WriteLine("{0}, {1}", currentPosition.x, currentPosition.y);
            }

            int manhattanDistance = Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);

            Console.WriteLine(manhattanDistance);
        }

        public static Point moveBy(Point startPoint, char direction, int magnitude)
        {
            if (direction == 'N')
            {
                startPoint.y += magnitude;
            }
            else if (direction == 'S')
            {
                startPoint.y -= magnitude;
            }
            else if (direction == 'E')
            {
                startPoint.x += magnitude;
            }
            else if (direction == 'W')
            {
                startPoint.x -= magnitude;
            }

            return startPoint;
        }

        public static char turnBy(char currentFacing, char turnDirection, int magnitude)
        {
            int outDirection = 0;//0 - north, 1 - east, 2 - south, 3 - west
            int leftOrRight = 1;//1 - right, -1 - left)

            if (currentFacing == 'N')
            {
                outDirection = 0;
            }
            else if (currentFacing == 'E')
            {
                outDirection = 1;
            }
            else if (currentFacing == 'S')
            {
                outDirection = 2;
            }
            else if (currentFacing == 'W')
            {
                outDirection = 3;
            }

            if (turnDirection == 'R')
            {
                leftOrRight = 1;
            }
            else if (turnDirection == 'L')
            {
                leftOrRight = -1;
            }

            int quarterTurns = magnitude / 90;

            outDirection += (quarterTurns*leftOrRight);

            outDirection = outDirection % 4;

            if (outDirection < 0)
            {
                outDirection = 4 + outDirection;
            }
            else
            {
                outDirection = outDirection;
            }
            
            char outputChar = currentFacing;

            if (outDirection == 0)
            {
                outputChar = 'N';
            }
            else if (outDirection == 1)
            {
                outputChar = 'E';
            }
            else if (outDirection == 2)
            {
                outputChar = 'S';
            }
            else
            {
                outputChar = 'W';
            }

            return outputChar;
        }

        public static int countChangesInStates(List<SeatingLocation> oldSet, List<SeatingLocation> newSet)
        {
            int changes = 0;

            if (oldSet.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < newSet.Count; i++)
            {
                if (oldSet[i].isSeat)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(newSet[i].occupied);
                    Console.WriteLine(oldSet[i].occupied);
                    if (oldSet[i].occupied != newSet[i].occupied)
                    {
                        Console.WriteLine("her");
                        changes++;
                    }
                }
                
            }

            return changes;
        }

        public static int getOccupiedCount(List<SeatingLocation> seatSet)
        {
            int occupiedSeats = 0;

            foreach (SeatingLocation s in seatSet)
            {
                if (s.occupied)
                {
                    occupiedSeats++;
                }
            }

            return occupiedSeats;
        }

        public static List<SeatingLocation> tickGame(List<SeatingLocation> seatSet)
        {
            List<SeatingLocation> returnSet = new List<SeatingLocation>();

            int changesInTick = 0;

            foreach (SeatingLocation s in seatSet)
            {
                int adjacentSeats = 0;

                for (int i = 0; i < s.nextAdjacentSeat.Count; i++)
                {
                    foreach (SeatingLocation e in seatSet)
                    {
                        if (e.xPos == s.nextAdjacentSeat[i].xPos && e.yPos == s.nextAdjacentSeat[i].yPos)
                        {
                            s.nextAdjacentSeat[i].occupied = e.occupied;
                            break;
                        }
                    }
                    if (s.nextAdjacentSeat[i].occupied)
                    {
                        adjacentSeats++;
                    }
                }
                
                bool toOccupy = s.occupied;

                if (adjacentSeats >= 5 && s.isSeat)
                {
                    toOccupy = false;
                }
                else if (adjacentSeats == 0 && !s.occupied && s.isSeat)
                {
                    toOccupy = true;
                }

                if (toOccupy != s.occupied)
                {
                    if (s.isSeat)
                    {
                        changesInTick++;
                    }
                }

                SeatingLocation newSeat = new SeatingLocation(s.xPos, s.yPos, s.isSeat, toOccupy);
                returnSet.Add(newSeat);
            }

            //Console.WriteLine(changesInTick);

            return returnSet;
        }

        public static List<SeatingLocation> tickGamePart2(List<SeatingLocation> seatSet)
        {
            List<SeatingLocation> returnSet = new List<SeatingLocation>();

            int changesInTick = 0;
            int seatsInSet = 0;

            foreach (SeatingLocation s in seatSet)
            {
                bool nowOccupied = false;
                if (s.isSeat)
                {
                    if (s.isOccupied(seatSet))
                    {
                        changesInTick++;
                        nowOccupied = true;
                    }
                    else
                    {
                        nowOccupied = false;
                    }
                    seatsInSet++;
                }
                SeatingLocation newSeat = new SeatingLocation(s.xPos, s.yPos, s.isSeat, nowOccupied);

                returnSet.Add(newSeat);
            }

            return returnSet;
        }

        public static void visualiseWaitingArea(List<SeatingLocation> seatSet)
        {
            for (int i = 0; i < 97; i++)
            {
                for (int e = 0; e < 98; e++)
                {
                    int currentHolder = i*97 + e;
                    if (seatSet[currentHolder].isSeat)
                    {
                        if (seatSet[currentHolder].occupied)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write("L");
                        }
                    }
                    else
                    {
                        Console.Write("_");
                    }
                }
                Console.Write('\n');
            }
        }

        public static long getAdapterCount(int adapterCount, List<int> adapters)
        {
            var permutationsForEachAdapter = new long[adapterCount];

            permutationsForEachAdapter[0] = 1;

            for (int i = 1; i < adapterCount; i++)
            {
                if (adapters[i] - adapters[i - 1] <= 3)
                {
                    permutationsForEachAdapter[i] += permutationsForEachAdapter[i - 1];
                }

                if (i > 1 && adapters[i] - adapters[i - 2] <= 3)
                {
                    permutationsForEachAdapter[i] += permutationsForEachAdapter[i - 2];
                }

                if (i > 2 && adapters[i] - adapters[i - 3] <= 3)
                {
                    permutationsForEachAdapter[i] += permutationsForEachAdapter[i - 3];
                }
                    
            }

            return permutationsForEachAdapter[adapterCount - 1];
        }

        public static bool getsToEnd(List<string> instructionSet)
        {
            int currentLine = 0;
            int accumulator = 0;
            bool getsToEnd = false;

            List<int> visitedInstructions = new List<int>();

            bool keepRunning = true;

            while (keepRunning)
            {

                if (currentLine >= instructionSet.Count)
                {
                    getsToEnd = true;
                    keepRunning = false;
                }
                else if (visitedInstructions.Contains(currentLine))
                {
                    keepRunning = false;
                }
                else
                {
                    string currentInstruction = instructionSet[currentLine].Split(" ")[0];
                    int currentInstructionValue = int.Parse(instructionSet[currentLine].Split(" ")[1]);
                    visitedInstructions.Add(currentLine);

                    if (currentInstruction == "nop")
                    {
                        currentLine++;
                    }
                    else if (currentInstruction == "acc")
                    {
                        accumulator += currentInstructionValue;
                        currentLine++;
                    }
                    else if (currentInstruction == "jmp")
                    {
                        currentLine += currentInstructionValue;
                    }
                }
            }

            if (getsToEnd)
            {
                Console.WriteLine(accumulator);
            }

            return getsToEnd;
        }

        public static string getBagString(string name, List<string> inputs)
        {
            foreach (string s in inputs)
            {
                string thisBaseType = s.Split("contain")[0];

                if (thisBaseType.Contains(name))
                {
                    return s;
                }
            }
            return null;
        }
        public static List<string> getChildren(string input)
        {
            List<string> heldBags = new List<string>();

            if (input.Contains("contain"))
            {
                if (input.Contains("no other"))
                {
                    return heldBags;
                }
                else
                {
                    string thisBaseType = input.Split("contain")[0];
                    string childTypes = input.Split("contain")[1];
                    childTypes = childTypes.Replace(".", " ");
                    childTypes = childTypes.Replace("bag", " ");
                    childTypes = childTypes.Trim();

                    heldBags = childTypes.Split(',').ToList();

                    for (int i = 0; i < heldBags.Count; i++)
                    {
                        heldBags[i] = heldBags[i].Trim();
                    }
                }
                
            }
            return heldBags;
        }

        public static int getHeldBagCount(string bagType, List<string> inputs)
        {
            int total = 0;

            List<string> children = getChildren(bagType);

            if (children.Count == 0)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    int countOfThisBag = int.Parse(children[i].Split(" ")[0]);
                    string thisBagType = children[i].Substring(2);
                    
                    int bagsBelowCount = getHeldBagCount(getBagString(thisBagType, inputs), inputs);

                    Console.WriteLine(bagsBelowCount);
                    Console.WriteLine(countOfThisBag);

                    total += countOfThisBag + (bagsBelowCount * countOfThisBag);
                }
            }

            return total;
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

    public class SeatingLocation
    {
        public int xPos, yPos;
        public bool isSeat;
        public bool occupied;
        public List<SeatingLocation> nextAdjacentSeat;

        public SeatingLocation(int desX, int desY, bool seat, bool deoccupied)
        {
            xPos = desX;
            yPos = desY;
            isSeat = seat;
            occupied = deoccupied;
            nextAdjacentSeat = new List<SeatingLocation>();
        }

        public bool isOccupied(List<SeatingLocation> seatSet)
        {
            int adjacentSeatsOccupied = 0;
            bool changeHappens = false;
            bool toBeOccupied = false;

            foreach (SeatingLocation s in nextAdjacentSeat)
            {
                foreach (SeatingLocation x in seatSet)
                {
                    if (x.xPos == s.xPos && x.yPos == s.yPos)
                    {
                        Console.WriteLine(x.occupied);
                        if (x.occupied)
                        {
                            adjacentSeatsOccupied++;
                            Console.WriteLine(adjacentSeatsOccupied);
                        }
                        break;
                    }
                    
                }
            }

            Console.WriteLine(adjacentSeatsOccupied);
            
            if (adjacentSeatsOccupied == 0)
            {
                toBeOccupied = true;
            }
            if (adjacentSeatsOccupied >= 4)
            {
                toBeOccupied = false;
            }

            return toBeOccupied;
        }

        public void buildAdjacencies(List<SeatingLocation> seatSet)
        {
            SeatingLocation upLeft, up, upRight, Left, Right, downLeft, down, downRight;

            upLeft = new SeatingLocation(-100, -100, true, false);
            up = new SeatingLocation(-100, -100, true, false);
            upRight = new SeatingLocation(-100, -100, true, false);
            Left = new SeatingLocation(-100, -100, true, false);
            Right = new SeatingLocation(-100, -100, true, false);
            downLeft = new SeatingLocation(-100, -100, true, false);
            down = new SeatingLocation(-100, -100, true, false);
            downRight = new SeatingLocation(-100, -100, true, false);
            if (isSeat)
            {
                foreach (SeatingLocation e in seatSet)
                {
                    if (xPos == e.xPos && yPos == e.yPos)
                    {
                        //Same position, so skip.
                    }
                    else if (e.isSeat)
                    {
                        if (e.xPos < xPos)
                        {
                            if (e.yPos == yPos)
                            {
                                //Left of on same row.
                                if (Math.Abs(xPos - e.xPos) < Math.Abs(xPos - Left.xPos))
                                {
                                    Left = e;
                                }
                            }
                            else if (e.yPos < yPos)
                            {
                                //Console.WriteLine("Left and above");
                                if (Math.Abs(xPos - e.xPos) + Math.Abs(yPos - e.yPos) < Math.Abs(xPos - upLeft.xPos) + Math.Abs(yPos - upLeft.yPos))
                                {
                                    if (Math.Abs(xPos - e.xPos) - Math.Abs(yPos - e.yPos) == 0)
                                    {
                                        upLeft = e;
                                    }
                                }
                            }
                            else if (e.yPos > yPos)
                            {
                                //Console.WriteLine("Left and below");
                                if (Math.Abs(xPos - e.xPos) + Math.Abs(yPos - e.yPos) < Math.Abs(xPos - downLeft.xPos) + Math.Abs(yPos - downLeft.yPos))
                                {
                                    if (Math.Abs(xPos - e.xPos) - Math.Abs(yPos - e.yPos) == 0)
                                    {
                                        downLeft = e;
                                    }
                                }
                            }
                        }
                        else if (e.xPos == xPos)
                        {
                            if (Math.Abs(yPos - e.yPos) < Math.Abs(yPos - up.yPos))
                            {
                                up = e;
                            }
                            else if (Math.Abs(yPos - e.yPos) < Math.Abs(yPos - down.yPos))
                            {
                                down = e;
                            }
                        }
                        else if (e.xPos > xPos)
                        {
                            if (e.yPos == yPos)
                            {
                                //Right of on same row.
                                if (Math.Abs(xPos - e.xPos) < Math.Abs(xPos - Right.xPos))
                                {
                                    Right = e;
                                }
                            }
                            else if (e.yPos < yPos)
                            {
                                //Console.WriteLine("Right and above");
                                if (Math.Abs(xPos - e.xPos) + Math.Abs(yPos - e.yPos) < Math.Abs(xPos - upRight.xPos) + Math.Abs(yPos - upRight.yPos))
                                {
                                    if (Math.Abs(xPos - e.xPos) - Math.Abs(yPos - e.yPos) == 0)
                                    {
                                        upRight = e;
                                    }
                                }
                            }
                            else if (e.yPos > yPos)
                            {
                                //Console.WriteLine("Right and below");
                                if (Math.Abs(xPos - e.xPos) + Math.Abs(yPos - e.yPos) < Math.Abs(xPos - downRight.xPos) + Math.Abs(yPos - downRight.yPos))
                                {
                                    if (Math.Abs(xPos - e.xPos) - Math.Abs(yPos - e.yPos) == 0)
                                    {
                                        downRight = e;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }

            if (upLeft.xPos != -100)
            {
                nextAdjacentSeat.Add(upLeft);
            }
            if (up.xPos != -100)
            {
                nextAdjacentSeat.Add(up);
            }
            if (upRight.xPos != -100)
            {
                nextAdjacentSeat.Add(upRight);
            }
            if (Left.xPos != -100)
            {
                nextAdjacentSeat.Add(Left);
            }
            if (Right.xPos != -100)
            {
                nextAdjacentSeat.Add(Right);
            }
            if (downLeft.xPos != -100)
            {
                nextAdjacentSeat.Add(downLeft);
            }
            if (down.xPos != -100)
            {
                nextAdjacentSeat.Add(down);
            }
            if (downRight.xPos != -100)
            {
                nextAdjacentSeat.Add(downRight);
            }

        }
    }

    public class Point
    {
        public int x {get; set;}
        public int y {get; set;}

        public Point(int xPos, int yPos)
        {
            x = xPos;
            y = yPos;
        }
    }
}