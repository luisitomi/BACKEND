using Backend.CrossCuting.Helpers;
using Backend.Infraestructure.Repository.LoginRepository;
using Backend.Infraestructure.Repository.ProductRepository;
using Backend.Infraestructure.Repository.SaleRepository;
using Backend.Infraestructure.Repository.UserRepository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Backend.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;

        public ILoginRepository LoginRepository => new LoginRepository(_transaction);
        public IUserRepository UserRepository => new UserRepository(_transaction);
        public IProductRepository ProductRepository => new ProductRepository(_transaction);
        public ISaleRepository SaleRepository => new SaleRepository(_transaction);

        public UnitOfWork()
        {
            _connection = new SqlConnection(new AppConfiguration()._SQLConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }
      
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
