using Homework11;
using HomeWork11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork06
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            orderService.AddOrder();
            orderService.DisplayOrder();
            orderService.FindOrder();
            orderService.DeleteOrder();
            //Order order = new Order(1, "地址1", "张三");
            //OrderItem orderItem = new OrderItem(1, "手机壳", 20, 2, order);
            //order.OrderItems.Add(orderItem);
            //Console.WriteLine(order);


        }
    }
}
