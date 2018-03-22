namespace TimeCard.Service
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class TimeCardDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public int EmployeeId { get; set; }
        [DataMember]
        public IEnumerable<TimeEntryDTO> Entries { get; set; }
    }
}