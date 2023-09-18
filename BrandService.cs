using DotNetTest.Entities;
using DotNetTest.Repositories;

namespace DotNetTest.Services.impl;

public class BrandService : IBrandService
{
    private readonly BrandRepository _brandRepository;

    public BrandService(BrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public void Create(Brand brand)
    {
        _brandRepository.Save(brand);
    }

    public Brand GetById(int id)
    {
        return _brandRepository.GetById(id);
    }

    public void Update(Brand brand)
    {
        _brandRepository.Update(brand);
    }

    public void Delete(Brand brand)
    {
        _brandRepository.Delete(brand);
    }
}