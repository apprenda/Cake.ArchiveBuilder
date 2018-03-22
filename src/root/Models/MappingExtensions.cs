namespace TimeCard.Models
{
    using global::TimeCard.Service;
    using System.Linq;
    using System;

    public static class MappingExtensions
    {
        public static EmployeeModel ToModel(this EmployeeDTO dto)
        {
            if(dto == null)
            {
                return null;
            }

            return new EmployeeModel
                       {
                           Id = dto.Id,
                           FirstName = dto.FirstName,
                           LastName = dto.LastName,
                           Title = dto.Title
                       };
        }

        public static TimeCardDTO ToDTO(this TimeCardModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TimeCardDTO
            {
                StartDate = model.StartDate,
                EmployeeId = model.EmployeeId,
                Entries = model.Entries.Select(ToDTO).ToArray()
            };
        }

        public static TimeCardModel ToModel(this TimeCardDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new TimeCardModel
            {
                EmployeeId = dto.EmployeeId,
                StartDate = dto.StartDate,
                Entries = dto.Entries.Select(ToModel).ToList()
            };
        }

        public static TimeEntryDTO ToDTO(this TimeCardEntryModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TimeEntryDTO
            {
                StartTime = model.Date.Add(DateTime.Parse(model.StartTime).TimeOfDay),
                EndTime = model.Date.Add(DateTime.Parse(model.EndTime).TimeOfDay),
                Task = model.Task
            };

        }

        public static TimeCardEntryModel ToModel(this TimeEntryDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new TimeCardEntryModel
            {
                Date = dto.StartTime.Date,
                StartTime = dto.StartTime.TimeOfDay.ToString(),
                EndTime = dto.EndTime.TimeOfDay.ToString(),
                Task = dto.Task
            };
        }
    }
}