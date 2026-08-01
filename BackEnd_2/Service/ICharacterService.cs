using Game106_API.Models;

namespace Game106_API.Services
{
    public interface ICharacterServices
    {
        Task<List<Character>>GetAllCharacters();
        Task<Character>GetCharacterById(int id);
        Task<List<Character>> GetCharacterByAccountId(int account_id);
        Task<List<Character>> GetCharacterByLevel(int level);
        Task<List<Character>> GetCharacterByLevelName(int level);
        Task<List<Character>> GetCharacterByLevelNameA(int level);
        Task<List<Character>> GetCharacterByLevelAndName(int level1, int level2, string name);
        Task<List<Character>> GetCharacterByLevel12(int level, int level2);
        Task<Character> CreateCharacter(int account_id, string? name, int level, int exp);
        Task<Character> UpdateCharacter(int id, string? name, int level, int exp);
        Task<List<Character>> UpdateCharacterByLevel(int level, int exp);
        Task<List<Character>> UpdateCharacterLevelByAccountId(int Account_id, int Level);
        Task<List<Character>> GetCharacterByEmail(string email);
        
       
    }
}