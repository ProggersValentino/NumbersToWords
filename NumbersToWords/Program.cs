using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NumbersToWords;
using NumbersToWords.WebMinRouteGroup;

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

app.MapGet("/nums/{id}", (int id, NumToWordDb db) => NTWEndpoints.GetNTW(id, db));

app.MapPost("/numspost", (NumToWord ntw, NumToWordDb db) => NTWEndpoints.PostNewNTW(ntw, db));

app.MapPut("/nums/{id}", (int id, NumToWord ntw, NumToWordDb db) => NTWEndpoints.TranslateNumberToWord(id, ntw, db));

app.Run();

