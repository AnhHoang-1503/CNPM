using CNPM;
using CNPM.Domain;
using CNPM.Domain.Common;
using CNPM.Domain.Interface.Repository;
using CNPM.Domain.Service;
using CNPM.Infrastructure;
using CNPM.Infrastructure.Repository;
using CNPM.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration["ConnectionString"];
var serverVersion = new MariaDbServerVersion(new Version(10, 5, 23));


// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Connect DB, create dbcontext for efcore
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(conectionString, serverVersion)
    );

// User DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService, UserService>();

// Person DI
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<PersonService, PersonService>();

// Family DI
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<FamilyService, FamilyService>();

// TemporaryPaper DI
builder.Services.AddScoped<ITemporaryPaperRepository, TemporaryPaperRepository>();
builder.Services.AddScoped<TemporaryPaperService, TemporaryPaperService>();

// Fee DI
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<FeeService, FeeService>();

// Family Fee mapping DI
builder.Services.AddScoped<IFamyliFeeMappingRepository, FamilyFeeMappingRepository>();

// Auth DI
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<LoginService, LoginService>();

builder.Services.AddScoped<UserSessionState, UserSessionState>();

// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.Events = new JwtBearerEvents()
    {
        OnTokenValidated = context =>
        {
            var userSessionState = context.HttpContext.RequestServices.GetRequiredService<UserSessionState>();

            var claims = context.Principal?.Claims;

            var userName = claims?.FirstOrDefault(c => c.Type == "user_name");

            var userId = claims?.FirstOrDefault(c => c.Type == "user_id");

            var userRole = claims?.FirstOrDefault(c => c.Type == "user_role");


            if (userName != null)
            {
                userSessionState.User_Name = userName.Value;
            }

            if (userId != null)
            {
                userSessionState.User_Id = int.Parse(userId.Value);
            }

            if (userRole != null)
            {
                userSessionState.Role = (Role)Enum.Parse(typeof(Role), userRole.Value);
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<Middleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
