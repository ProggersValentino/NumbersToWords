using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NumbersToWords;

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

    //activate algorithm  

    return ntw;
}

async Task<string> TranslateNumberToWord()
{
    


    return "";
}