using DotNetTest.Entities;

namespace DotNetTest.Services;

public interface IAmountConsumedService
{
    void Create(AmountConsumed amountConsumed);
    AmountConsumed GetById(int id);
    void Update(AmountConsumed amountConsumed);
    void Delete(AmountConsumed amountConsumed);
}