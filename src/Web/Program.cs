var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging()){}
    app.UseDeveloperExceptionPage();

app.Run();
