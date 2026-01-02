namespace NumbersToWords.WebMinRouteGroup.data
{
    public class NumToWord
    {
        public NumToWord(int id, decimal numInput, string? numConvertedOutput)
        {
            Id = id;
            NumInput = numInput;
            NumConvertedOutput = numConvertedOutput;
        }

        public int Id { get; set; }
        public decimal NumInput { get; set; }
        public string? NumConvertedOutput { get; set; }
    }
}
