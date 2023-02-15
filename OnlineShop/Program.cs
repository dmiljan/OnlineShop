using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Configuration;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;
using OnlineShop.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>();
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key: "JwtConfig:Secret").Value);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = false,
    };
});

builder.Services.AddScoped<IProductType, ProductTypeService>();
builder.Services.AddScoped<IAttribute, AttributeService>();
builder.Services.AddScoped<IProductTypeAttribute, ProductTypeAttributeService>();
builder.Services.AddScoped<IAttributeValue, AttributeValueService>();
builder.Services.AddScoped<IProductTypeAttributeValue, ProductTypeAttributeValueService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
    context.Seed();
}


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
