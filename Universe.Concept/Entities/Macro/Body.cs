 

namespace Universe.Concept.Entities.Macro;

public abstract class Body
{
   

    protected Body(int protonCount, int neutronCount, int electronCount, List<Dimensionality> dimensions = null) : base(dimensions) // 将维度传递给基类
    {
        ListProton = InitializeParticles<Proton>(protonCount);
        ListNeutron = InitializeParticles<Neutron>(neutronCount);
        ListElectron = InitializeParticles<Electron>(electronCount);
    }

    // 通用的粒子初始化方法，用于质子、中子、电子
    private List<T> InitializeParticles<T>(int count) where T : new()
    {
        var particles = new List<T>();
        for (int i = 0; i < count; i++)
        {
            particles.Add(new T());  // 使用无参数构造函数创建粒子
        }
        return particles;
    }

    // 获取原子基本描述信息
    public virtual string GetAtomDescription()
    {
        return $"{Name}: {ListProton.Count} Protons, {ListNeutron.Count} Neutrons, {ListElectron.Count} Electrons";
    }
}
