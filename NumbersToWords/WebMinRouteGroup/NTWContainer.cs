using System;
using System.Text.RegularExpressions;

namespace NumbersToWords.WebMinRouteGroup
{
    /// <summary>
    /// A class holding the necessary data and utility functions to translate a number to its word form
    /// </summary>
    public class NTWContainer
    {
        //to store the word conversions of a number
        private Dictionary<int, Dictionary<int, string>>? numberWordsData;

        /// <summary>
        /// used to determine what suffix to add at the end of stage depending how deep in the recurrsion the algorithm
        /// </summary>
        private Dictionary<int, string>? depthStagesData;

        private enum NumberTypes
        {
            unit, tenth, hundredth
        }

        public NTWContainer()
        {
            InitalizeDatabases();
        }


        void InitalizeDatabases()
        {
            numberWordsData = new Dictionary<int, Dictionary<int, string>>
            {
                //{0, new Dictionary<int, string> {{0, string.Empty}} },
                {1, new Dictionary<int, string> {  {0, "ONE" } } },
                {2, new Dictionary<int, string> {  {0, "TWO" }, { 1, "TWENTY" } } },
                {3, new Dictionary<int, string> {  {0, "THREE" }, { 1, "THIRTY" } } },
                {4, new Dictionary<int, string> {  {0, "FOUR" }, { 1, "FOURTY" } } },
                {5, new Dictionary<int, string> {  {0, "FIVE" }, { 1, "FIFTY" } } },
                {6, new Dictionary<int, string> {  {0, "SIX" }, { 1, "SIXTY" } } },
                {7, new Dictionary<int, string> {  {0, "SEVEN" }, { 1, "SEVENTY" } } },
                {8, new Dictionary<int, string> {  {0, "EIGHT" }, { 1, "EIGHTY" } } },
                {9, new Dictionary<int, string> {  {0, "NINE" }, { 1, "NINETY" } } },
                {10, new Dictionary<int, string> {  {0, "TEN" }} },
                {11, new Dictionary<int, string> {  {0, "ELEVEN" }} },
                {12, new Dictionary<int, string> {  {0, "TWELVE" }} },
                {13, new Dictionary<int, string> {  {0, "THIRTEEN" }} },
                {14, new Dictionary<int, string> {  {0, "FOURTEEN" }} },
                {15, new Dictionary<int, string> {  {0, "FIFTHTEEN" }} },
                {16, new Dictionary<int, string> {  {0, "SIXTEEN" }} },
                {17, new Dictionary<int, string> {  {0, "SEVENTEEN" }} },
                {18, new Dictionary<int, string> {  {0, "EIGHTEEN" }} },
                {19, new Dictionary<int, string> {  {0, "NINETEEN" }} },
            };

            depthStagesData = new Dictionary<int, string>
            {
                {3, "THOUSAND"},
                {4, "MILLION"},
                {5, "BILLION"},
                {6, "TRILLION"},
                {7, "QUADRILLION"},
                {8, "QUINTILLION"},
                {9, "SEXTILLION"},
                {10, "SEPTILLION"},
                {11, "OCTILLION"},
                {12, "NONILLION"},
                {13, "DECILLION"},
                {14, "UNDECILLION"},
            };
        }

        /// <summary>
        /// Prepares the inputted number to give to TranslateNumberToWord() and formats the final result to 
        /// return 
        /// </summary>
        /// <param name="numberInputed">the number the user inputted</param>
        public string StartTranslatingInputtedValue(decimal number)
        {
            number = Math.Round(number, 2); //round number to 2 decimal places at minimum

            //setting up the number for the algorithm by splitting it and clearing any unecessary symbols 
            string numberToString = number.ToString();

            numberToString = Regex.Replace(numberToString, @"-+", ""); //rid of any foreign symbol 

            string[] split = numberToString.Split('.', 2);
            string finalTranslatedNumber = "";

            string centsResult = string.Empty;
            string dollarResult = string.Empty;

            string[] translatedNumber = { "* DOLLARS", "* CENTS"};

            //if the user inserted no cents then it will be caught by the try catch and then only execute the dollars 
            try
            {
                centsResult = TranslateNumberToWord(1, ref split[1]).TrimEnd();
                dollarResult = TranslateNumberToWord(1, ref split[0]).TrimEnd();

                translatedNumber[1] = translatedNumber[1].Replace("*", centsResult);

                translatedNumber[0] = translatedNumber[0].Replace("*", dollarResult);
            }
            catch (IndexOutOfRangeException e)
            {
                dollarResult = TranslateNumberToWord(1, ref split[0]);
                translatedNumber[0] = translatedNumber[0].Replace("*", dollarResult);               
            }

            bool isValidCents = !string.IsNullOrEmpty(centsResult);
            bool isValidDollars = !string.IsNullOrEmpty(dollarResult);


            bool hasBothCentsAndDollars = isValidCents && isValidDollars;


            //based on the results what format will be chosen and which 
            if (hasBothCentsAndDollars)
            {
                finalTranslatedNumber = $"{translatedNumber[0]} AND {translatedNumber[1]}";
            }
            else if(isValidDollars) 
            {
                finalTranslatedNumber = $"{translatedNumber[0]}";
            }
            else if (isValidCents)
            {
                finalTranslatedNumber = $"{translatedNumber[1]}";
            }
            else
            {
                finalTranslatedNumber = "";
            }

            return finalTranslatedNumber;
        }

        /// <summary>
        /// given the pool of numbers in a string recursively break down the number into sets of threes and translate them to their word form
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="mainNumberPool"></param>
        /// <returns></returns>
        private string TranslateNumberToWord(int depth, ref string mainNumberPool)
        {
            string localTranslatedNumber = string.Empty;

            //increase depth
            depth++;

            //if the number pool is empty return
            if (mainNumberPool == string.Empty)
            {
                return string.Empty;
            }

            //extract next set of numbers from the pool
            string numberSet = ExtractNextSetOfNumbers(ref mainNumberPool, depth);

            //go down deeper and get the next set of numbers 
            string recursedTransResult = TranslateNumberToWord(depth, ref mainNumberPool);

            string numberSetMadeIntoWord = MakeNumberWord(numberSet);

            //translate each number to their corrosponding position
            localTranslatedNumber = $"{recursedTransResult} {numberSetMadeIntoWord}";
            string finalResult = localTranslatedNumber.TrimStart();
            finalResult = finalResult.TrimEnd();

            //add the necessary suffix based on the depth 
            if(depthStagesData.TryGetValue(depth, out string numberStageType) && numberSetMadeIntoWord != string.Empty)
            {
                finalResult += $" {numberStageType}";
            }
            

            return finalResult;
        }

        /// <summary>
        /// extracts three numbers and removes them from the numberpool
        /// </summary>
        /// <param name="numberPool">collection of numbers within a string</param>
        /// <param name="depth">how deep recursively has the algorithm tranversed through. Every time the algorithm recurses it adds 1 to the depth</param>
        /// <returns>a string at a max length of 3 containing the number pulled from the number pool</returns>
        private string ExtractNextSetOfNumbers(ref string numberPool, int depthCounter)
        {
            int numberLength = numberPool.Length;

            //if the total pool left is less or equal to three just return the pool
            if (numberLength <= 3)
            {
                string nextSet = numberPool;
                numberPool = string.Empty; 
                return nextSet;
            }

            //extract the 3 end numbers from the number pool
            int numberExtractEndPoint = numberLength - 3;

            string extractedNumber = string.Empty;

            for (int i = numberLength - 1; i > numberExtractEndPoint - 1; i--)
            {
                extractedNumber = numberPool[i] + extractedNumber; //adds each number to the front of the string due to working from back to front
                numberPool = numberPool.Remove(i); //removes the number from the overall pool
            }

            return extractedNumber;
        }


        /// <summary>
        /// translate up to three separate numbers into the necessary format of hundreds, tens and units e.g. 123 => ONE HUNDRED AND TWENTY-THREE
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string MakeNumberWord(string? number)
        {
            int numberLength = number.Length;

            if(numberLength == 1 && int.Parse(number) == 0)
            {
                return string.Empty;
            }

            string numberTranslatedToWord = string.Empty;
            string[] inidivTranslatedNumbers = new string[3];

            bool isInHundreds = numberLength == 3;
            string tenthUnitValueSeparated = number;

            //when we parse through a 3 number set then we include 
            if (isInHundreds)
            {
                inidivTranslatedNumbers[0] = ExtractWordTranslationFromNumber(number[0], NumberTypes.hundredth);
                tenthUnitValueSeparated = tenthUnitValueSeparated.Remove(0, 1);
            }


            int tenUnitValue = int.Parse(tenthUnitValueSeparated); //parse the rest of the number into a int value

            //for single digit or any number between 10 - 19
            if (numberWordsData.TryGetValue(tenUnitValue, out Dictionary<int, string>? result))
            {
                //is the first value 0? if so then get the unit word otherwise get the tenth word
                inidivTranslatedNumbers[2] = result[0]; 

                numberTranslatedToWord = PieceNumberTranslationsTogether(inidivTranslatedNumbers);

                return numberTranslatedToWord;
            }

            inidivTranslatedNumbers[1] = ExtractWordTranslationFromNumber(tenthUnitValueSeparated[0], NumberTypes.tenth);
            inidivTranslatedNumbers[2] = ExtractWordTranslationFromNumber(tenthUnitValueSeparated[1], NumberTypes.unit);

            //piece together each translated number into a single string
            numberTranslatedToWord = PieceNumberTranslationsTogether(inidivTranslatedNumbers);

            return numberTranslatedToWord;
        }

        /// <summary>
        /// format together a single string with the given string collection of individual translated numbers 
        /// </summary>
        /// <param name="numbersCollected">a group individual string translations of numbers</param>
        /// <returns>a single string joining all string in the array</returns>
        private string PieceNumberTranslationsTogether(string[] numbersCollected)
        {
            string finalNumberWordTranslated = string.Empty;
            string[] suffixToApplyAfterEachNumberType = new string[2] { " AND ", "-" };

            int endpoint = numbersCollected.Length;

            for (int i = 0; i < endpoint; i++)
            {
                //dont add to the final result if its null or empty
                if (string.IsNullOrEmpty(numbersCollected[i]))
                {
                    continue;
                }

                finalNumberWordTranslated += numbersCollected[i];

                bool isFinalElement = endpoint - i == 0;

                //add necessary suffix only if its not the last element
                if (!isFinalElement && i < suffixToApplyAfterEachNumberType.Length && numbersCollected[i + 1] != string.Empty)
                {
                    finalNumberWordTranslated += suffixToApplyAfterEachNumberType[i];
                }
            }


            return finalNumberWordTranslated;
        }

        /// <summary>
        /// given the character and what type of number it is (unit, tenth, or hundredth) find its word translation from the numberWordsData Dictionary
        /// </summary>
        /// <param name="number">a single digit number from the set</param>
        /// <param name="unitSymbolType">the type of number that char is whether its a unit, tenth, or hundredth</param>
        /// <returns>a string that represents the number in word form</returns>
        private string ExtractWordTranslationFromNumber(char number, NumberTypes unitSymbolType)
        {
            int unitValue = (int)char.GetNumericValue(number);

            if (unitValue == 0)
            {
                return string.Empty;
            }

            switch (unitSymbolType)
            {
                case NumberTypes.unit:
                    return numberWordsData[unitValue][0];
                case NumberTypes.tenth:
                    return numberWordsData[unitValue][1];
                case NumberTypes.hundredth:
                    return $"{numberWordsData[unitValue][0]} HUNDRED";
                default:
                    Console.Error.WriteLine("Could not find the translation for the number character");
                    return "";
            }
        }
    }
}
