using Essence_Link_API.Models;
using Essence_Link_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("BaseAccess",
        policy =>
        {
            policy.WithOrigins("*"); //Nedd to change later
        }
        );
});

builder.Services.Configure<ELDatabaseSettings>(
    builder.Configuration.GetSection("ELDatabase"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProductTypeService>();
builder.Services.AddSingleton<ProductPictureService>();
builder.Services.AddSingleton<ReviewService>();
builder.Services.AddSingleton<CommandService>();
builder.Services.AddSingleton<CommandProductService>();
builder.Services.AddSingleton<ProductService>();

builder.Services.AddControllers()
    .AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

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

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
