namespace MusicStoreApi.Models
{
    public class VisitStat
    {
        public int Id { get; set; }

        // Хранит год и месяц, день будет игнорироваться
        public DateTime Month { get; set; }

        public int VisitsCount { get; set; } = 0;
    }
}
