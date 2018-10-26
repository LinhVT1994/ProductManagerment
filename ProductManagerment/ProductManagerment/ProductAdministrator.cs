using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerment
{
    class ProductAdministrator
    {
        private static ProductAdministrator _ProductAdministrator = null;
        private readonly IList<Product> _ListProduct = null;
        public static ProductAdministrator GetInstance()
        {
            if (_ProductAdministrator == null)
            {
                _ProductAdministrator = new ProductAdministrator();
            }
            return _ProductAdministrator;
        }
        public ProductAdministrator()
        {
            _ListProduct = new List<Product>();
        }

        public bool AddProduct(Product product)
        {
            // validate the validation of parameters of the entered product.
            Product foundProduct = FindById(product.Id);
            if (foundProduct != null) // This product is exist on the product list.
            {
                foundProduct.Amount += 1;
                return true;
            }
            else
            {
                _ListProduct.Add(product);
                return true;
            }
        }
        public bool AddProducts(List<Product> products)
        {
            bool sucess = false;
            foreach (Product product in products)
            {
                sucess = AddProduct(product);
                if (!sucess)
                {
                    throw new ArgumentException("Addtion a product in the list is not sucess. ",nameof(AddProducts));
                }
            }
            return true;
        }
        public bool IsExist(string id)
        {
           return FindById(id) != null;
        }
        /// <summary>
        /// To find products by an Id.
        /// </summary>
        /// <param name="id">The Id what wants to find.</param>
        public Product FindById(string id, bool deleteFlag = false)// the default is get all of items includes deleted items.
        {
            return _ListProduct.FirstOrDefault(item => item.Id.ToLower().Equals(id.ToLower()) && (!deleteFlag));
        }
        /// <summary>
        /// To find products in the products list by a name of the product.
        /// </summary>
        /// <param name="name">The name of the product what wants to find.</param>
        /// <returns>A list of products what marched with a name.</returns>
        public List<Product> FindByName(string name, bool deleteFlag = false) // the default is get all of items includes deleted items.
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            IEnumerable<Product> foundItems = _ListProduct.Where(item => item.Name.ToLower().Contains(name.ToLower())&&(!deleteFlag));
            if (foundItems == null)
            {
                return null;
            }
            return foundItems.ToList();

        }
        public void ShowItems()
        {
            Console.WriteLine("There are {0} items in the stock.",_ListProduct.Count);
            int count = 0;
            foreach (Product item in _ListProduct)
            {
                Console.WriteLine("-->"+ ++count + " "+ item.ToString());
            }
        }
        public bool Delete(string id)
        {
            Product foundProduct = FindById(id);
            if (foundProduct == null)
            {
                return false;
            }
            foundProduct.DeleteFlag = true;
            foundProduct.Amount = 0;
            return true;
        }
        public List<Product> Products
        {
            get
            {
                return _ListProduct.ToList();
            }
        }
    }
}
