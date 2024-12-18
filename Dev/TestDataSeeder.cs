using MusicStoreApi.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class TestDataSeeder
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IUserService _userService;
    private readonly IVisitStatService _visitStatService;

    public TestDataSeeder(ICategoryService categoryService, IProductService productService, IUserService userService, IVisitStatService visitStatService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _userService = userService;
        _visitStatService = visitStatService;
    }

    public async Task SeedDataAsync()
    {
        // Проверяем, что база пустая
        var categories = await _categoryService.GetCategoriesAsync();
        var products = await _productService.GetProductsAsync();

        if (categories.Any() || products.Any())
        {
            Console.WriteLine("База данных уже содержит данные. Удалите их вручную перед заполнением.");
            return;
        }

        // Создаём категории
        var stringsCategory = new Category { Name = "Струнные", ParentId = null };
        await _categoryService.AddCategoryAsync(stringsCategory);

        var guitarsCategory = new Category { Name = "Гитары", ParentId = stringsCategory.Id };
        await _categoryService.AddCategoryAsync(guitarsCategory);

        var acousticGuitarsCategory = new Category { Name = "Акустические гитары", ParentId = guitarsCategory.Id };
        await _categoryService.AddCategoryAsync(acousticGuitarsCategory);

        var electricGuitarsCategory = new Category { Name = "Электрогитары", ParentId = guitarsCategory.Id };
        await _categoryService.AddCategoryAsync(electricGuitarsCategory);

        var bassGuitarsCategory = new Category { Name = "Бас-гитары", ParentId = guitarsCategory.Id };
        await _categoryService.AddCategoryAsync(bassGuitarsCategory);

        var orchestralStringsCategory = new Category { Name = "Смычковые", ParentId = stringsCategory.Id };
        await _categoryService.AddCategoryAsync(orchestralStringsCategory);

        var violinsCategory = new Category { Name = "Скрипки", ParentId = orchestralStringsCategory.Id };
        await _categoryService.AddCategoryAsync(violinsCategory);

        var cellosCategory = new Category { Name = "Виолончели", ParentId = orchestralStringsCategory.Id };
        await _categoryService.AddCategoryAsync(cellosCategory);

        var keyboardsCategory = new Category { Name = "Клавишные", ParentId = null };
        await _categoryService.AddCategoryAsync(keyboardsCategory);

        var pianosCategory = new Category { Name = "Пианино", ParentId = keyboardsCategory.Id };
        await _categoryService.AddCategoryAsync(pianosCategory);

        var synthesizersCategory = new Category { Name = "Синтезаторы", ParentId = keyboardsCategory.Id };
        await _categoryService.AddCategoryAsync(synthesizersCategory);

        var drumsCategory = new Category { Name = "Ударные", ParentId = null };
        await _categoryService.AddCategoryAsync(drumsCategory);

        var drumKitsCategory = new Category { Name = "Барабанные установки", ParentId = drumsCategory.Id };
        await _categoryService.AddCategoryAsync(drumKitsCategory);

        var percussionCategory = new Category { Name = "Перкуссия", ParentId = drumsCategory.Id };
        await _categoryService.AddCategoryAsync(percussionCategory);

        var windInstrumentsCategory = new Category { Name = "Духовые", ParentId = null };
        await _categoryService.AddCategoryAsync(windInstrumentsCategory);

        var flutesCategory = new Category { Name = "Флейты", ParentId = windInstrumentsCategory.Id };
        await _categoryService.AddCategoryAsync(flutesCategory);

        var saxophonesCategory = new Category { Name = "Саксофоны", ParentId = windInstrumentsCategory.Id };
        await _categoryService.AddCategoryAsync(saxophonesCategory);

        var brassCategory = new Category { Name = "Медные духовые", ParentId = windInstrumentsCategory.Id };
        await _categoryService.AddCategoryAsync(brassCategory);

        Console.WriteLine("Категории созданы.");

        // Добавляем товары для акустических гитар
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "F310",
            Color = "Естественный",
            Description = "Классическая акустическая гитара.",
            Price = 15000,
            ImageUrl = "https://tdm.su/upload/resize_cache/webp/iblock/02d/pe09dsw31c5bdedvkypke1lk0iaa3l2b.webp",
            CategoryId = acousticGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Fender",
            Model = "CD-60",
            Color = "Черный",
            Description = "Стандартная акустическая гитара.",
            Price = 20000,
            ImageUrl = "https://images.musicstore.de/images/1280/fender-cd-60-v3-black-_1_GIT0049881-000.jpg",
            CategoryId = acousticGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Ibanez",
            Model = "AW54",
            Color = "Коричневый",
            Description = "Дредноут с великолепным звучанием.",
            Price = 25000,
            ImageUrl = "https://st-petersburg.pult.ru/upload/iblock/d2e/d2eb29aa5826c833a1f250a216d8e2f6.jpg",
            CategoryId = acousticGuitarsCategory.Id
        });

        // Добавляем товары для электрогитар
        await _productService.AddProductAsync(new Product
        {
            Brand = "Fender",
            Model = "Stratocaster",
            Color = "Cолнечный взрыв",
            Description = "Легендарная электрогитара.",
            Price = 120000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThfWrNaB_23Ap8SoDU8axP4YewKtyaeyz4KQ&s",
            CategoryId = electricGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Gibson",
            Model = "Les Paul",
            Color = "Золотой",
            Description = "Классическая электрогитара.",
            Price = 140000,
            ImageUrl = "https://images.musicstore.de/images/0960/gibson-les-paul-70s-deluxe-gold-top_1_GIT0056344-000.jpg",
            CategoryId = electricGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Ibanez",
            Model = "RG550",
            Color = "Красный",
            Description = "Гитара для металла.",
            Price = 80000,
            ImageUrl = "https://skifmusic.ru/thumbs/31/e4/x_1_normal_c0b6b1b1100f70eb6bed9133ac98.webp",
            CategoryId = electricGuitarsCategory.Id
        });

        // Бас-гитары
        await _productService.AddProductAsync(new Product
        {
            Brand = "Fender",
            Model = "Precision Bass",
            Color = "Белый",
            Description = "Идеальный бас для рок-музыки.",
            Price = 90000,
            ImageUrl = "https://skifmusic.ru/thumbs/8f/64/600x600_1_normal_66c4e4db161660d5bd86a4ade215.webp",
            CategoryId = bassGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Ibanez",
            Model = "SR500E",
            Color = "Коричневый",
            Description = "Современный бас с активной электроникой.",
            Price = 70000,
            ImageUrl = "https://impult.ru/preview/r/456x456/upload/iblock/d1b/d1b94476962165ce2778437b7d432605.jpg",
            CategoryId = bassGuitarsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "TRBX304",
            Color = "Красный",
            Description = "Универсальный бас-гитарный инструмент.",
            Price = 60000,
            ImageUrl = "https://impult.ru/preview/r/456x456/upload/iblock/2d3/2d3bec01e6b3b66b0d27e2be61c1db2a.jpg",
            CategoryId = bassGuitarsCategory.Id
        });

        // Скрипки
        await _productService.AddProductAsync(new Product
        {
            Brand = "Stentor",
            Model = "Student II",
            Color = "Коричневый",
            Description = "Скрипка для начинающих.",
            Price = 15000,
            ImageUrl = "https://audiobe.ru/images/stories/virtuemart/product/resized/8129374_800_0x400.jpg",
            CategoryId = violinsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "V5",
            Color = "Коричневый",
            Description = "Скрипка для студентов.",
            Price = 30000,
            ImageUrl = "https://skifmusic.ru/thumbs/d1/6f/600x600_1_normal_a5a8b88a74ef8df80d959b603f55.webp",
            CategoryId = violinsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Cremona",
            Model = "SV-175",
            Color = "Естественный",
            Description = "Качественная бюджетная скрипка.",
            Price = 20000,
            ImageUrl = "https://skifmusic.ru/thumbs/56/3d/600x600_1_normal_1036aa1a04baa5b467fc8b6602b1.webp",
            CategoryId = violinsCategory.Id
        });

        // Виолончели
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "SVC110",
            Color = "Коричневый",
            Description = "Удобная и прочная виолончель.",
            Price = 80000,
            ImageUrl = "https://skybeat.ru/upload/iblock/04f/yamaha-svc110_img.jpg",
            CategoryId = cellosCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Prima",
            Model = "P-100",
            Color = "Коричневый",
            Description = "Виолончель для начинающих музыкантов.",
            Price = 60000,
            ImageUrl = "https://www.legato.su/upload/iblock/071/dey7x7sl6g4zpb596xl99rwg0k2a5745.jpg",
            CategoryId = cellosCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Eastman",
            Model = "VC80",
            Color = "Естественный",
            Description = "Качественная виолончель для студентов.",
            Price = 100000,
            ImageUrl = "https://ae04.alicdn.com/kf/HTB1.XvFHFXXXXbNapXXq6xXFXXX1.jpg",
            CategoryId = cellosCategory.Id
        });

        // Фортепиано
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "U1",
            Color = "Черный",
            Description = "Классическое акустическое пианино.",
            Price = 300000,
            ImageUrl = "https://love-piano.ru/upload/resize_cache/iblock/513/tixkz2me7771kjhrh9vd5hm361e4wy4i/400_400_1/Yamaha_U1_TA2_1.jpg",
            CategoryId = pianosCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Kawai",
            Model = "K-300",
            Color = "Белый",
            Description = "Акустическое пианино высокого качества.",
            Price = 350000,
            ImageUrl = "https://love-piano.ru/upload/resize_cache/iblock/94d/400_400_1/kawai-k-300.jpg",
            CategoryId = pianosCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Casio",
            Model = "AP-470",
            Color = "Коричневый",
            Description = "Электронное пианино для дома.",
            Price = 120000,
            ImageUrl = "https://love-piano.ru/upload/resize_cache/iblock/036/bn1sg6utd0uwk3yhp2gacvpy5hctyg1u/489_489_1/Casio_Celviano_AP_470_BN_4.jpg",
            CategoryId = pianosCategory.Id
        });

        // Синтезаторы
        await _productService.AddProductAsync(new Product
        {
            Brand = "Roland",
            Model = "JUNO-DS61",
            Color = "Черный",
            Description = "Универсальный синтезатор для сцены.",
            Price = 70000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_fTKlNozVe6RTiiVow26qjNIniLughjrIsA&s",
            CategoryId = synthesizersCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Korg",
            Model = "KROSS 2",
            Color = "Черный",
            Description = "Портативный и мощный синтезатор.",
            Price = 65000,
            ImageUrl = "https://images.musicstore.de/images/1280/korg-kross-2-61-black-_1_SYN0006137-000.jpg",
            CategoryId = synthesizersCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "MODX6",
            Color = "Черный",
            Description = "Синтезатор с мощными функциями.",
            Price = 90000,
            ImageUrl = "https://love-piano.ru/upload/iblock/b65/modx6plus_o_0001_332651c3177d5466f3ddd044c186c21d.jpeg",
            CategoryId = synthesizersCategory.Id
        });

        // Добавляем товары для барабанных установок
        await _productService.AddProductAsync(new Product
        {
            Brand = "Tama",
            Model = "Imperialstar",
            Color = "Черный",
            Description = "Полный комплект ударных.",
            Price = 85000,
            ImageUrl = "https://tama.ru/upload/resize_cache/iblock/82b/trlpm40afdq21i2ozp4csikfwsrjzrk7/800_0_1/A155679_318839_8.jpg",
            CategoryId = drumKitsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Pearl",
            Model = "Export EXX",
            Color = "Красный",
            Description = "Классическая барабанная установка для начинающих.",
            Price = 75000,
            ImageUrl = "https://www.kombik.com/resources/img/000/001/247/img_124737.jpg",
            CategoryId = drumKitsCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Mapex",
            Model = "Mars",
            Color = "Синий",
            Description = "Профессиональная барабанная установка.",
            Price = 120000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzAE1vfZSvNeLJv20L_Vu8ve87EKT13aGC5w&s",
            CategoryId = drumKitsCategory.Id
        });

        // Добавляем товары для перкуссии
        await _productService.AddProductAsync(new Product
        {
            Brand = "LP",
            Model = "Classic Timbales",
            Color = "Зеленый",
            Description = "Тимбали для перкуссионистов.",
            Price = 25000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkQP0PPF7nVD9Gv0jp7KptgAKwDynsobsd7A&s",
            CategoryId = percussionCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Meinl",
            Model = "Cajón",
            Color = "Коричневый",
            Description = "Кахон для ритмичных выступлений.",
            Price = 15000,
            ImageUrl = "https://skifmusic.ru/thumbs/8f/55/600x600_1_normal_8d91fd148f87de488564c2ea21c4.webp",
            CategoryId = percussionCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Remo",
            Model = "Djembe",
            Color = "Черный",
            Description = "Джембе для создания уникальных звуков.",
            Price = 30000,
            ImageUrl = "https://images.musicstore.de/images/0960/remo-djembe-black-earth-dj-0014-be-14-_1_DRU0013546-000.jpg",
            CategoryId = percussionCategory.Id
        });

        // Добавляем товары для флейт
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "YFL-222",
            Color = "Серебряный",
            Description = "Профессиональная флейта для начинающих.",
            Price = 35000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTgHmfcnjfyuYYMrUzHUc2myUeIpkg301zWrQ&s",
            CategoryId = flutesCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Gemeinhardt",
            Model = "3SHB",
            Color = "Золотой",
            Description = "Флейта с отличным звучанием.",
            Price = 45000,
            ImageUrl = "https://m.media-amazon.com/images/I/61h5EP7v+oL._AC_UF894,1000_QL80_.jpg",
            CategoryId = flutesCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Pearl",
            Model = "PF-501E",
            Color = "Черный",
            Description = "Флейта для более опытных музыкантов.",
            Price = 50000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQH2Rhc3YiCOyFDk4kATOdGHCmYTA_pek2xbg&s",
            CategoryId = flutesCategory.Id
        });

        // Добавляем товары для саксофонов
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "YAS-280",
            Color = "Золотой",
            Description = "Профессиональный саксофон для начинающих.",
            Price = 80000,
            ImageUrl = "https://yamaha.ru/d/239.jpg",
            CategoryId = saxophonesCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Selmer",
            Model = "AS42",
            Color = "Черный",
            Description = "Саксофон с отличным звучанием.",
            Price = 120000,
            ImageUrl = "https://www.omalleymusicalinstruments.com/cdn/shop/products/AS42B-Selmer-AS42B-Professional-Alto-Saxophone.jpg?v=1624649519",
            CategoryId = saxophonesCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Jupiter",
            Model = "JAS1100",
            Color = "Синий",
            Description = "Саксофон высокого качества.",
            Price = 95000,
            ImageUrl = "https://r2.gear4music.com/media/104/1047248/600/preview_1.jpg",
            CategoryId = saxophonesCategory.Id
        });

        // Добавляем товары для медных духовых
        await _productService.AddProductAsync(new Product
        {
            Brand = "Bach",
            Model = "Stradivarius",
            Color = "Медный",
            Description = "Трубка для профессионалов.",
            Price = 150000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSCGmXKLlT-VXGIGsU_tJY_x6xSW3NWP11tMw&s",
            CategoryId = brassCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Yamaha",
            Model = "YTR-4335GS",
            Color = "Золотой",
            Description = "Трубка с отличным звучанием.",
            Price = 130000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbn9TsZM4NgL2qjseff4xhjSlN_IEHbX96Mg&s",
            CategoryId = brassCategory.Id
        });
        await _productService.AddProductAsync(new Product
        {
            Brand = "Getzen",
            Model = "390S",
            Color = "Серебряный",
            Description = "Трубка для начинающих.",
            Price = 80000,
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEUP8Sjqx-NlTYzzDRufVygCz0pn8zpkfaaw&s",
            CategoryId = brassCategory.Id
        });

        Console.WriteLine("Продукты созданы.");

        var admin1 = new User
        {
            Email = "admin@mail.com",
            PasswordHash = "password",
            Role = "Admin"
        };

        await  _userService.RegisterAsync(admin1.Email,admin1.PasswordHash,admin1.Role);

        await AddVisitStatsForLastMonthsAsync();

    }

    private async Task AddVisitStatsForLastMonthsAsync()
    {
        var currentDate = DateTime.Now;

        // Генерируем статистику для текущего месяца
        var currentMonthVisits = new Random().Next(1000, 5000); // Генерация случайного числа посещений для текущего месяца
        await _visitStatService.AddOrUpdateStatAsync(currentDate, currentMonthVisits);
        Console.WriteLine($"Добавлены данные о посещениях за {currentDate:MMMM yyyy}: {currentMonthVisits} посещений.");

        // Генерируем статистику за последние 7 месяцев (предыдущие)
        for (int i = 1; i <= 7; i++)
        {
            var month = currentDate.AddMonths(-i); // Получаем дату для месяца
            var visits = new Random().Next(1000, 5000); // Генерируем случайное число посещений

            // Используем существующий метод для добавления или обновления статистики
            await _visitStatService.AddOrUpdateStatAsync(month, visits);

            Console.WriteLine($"Добавлены данные о посещениях за {month:MMMM yyyy}: {visits} посещений.");
        }
    }
}
