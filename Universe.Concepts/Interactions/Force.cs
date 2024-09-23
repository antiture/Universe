using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Concepts.Interactions
{
    public abstract class Force
    {
        public abstract string Name { get; }

        // 子类将实现的计算力的抽象方法
        public abstract double CalculateForce();

        // 可选的：力之间的距离属性，通用的属性可以放在父类中
        public double Distance { get; set; }

        protected Force(double distance)
        {
            Distance = distance;
        }
    }
}
