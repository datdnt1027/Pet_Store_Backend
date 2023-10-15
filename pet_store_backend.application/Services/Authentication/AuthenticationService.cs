using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities;

namespace pet_store_backend.application.Services.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthenticationResult Login(string email, string password)
        {
            // Check if user already exists
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exist.");
            }

            // Check User Password
            if(user.Password != password)
            {
                throw new Exception("Invalid Password");
            }

            //Create JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // Validate User doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given Email already exists. ");
            }

            // Create user (generate unique Id) & Persist to DB
            var user = new User {FirstName = firstName, LastName = lastName, Email = email, Password = password };
            _userRepository.Add(user); 

            // Create JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token);
        }
    }
}
