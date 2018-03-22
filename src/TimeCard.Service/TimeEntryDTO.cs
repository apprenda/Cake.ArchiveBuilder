namespace TimeCard.Service
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class TimeEntryDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public string Task { get; set; }
    }
}