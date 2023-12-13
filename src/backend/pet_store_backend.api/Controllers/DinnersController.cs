using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.infrastructure.Authentication;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.api.Controllers;

[Route("[controller]")]
public class DinnersController : ApiController
{
    [Authorize(Roles = UserRoleKey.AdminRoleName)]
    [HasPermission(TableKey.Products, PermissionType.Read)]
    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }
}