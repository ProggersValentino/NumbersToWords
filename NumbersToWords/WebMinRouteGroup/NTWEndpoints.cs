


namespace NumbersToWords.WebMinRouteGroup
{
    public static class NTWEndpoints
    {
        public static async Task<IResult> PostNewNTW(NumToWord ntw, NumToWordDb db)
        {
            db.NumToWords.Add(ntw);
            await db.SaveChangesAsync();

            return Results.Created($"/numspost/{ntw.Id}", ntw);
        }

        public static async Task<NumToWord> GetNTW(int ntwID, NumToWordDb db)
        {
            NumToWord? ntw = db.NumToWords.Find(ntwID);

            /*if(ntw.NumConvertedOutput == string.Empty)
            {*/
            //activate algorithm  
            NTWContainer ntwContainer = new NTWContainer();
            string translatedNumber = ntwContainer.mainAlgo(ntw.NumInput);

            ntw.NumConvertedOutput = translatedNumber;
            db.SaveChanges();
            //}

            return ntw;
        }

        public static async Task<IResult> TranslateNumberToWord(int id, NumToWord inputNtw, NumToWordDb db)
        {
            var ntw = await db.NumToWords.FindAsync(id);

            if (ntw is null) return Results.NotFound();

            ntw.NumInput = inputNtw.NumInput;
            ntw.NumConvertedOutput = inputNtw.NumConvertedOutput;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
