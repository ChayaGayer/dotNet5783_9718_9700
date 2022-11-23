using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductItem?> GetProducts();
    IEnumerable<ProductForList?> GetListedProducts();
    BO.Product RequestProductDetalisForM(int productID);
}
