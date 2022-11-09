using ProjectControlAPI.BusinessLogic.Services.Extensions;
using ProjectControlAPI.Common.Mapping;
using ProjectControlAPI.DataAccess;
using ProjectControlAPI.Presentation;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddService();
builder.Services.AddAutoMapper();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Director", builder =>
    {
        builder.RequireRole(ClaimTypes.Role, "Director");
    });

    options.AddPolicy("DirectorOrPM", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Director")
                                    || x.User.HasClaim(ClaimTypes.Role, "ProjectManager"));
    });

    options.AddPolicy("AllWorkers", builder =>
    {
        builder.RequireAuthenticatedUser(); 
        //builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Director")
        //                            || x.User.HasClaim(ClaimTypes.Role, "ProjectManager"));
    });
}); 

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

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
