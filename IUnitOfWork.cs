using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    { 
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        IShoppingCardRepository ShoppingCart { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}
