using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Mapping.Config;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


/*Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/villalogs.txt",rollingInterval:RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog(); */


// Add services to the container.
builder.Services.AddAutoMapper(typeof(ModelMapConfig));
builder.Services.AddDbContext<VillaDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));
builder.Services.AddControllers(option =>
{
   // option.ReturnHttpNotAcceptable = true; 
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); //accpet xml and json data too based on request accept type header
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
