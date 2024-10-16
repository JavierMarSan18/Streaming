var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWeb(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging()){}
    app.UseDeveloperExceptionPage();

app.UseWeb();
app.MapGraphQL();
app.Run();
// app.RunWithGraphQLCommands(args);