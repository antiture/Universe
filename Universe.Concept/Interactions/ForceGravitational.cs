using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Concept.Interactions
{
    public class ForceGravitational : Force
    {
        public double Mass1 { get; set; }
        public double Mass2 { get; set; }

        private const double G = 6.67430e-11; // 引力常数 (m³/kg/s²)

        public ForceGravitational(double mass1, double mass2, double distance)
            : base(distance)
        {
            Mass1 = mass1;
            Mass2 = mass2;
        }

        public override string Name => "ForceGravitational";

        public override double CalculateForce()
        {
            return G * (Mass1 * Mass2) / (Distance * Distance);
        }
    }

}
