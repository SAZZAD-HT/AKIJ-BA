using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Samurai_V2_.Net_8.DbContexts;
using Samurai_V2_.Net_8.DbContexts.Models;
using Samurai_V2_.Net_8.DTOs;
using Samurai_V2_.Net_8.IRepository;
using System.Net;

namespace Samurai_V2_.Net_8.Repository
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private ReadWriteContext _dbContexts;

        public EmployeeRepo(ReadWriteContext dbContexts)
        {
            _dbContexts = dbContexts;
        }

        public async Task<string> CreateBook(EmployeeDtos book)
        {
            string message = "";

            try
            {
                // Check if the email is in a valid format
                if (!IsValidEmail(book.Email))
                {
                    return "Invalid email format";
                }

                // Check if the password meets the required criteria
                if (!IsValidPassword(book.Password))
                {
                    return "Invalid password. Password must be at least 8 characters long.";
                }

                var data = await _dbContexts.Employees.Where(e => e.EmployeeId == book.EmployeeId).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.Name = book.Name;
                    data.Email = book.Email;
                    data.Password = book.Password;
                    data.Mobile = book.Mobile;
                    data.DateofBirth = book.DateofBirth;
                    data.ActionDate = DateTime.Now;
                    _dbContexts.Employees.Update(data);

                    message = "Updated";
                }
                else
                {
                    Employee n = new Employee();
                    n.Name = book.Name;
                    n.Email = book.Email;
                    n.Password = book.Password;
                    n.Mobile = book.Mobile;
                    n.DateofBirth = book.DateofBirth;
                    n.ActionDate = DateTime.Now;
                    n.IsActive = true;
                    _dbContexts.Employees.Add(n);

                    message = "Created";
                }
                await _dbContexts.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
            return message;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            // You can add more password strength criteria as needed
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }

        public async Task<string> DeleteEmp(int Id)
        {
            string messge = "";
            try
            {

               var existData= await _dbContexts.Employees.Where(e => e.EmployeeId == Id).FirstOrDefaultAsync();
                if (existData != null)
                {
                    //For Soft Delete

                    existData.IsActive = false;
                    _dbContexts.Employees.Update(existData);
                    //_dbContexts.Employees.Remove(existData);
                    await _dbContexts.SaveChangesAsync();
                    messge = "Deleted";
                }
                else
                {
                    messge = "Not Found";
                }
                
                return messge;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public async Task<Allemp> GetAllEmp(int PageSize,int PageNo)
        {
            Allemp allemp = new Allemp();
            try
            {
                allemp.TotalCount = await _dbContexts.Employees.Where(x=>x.IsActive==true).CountAsync();
                allemp.Datas = await _dbContexts.Employees.Where(x => x.IsActive == true).Skip(PageSize * (PageNo - 1)).Take(PageSize).Select(x => new EmployeeDtos
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                    Mobile = x.Mobile,
                    DateofBirth = x.DateofBirth,
                    ActionDate = x.ActionDate,
                    IsActive = x.IsActive
                }).ToListAsync();
                allemp.TotalPages = (int)Math.Ceiling((double)allemp.TotalCount / PageSize);

                return allemp;
           

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}