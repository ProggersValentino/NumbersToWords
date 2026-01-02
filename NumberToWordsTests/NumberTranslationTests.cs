using NumberToWordsTests.UnitTestHelpers;
using NumbersToWords.WebMinRouteGroup;
using NumbersToWords.WebMinRouteGroup.data;


namespace NumberToWordsTests
{
    [TestClass]
    public sealed class NumberTranslationTests
    {
        [TestMethod]
        public async Task NTW_CentsJustInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 0.15m, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResponse = "FIFTHTEEN CENTS";

            Assert.AreEqual(expectedTranslatedResponse, translationResult.NumConvertedOutput, "Incorrect translation for cents only");

        }

        [TestMethod]
        public async Task NTW_DollarsJustInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 16, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "SIXTEEN DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for dollars only");
        }

        [TestMethod]
        public async Task NTW_HundredsInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 100, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "ONE HUNDRED DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for hundred dollars only");
        }

        [TestMethod]
        public async Task NTW_ExcessZerosInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 00003, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "THREE DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for number with excess zeros");
        }

        [TestMethod]
        public async Task NTW_ThousandInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 1000, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "ONE THOUSAND DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for hundred dollars only");
        }

        [TestMethod]
        public async Task NTW_EToThePowerOfInputted()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 5.3e10m, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "FIFTY-THREE BILLION DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for e notational numbers");
        }

        [TestMethod]
        public async Task NTW_MaxPossibleNumber()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, decimal.MaxValue, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "SEVENTY-NINE OCTILLION TWO HUNDRED AND TWENTY-EIGHT SEPTILLION ONE HUNDRED " +
                "AND SIXTY-TWO SEXTILLION FIVE HUNDRED AND FOURTEEN QUINTILLION TWO HUNDRED AND SIXTY-FOUR QUADRILLION THREE " +
                "HUNDRED AND THIRTY-SEVEN TRILLION FIVE HUNDRED AND NINETY-THREE BILLION FIVE HUNDRED AND FOURTY-THREE MILLION NINE HUNDRED AND " +
                "FIFTY THOUSAND THREE HUNDRED AND THIRTY-FIVE DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for max number");
        }

        [TestMethod]
        public async Task NTW_SmallestPositiveNumber()
        {
            //setting environment up
            await using var context = new MockDb().CreateDbContext();

            NumToWord ntw = new NumToWord(1, 0.00000000000000001m, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation cents with no zeros proceeding the number");
        }

    }
}
