namespace TimeCard.Data
{
    using System;

    public class TimeEntry
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Task { get; set; }
    }
}