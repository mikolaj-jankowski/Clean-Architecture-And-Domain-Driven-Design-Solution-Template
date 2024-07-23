namespace Clean.Architecture.And.DDD.Template.Application.Employee.CreateEmployee
{
    public class EmployeeCreatedIntegrationEvent
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
