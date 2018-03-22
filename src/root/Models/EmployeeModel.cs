namespace TimeCard.Models
{
    using System.ComponentModel;
    using Lib.Web.Mvc.JQuery.JqGrid.DataAnnotations;

    public class EmployeeModel
    {
        [JqGridColumnSortable(false)]
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Title { get; set; }

        [DisplayName("Time Cards")]
        [JqGridColumnFormatter("$.timeCardLinkFormatter")]
        public string TimeCardLink
        {
            get { return "View"; }
        }
    }
}