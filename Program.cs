using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������� ��� Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();

// ����������� � SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������� JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular ������
              .AllowAnyMethod()                    // ��������� ��� ������
              .AllowAnyHeader();                   // ��������� ��� ���������
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// �����: UseCors ������ ���� ������ �� UseRouting
app.UseCors("AllowAngularApp");

app.UseRouting();

// ����������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

// ������������ ��������
app.MapControllers();

// �������� ���������� ������
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // ������� � ���������� ���� ������
    context.Database.EnsureDeleted(); // ������� ������������ ����
    context.Database.EnsureCreated(); // ������ ����� ���� ������
}

using (var scope = app.Services.CreateScope())
{
    var seeder = new TestDataSeeder(
        scope.ServiceProvider.GetRequiredService<ICategoryService>(),
        scope.ServiceProvider.GetRequiredService<IProductService>(),
        scope.ServiceProvider.GetRequiredService<IUserService>()
    );
    await seeder.SeedDataAsync();
}

app.Run();