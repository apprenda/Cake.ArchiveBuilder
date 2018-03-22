namespace TimeCard.Controllers
{
    using System;
    using System.Web.Mvc;
    using global::TimeCard.Models;
    using global::TimeCard.Service;
    using Lib.Web.Mvc.JQuery.JqGrid;
    using System.Linq;
   

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees(JqGridRequest request)
        {
            using (var proxy = new TimeCardServiceClient())
            {
                var employees = proxy.SearchEmployees(null, null, null, false);

                var response = new JqGridResponse();

                response.Records.AddRange(employees.Select(e => new JqGridRecord<EmployeeModel>(Convert.ToString(e.Id), e.ToModel())));

                return new JqGridJsonResult
                           {
                               Data = response
                           };
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TimeCards(int id)
        {
            using (var proxy = new TimeCardServiceClient())
            {
                var employee = proxy.GetEmployee(id, true);

                var timecards = employee.TimeCards.Select(tc => new 
                                           {
                                               Id = tc.Id,
                                               StartDate = tc.StartDate
                                           }).ToList();

                timecards.Sort((a,b) => a.StartDate.CompareTo(b.StartDate));

                var employeeData = new
                                       {
                                           Id = employee.Id,
                                           FirstName = employee.FirstName,
                                           LastName = employee.LastName,
                                           Title = employee.Title,
                                           TimeCards = timecards
                                       };

                return Json(employeeData);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEmployee(string firstName, string lastName, string title)
        {
            using (var proxy = new TimeCardServiceClient())
            {
                var employee = new EmployeeDTO
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Title = title
                };

                proxy.AddEmployee(employee);
            }

            return Json("result: 'success'");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddTimeCard(TimeCardModel model)
        {
            using (var proxy = new TimeCardServiceClient())
            {
                proxy.AddTimeCard(model.ToDTO());
            }

            return Json("result: 'success'");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TimeCardDetail(int id)
        {
            using (var proxy = new TimeCardServiceClient())
            {
                return Json(proxy.GetTimeCard(id).ToModel());
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
