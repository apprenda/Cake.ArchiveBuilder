namespace TimeCard.Data
{
    using System.Data.Entity;

    public class TimeCardContext: DbContext
    {
        public DbSet<TimeCard> TimeCards { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
    }
}