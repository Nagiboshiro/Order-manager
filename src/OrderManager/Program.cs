using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using OrderManager.Application;
using OrderManager.DataAccess.Postgres;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("postgres")!;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPostgresSqlContext(connectionString);
builder.Services.AddPostgresSqlDataAccessSchemaMigrator(connectionString);

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5018")
                .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});

builder.Services.AddMediatR(typeof(ApplicationErrors));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();