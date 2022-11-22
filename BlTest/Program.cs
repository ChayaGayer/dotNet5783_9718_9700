using BlApi;
using BlImplementation;
using BO;

namespace BlTest;
    
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        IBl bl=new Bl();
        IEnumerable<ProductForList> productFL = bl.Product.GetListedProducts();
    }
}