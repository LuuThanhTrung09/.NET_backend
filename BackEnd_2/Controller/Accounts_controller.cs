using Game106_API.Models;
using Game106_API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Game106_API.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountServices _accountServices;
        private ResponseApi _responseApi;

        public AccountsController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
            _responseApi = new();
        }
        
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-all-accounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var allAcc = await _accountServices.GetAllAccounts();
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Accounts retrieved successfully";
                _responseApi.Data = allAcc;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-account-by-id/{id}")]

        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var accountId = await _accountServices.GetAccountById(id);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountId;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, e.Message);
            }
        }

        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-account-by-email/")]
        public async Task<IActionResult> GetAccountByEmail( [FromQuery ]string email)
        {
            try
            {
                var accountEmail = await _accountServices.GetAccountByEmail(email);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountEmail;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, e.Message);
            }
        }

        //Lấy danh sách tài khoản mà email bắt đầu " "
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-account-by-email-start-with-x/email")]
        public async Task<IActionResult> GetAccountByEmailStartWith(string x)
        {
            try
            {
                var accountByEmailStartWith = await _accountServices.GetAccountByEmailStartWith(x);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountByEmailStartWith;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, e.Message);
            }
        }

        //lấy danh sách các tài khoản mà tên có chứa " "
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-account-by-name-start-with-x/name")]
        public async Task<IActionResult> GetAccountByNameStartWith(string x)
        {
            try
            {
                var accountByNameStartWith = await _accountServices.GetAccountByNameStartWith(x);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountByNameStartWith;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, e.Message);
            }
        }
        //lấy danh sách tài khoản có ngày tạo từ ngày " " đến " ", sắp xếp theo ngày tạo giảm dần
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-accounts-by-date")]
        public async Task<IActionResult> GetAccountsByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var accountByDay = await _accountServices.GetAccountsByDate(startDate, endDate);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountByDay;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, _responseApi);
            }
        }
        //lấy các tài khoản có tên chứ chuỗi " " và email chứa chuỗi " "
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-accounts-by-name-email")]
        public async Task<IActionResult> GetAccountNameEmail(string x, string y)
        {
            try
            {
                var accountByNameEmail = await _accountServices.GetAccountByNameEmail(x, y);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = accountByNameEmail;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500,_responseApi);
            }
        }
        //Đăng ký tài khoản
        //URL: http://localhost:5119/
        [HttpPost]
        [Route("/api/register")]
        public async Task<IActionResult>Register([FromBody] Account model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Password) || (string.IsNullOrEmpty(model.Email)))
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Data is not Success";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                } 
                var account = await _accountServices.Register(model.Email, model.Password, model.Name);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Import account success";
                _responseApi.Data = account;
                return Ok(_responseApi);
            }
            catch(System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return BadRequest(_responseApi);
            }
        }
        //Login
        //URL: http://localhost:5119/
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult>Login([FromBody] Account model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Data is not Success";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                var account = await _accountServices.Login(model.Email, model.Password);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Login is Success";
                _responseApi.Data = account;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return BadRequest(_responseApi);
            }
        }
        //Đổi mật khẩu
        //URL: http://localhost:5119/
        [HttpPut]
        [Route("/api/change-password")]
        public async Task<IActionResult>ChangePassword([FromBody] Account model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Email is not ready";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                var account = await _accountServices.ChangePassword(model.Id, model.Password);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Change password is success";
                _responseApi.Data = account;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return BadRequest(_responseApi);
            }
        }

        [HttpDelete]
        [Route("/api/accounts/{id}")]
        public async Task<IActionResult> AccountDelete(int id)
        {
            try
            {
                var account = await _accountServices.AccountDelete(id);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Account deleted successfully";
                _responseApi.Data = account;
                return Ok(_responseApi);
            }
            catch (Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, _responseApi);
            }
        }

    }
}