using KnowledgeTest.Commands;
using KnowledgeTest.Models;
using KnowledgeTest.Queries;
using KnowledgeTest.Repositorys;
using Oakton;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseWolverine();

builder.Services.AddSingleton<ICandidateRepository,CandidateRepository>();

var app = builder.Build();

app.MapPost("/candidates/create", async (CreateCandidate body, IMessageBus bus) => await bus.InvokeAsync(body))
     .WithOpenApi();
app.MapGet("/candidates/getAll", async (IMessageBus bus) => await bus.InvokeAsync<IEnumerable<Candidate>>(new GetAllCandidate()));

app.MapGet("/candidates/get{id}",async (Guid id,IMessageBus bus) => await bus.InvokeAsync(new GetByIdCandidate(Id: id)));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();



//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.Run();
return await app.RunOaktonCommands(args);
