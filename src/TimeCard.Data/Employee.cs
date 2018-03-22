namespace TimeCard.Data
{
    using System.Collections.Generic;

    public class Employee
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public virtual ICollection<TimeCard> TimeCards { get; set; }
        
    }
}
