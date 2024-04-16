using Samurai_V2_.Net_8.DTOs;

namespace Samurai_V2_.Net_8.IRepository
{
    public interface IEmployeeRepo
    {
        Task<string> CreateBook(EmployeeDtos book);
        Task<string> DeleteEmp(int Id);
        Task<Allemp> GetAllEmp(int PageSize, int PageNo);

    }
}
