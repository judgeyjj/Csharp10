using Homework11;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeWork11
{

    public class OrderService
    {
        public void AddOrder()
        {
            Console.WriteLine("添加订单");
            using (var context = new OrderContext())
            {
                Order order = new Order();
                order.OrderItems = new List<OrderItem>();

                //录入信息
                order.OrderId = CreatOrderId();
                order.Client = AddCilent();
                order.Adress = AddAdress();
                //订单明细
                AddOrderItem(order);
                context.Orders.Add(order);
                context.SaveChanges();
                Console.WriteLine("添加成功! 内容如下：");
                Console.WriteLine(order);
            }
        }

        //删除订单
        public void DeleteOrder()
        {
            int orderId;
            Console.WriteLine("请输入要删除的订单号：");
            try
            {
                orderId = Convert.ToInt32(Console.ReadLine());
                if (orderId <= 0) 
                { Console.WriteLine("请输入正确的订单号！"); 
                    return; }
                using (var context = new OrderContext())
                {
                    var order = context.Orders.Include("OrderItems")
                        .SingleOrDefault(o => o.OrderId == orderId);
                    if(order!=null)
                    {
                        context.Orders.Remove(order);
                        context.SaveChanges();
                    }
                    else { Console.WriteLine("无法找到该订单!"); }
                }
            }
            catch
            {
                Console.WriteLine("请输入正确的订单号!");
                return;
            }
        }

        //查找订单
        public void FindOrder()
        {
            Console.WriteLine("选择查询方式");
            Console.WriteLine("C/c：按客户查询； O/o：按订单号查询");
            switch(Console.ReadLine())
            {
                case "C":
                case "c":
                    Console.WriteLine("请输入客户名:");
                    FindOrderByClient(Console.ReadLine());
                    break;
                case "O":
                case "o":
                    Console.WriteLine("输入订单号:");
                    try
                    {
                        int orderId = Convert.ToInt32(Console.ReadLine());
                        if(orderId<=0)
                        { 
                            Console.WriteLine("输入正确的订单号");
                            break;
                        }
                        FindOrderByOrderId(orderId);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("输入正确的订单号");
                        break;
                    }
                default:
                    Console.WriteLine("请输入正确的操作符！");
                    break;
            }
        }

        //创建订单号
        public int CreatOrderId()
        {
            using (var context = new OrderContext())
            {
                var query = context.Orders.Where(o => true);
                return query.Count()+1;              
            }
        }
       
        //添加客户
        public string AddCilent()
        {
            string client = "";
            using (var context = new OrderContext())
            {
                Console.WriteLine("输入客户姓名:");
                client = Console.ReadLine();
                return client;
            }
            
        }
       
        //添加地址
        public string AddAdress()
        {
            string adress = "";
            Console.WriteLine("输入地址:");
            adress = Console.ReadLine();
            return adress;
        }
        
        
        public void AddOrderItem(Order order)
        {
            bool quit = false;
            int orderItemNum = 0;
            int maxOrderItemNum = 20;
            int number = 0;
            string orderItemName = "";
            double price = 0;
            while(!quit && orderItemNum <= maxOrderItemNum)
            {
                Console.WriteLine("选择你需要的操作");
                Console.WriteLine("C/c:添加商品，Q/q:退出");
                switch (Console.ReadLine())
                {
                    case "C":
                    case "c":
                        Console.WriteLine("输入商品名称：");
                        orderItemName = Console.ReadLine();
                        try
                        {
                            Console.WriteLine("输入商品价格：");
                            price = Convert.ToDouble(Console.ReadLine());
                            if(price<=0)
                            {
                                Console.WriteLine("输入正确的价格");
                                break;
                            }
                            Console.WriteLine("输入商品数目：");
                            number = Convert.ToInt32(Console.ReadLine());
                            if (number <= 0)
                            {
                                Console.WriteLine("输入正确的数目");
                                break; 
                            }
                            OrderItem orderItem = new OrderItem(order.OrderId, orderItemName
                                , price,number ,order);
                            order.OrderItems.Add(orderItem);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("请输入正确的数字!");
                            break;
                        }
                    case "Q":
                    case "q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("请输入正确的操作符！");
                        break;

                }
                
            }

        }

        //按客户名查找
        public void FindOrderByClient(string client)
        {
            using (var context = new OrderContext())
            {
                context.Orders.Where(o => o.Client == client)
                     .ForEachAsync(o => Console.WriteLine(o));
            }
        }
        
        //按订单号查找
        public void FindOrderByOrderId(int orderId)
        {
            using (var context = new OrderContext())
            {
                context.Orders.Where(o => o.OrderId == orderId)
                     .ForEachAsync(o => Console.WriteLine(o));
            }
        }
    
        public void DisplayOrder()
        {
            using(var context = new OrderContext())
            {
                var orderQuery = context.Orders.Where(o => true).ForEachAsync(o => Console.WriteLine(o));
            }
        }
    
    }
}