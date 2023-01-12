using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
   const string s_products = "products";
    static DO.Product? createProductFromXElement(XElement p)
    {
        DO.Category category;
        Enum.TryParse((string?)p.Element("Category"),out category); 
        return new DO.Product()
        {
            ID = p.ToIntNullable("ID") ?? throw new Exception(),
            ProductName = (string?)p.Element("ProductName"),
            InStock = p.ToIntNullable("InStock") ?? throw new Exception(),
            Category = category,
            Price = (double)p.ToDoubleNullable("Price")

        };
    }
    public int Add(DO.Product product)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? prod = (from st in productRootElem.Elements()
                          where st.ToIntNullable("ID") == product.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (prod != null)
            throw new Exception("id already exist"); // fix to: throw new DalMissingIdException(id);

        XElement prodElem = new XElement("Product",
                                   new XElement("ID", product.ID),
                                   new XElement("ProductName", product.ProductName),
                                   new XElement(" InStock", product.InStock),
                                   new XElement("Category", product.Category),
                                   new XElement("Price", product.Price)
                                   );

        productRootElem.Add(prodElem);

        XMLTools.SaveListToXMLElement(productRootElem, s_products);

        return product.ID; ;
    }

    public void Delete(int id)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? prod = (from p in productRootElem.Elements()
                          where (int?)p.Element("ID") == id
                          select p).FirstOrDefault() ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);

        prod.Remove(); //<==>   Remove stud from studentsRootElem

        XMLTools.SaveListToXMLElement(productRootElem, s_products);
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(s_products);


        //return studentsRootElem.Elements().Select(s => createStudentfromXElement(s)).Where(filter);

        if (filter != null)
        {
            return from p in productRootElem.Elements()
                   let product = createProductFromXElement(p)
                   where filter(product)
                   select (DO.Product?)product;
        }
        else
        {
            return from p in productRootElem.Elements()
                   select createProductFromXElement(p);
        }

    }

    public Product GetById(int id)
    {
        XElement productRootElem = XMLTools.LoadListFromXMLElement(s_products);
        return (from p in productRootElem?.Elements()
                where p.ToIntNullable("ID") == id
                select (DO.Product?)createProductFromXElement(p)).FirstOrDefault()
                ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);
    }

    public void Update(Product product)
    {
        Delete(product.ID);
        Add(product);
    }
}
