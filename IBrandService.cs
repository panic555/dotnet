using DotNetTest.Entities;

namespace DotNetTest.Services;

public interface IBrandService
{
    void Create(Brand brand);
    Brand GetById(int id);
    void Update(Brand brand);
    void Delete(Brand brand);
}