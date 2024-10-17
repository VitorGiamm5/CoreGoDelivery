using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Globalization;

namespace CoreGoDelivery.Domain.Interceptor
{
    public class DateTimeSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            // Intercepta todas as entidades que estão sendo salvas
            foreach (var entry in eventData!.Context!.ChangeTracker.Entries())
            {
                // Verifica se o tipo da entidade tem uma propriedade DateTime
                foreach (var property in entry.Properties)
                {
                    if (property.CurrentValue is DateTime dateTimeValue)
                    {
                        // Formata o DateTime no formato desejado antes de salvar
                        string formattedDate = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture);

                        // Aqui, você pode ou manipular o valor antes do save, ou logar ele
                        Console.WriteLine($"Formatted Date: {formattedDate}");
                    }
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
