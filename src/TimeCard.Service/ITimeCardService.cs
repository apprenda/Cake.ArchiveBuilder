namespace TimeCard.Service
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract]
    public interface ITimeCardService
    {

        [OperationContract]
        EmployeeDTO GetEmployee(int id, bool includeTimeCards);

        [OperationContract]
        IEnumerable<EmployeeDTO> SearchEmployees(string firstName, string lastName, string title, bool includeTimeCards);

        [OperationContract]
        void AddEmployee(EmployeeDTO employee);

        [OperationContract]
        IEnumerable<TimeCardDTO> GetTimeCardsForEmployee(int employeeId);

        [OperationContract]
        TimeCardDTO GetTimeCard(int id);

        [OperationContract]
        IEnumerable<TimeCardDTO> GetTimeCardsByDate(DateTime startDate);

        [OperationContract]
        void AddTimeCard(TimeCardDTO timeCard);
    }

    
}
