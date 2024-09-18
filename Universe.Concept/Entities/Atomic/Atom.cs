

namespace Universe.Concept.Entities.Atomic;

public abstract class Atom : Entity
{
    // 重写 Name 属性为只读，确保外部不能修改
    // 用 readonly 字段存储名称，确保名称完全不可修改
    private readonly string _name;
    public override string Name => _name;
    public List<Proton> ListProton { get; set; }
    public List<Neutron> ListNeutron { get; set; }
    public List<Electron> ListElectron { get; set; }
    public Mass AtomMass { get; set; }
    public Charge AtomCharge { get; set; }

    // 在构造函数中强制初始化质量和电荷
    protected Atom(string name, int protonCount, int neutronCount, int electronCount) : base(name)
    {
        _name = name; // 初始化名称
        // 强制初始化原子的基础物理量纲
        AtomMass = new Mass();
        AtomCharge = new Charge();

        // 计算原子的质量和电荷
        InitializeAtomicDimensions(protonCount, neutronCount, electronCount);

        // 初始化粒子列表
        ListProton = InitializeParticles<Proton>(protonCount);
        ListNeutron = InitializeParticles<Neutron>(neutronCount);
        ListElectron = InitializeParticles<Electron>(electronCount);

        // 添加维度到 `ListDimension`
        AddDimension(AtomMass);
        AddDimension(AtomCharge);
    }

    private List<T> InitializeParticles<T>(int count) where T : new()
    {
        var particles = new List<T>();
        for (int i = 0; i < count; i++)
        {
            particles.Add(new T());
        }
        return particles;
    }

    // 计算并设置原子的质量和电荷量
    private void InitializeAtomicDimensions(int protonCount, int neutronCount, int electronCount)
    {
        // 假设每个质子和中子的质量分别为1.6726e-27 kg，电子质量为9.10938356e-31 kg
        double protonMass = 1.6726e-27;
        double neutronMass = 1.6749e-27;
        double electronMass = 9.10938356e-31;

        // 计算原子的总质量
        double totalMass = protonCount * protonMass + neutronCount * neutronMass + electronCount * electronMass;
        AtomMass.UnitStandard.ConversionFactor = totalMass; // 设置原子的总质量

        // 计算原子的电荷（假设质子+1，电子-1）
        int totalCharge = protonCount - electronCount;
        AtomCharge.UnitStandard.ConversionFactor = totalCharge; // 设置原子的总电荷
    }

    // 获取原子的描述，包括质量和电荷
    public virtual string GetAtomDescription()
    {
        return $"{Name}: {ListProton.Count} Protons, {ListNeutron.Count} Neutrons, {ListElectron.Count} Electrons, " +
               $"Mass: {AtomMass.UnitStandard.ConversionFactor} kg, Charge: {AtomCharge.UnitStandard.ConversionFactor} C";
    }
}

// 子类 AtomHydrogen (氢原子)
public class AtomHydrogen : Atom
{
    public AtomHydrogen() : base("Hydrogen", 1, 0, 1) // 1个质子, 0个中子, 1个电子
    {
    }
}

// 子类 AtomHelium (氦原子)
public class AtomHelium : Atom
{
    public AtomHelium() : base("Helium", 2, 2, 2) // 2个质子, 2个中子, 2个电子
    {
    }
}

// 更多元素子类可以类似这样定义
public class AtomLithium : Atom
{
    public AtomLithium() : base("Lithium", 3, 3, 3) // 3个质子, 3个中子, 3个电子
    {
    }
}
