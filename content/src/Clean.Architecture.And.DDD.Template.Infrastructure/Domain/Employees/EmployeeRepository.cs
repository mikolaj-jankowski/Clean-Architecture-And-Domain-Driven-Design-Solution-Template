using Clean.Architecture.And.DDD.Template.Domian.Employees;
using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Domain.Employees
{
    public class EmployeeRepository : IEmployeeRespository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Employee employee)
        {
            await _appDbContext.Employees.AddAsync(employee);
        }
    }
}
