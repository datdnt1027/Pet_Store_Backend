using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pet_store_backend.contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}
