using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    public class Order
    {
        private int _id;
        private DateTime _orderDate;
        private Equipment _equipment;

        public Order(DateTime orderDate,Equipment equipment)
        {
            this._orderDate = orderDate;
            this._equipment = equipment;
        }
        public Order() { }

        public int Id { get { return _id; } set { _id = value; } }
        public DateTime OrderDate { get { return _orderDate; } set { _orderDate = value;} }
        public Equipment Equipment { get { return _equipment; } set { _equipment = value;} }
    }
}
