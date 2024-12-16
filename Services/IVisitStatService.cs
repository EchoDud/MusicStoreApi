using MusicStoreApi.Models;
using System.Threading.Tasks;

namespace MusicStoreApi.Services
{
    public interface IVisitStatService
    {
        Task<List<VisitStat>> GetAllStatsAsync();
        Task<VisitStat?> GetStatByMonthAsync(DateTime month);
        Task AddOrUpdateStatAsync(DateTime month, int visits);
        Task IncrementCurrentMonthVisitsAsync();
    }
}
