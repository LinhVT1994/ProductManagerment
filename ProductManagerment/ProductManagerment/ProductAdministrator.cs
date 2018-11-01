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
        public Product FindById(string id, bool includingDeletedElements = false)// the default is get all of items includes deleted items.
        {
            if (includingDeletedElements)
            {
                return _ListProduct.FirstOrDefault(item => item.Id.ToLower().Equals(id.ToLower()));
            }
            else
            {
                return _ListProduct.FirstOrDefault(item => item.Id.ToLower().Equals(id.ToLower()) && (!item.DeleteFlag));
               
            }
         
        }
        /// <summary>
        /// To find products in the products list by a name of the product.
        /// </summary>
        /// <param name="name">The name of the product what wants to find.</param>
        /// <returns>A list of products what marched with a name.</returns>
        public List<Product> FindByName(string name, bool includingDeletedElements = false) // the default is get all of items includes deleted items.
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            IEnumerable<Product> foundItems = null;
            if (includingDeletedElements)
            {
                foundItems = _ListProduct.Where(item => item.Name.ToLower().Contains(name.ToLower()));
            }
            else
            {
                foundItems = _ListProduct.Where(item => item.Name.ToLower().Contains(name.ToLower()) && (!item.DeleteFlag));
            }
            if (foundItems == null)
            {
                return null;
            }
            return foundItems.ToList();

        }
        /// <summary>
        /// 商品の数量を計算する。
        /// </summary>
        /// <param name="includingDeletedElements">削除した商品も含むかどうか。既定は含まないです</param>
        /// <returns></returns>
        public int CountAmountOfProducts(bool includingDeletedElements = false)// the default is get all of items includes deleted items
        {
            if (includingDeletedElements)
            {
                return Products.Count;
            }
            else
            {
                return Products.Count(item => !item.DeleteFlag);
            }
        }
        /// <summary>
        /// 製品を表示する。
        /// </summary>
        /// <param name="includingDeletedElements">削除した商品も表示するかどうか。既定は表示しない事です</param>
        public void ShowItems(bool includingDeletedElements = false)
        {
            Console.WriteLine("There are {0} items in the stock.", _ListProduct.Count);
            int count = 0;
            foreach (Product item in _ListProduct)
            {
                if (includingDeletedElements)
                {
                    Console.WriteLine("-->" + ++count + " " + item.ToString());
                }
                else
                {
                    if (item.DeleteFlag)
                    {
                        continue;
                    }
                    Console.WriteLine("-->" + ++count + " " + item.ToString());
                }
            }
        }

        /// <summary>
        /// 商品を削除する。
        /// </summary>
        /// <param name="id">削除したい製品のIDです</param>
        /// <returns>成功だったら、True値を戻る。反対はFalse値を戻る。</returns>
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
