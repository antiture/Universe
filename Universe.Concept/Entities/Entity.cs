 

namespace Universe.Concept.Entities;

public abstract class Entity
{
    // 虚拟的 Name 属性，子类可以根据需求重写
    public virtual string Name { get; protected set; }
    public List<Dimensionality> ListDimension { get; set; }

    protected Entity(string name, List<Dimensionality> dimensions = null)
    {
        Name = name; // 初始化 Name
        ListDimension = dimensions ?? new List<Dimensionality>();
    }

    // 添加物理量纲到物体
    public void AddDimension(Dimensionality dimension)
    {
        if (dimension != null)
        {
            ListDimension.Add(dimension);
        }
    }

    // 获取物体所有量纲的描述信息
    public string GetDescription()
    {
        if (ListDimension == null || !ListDimension.Any())
        {
            return $"{this.GetType().Name} has no specific dimensions.";
        }

        List<string> descriptions = new List<string>();
        foreach (var dimension in ListDimension)
        {
            descriptions.Add($"{dimension.Name}: {dimension.GetDescription()} (Standard Unit: {dimension.UnitStandard.Symbol})");
        }
        return string.Join("\n", descriptions);
    }
}
