//namespace Universe.Concept.Entities.Atomic;
//public abstract class ParticleSub : Entity
//{
//    // 用 readonly 字段存储名称，确保名称不可修改
//    private readonly string _name;
//    public override string Name => _name;

//    public Charge Charge { get; private set; }
//    public Mass Mass { get; private set; }

//    // 构造函数传递名称并初始化 Charge 和 Mass
//    public ParticleSub(string name, double chargeValue, double massValue) : base(name)
//    {
//        _name = name; // 设置名称
//                      // 直接通过构造函数传递值，简化代码
//        Charge = new Charge(chargeValue, UnitCharge.C);
//        Mass = new Mass(massValue, UnitMass.kg);

//        // 将它们添加到 ListDimension
//        AddDimension(Charge);
//        AddDimension(Mass);
//    }
//}


//public class Proton : ParticleSub
//{
//    public Proton() : base("Proton", 1.0, 1.6726219e-27) // 1 库仑，1.6726219e-27 千克
//    {
//    }
//}

//public class Neutron : ParticleSub
//{
//    public Neutron() : base("Neutron", 0.0, 1.6749275e-27) // 无电荷，1.6749275e-27 千克
//    {
//    }
//}

//public class Electron : ParticleSub
//{
//    public Electron() : base("Electron", -1.0, 9.10938356e-31) // -1 库仑，9.10938356e-31 千克
//    {
//    }
//}
