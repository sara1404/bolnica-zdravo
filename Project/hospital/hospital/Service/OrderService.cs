﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
using System.Threading;

namespace Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly RoomRepository _roomRepository;
        private ObservableCollection<Order> _orders;

        public OrderService(OrderRepository orderRepo,RoomRepository roomRepo) { _orderRepository = orderRepo; _roomRepository = roomRepo; }

        public void Create(Order newOrder)
        {
            newOrder.Id = GetNewId();
            _orderRepository.Create(newOrder);
        }

        public int GetNewId()
        {
            _orders = _orderRepository.FindAll();
            if (_orders.Count == 0)
                return 100;
            else
                return _orders[_orders.Count - 1].Id + 1;
        }
        public ObservableCollection<Order> FindAll()
        {
            return _orderRepository.FindAll();
        }

        public void orderTracker()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(10000);
                    foreach (Order order in FindAll())
                    {
                        if (order.OrderDate.AddDays(3)<DateTime.Now)
                        {
                            AddEquipmentInWarehouse(order);
                            _orderRepository.DeleteById(order.Id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void AddEquipmentInWarehouse(Order order)
        {
            Room warhouse = _roomRepository.FindRoomById("9559");
            warhouse.AddEquipment(order.Equipment);
        }
    }
}
