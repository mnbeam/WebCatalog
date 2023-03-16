using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Infrastructure.Services;

internal class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}