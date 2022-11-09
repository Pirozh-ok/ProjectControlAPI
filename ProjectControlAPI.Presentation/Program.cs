using ProjectControlAPI.BusinessLogic.Services.Extensions;
using ProjectControlAPI.Common.Mapping;
using ProjectControlAPI.Presentation;
using ProjectControlAPI.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddService();
builder.Services.AddAutoMapper();

builder.Services.AddOptionsAutorization();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
