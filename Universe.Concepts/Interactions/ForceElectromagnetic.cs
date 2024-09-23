using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Concepts.Interactions
{
    public class ForceElectromagnetic : Force
    {
        public double Charge1 { get; set; }
        public double Charge2 { get; set; }

        private const double K = 8.9875517873681764e9; // 电磁常数 (N·m²/C²)

        public ForceElectromagnetic(double charge1, double charge2, double distance)
            : base(distance)
        {
            Charge1 = charge1;
            Charge2 = charge2;
        }

        public override string Name => "Electromagnetic Force";

        public override double CalculateForce()
        {
            return K * (Charge1 * Charge2) / (Distance * Distance);
        }
    }

}
