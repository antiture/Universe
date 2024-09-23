

//namespace Universe.Concept.Entities.Atomic;

//public abstract class Atom : Entity
//{
//    private readonly string _name;
//    public override string Name => _name;

//    public List<Proton> ListProton { get; set; }
//    public List<Neutron> ListNeutron { get; set; }
//    public List<Electron> ListElectron { get; set; }
//    public Mass Mass { get; private set; }
//    public Charge Charge { get; private set; }

//    // 构造函数初始化质子、中子和电子
//    protected Atom(string name, int protonCount, int neutronCount, int electronCount) : base(name)
//    {
//        _name = name;

//        // 初始化粒子列表
//        ListProton = InitializeParticles<Proton>(protonCount);
//        ListNeutron = InitializeParticles<Neutron>(neutronCount);
//        ListElectron = InitializeParticles<Electron>(electronCount);

//        // 计算原子的总质量和总电荷
//        Mass = CalculateTotalMass();
//        Charge = CalculateTotalCharge();

//        // 将质量和电荷添加到 ListDimension
//        AddDimension(Mass);
//        AddDimension(Charge);
//    }

//    // 使用粒子类来初始化质子、中子和电子
//    private List<T> InitializeParticles<T>(int count) where T : ParticleSub, new()
//    {
//        var particles = new List<T>();
//        for (int i = 0; i < count; i++)
//        {
//            particles.Add(new T());
//        }
//        return particles;
//    }

//    // 通过质子和中子对象来计算总质量
//    private Mass CalculateTotalMass()
//    {
//        double totalMass = ListProton.Sum(p => p.Mass.Quantity.Value) +
//                           ListNeutron.Sum(n => n.Mass.Quantity.Value) +
//                           ListElectron.Sum(e => e.Mass.Quantity.Value);

//        return new Mass(totalMass, UnitMass.kg);
//    }

//    // 通过质子和电子对象来计算总电荷
//    private Charge CalculateTotalCharge()
//    {
//        double totalCharge = ListProton.Sum(p => p.Charge.Quantity.Value) +
//                             ListElectron.Sum(e => e.Charge.Quantity.Value);

//        return new Charge(totalCharge, UnitCharge.C);
//    }

//    // 获取原子的描述，包括质量和电荷
//    public virtual string GetAtomDescription()
//    {
//        return $"{Name}: {ListProton.Count} Protons, {ListNeutron.Count} Neutrons, {ListElectron.Count} Electrons, " +
//               $"Mass: {Mass.Quantity.Value} {Mass.Quantity.Unit.Symbol}, " +
//               $"Charge: {Charge.Quantity.Value} {Charge.Quantity.Unit.Symbol}";
//    }
//}

//// 子类 AtomHydrogen (氢原子)
//public class AtomHydrogen : Atom
//{
//    public AtomHydrogen() : base("Hydrogen", 1, 0, 1) // 1个质子, 0个中子, 1个电子
//    {
//    }
//}

//// 子类 AtomHelium (氦原子)
//public class AtomHelium : Atom
//{
//    public AtomHelium() : base("Helium", 2, 2, 2) // 2个质子, 2个中子, 2个电子
//    {
//    }
//}
