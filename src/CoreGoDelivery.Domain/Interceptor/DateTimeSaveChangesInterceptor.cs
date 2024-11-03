using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Globalization;

namespace CoreGoDelivery.Domain.Interceptor;

public class DateTimeSaveChangesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        foreach (var entry in eventData!.Context!.ChangeTracker.Entries())
        {
            foreach (var property in entry.Properties)
            {
                if (property.CurrentValue is DateTime dateTimeValue)
                {
                    string formattedDate = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture);

                    Console.WriteLine($"Formatted Date: {formattedDate}");
                }
            }
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
