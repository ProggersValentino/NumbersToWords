namespace NumbersToWords
{
    public class NTWContainer
    {
        //to store the word conversions of a number
        Dictionary<int, Dictionary<int, string>>? numberWordsData;

        /// <summary>
        /// used to determine what suffix to add at the end of stage depending how deep in the recurrsion the algorithm
        /// </summary>
        Dictionary<int, string>? depthStagesData;

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
                {0, "CENTS"},
                {1, "DOLLARS"},
                {2, "THOUSAND"},
                {3, "MILLION"},
                {4, "BILLION"},
            };
        }

        /// <summary>
        /// starts the initial process to activate the recurrsion
        /// </summary>
        /// <param name="numberInputed"></param>
        void MainAlgo(string numberInputed)
        {
            //split decimal 
            //
        }

        //method to do the main recursion 
    }
}
