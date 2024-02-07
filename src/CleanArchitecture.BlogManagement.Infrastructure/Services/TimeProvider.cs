using SharedKernel;

namespace CleanArchitecture.BlogManagement.Infrastructure.Services;
internal sealed class TimeProvider : ITimeProvider
{
    public DateTimeOffset TimeNow => DateTimeOffset.UtcNow;
}
