using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using pet_store_backend.infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _tableName;
    private readonly PermissionType _permissionType;

    public HasPermissionAttribute(string tableName, PermissionType permissionType)
    {
        _tableName = tableName;
        _permissionType = permissionType;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            // Check if the user has the required permissions
            var permissionsClaim = user.FindFirst("permissions")?.Value;

            if (!string.IsNullOrEmpty(permissionsClaim))
            {
                var permissions = JsonConvert.DeserializeObject<Dictionary<string, TablePermission>>(permissionsClaim);

                if (permissions != null && HasPermission(permissions))
                {
                    // User has the required permission
                    return;
                }
            }
        }

        // User doesn't have the required permission, deny access
        context.Result = new ForbidResult();
    }

    private bool HasPermission(Dictionary<string, TablePermission> permissions)
    {
        // Check for null before accessing properties
        if (permissions == null)
        {
            return false;
        }

        // Find the specific table in the permissions
        if (permissions.TryGetValue(_tableName, out var tablePermission))
        {
            // Use the null-conditional operator to safely access the property
            var permissionValue = tablePermission?.GetType().GetProperty(_permissionType.ToString())?.GetValue(tablePermission);

            // If _permissionType is not found in the permissions, deny access
            return permissionValue as bool? ?? false;
        }

        // Table not found in the permissions, deny access
        return false;
    }
}
