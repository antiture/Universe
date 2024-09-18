 

namespace Universe.Concept.Entities.Quan;

internal class Lepton
{
}
internal class ParticleEle
{
}

// 基础夸克类
public abstract class Quark
{
    public abstract string Flavor { get; }
    public double Charge { get; set; }  // 电荷
    public double Mass { get; set; }  // 质量
}

// 上夸克类
public class UpQuark : Quark
{
    public override string Flavor => "Up";
    public UpQuark()
    {
        Charge = 2 / 3.0;  // 上夸克的电荷
        Mass = 2.2e-3;     // 上夸克的质量 (单位：GeV/c^2)
    }
}

// 下夸克类
public class DownQuark : Quark
{
    public override string Flavor => "Down";
    public DownQuark()
    {
        Charge = -1 / 3.0;  // 下夸克的电荷
        Mass = 4.7e-3;      // 下夸克的质量 (单位：GeV/c^2)
    }
}

// 其他夸克类型可以类似定义
