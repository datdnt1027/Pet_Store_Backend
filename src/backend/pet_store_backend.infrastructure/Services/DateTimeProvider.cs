using pet_store_backend.application.Common.Interfaces.Services;

namespace pet_store_backend.infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
