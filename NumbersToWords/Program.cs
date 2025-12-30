using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NumbersToWords;

NTWContainer ntwContainer = new NTWContainer();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Look for static files in webroot
    WebRootPath = "webroot"
});

builder.Services.AddDbContext<NumToWordDb>(opt => opt.UseInMemoryDatabase("NTWList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

//configuring the program to use the wwwroot files for the frontend
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/nums", async (NumToWordDb db) => 
await db.NumToWords.ToListAsync());

app.MapGet("/nums/{id}", (int id, NumToWordDb db) => GetNTW(id, db));

app.MapPost("/numspost", (NumToWord ntw, NumToWordDb db) => PostNewNTW(ntw, db));

app.MapPut("/nums/{id}", (int id, NumToWord ntw, NumToWordDb db) => TranslateNumberToWord(id, ntw, db));

app.Run();

async Task<IResult> PostNewNTW(NumToWord ntw, NumToWordDb db)
{
    db.NumToWords.Add(ntw);
    await db.SaveChangesAsync();

    return Results.Created($"/numspost/{ntw.Id}", ntw);
}

async Task<NumToWord> GetNTW(int ntwID, NumToWordDb db)
{
    NumToWord? ntw = db.NumToWords.Find(ntwID);

    /*if(ntw.NumConvertedOutput == string.Empty)
    {*/
        //activate algorithm  
        string translatedNumber = ntwContainer.mainAlgo(ntw.NumInput);

        ntw.NumConvertedOutput = translatedNumber;
        db.SaveChanges();
    //}
    
    return ntw;
}

async Task<IResult> TranslateNumberToWord(int id, NumToWord inputNtw, NumToWordDb db)
{
    var ntw = await db.NumToWords.FindAsync(id);

    if (ntw is null) return Results.NotFound();

    ntw.NumInput = inputNtw.NumInput;
    ntw.NumConvertedOutput = inputNtw.NumConvertedOutput;

    await db.SaveChangesAsync();

    return Results.NoContent();
}