using Book_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Model.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> listCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
        
    }
}
