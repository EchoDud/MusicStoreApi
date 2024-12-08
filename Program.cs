using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����������� �������� ��� Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

// ����������� � SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        scope.ServiceProvider.GetRequiredService<IProductService>()
    );
    await seeder.SeedDataAsync();
}

app.Run();
