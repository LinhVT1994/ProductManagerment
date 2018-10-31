using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerment
{
    class ProductAdministratorExcecuter
    {
        static List<Product> listProductTest = new List<Product>()
        {
            new Product(Category.Book,"BookO1","Harry Potter and the Philosopher's Stone",12000,16.7676),
            new Product(Category.Book,"BookO2","Harry Potter and the Chamber of Secrets",13000,13.123434),
            new Product(Category.Book,"BookO3","Harry Potter and the Prisoner of Azkaban",11500,20),
            new Product(Category.Book,"BookO4","Harry Potter and the Goblet of Fire",13000,0.13),
            new Product(Category.Book,"BookO5","Harry Potter and the Order of the Phoenix",14000,0.14),
            new Product(Category.Book,"BookO6","Harry Potter and the Half-Blood Prince",12400,0.13),
            new Product(Category.Book,"BookO7","Harry Potter and the Deathly Hallows",14000,0.13),
            new Product(Category.Book,"Book08","Head First Java",14000,0.13),
            new Product(Category.Book,"Book09","Effective Java",14000,0.13),
            new Product(Category.Book,"Book10","Thinking in java",14000,0.13),
            new Product(Category.Book,"Book11","Leaning Java",14000,0.13),
            new Product(Category.Book,"Book12","the java language specification, java se 9 edition",14000,0.13),
            new Product(Category.CD,"CD0001","Harry Potter and the Philosopher's Stone",30000,1.2),
            new Product(Category.CD,"CD0002","Harry Potter and the Chamber of Secrets",35000,1.2),
            new Product(Category.CD,"CD0003","Harry Potter and the Prisoner of Azkaban",35000,1.26),
            new Product(Category.CD,"CD0004","Harry Potter and the Goblet of Fire",37000,1.6),
            new Product(Category.CD,"CD0005","Harry Potter and the Order of the Phoenix",37000,1.4),
            new Product(Category.CD,"CD0006","Harry Potter and the Half-Blood Prince",38000,1.13),
            new Product(Category.CD,"CD0007","Harry Potter and the Deathly Hallows",39000,1.13),
        };
        private ProductAdministrator productAdministrator = null;
        public  ProductAdministratorExcecuter(List<Product> list)
        {
            productAdministrator = ProductAdministrator.GetInstance();
            productAdministrator.AddProducts(list);
        }
      
        public static void Execute()
        {
            ProductAdministratorExcecuter excecuter = new ProductAdministratorExcecuter(listProductTest);
            Console.WriteLine("Total price of products: {0} Average price: {1} ",excecuter.TotalPrice,excecuter.PriceAverage);
        }

        public double Profit
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.ProfitRate * item.Price / 100);
            }
          
        }
        public double MaximumOfProfit
        {
            get
            {
                return productAdministrator.Products.Max(item => item.DeleteFlag ? int.MinValue : item.ProfitRate * item.Price / 100);
            }
          
        }
        public double MinimumOfProfit
        {
            get
            {
                return productAdministrator.Products.Min(item => item.DeleteFlag ? int.MaxValue : item.ProfitRate * item.Price / 100);

            }
        }
        public double AverageOfProfit
        {
            get
            {
                return Profit / productAdministrator.CountAmountOfProducts();
            }
             
        }
        public double TotalPrice
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.Price * item.Amount);
            }
           
        }
        public double PriceAverage
        {
            get
            {
                double sumOfPrices = TotalPrice;
                return sumOfPrices / productAdministrator.CountAmountOfProducts();
            }
            
        }
        public double MaximumOfProfitRate
        {
            get
            {
                return Math.Round(productAdministrator.Products.Max(item => item.DeleteFlag ? int.MinValue : item.ProfitRate));
            }
        }
        public double MinimumOfProfitRate
        {
            get
            {
              return  Math.Round(productAdministrator.Products.Min(item => item.DeleteFlag ? int.MaxValue : item.ProfitRate));
            }
        }
        public double AverageOfProfitRate
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.ProfitRate) / productAdministrator.CountAmountOfProducts();
            }
           
        }
        public static void ShowAList(List<Product> products)
        {
            Console.WriteLine("There are {0} items found", products.Count);
            int count = 0;
            foreach (Product item in products)
            {
                Console.WriteLine("-->" + ++count + " " + item.ToString());
            }
        }
        public static void ShowAProduct(Product product)
        {
            if (product==null)
            {
                Console.WriteLine("Not found.");
                return;
            }
            Console.WriteLine("-->" + product.ToString());
        }
    }
}
