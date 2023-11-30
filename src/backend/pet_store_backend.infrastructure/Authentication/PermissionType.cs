namespace pet_store_backend.infrastructure.Authentication;

public enum PermissionType
{
    Read,
    Create,
    Update,
    Deactivate
}

// Define the TablePermission class
public class TablePermission
{
    public bool Create { get; set; }
    public bool Read { get; set; }
    public bool Update { get; set; }
    public bool Deactivate { get; set; }
}
