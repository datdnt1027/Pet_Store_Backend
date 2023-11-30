using pet_store_backend.application.Common.Interfaces.Services;

namespace pet_store_backend.infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
