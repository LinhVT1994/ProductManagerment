using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerment
{
    enum Category
    {
        Book,
        CD
    }
    class Product
    {
        private string _Id;　　     　//　ID。
        private string _Name;  　    //　名前。
        private double _Price;     　//　値段。
        private double _ProfitRate;　//　利益率。
        private bool   _DeleteFlag = false;　//　削除フラグ。

        //  Additional some fields.
        private Category _Category;  // The type of the product (CD or BOOK).
        private int      _Amount = 1;    // Amount of products.
        public static Product Create(Category category, string id, string name, double price, double profitRate)
        {
            // validate inputs.
            if (string.IsNullOrWhiteSpace(id)|| string.IsNullOrWhiteSpace(name) || price < 0)
            {
                return null;
            }
            // create a new product and return.
            return new Product(category, id, name, price, profitRate);
        }

        public Product(Category category, string id, string name, double price, double profitRate)
        {
            Id = id;
            Name = name;
            Price = price;
            ProfitRate = profitRate;
            Category = category;
        }
        

        public override string ToString()
        {
            return string.Format("ID: {0} Name: {1} Price: {2}  Profit rate: {3}", Id, Name, Price, ProfitRate);
        }

        #region Product properties.
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("This parameter must be not null or spaces.", nameof(Name));
                _Name = value;
            }
        }
        /// <summary>
        /// ID of the product.
        /// </summary>
        public string Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("This parameter must be not null or spaces.", nameof(Id));
                _Id = value;
            }
        }


        public Category Category
        {
            get
            {
                return  _Category;
            }
            private set
            {
                _Category = value;  
            }
        }
        /// <summary>
        /// The price of a product.
        /// </summary>
        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("This parameter must be positive value.", nameof(Price));
                _Price = value;
            }
        }
        public int Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("This parameter must be positive value.", nameof(Amount));
                _Amount = value;
            }
        }
        /// <summary>
        /// The profit rate in a product.
        /// </summary>

        public double ProfitRate
        {
            get
            {
                return _ProfitRate;
            }
            set
            {
                _ProfitRate = value;
            }
        }
        /// <summary>
        /// Delete flag.
        /// </summary>
        public bool DeleteFlag
        {
            get
            {
                return _DeleteFlag;
            }
            set
            {
                _DeleteFlag = value;
            }
        }
        #endregion 

    }
}
