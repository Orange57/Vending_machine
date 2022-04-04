using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    internal class Products_List : Error_ID
    {
       

        private int _id;
        private string _product_name;
        private double _product_price;
        private int _product_quantity;
        language _language;

        public string getName() { return _product_name; }

        public void setLangague(language lang)
        {
            _language = lang;
        }
        public double getProductPrice() { return _product_price; }

        public Products_List(int p_id,string p_name, double p_price, language internallang, int p_quantity = 0)
        {
            _language = internallang;
            if (p_id > -1)
            {
                _id = p_id;
            }
            else
            {
                throw new ArgumentException(_language.idcannotbeempty);
            }
            if (p_name != null)
            {
                _product_name = p_name;
            }
            else
            {
                throw new ArgumentException(_language.namecannotbeempty);
            }
            if (p_price != 0)
            {
                _product_price = p_price;

            }
            else
            {
                throw new ArgumentException(_language.pricecannotbeempty);
            }
            if (p_quantity != 0)
            {
                _product_quantity = p_quantity;
            }
            else
            {
                _product_quantity = 0;
            }
        }
        public int isAvailable()
        {
            if (_product_quantity > 0)
            {
                return SUCCESS;
            }
            else
            {
                return SOLD_OUT;
            }
        }
        public void purchaseOne()
        {
            _product_quantity--;
        }


        public string getList(bool isExactChange)
        {
            if (isAvailable() == SUCCESS)
            {
                if (isExactChange)
                {
                    return _product_name.ToUpper() + " " + _product_price + "€ - " + _product_quantity + _language.itemLeftExactOnly;
                }
                else
                {
                    return _product_name.ToUpper() + " " + _product_price + "€ - " + _product_quantity + _language.itemLeft;
                }
            }
            else
            {
                return _product_name.ToUpper() + " " + _product_price + "€ - "+_language.soldout;
            }
        }

    }
}
