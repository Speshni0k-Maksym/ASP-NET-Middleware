namespace ASP_NET_Middleware.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public class WishService : IWishService
    {
        private readonly string _filePath;
        private readonly List<string> _wishes;
        private readonly Random _rng = new();
        private readonly object _lock = new();

        public WishService(IWebHostEnvironment env)
        {
            var dataDir = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
            _filePath = Path.Combine(dataDir, "wishes.txt");
            if (!File.Exists(_filePath)) File.WriteAllText(_filePath, string.Empty);

            _wishes = new List<string>
            {
                "Нехай кожен день приносить нові відкриття й радість.",
                "Щоб здоров'я було міцним, а плани здійснювались легко.",
                "Нехай вдача супроводжує у всіх починаннях.",
                "Бажаю натхнення і сил для реалізації мрій.",
                "Хай поруч будуть вірні друзі і підтримка.",
                "Нехай робота приносить задоволення і розвиток.",
                "Бажаю мирного неба та спокою в душі.",
                "Щоб кожна помилка ставала кроком до успіху.",
                "Нехай любов і тепло наповнюють ваш дім.",
                "Бажаю фінансової впевненості і стабільності.",
                "Хай кожен ранок починається з хороших новин.",
                "Нехай творчість відчиняє нові горизонти.",
                "Бажаю ясних цілей і рішучих кроків до них.",
                "Нехай гумор і сміх роблять життя легшим.",
                "Бажаю цікавих знайомств і корисних зв'язків.",
                "Хай відпочинок буде справжнім відновленням сил."
            };
        }

        public Task<string> WriteRandomWishAsync()
        {
            string wish;
            lock (_lock)
            {
                var index = _rng.Next(0, _wishes.Count);
                wish = _wishes[index];
                var line = $"{DateTime.UtcNow:o} - {wish}{Environment.NewLine}";
                File.AppendAllText(_filePath, line);
            }

            return Task.FromResult(wish);
        }
    }
}