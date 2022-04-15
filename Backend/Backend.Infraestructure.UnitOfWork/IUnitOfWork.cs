using Backend.Infraestructure.Repository.LoginRepository;
using Backend.Infraestructure.Repository.ProductRepository;
using Backend.Infraestructure.Repository.SaleRepository;
using Backend.Infraestructure.Repository.UserRepository;
using System;

namespace Backend.Infraestructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ILoginRepository LoginRepository { get; }
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        ISaleRepository SaleRepository { get; }
        void Commit();
        void RollBack();
    }
}
