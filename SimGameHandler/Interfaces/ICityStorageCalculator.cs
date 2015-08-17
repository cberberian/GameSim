using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public interface ICityStorageCalculator
    {
        /// <summary>
        /// Calculates new city storage amounts based on new product quantities queued to produce.
        /// </summary>
        /// <param name="calculateStorageRequest"></param>
        /// <returns></returns>
        CalculateStorageResponse CalculateNewStorageAmounts(CalculateStorageRequest calculateStorageRequest);
    }
}