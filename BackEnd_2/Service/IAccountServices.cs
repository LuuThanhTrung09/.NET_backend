using Game106_API.Models;

namespace Game106_API.Services
{
    public interface IAccountServices
    {
        Task<List<Account>>GetAllAccounts();
        Task<Account>GetAccountById(int id);
        Task<Account>GetAccountByEmail(string? email);
        Task<List<Account>>GetAccountByEmailStartWith(string x);
        Task<List<Account>>GetAccountByNameStartWith(string x);
        Task<List<Account>> GetAccountsByDate(DateTime startDate, DateTime endDate);
        Task<List<Account>> GetAccountByNameEmail(string x, string y);
        Task<Account>Register(string email, string password, string name);
        Task<Account>Login(string email, string password);
        Task<Account>ChangePassword(int id, string password);
        Task<Account>AccountDelete(int id);
    }
}