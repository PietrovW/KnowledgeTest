using KnowledgeTest.Commands;
using KnowledgeTest.DAL.DataAccess;
using KnowledgeTest.Models;
using KnowledgeTest.Options;
using KnowledgeTest.Queries;
using KnowledgeTest.Repositorys;
using Oakton;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DatabaseNeo4jOptions>(
    builder.Configuration.GetSection(DatabaseNeo4jOptions.DatabaseNeo4j));
builder.Configuration.AddEnvironmentVariables(prefix: "KnowledgeTest_");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
    opts.UseFluentValidation(RegistrationBehavior.ExplicitRegistration);
});
builder.Services.AddScoped<INeo4jDataAccess, Neo4jDataAccess>();
builder.Services.AddTransient<ICandidateRepository, CandidateRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<ITestRepository, TestRepository>();
var app = builder.Build();

app.MapPost("/candidates", async (CreateCandidate body, IMessageBus bus) =>
{
    await bus.InvokeAsync(body);
    return Results.Created($"/candidates/{body.Id}", body);
}).WithOpenApi();
app.MapGet("/candidates/", async (IMessageBus bus) => await bus.InvokeAsync<IEnumerable<Candidate>>(new GetAllCandidate())
is IEnumerable<Candidate> candidates
         ? Results.Ok(candidates)
         : Results.NotFound())
     .Produces<Candidate>(StatusCodes.Status200OK)
   .Produces(StatusCodes.Status404NotFound);

app.MapGet("/candidates/{id}", async (Guid id, IMessageBus bus) => await bus.InvokeAsync<Candidate>(new GetByIdCandidate(Id: id)) is Candidate item
            ? Results.Ok(item)
            : Results.NotFound())
     .Produces<Candidate>(StatusCodes.Status200OK)
   .Produces(StatusCodes.Status404NotFound)
    .WithOpenApi();

app.MapPost("/questions", async (CreateQuestion body, IMessageBus bus) =>
{
    await bus.InvokeAsync(body);
    return Results.Created($"/questions/{body.Id}", body);
}).Produces<CandidateCreated>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest).WithOpenApi();

app.MapGet("/questions/{id}", async (Guid id, IMessageBus bus) =>
 await bus.InvokeAsync<Question>(new GetByIdQuestion(Id: id)) is Question item
            ? Results.Ok(item)
            : Results.NotFound())
     .Produces<Candidate>(StatusCodes.Status200OK)
   .Produces(StatusCodes.Status404NotFound)
    .WithOpenApi();
app.MapGet("/questions/", async (IMessageBus bus) => await bus.InvokeAsync<IEnumerable<Question>>(new GetAllQuestion()));


app.MapPost("/tests", async (CreateTest body, IMessageBus bus) =>
{
    await bus.InvokeAsync(body);
    return Results.Created($"/tests/{body.Id}", body);
}).WithOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

return await app.RunOaktonCommands(args);
