
using Game106_API.AppDataContext;
using Game106_API.Models;
namespace Game106_API.Services
{
    public class AccountsServices : IAccountServices
    {
        private readonly AppDbContext _context;
        private readonly IEmailServices _emailServices;
        public AccountsServices(AppDbContext context, IEmailServices emailServices)
        {
            _context = context;
            _emailServices = emailServices;
        }

        public Task<List<Account>>GetAllAccounts()
        {
            try
            {
                var accounts = _context.accounts .ToList();
                return Task.FromResult(accounts);
            }
            catch(System.Exception e)
            {
                throw  new Exception(e.Message);
            }
        }

        public Task<Account>GetAccountById(int id)
        {
            try
            {
                var accountId = _context.accounts
                .Where(a => a.Id == id). SingleOrDefault();
                if(accountId == null)
                {
                    throw new Exception("Account not found");
                }
                return Task.FromResult(accountId);
            }
            catch(System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public Task<Account>GetAccountByEmail(string? email)
        {
            try
            {
                var accountEmail = _context.accounts
                .Where(a => a.Email == email).SingleOrDefault();
                if(accountEmail == null)
                {
                    throw new Exception("Account not found");
                }
                return Task.FromResult(accountEmail);
            }
            catch(System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Account>> GetAccountByEmailStartWith(string x)
        {
            try
            {
                var accountByEmailStartWith = _context.accounts
                    .Where(a => a.Email != null && a.Email.StartsWith(x)).ToList();
                if (accountByEmailStartWith.Count == 0)
                {
                    throw new Exception("Account not found");
                }
                return Task.FromResult(accountByEmailStartWith);
            }
            catch(System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<List<Account>> GetAccountByNameStartWith(string x)
        {
            try
            {
                var accountByNameStartWith = _context.accounts
                    .Where(a => a.Name != null && a.Name.StartsWith(x)).ToList();
                if (accountByNameStartWith.Count == 0)
                {
                    throw new Exception("Account not found");
                }
                return Task.FromResult(accountByNameStartWith);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<List<Account>>  GetAccountsByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var accountByDay = _context.accounts
                    .Where(a => a.Created_at >= startDate && a.Created_at <= endDate).OrderByDescending(a => a.Created_at).ToList();
                if (accountByDay.Count == 0)
                {
                    throw new Exception("Account not found");
                }
                return Task.FromResult(accountByDay);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Account>> GetAccountByNameEmail(string x, string y)
        {
            try
            {
                var accountByNameEmail = _context.accounts
                    .Where(a => a.Name != null && a.Email != null && a.Name.Contains(x) && a.Email.Contains(y)).ToList();
                if (accountByNameEmail.Count == 0)
                {
                    throw new Exception ("Account not found");
                }
                return Task.FromResult(accountByNameEmail);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<Account>Register(string email, string password, string name)
        {
            try
            {
                var account = _context.accounts.FirstOrDefault(x => x.Email == email);
                if (account != null) throw new Exception("Email is already !");
                PasswordHasher.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                var newAccount = new Account
                {
                    Email = email,
                    Password = Convert.ToBase64String(passwordHash),
                    Password_Salt = Convert.ToBase64String(passwordSalt),
                    Name = name,
                    Created_at = DateTime.Now
                    
                };
                Thread.Sleep(500);
                _context.accounts.Add(newAccount);
                _context.SaveChanges();
                _emailServices.SendEmail(email, "regsiter is successfully !", "Congratulation your account");
                newAccount.Password = null;
                return Task.FromResult(newAccount);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
        public Task<Account>Login(string email, string password)
        {
            try
            {
                var account = _context.accounts.FirstOrDefault(x => x.Email == email);
                if (account == null) throw new Exception("Email is not ready");
                var check = PasswordHasher.VerifyPasswordHash(password, Convert.FromBase64String(account.Password), Convert.FromBase64String(account.Password_Salt));
                if (!check) throw new Exception("Password is wrong");
                var obj = new Account
                {
                    Id = account.Id,
                    Email = account.Email,
                    Name = account.Name,
                    Created_at = account.Created_at
                };
                return Task.FromResult(obj);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
        public Task<Account>ChangePassword(int id, string password)
        {
            try
            {
                var account = _context.accounts.SingleOrDefault(x => x.Id == id);
                if (account == null) throw new Exception("Account is not ready");
                account.Password = password;
                _context.SaveChanges();
                account.Password = null;
                return Task.FromResult(account);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
        public Task<Account>AccountDelete(int id)
        {
            try
            {
                var delete = _context.accounts.SingleOrDefault(d => d.Id == id);
                if(delete == null) throw new Exception("Account not found");
                _context.accounts.Remove(delete);
                _context.SaveChanges();
                return Task.FromResult(delete);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}