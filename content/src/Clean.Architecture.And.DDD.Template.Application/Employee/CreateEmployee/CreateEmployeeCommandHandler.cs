using Clean.Architecture.And.DDD.Template.Domian.Employees;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.And.DDD.Template.Application.Employee.CreateEmployee;

public class CreateEmployeeCommandHandler : IConsumer<CreateEmployeeCommand>
{
    private readonly IEmployeeRespository _employeeRespository;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;
    public CreateEmployeeCommandHandler(IEmployeeRespository employeeRespository, ILogger<CreateEmployeeCommandHandler> logger)
    {
        _employeeRespository = employeeRespository;
        _logger = logger;
    }



    public async Task Consume(ConsumeContext<CreateEmployeeCommand> command)
    {
        _logger.LogInformation($"Inserting an employee: {command.Message.Name} {command.Message.Surname}");

        var employee = Domian.Employees.Employee.CreateEmployee(command.Message.Name, command.Message.Surname);
        await _employeeRespository.AddAsync(employee);
    }
}
