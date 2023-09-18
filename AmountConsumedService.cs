using DotNetTest.Entities;
using DotNetTest.Repositories;

namespace DotNetTest.Services.impl;

public class AmountConsumedService : IAmountConsumedService
{
    private readonly AmountConsumedRepository _amountConsumedRepository;

    public AmountConsumedService(AmountConsumedRepository amountConsumedRepository)
    {
        _amountConsumedRepository = amountConsumedRepository;
    }

    public void Create(AmountConsumed amountConsumed)
    {
        _amountConsumedRepository.Save(amountConsumed);   
    }

    public AmountConsumed GetById(int id)
    {
        return _amountConsumedRepository.GetById(id);
    }

    public void Update(AmountConsumed amountConsumed)
    {
        _amountConsumedRepository.Update(amountConsumed);
    }

    public void Delete(AmountConsumed amountConsumed)
    {
        _amountConsumedRepository.Delete(amountConsumed);
    }
}