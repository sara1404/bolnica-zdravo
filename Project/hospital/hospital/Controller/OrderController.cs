using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class OrderController
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService _service) { _orderService = _service; }

        public void Create(Order newOrder)
        {
             _orderService.Create(newOrder);
        }
    }
}
