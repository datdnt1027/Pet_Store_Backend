namespace pet_store_backend.domain.Entities.Users.ValueObjects
{
    public enum Gender
    {
        Male,
        Female,
        NonBinary,
        Other
    }

    public static class GenderExtensions
    {
        public static string ToCustomString(this Gender gender)
        {
            // Implement custom logic for converting enum to string
            // In this example, using the enum name
            return Enum.GetName(typeof(Gender), gender) ?? string.Empty;
        }
    }
}
