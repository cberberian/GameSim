using SimGame.Data;
using SimGame.Domain;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GameSimContext())
            {
                var productType = new ProductType
                {
                    Name = "test"
                };
                db.ProductTypes.Add(productType);
                db.SaveChanges();
            }
        }
    }
}
