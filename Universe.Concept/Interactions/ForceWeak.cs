using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Concept.Interactions
{
    public class ForceWeak : Force
    {
        public double WeakCharge1 { get; set; }
        public double WeakCharge2 { get; set; }

        private const double ConstantWeakCoupling = 1.1663787e-5; // Fermi 常数 (GeV⁻²)

        public ForceWeak(double weakCharge1, double weakCharge2, double distance)
            : base(distance)
        {
            WeakCharge1 = weakCharge1;
            WeakCharge2 = weakCharge2;
        }

        public override string Name => "ForceWeak";

        public override double CalculateForce()
        {
            return WeakCouplingConstant * (WeakCharge1 * WeakCharge2) / (Distance * Distance);
        }
    }

}
