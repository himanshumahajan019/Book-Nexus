using Book_DataAccess.Data;
using Book_DataAccess.Repository.IRepository;
using Book_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Product = new ProductRepository(_context);
            Company= new CompanyRepository(_context);
            ApplicationUser= new ApplicationUserRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            SP_Call = new SP_Call(_context);
        }
        public ICategoryRepository Category { private set; get; }

        public ICoverTypeRepository CoverType { private set; get; }
        public ISP_Call SP_Call { private set; get; }

        public IProductRepository Product { private set; get; }

        public ICompanyRepository Company { private set; get; }

        public IApplicationUserRepository ApplicationUser { private set; get; }

        public IOrderHeaderRepository OrderHeader { private set; get; }

        public IOrderDetailRepository OrderDetail { private set; get; }

        public IShoppingCardRepository ShoppingCart {private set; get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
