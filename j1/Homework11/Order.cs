using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork11
{
    public class Order : IComparable
    {
        public int OrderId { get; set; }  //主键
        public string Client { get; set; }  //下单人
        public DateTime Date { get; set; }  //订单时间
        public string Adress{get;set;}  //地址
        public double TotalAmount { get; set; }//总金额
        public List<OrderItem> OrderItems { get; set; }

        public Order() { 
            Date = DateTime.Now;
            OrderItems = new List<OrderItem>();
        }

        public Order(int orderId, string adress, string client)
        {
            Date = DateTime.Now;
            this.OrderId = orderId;
            this.Adress = adress;
            this.Client = client;
            this.OrderItems = new List<OrderItem>();
            this.TotalAmount = 0;
        }

        public override bool Equals(object obj)
        {
            Order m = obj as Order;
            return m != null && m.OrderId == OrderId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

        }

        public override string ToString()
        {
            String str;
            str = "订单号:" + OrderId + "\t地址:" + Adress + "\t下单人:" + Client + "\t创建时间:" + Date + "\n明细：\n";
            for (int i = 0; i < OrderItems.Count; i++)
                str += OrderItems[i];
            str += "总金额：" + TotalAmount;
            return str;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Order))
                throw new NotImplementedException();
            Order rec = (Order)obj;
            return this.OrderId.CompareTo(rec.OrderId);
        }
    }
}
