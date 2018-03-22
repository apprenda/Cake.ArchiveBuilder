using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeCard.Models
{
    public class TimeCardModel: IComparable<TimeCardModel>
    {

        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public List<TimeCardEntryModel> Entries { get; set; }

        public int CompareTo(TimeCardModel other)
        {
            return StartDate.CompareTo(other);
        }
    }


    public class TimeCardEntryModel
    {
    
        public DateTime Date {get; set;}
        public string StartTime {get; set;}
        public string EndTime {get; set;}
        public string Task {get; set;}

    }
}