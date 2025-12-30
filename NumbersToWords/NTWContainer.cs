namespace NumbersToWords
{
    /// <summary>
    /// A class holding the necessary data and utility functions to translate a number to its word form
    /// </summary>
    public class NTWContainer
    {
        //to store the word conversions of a number
        Dictionary<int, Dictionary<int, string>>? numberWordsData;

        /// <summary>
        /// used to determine what suffix to add at the end of stage depending how deep in the recurrsion the algorithm
        /// </summary>
        Dictionary<int, string>? depthStagesData;

       

        enum NumberTypes
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
                {15, new Dictionary<int, string> {  {0, "FIFTEEN" }} },
                {16, new Dictionary<int, string> {  {0, "SIXTEEN" }} },
                {17, new Dictionary<int, string> {  {0, "SEVENTEEN" }} },
                {18, new Dictionary<int, string> {  {0, "EIGHTEEN" }} },
                {19, new Dictionary<int, string> {  {0, "NINETEEN" }} },
            };

            depthStagesData = new Dictionary<int, string>
            {
                {1, "CENTS"},
                {2, "DOLLARS"},
                {3, "THOUSAND"},
                {4, "MILLION"},
                {5, "BILLION"},
            };
        }

        /// <summary>
        /// starts the initial process to activate the recurrsion
        /// </summary>
        /// <param name="numberInputed"></param>
        public string mainAlgo(float number)
        {
            string numberToString = number.ToString();  

            string[] split = numberToString.Split('.', 2);
            string finalTranslatedNumber = "";

            string[] translatedNumber = new string[2];

            //if the user inserted no cents then it will be caught by the try catch and then only execute the dollars 
            try
            {
                translatedNumber[1] = TranslateNumberToWord(0, ref split[1]);
                translatedNumber[0] = TranslateNumberToWord(1, ref split[0]);
            }
            catch (IndexOutOfRangeException e)
            {
                translatedNumber[0] = TranslateNumberToWord(1, ref split[0]);
            }

            //to ensure we have control over the formatting 
            for (int i = 0; i < translatedNumber.Length; i++)
            {
                if (translatedNumber[i] == null || translatedNumber[i] == string.Empty)
                {
                    continue;
                }

                finalTranslatedNumber += translatedNumber[i];

                if (i + 1 != translatedNumber.Length - 1)
                {
                    finalTranslatedNumber += " AND ";
                }

            }

            return finalTranslatedNumber;
        }

        //method to do the main recursion 
        string TranslateNumberToWord(int depth, ref string mainNumberPool)
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

            //translate each number to their corrosponding position
            localTranslatedNumber = $"{recursedTransResult} {MakeNumberWord(numberSet)}";

            //add the necessary suffix based on the depth 
            localTranslatedNumber += $" {depthStagesData[depth]}";

            return localTranslatedNumber;
        }

        /// <summary>
        /// return a string at a max length of 3
        /// </summary>
        /// <param name="numberPool"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        string ExtractNextSetOfNumbers(ref string numberPool, int depthCounter)
        {
            int numberLength = numberPool.Length;

            if (numberLength <= 3)
            {
                string nextSet = numberPool;
                numberPool = string.Empty; //
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


        string MakeNumberWord(string? number)
        {
            int numberLength = number.Length;

            string numberTranslatedToWord = string.Empty;
            string[] inidivTranslatedNumbers = new string[3];


            bool isInHundreds = numberLength == 3;
            string tenthUnitValueSeparated = number;

            //when we parse through a 3 number set then we include 
            if (isInHundreds)
            {
                inidivTranslatedNumbers[0] = ExtractUnitNumberWord(number[0], NumberTypes.hundredth);
                tenthUnitValueSeparated = tenthUnitValueSeparated.Remove(0, 1);
            }


            int tenUnitValue = Int32.Parse(tenthUnitValueSeparated); //parse the rest of the number into a int value 

            //for single digit or any number between 10 - 19
            if (numberWordsData.TryGetValue(tenUnitValue, out Dictionary<int, string>? result))
            {
                inidivTranslatedNumbers[2] = result[0];

                numberTranslatedToWord = PieceNumberWordTogether(inidivTranslatedNumbers);

                return numberTranslatedToWord;
            }

            inidivTranslatedNumbers[1] = ExtractUnitNumberWord(number[1], NumberTypes.tenth);

            inidivTranslatedNumbers[2] = ExtractUnitNumberWord(number[2], NumberTypes.unit);

            numberTranslatedToWord = PieceNumberWordTogether(inidivTranslatedNumbers);

            return numberTranslatedToWord;
        }

        string PieceNumberWordTogether(string[] numbersCollected)
        {
            string finalNumberWordTranslated = string.Empty;
            string[] suffixToApplyAfterEachNumberType = new string[2] { " AND ", "-" };

            int endpoint = numbersCollected.Length;

            for (int i = 0; i < endpoint; i++)
            {
                if (numbersCollected[i] == null)
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

        string ExtractUnitNumberWord(char number, NumberTypes unitSymbolType)
        {
            int unitValue = (int)Char.GetNumericValue(number);

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
                    return "Cant find translatin";
            }
        }
    }
}
