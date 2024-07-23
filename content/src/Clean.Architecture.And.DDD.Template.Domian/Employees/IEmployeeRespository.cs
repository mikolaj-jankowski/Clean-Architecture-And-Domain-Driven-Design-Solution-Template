namespace Clean.Architecture.And.DDD.Template.Domian.Employees
{
    public interface IEmployeeRespository
    {
        Task AddAsync(Employee employee);
    }
}
