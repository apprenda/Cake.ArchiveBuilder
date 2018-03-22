namespace TimeCard.Service
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class EmployeeDTO
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string LastName { get; set; }
        
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public IEnumerable<TimeCardDTO> TimeCards { get; set; }
    }
}