using Game106_API.Services;
using Game106_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Game106_API.Controllers
{
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private ICharacterServices _characterServices;
        private ResponseApi _responseApi;
        public CharacterController(ICharacterServices characterServices)
        {
            _characterServices = characterServices;
            _responseApi = new();
        }
        
        [HttpGet]
        [Route("/api/get-all-charaters")]
        public async Task<IActionResult> GetAllCharacters()
        {
            try
            {
                var allCharacter = await _characterServices.GetAllCharacters();
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Character not found";
                _responseApi.Data = allCharacter;
                return Ok(_responseApi);
            }
            catch (System.Exception e)
            {
                return StatusCode (500, e.Message);
            }
        }
        //URL: http://localhost:5119/
        [HttpGet]
        [Route("/api/get-character-by-id/{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            try
            {
                var characterId = await _characterServices.GetCharacterById(id);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterId;
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

        //lấy danh sách Character của acount id " "
        [HttpGet]
        [Route("/api/get-character-by-account_id/{id}")]
        public async Task<IActionResult> GetCharacterByAccountId(int id)
        {
            try
            {
                var charactersAccountId = await _characterServices.GetCharacterByAccountId(id);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = charactersAccountId;
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
        //lấy danh sách character có level > " "
        [HttpGet]
        [Route("/api/get-character-by-level")]
        public async Task<IActionResult> GetCharacterByLevel(int level)
        {
            try
            {
                var characterLevel = await _characterServices.GetCharacterByLevel(level);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterLevel;
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
        //lấy danh sách Character có level > " " và tên kết thúc bằng "N"
        [HttpGet]
        [Route("/api/get-character-by-level-name")]
        public async Task<IActionResult> GetCharacterByLevelName(int level)
        {
            try
            {
                var characterLevelName = await _characterServices.GetCharacterByLevelName(level);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterLevelName;
                return Ok(_responseApi);
            }
            catch(System.Exception e)
            {
                _responseApi.isSuccess = false;
                _responseApi.Notification = "Error";
                _responseApi.Data = e.Message;
                return StatusCode(500, _responseApi);
            }
        }
        //lấy danh sách Character có level > " " và tên có chứa "A"
        [HttpGet]
        [Route("/api/get-character-by-level-name-A")]
        public async Task<IActionResult>  GetCharacterByLevelNameA(int level)
        {
            try
            {
                var characterLevelNameA = await _characterServices.GetCharacterByLevelNameA(level);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterLevelNameA;
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
        //lấy danh sách Character có level > " " hoặc level < " " và tên có chứa " "
        [HttpGet]
        [Route("/api/get-character-by-level1-level2-name")]
        public async Task<IActionResult>  GetCharacterByLevelAndName(int level1, int level2, string name)
        {
            try
            {
                var characterLevelAndName = await _characterServices.GetCharacterByLevelAndName(level1, level2, name);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterLevelAndName;
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
        //lấy danh sách Character có level từ " " đến " " sắp theo level tăng dần
        [HttpGet]
        [Route("/api/get-character-by-level-from-level1-to-level2")]
        public async Task<IActionResult> GetCharacterByLevel12(int level1, int level2)
        {
            try
            {
                var characterLevel12 = await _characterServices.GetCharacterByLevel12(level1, level2);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = characterLevel12;
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
        //Thêm 1 character mới vào bảng Characters
        [HttpPost]
        [Route("/api/create-new-character")]
        public async Task<IActionResult>CreateCharacter([FromBody] Character model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Name is not empty";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                if (model.Level <= 0 || model.Level >= 100)
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Level must be >= 0 and <= 100";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                if (model.Exp <= 0)
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Exp must be >= 0";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                var character = await _characterServices.CreateCharacter(model.Account_id, model.Name, model.Level, model.Exp);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Create success";
                _responseApi.Data = character;
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

        [HttpPut]
        [Route("/api/update-character-by-id")]
        public async Task<IActionResult> UpdateCharacter([FromBody] Character model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Name is not ready";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                if (model.Level <= 0 || model.Level >= 100)
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Level must be > 0 and level < 100";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                if (model.Exp <= 0)
                {
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "EXP must be > 0";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                var character = await _characterServices.UpdateCharacter(model.Id, model.Name, model.Level, model.Exp);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Update success";
                _responseApi.Data = character;
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
        [HttpPut]
        [Route("/api/update-exp-character-by-level")]

        public async Task<IActionResult> UpdateCharacterByLevel(int level, int exp)
        {   
            try
            {
                var characters = await _characterServices.UpdateCharacterByLevel(level, exp);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Character update is successfully";
                _responseApi.Data = characters;
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
        [HttpPut]
        [Route("/api/update-character-level-by-account_id")]
        public async Task<IActionResult>UpdateCharacterLevelByAccountId(int level, int account_id)
        {
            try
            {
                var characters1 = await _characterServices.UpdateCharacterLevelByAccountId(level,account_id);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "character level update is successfully";
                _responseApi.Data = characters1;
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
        [HttpGet]
        [Route("/api/get-character-by-email")]
        public async Task<IActionResult> GetCharacterByEmail([FromQuery]string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Email has not been logged in");
                    _responseApi.isSuccess = false;
                    _responseApi.Notification = "Invalid data.";
                    _responseApi.Data = null;
                    return BadRequest(_responseApi);
                }
                var character4 = await _characterServices.GetCharacterByEmail(email);
                _responseApi.isSuccess = true;
                _responseApi.Notification = "Data get Success";
                _responseApi.Data = character4;
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
        
    }
}