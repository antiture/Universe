using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Concepts.Interactions
{
    public class ForceStrong : Force
    {
        public double ColorCharge1 { get; set; }
        public double ColorCharge2 { get; set; }

        private const double ConstantStrongCoupling = 1.0; // 强相互作用的简化常数

        public ForceStrong(double colorCharge1, double colorCharge2, double distance)
            : base(distance)
        {
            ColorCharge1 = colorCharge1;
            ColorCharge2 = colorCharge2;
        }

        public override string Name => "ForceStrong";

        public override double CalculateForce()
        {
            //return StrongCouplingConstant * (ColorCharge1 * ColorCharge2) / Distance;
            return ColorCharge1;
        }
    }

}
