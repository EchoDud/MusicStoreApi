using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов для Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Подключение к SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular клиент
              .AllowAnyMethod()                    // Разрешить все методы
              .AllowAnyHeader();                   // Разрешить все заголовки
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Важно: UseCors должен быть вызван ДО UseRouting
app.UseCors("AllowAngularApp");

app.UseRouting();

// Регистрируем маршруты
app.MapControllers();


// Тестовое заполнение данных


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Удаляем и пересоздаём базу данных
    context.Database.EnsureDeleted(); // Удаляет существующую базу
    context.Database.EnsureCreated(); // Создаёт новую базу данных
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
