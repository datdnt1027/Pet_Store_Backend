namespace pet_store_backend.application.Common.Interfaces.Authentication;

public interface IPasswordConfiguration
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

}