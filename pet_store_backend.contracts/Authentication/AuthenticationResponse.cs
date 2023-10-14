using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pet_store_backend.contracts.Authentication
{
    public record AuthenticationResponse(
        Guid id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
