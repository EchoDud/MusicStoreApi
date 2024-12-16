using MusicStoreApi.Models;
using Microsoft.EntityFrameworkCore;


namespace MusicStoreApi.Services
{
    public class VisitStatService : IVisitStatService

    {
        private readonly AppDbContext _context;
        private static readonly object _lock = new object();

        public VisitStatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VisitStat>> GetAllStatsAsync()
        {
            return await _context.VisitStats
                .OrderBy(vs => vs.Month)
                .ToListAsync();
        }

        public async Task<VisitStat?> GetStatByMonthAsync(DateTime month)
        {
            var normalizedMonth = new DateTime(month.Year, month.Month, 1); // Оставляем только год и месяц
            return await _context.VisitStats
                .FirstOrDefaultAsync(vs => vs.Month == normalizedMonth);
        }

        public async Task AddOrUpdateStatAsync(DateTime month, int visits)
        {
            var normalizedMonth = new DateTime(month.Year, month.Month, 1);

            var stat = await GetStatByMonthAsync(normalizedMonth);

            if (stat != null)
            {
                stat.VisitsCount += visits;
            }
            else
            {
                stat = new VisitStat
                {
                    Month = normalizedMonth,
                    VisitsCount = visits
                };
                _context.VisitStats.Add(stat);
            }

            await _context.SaveChangesAsync();
        }


        public async Task IncrementCurrentMonthVisitsAsync()
        {
            var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Начало текущего месяца

            lock (_lock) // Гарантирует, что только один поток одновременно выполняет этот код
            {
                // Найти запись для текущего месяца
                var stat = _context.VisitStats.FirstOrDefault(v => v.Month == currentMonth);

                if (stat != null)
                {
                    // Если запись найдена, увеличиваем количество посещений
                    stat.VisitsCount++;
                    _context.VisitStats.Update(stat);
                }
                else
                {
                    // Если записи нет, создаём новую
                    stat = new VisitStat
                    {
                        Month = currentMonth,
                        VisitsCount = 1
                    };
                    _context.VisitStats.Add(stat);
                }

                // Сохраняем изменения в базе данных
                _context.SaveChanges();
            }
        }
    }
}
