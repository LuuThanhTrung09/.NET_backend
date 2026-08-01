using System.ComponentModel;
using Game106_API.AppDataContext;
using Game106_API.Models;
using Microsoft.Identity.Client;

namespace Game106_API.Services
{
    public class CharacterServices : ICharacterServices
    {
        private readonly AppDbContext _context;
        public CharacterServices(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Character>> GetAllCharacters()
        {
            try
            {
                var characters = _context.characters.ToList();
                return Task.FromResult(characters);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<Character>GetCharacterById(int id)
        {
            try
            {
                var charactersId = _context.characters.Where(c => c.Id == id).SingleOrDefault();
                if(charactersId == null)
                {
                    throw new Exception ("Character not found");
                }
                return Task.FromResult(charactersId);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Character>> GetCharacterByAccountId(int account_id)
        {
            try
            {
                var charactersAccountId = _context.characters.Where(c => c.Account_id == account_id).ToList();
                if(charactersAccountId.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(charactersAccountId);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Character>> GetCharacterByLevel(int level)
        {
            try
            {
                var characterLevel = _context.characters.Where(c => c.Level > level).ToList();
                if(characterLevel.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(characterLevel);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Character>> GetCharacterByLevelName(int level)
        {
            try
            {
                var characterLevelName = _context.characters.
                Where(c => c.Level > level && c.Name != null && c.Name.EndsWith("A".ToLower())).ToList();
                if(characterLevelName.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(characterLevelName);
            }
            catch(System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<List<Character>> GetCharacterByLevelNameA(int level)
        {
            try
            {
                var characterLevelNameA = _context.characters
                    .Where(c => c.Level > level && c.Name != null && c.Name.Contains("N".ToLower())).ToList();
                if (characterLevelNameA.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(characterLevelNameA);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<Character>> GetCharacterByLevelAndName(int level1, int level2, string name)
        {
            try
            {
                var characterByLevelAndName = _context.characters
                    .Where(c => c.Level > level1 || c.Level < level2 && c.Name != null && c.Name.Contains(name)).ToList();
                if (characterByLevelAndName.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(characterByLevelAndName);
            }
            catch (System.Exception e)
            {
                throw new Exception (e.Message);
            }
        }

        public Task<List<Character>> GetCharacterByLevel12(int level1, int level2)
        {
            try
            {
                var characterLevel12 = _context.characters.Where(c => c.Level >= level1 && c.Level <= level2).OrderBy(c => c.Level).ToList();
                if (characterLevel12.Count == 0)
                {
                    throw new Exception("Character not found");
                }
                return Task.FromResult(characterLevel12);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Thêm 1 character mới vào bảng Characters
        public async Task<Character>CreateCharacter(int account_id, string? name, int level, int exp)
        {
            try
            {
                var character = _context.characters.FirstOrDefault(x => x.Account_id == account_id);
                if (character != null)  throw new Exception ("Character is already");
                var newCharacter = new Character
                {
                    Account_id = account_id,
                    Name = name,
                    Level = level,
                    Exp = exp,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                };
                _context.characters.Add(newCharacter);
                _context.SaveChanges();
                
                return await Task.FromResult(newCharacter);
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        //Cập nhật thông tin Character(name, level, exp ) theo id
        public async Task<Character> UpdateCharacter(int id, string? name, int level, int exp)
        {
            try
            {
                var character = _context.characters.SingleOrDefault(c => c.Id == id);
                if (character == null) throw new Exception("Character is not ready");
                character.Name = name;
                character.Level = level;
                character.Exp = exp;
                
                // _context.SaveChanges();
                return await Task.FromResult(character);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Cập nhật exp = " " cho các Character có level > " "
        public async Task<List<Character>>UpdateCharacterByLevel(int level, int exp)
        {
            try
            {
                var characters = _context.characters.Where(c => c.Level > level).ToList();
                if (characters == null) throw new Exception("Character is not read");
                foreach (var character in characters)
                {
                    character.Exp = exp;
                    character.Updated_at = DateTime.Now;
                }
                // _context.SaveChanges();
                return await Task.FromResult(characters);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
        
        //Cập nhật level = " " cho các Character có Account_id = " "
        public async Task<List<Character>>UpdateCharacterLevelByAccountId(int account_id, int level)
        {
            try
            {
                var characters1 = _context.characters.Where(x => x.Account_id == account_id).ToList();
                if (characters1 == null) throw new Exception("Character is not ready");
                foreach (var character in characters1)
                {
                    character.Level = level;
                    character.Updated_at = DateTime.Now;
                }
                _context.SaveChanges();
                return await Task.FromResult(characters1);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

       public async Task<List<Character>>GetCharacterByEmail(string email)
        {
            try
            {
                var AccountId = _context.accounts.SingleOrDefault(c => c.Email == email);
                if(AccountId == null)
                {
                    throw new Exception ($"Khong co Account nao co email: {email}");
                }
                var character4 = _context.characters.Where(c => c.Account_id == AccountId.Id).ToList();
                if(character4.Count == 0) throw new Exception ($"There is no character with an email: {email}");
                return await Task.FromResult(character4);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //thực hiện bắt lỗi các API trên:
        //cập nhật level
        //Name không được rỗng
        //Level phải > 0 và < 100
        //Exp phải > 0
    }
}