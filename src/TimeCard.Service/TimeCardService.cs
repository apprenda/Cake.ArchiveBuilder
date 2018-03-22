using Apprenda.Services.Logging;

namespace TimeCard.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TimeCard.Data;

    public class TimeCardService : ITimeCardService
    {
        private static ILogger log = LogManager.Instance().GetLogger(typeof(TimeCardService));

        public EmployeeDTO GetEmployee(int id, bool includeTimeCards)
        {
            using (var context = new TimeCardContext())
            {
                var employee = context.Employees.Find(id);

                return employee != null ? employee.ToDTO(includeTimeCards) : null;
            }
        }

        public IEnumerable<EmployeeDTO> SearchEmployees(string firstName, string lastName, string title, bool includeTimeCards)
        {
            using (var context = new TimeCardContext())
            {
                var employees =
                    context.Employees.AsEnumerable().Where(
                        e =>
                        String.IsNullOrWhiteSpace(firstName) ||
                        e.FirstName.Equals(firstName, StringComparison.InvariantCultureIgnoreCase)).Where(
                            e =>
                            String.IsNullOrWhiteSpace(lastName) ||
                            e.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase)).Where(
                                e =>
                                String.IsNullOrWhiteSpace(title) ||
                                e.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));

                return employees.Select(e => e.ToDTO(includeTimeCards)).ToList();
            }
        }

        public void AddEmployee(EmployeeDTO employee)
        {
            using (var context = new TimeCardContext())
            {
                context.Employees.Add(employee.ToDomain(true));

                context.SaveChanges();
            }
        }

        public IEnumerable<TimeCardDTO> GetTimeCardsForEmployee(int employeeId)
        {
            using (var context = new TimeCardContext())
            {
                var employee = context.Employees.Find(employeeId);

                return employee != null ? employee.TimeCards.Select(tc => tc.ToDTO(true)) : new List<TimeCardDTO>();
            }
        }

        public TimeCardDTO GetTimeCard(int id)
        {
            using (var context = new TimeCardContext())
            {
                var timeCard = context.TimeCards.Find(id);

                return timeCard != null ? timeCard.ToDTO(true) : null;
            }
        }

        public IEnumerable<TimeCardDTO> GetTimeCardsByDate(DateTime startDate)
        {
            using (var context = new TimeCardContext())
            {
                return context.TimeCards.Where(tc => tc.StartDate.Equals(startDate)).Select(tc => tc.ToDTO(true));
            }
        }

        public void AddTimeCard(TimeCardDTO timeCard)
        {
            using (var context = new TimeCardContext())
            {
                context.TimeCards.Add(timeCard.ToDomain(true));

                context.SaveChanges();
            }
        }
    }
}
