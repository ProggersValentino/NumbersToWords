using NumbersToWords;
using NumberToWordsTests.UnitTestHelpers;
using NumbersToWords.WebMinRouteGroup;


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

            NumToWord ntw = new NumToWord(1, 0.15f, "");

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

            NumToWord ntw = new NumToWord(1, 5.3e10, "");

            var result = await NTWEndpoints.PostNewNTW(ntw, context);

            NumToWord translationResult = await NTWEndpoints.GetNTW(1, context);

            string expectedTranslatedResult = "ONE HUNDRED DOLLARS";

            Assert.AreEqual(expectedTranslatedResult, translationResult.NumConvertedOutput, "Incorrect translation for hundred dollars only");
        }
    }
}
