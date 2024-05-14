using Deli.DATA.DTOs.User;

namespace Deli.Interface
{
    public interface ITokenService
    {
        string CreateToken(TokenDTO user);
    }
}