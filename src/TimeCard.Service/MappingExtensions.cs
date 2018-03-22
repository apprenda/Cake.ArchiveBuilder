namespace TimeCard.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using TimeCard.Data;

    public static class MappingExtensions
    {

        public static TimeEntryDTO ToDTO(this TimeEntry entry)
        {
            if (entry == null)
            {
                return null;
            }

            return new TimeEntryDTO
                       {
                           Id = entry.Id,
                           StartTime = entry.StartTime,
                           EndTime = entry.EndTime,
                           Task = entry.Task
                       };
        }

        public static TimeEntry ToDomain(this TimeEntryDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new TimeEntry
                       {
                           Id = dto.Id,
                           StartTime = dto.StartTime,
                           EndTime = dto.EndTime,
                           Task = dto.Task
                       };
        }
        

        public static TimeCardDTO ToDTO(this TimeCard timeCard, bool mapEntries)
        {
            if (timeCard == null)
            {
                return null;
            }

            var dto = new TimeCardDTO
                          {
                              Id = timeCard.Id,
                              StartDate = timeCard.StartDate,
                              EmployeeId = timeCard.EmployeeId,
                              Entries = new List<TimeEntryDTO>()
                          };

            if (mapEntries && timeCard.Entries != null)
            {
                dto.Entries = timeCard.Entries.Select(ToDTO).ToList();
            }

            return dto;
        }

        public static TimeCard ToDomain(this TimeCardDTO dto, bool mapEntries)
        {
            if (dto == null)
            {
                return null;
            }

            var timeCard = new TimeCard
                               {
                                   Id = dto.Id,
                                   StartDate = dto.StartDate,
                                   EmployeeId = dto.EmployeeId
                               };

            if (mapEntries && dto.Entries != null)
            {
                timeCard.Entries = dto.Entries.Select(ToDomain).ToList();
            }

            return timeCard;
        }
        
        public static EmployeeDTO ToDTO(this Employee employee, bool mapTimeCards)
        {
            if (employee == null)
            {
                return null;
            }

            var dto = new EmployeeDTO
                          {
                              Id = employee.Id,
                              FirstName = employee.FirstName,
                              LastName = employee.LastName,
                              Title = employee.Title,
                              TimeCards = new List<TimeCardDTO>()
                          };

            if (mapTimeCards && employee.TimeCards != null)
            {
                dto.TimeCards = employee.TimeCards.Select(tc => tc.ToDTO(true)).ToList();
            }

            return dto;
        }

        public static Employee ToDomain(this EmployeeDTO dto, bool mapTimeCards)
        {
            if (dto == null)
            {
                return null;
            }

            var employee = new Employee
                               {
                                   Id = dto.Id,
                                   FirstName = dto.FirstName,
                                   LastName = dto.LastName,
                                   Title = dto.Title
                               };

            if (mapTimeCards && dto.TimeCards != null)
            {
                employee.TimeCards = dto.TimeCards.Select(tc => tc.ToDomain(true)).ToList();
            }

            return employee;
        }

    }
}