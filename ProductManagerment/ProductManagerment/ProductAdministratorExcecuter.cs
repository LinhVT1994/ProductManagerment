using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerment
{
    class ProductAdministratorExcecuter
    {
        #region Datas for testing
        static List<Product> listProductTest = new List<Product>()
        {
            new Product(Category.Book,"BookO1","Harry Potter and the Philosopher's Stone",12000,16.7676),
            new Product(Category.Book,"BookO2","Harry Potter and the Chamber of Secrets",13000,13.123434),
            new Product(Category.Book,"BookO3","Harry Potter and the Prisoner of Azkaban",11500,20),
            new Product(Category.Book,"bookO4","Harry Potter and the Goblet of Fire",13000,13),
            new Product(Category.Book,"bookO5","Harry Potter and the Order of the Phoenix",14000,14),
            new Product(Category.Book,"BookO6","Harry Potter and the Half-Blood Prince",12400,13),
            new Product(Category.Book,"BookO7","Harry Potter and the Deathly Hallows",14000,13),
            new Product(Category.Book,"Book08","Head First Java",14000,13),
            new Product(Category.Book,"Book09","Effective Java",14000,13),
            new Product(Category.Book,"Book10","Thinking in java",14000,13),
            new Product(Category.Book,"Book11","Leaning Java",14000,13),
            new Product(Category.Book,"Book12","the java language specification, java se 9 edition",14000,13),
            new Product(Category.CD,"0001","Harry Potter and the Philosopher's Stone",30000,12),
            new Product(Category.CD,"0002","Harry Potter and the Chamber of Secrets",35000,12),
            new Product(Category.CD,"0003","Harry Potter and the Prisoner of Azkaban",35000,12.6),
            new Product(Category.CD,"0004","Harry Potter and the Goblet of Fire",37000,16),
            new Product(Category.CD,"0005","Harry Potter and the Order of the Phoenix",37000,14),
            new Product(Category.CD,"0006","Harry Potter and the Half-Blood Prince",38000,13),
            new Product(Category.CD,"0007","Harry Potter and the Deathly Hallows",39000,13),
        };
        #endregion
        private ProductAdministrator productAdministrator = null;
        public  ProductAdministratorExcecuter(List<Product> list)
        {
            productAdministrator = ProductAdministrator.GetInstance();
            productAdministrator.AddProducts(list);
        }
      
        public static void Execute()
        {
            ProductAdministratorExcecuter excecuter = new ProductAdministratorExcecuter(listProductTest);

            ProductAdministrator productAdministrator = ProductAdministrator.GetInstance();
            Console.WriteLine("==============================================================================");
            Console.WriteLine("-->Add a new product.");
            Product newProduct = new Product(Category.Book, "123", "Naruto (3-in-1 Edition) Vol. 1: Includes vols. 1, 2 & 3 ", 1462, 16.7676);
            bool isSuccess=  productAdministrator.AddProduct(newProduct);
            if (isSuccess)
            {
                Console.WriteLine("---->The {0} have just added in the store.",newProduct.Name);
                excecuter.ShowItemsWithTag();
            }
            else
            {
                Console.WriteLine("---->The {0} have just added in the store.", newProduct.Name);
            }
            Console.WriteLine("---------------------------------------------------------------------------");
            string itemId = "bookO2";
            Console.WriteLine("-->Find a product what has id is " + itemId);
            Product foundProduct= productAdministrator.FindById(itemId, true);
            if (foundProduct != null)
            {
                Console.WriteLine("---->The infomation of the product has just found "+ foundProduct.ToString());
            }
            else
            {
                Console.WriteLine("----> The {0} is not found.", itemId);
            }
            Console.WriteLine("---------------------------------------------------------------------------");
            string keyword = "java";
            Console.WriteLine("-->Find products what names include \"{0}\" word", keyword);
            List<Product> foundProducts = productAdministrator.FindByName(keyword, true);
            if (foundProducts==null)
            {
                Console.WriteLine("---->Not found anything with the \"{0}\" keyword.",foundProduct.ToString());
            }
            else
            {
                ShowAList(foundProducts);
            }

            Console.WriteLine("---------------------------------------------------------------------------");
            string delete_id = "123";
            Console.WriteLine("--> Delete a product with id is  \"{0}\" ", delete_id);
            isSuccess= productAdministrator.Delete(delete_id);
            if (isSuccess)
            {
                Console.WriteLine("----> Deleting the product has Id is \"{0}\" successful", delete_id);
            }
            else
            {
                Console.WriteLine("----> Deleting the product has Id is \"{0}\" unsuccessful", delete_id);
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine("商品の値段の合計: {0} 商品の値段の平均: {1} ", excecuter.TotalPrice,excecuter.PriceAverage);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("商品の利益率の最大: {0}　商品の利益率の最小: {1}　商品の利益率の平均: {2} ", excecuter.MaximumOfProfitRate, excecuter.MinimumOfProfitRate,excecuter.AverageOfProfitRate);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("利益の合計: {0}　利益の最大: {1}　利益の最小 {2}　利益の平均: {3} ", excecuter.Profit, excecuter.MaximumOfProfit,excecuter.MinimumOfProfit, excecuter.AverageOfProfit);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("-->Display all information of all of products");
            excecuter.ShowProducts();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("-->Display all information of all of products with tags");
            excecuter.ShowItemsWithTag();
        }

        /// <summary>
        ///　商品の利益の合計。
        /// </summary>
        public double Profit
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.Amount*item.ProfitRate * item.Price / 100);
            }
          
        }
        /// <summary>
        /// 商品の利益の最大。
        /// </summary>
        public double MaximumOfProfit
        {
            get
            {
                return productAdministrator.Products.Max(item => item.DeleteFlag ? int.MinValue : item.Amount * item.ProfitRate * item.Price / 100);
            }
          
        }
        /// <summary>
        /// 商品の利益の最小。
        /// </summary>
        public double MinimumOfProfit
        {
            get
            {
                return productAdministrator.Products.Min(item => item.DeleteFlag ? int.MaxValue : item.Amount * item.ProfitRate * item.Price / 100);
            }
        }
        /// <summary>
        /// 商品の利益の平均。
        /// </summary>
        public double AverageOfProfit
        {
            get
            {
                return Profit / productAdministrator.CountAmountOfProducts();
            }
             
        }
        /// <summary>
        /// 商品の値段の合計
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.Price * item.Amount);
            }
           
        }
        /// <summary>
        /// 商品の値段の平均。
        /// </summary>
        public double PriceAverage
        {
            get
            {
                double sumOfPrices = TotalPrice;
                return sumOfPrices / productAdministrator.CountAmountOfProducts();
            }
            
        }
        /// <summary>
        /// 商品の利益率の最大。
        /// </summary>
        public double MaximumOfProfitRate
        {
            get
            {
                return Math.Round(productAdministrator.Products.Max(item => item.DeleteFlag ? int.MinValue : item.ProfitRate));
            }
        }
        /// <summary>
        /// 商品の利益率の最小。
        /// </summary>
        public double MinimumOfProfitRate
        {
            get
            {
              return  Math.Round(productAdministrator.Products.Min(item => item.DeleteFlag ? int.MaxValue : item.ProfitRate));
            }
        }
        /// <summary>
        /// 商品の利益率の平均。
        /// </summary>
        public double AverageOfProfitRate
        {
            get
            {
                return productAdministrator.Products.Sum(item => item.DeleteFlag ? 0 : item.ProfitRate) / productAdministrator.CountAmountOfProducts();
            }
           
        }

        /// <summary>
        ///　リストの商品を表示する。
        /// </summary>
        public static void ShowAList(List<Product> products)
        {
            Console.WriteLine("There are {0} items found", products.Count);
            int count = 0;
            foreach (Product item in products)
            {
                Console.WriteLine("-->" + ++count + " " + item.ToString());
            }
        }

        /// <summary>
        ///　リストの商品を表示する。
        /// </summary>
        public void ShowProducts()
        {
            productAdministrator.ShowItems(true); 
        }
        /// <summary>
        ///　商品名にダグを付いで表示する。
        /// </summary>
        public void ShowItemsWithTag(bool includingDeletedElements = false)
        {
            Console.WriteLine("-->There are {0} items in the stock.", productAdministrator.Products.Count);
            int count = 0;
            string tagBook = "[本]";
            string tagCD = "[CD]";
            foreach (Product item in productAdministrator.Products)
            {
                if (includingDeletedElements)
                {
                    Console.WriteLine("---->" + ++count + " " + (item.Category == Category.Book?tagBook:tagCD) +item.Name+ " Price: " + item.Price );
                }
                else
                {
                    if (item.DeleteFlag)
                    {
                        continue;
                    }
                    Console.WriteLine("---->" + ++count + " " + (item.Category == Category.Book ? tagBook : tagCD) + item.Name + "  Price: " + item.Price);
                }

            }
        }
    }
}
