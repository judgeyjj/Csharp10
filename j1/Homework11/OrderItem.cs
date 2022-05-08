using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork11
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }//主键
        public string Name { get; set; }  //商品名称
        public double Price { get; set; } //商品单价
        public int Number { get; set; }//需求量

        public OrderItem() { }
        public OrderItem(int orderItemId,string name, double price, int numb, Order order)
        {
            this.OrderItemId = orderItemId;
            this.Name = name;
            this.Price = price;
            this.Number = numb;
            TotalAcount(order);
        }
        public void TotalAcount(Order order)
        {
            order.TotalAmount += Price * Number;
        }
        public override bool Equals(object obj)
        {
            OrderItem m = obj as OrderItem;
            return m != null && m.OrderItemId == OrderItemId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();

        }

        public override string ToString()
        {
            return "\t商品名称：" + Name + "\t单价：" + Price + "\t需求量:" + Number + "\n";
        }
    }
}
