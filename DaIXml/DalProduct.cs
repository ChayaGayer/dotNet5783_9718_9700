using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string s_products = "products";
    public int Add(Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filtar = null)
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product item)
    {
        throw new NotImplementedException();
    }
}
