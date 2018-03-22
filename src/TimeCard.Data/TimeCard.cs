namespace TimeCard.Data
{
    using System;
    using System.Collections.Generic;

    public class TimeCard
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        public int EmployeeId { get; set; }
        public virtual ICollection<TimeEntry> Entries { get; set; }
    }
}