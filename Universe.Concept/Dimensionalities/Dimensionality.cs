

namespace Universe.Concept.Dimensionalities;

public class Unit : IEquatable<Unit>
{
    public string Name { get; }         // 单位名称
    public string Symbol { get; }       // 单位符号
    public double ConversionFactor { get; } // 转换为标准单位的系数
    public Dimensionality Dimensionality { get; } // 所属的 Dimensionality

    public Unit(string name, string symbol, double conversionFactor, Dimensionality dimensionality)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        ConversionFactor = conversionFactor;
        Dimensionality = dimensionality ?? throw new ArgumentNullException(nameof(dimensionality));
    }

    public override string ToString()
    {
        return $"{Name} ({Symbol})";
    }

    // 实现 IEquatable<Unit>
    public bool Equals(Unit other)
    {
        if (other is null)
            return false;

        return Name == other.Name &&
               Symbol == other.Symbol &&
               ConversionFactor == other.ConversionFactor &&
               Dimensionality.Equals(other.Dimensionality);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Unit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Symbol, ConversionFactor, Dimensionality);
    }
}

public class Quantity
{
    public double Value { get; set; }   // 数值
    public Unit Unit { get; set; }      // 单位

    public Quantity(double value, Unit unit)
    {
        Value = value;
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
    }

    // 转换到指定单位
    public Quantity ConvertTo(Unit targetUnit)
    {
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));

        // 使用所属的 Dimensionality 进行转换
        return Unit.Dimensionality.ConvertTo(this, targetUnit);
    }

    public override string ToString()
    {
        return $"{Value} {Unit.Symbol}";
    }
    // 可选：实现运算符重载
    public static Quantity operator +(Quantity q1, Quantity q2)
    {
        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
            throw new InvalidOperationException("Cannot add quantities of different dimensionalities.");

        // 将 q2 转换为 q1 的单位
        Quantity q2Converted = q2.ConvertTo(q1.Unit);
        return new Quantity(q1.Value + q2Converted.Value, q1.Unit);
    }

    public static Quantity operator -(Quantity q1, Quantity q2)
    {
        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
            throw new InvalidOperationException("Cannot subtract quantities of different dimensionalities.");

        // 将 q2 转换为 q1 的单位
        Quantity q2Converted = q2.ConvertTo(q1.Unit);
        return new Quantity(q1.Value - q2Converted.Value, q1.Unit);
    }
}


public abstract class Dimensionality
{
    
    public abstract string Name { get; }
    public abstract string Symbol { get; }
    public Unit UnitStandard { get; }
    public HashSet<Unit> ListUnit { get; } = new HashSet<Unit>();

    protected Dimensionality(Unit standardUnit)
    {
        UnitStandard = standardUnit ?? throw new ArgumentNullException(nameof(standardUnit));
        ListUnit.Add(standardUnit);  // 将标准单位添加到单位列表
    }

    protected void AddUnits(params Unit[] units)
    {
        foreach (var unit in units)
        {
            if (!ListUnit.Contains(unit))
            {
                ListUnit.Add(unit);
            }
        }
    }

    // 将数量转换为目标单位，允许子类重写以处理特殊转换逻辑
    public virtual Quantity ConvertTo(Quantity quantity, Unit targetUnit)
    {
        if (quantity == null)
            throw new ArgumentNullException(nameof(quantity));

        if (!ListUnit.Contains(quantity.Unit))
        {
            throw new ArgumentException($"Unit {quantity.Unit.Name} is not recognized for {Name}.");
        }

        if (!ListUnit.Contains(targetUnit))
        {
            throw new ArgumentException($"Target unit {targetUnit.Name} is not recognized for {Name}.");
        }

        return quantity.ConvertTo(targetUnit);
    }

    public Quantity ConvertToStandard(Quantity quantity)
    {
        return ConvertTo(quantity, UnitStandard);
    }

    public virtual string GetDescription()
    {
        return $"{Name} ({Symbol})";
    }

    public string GetAllUnits()
    {
        return string.Join(", ", ListUnit.Select(u => u.ToString()));
    }

    public override string ToString()
    {
        return $"{Name} ({Symbol}) [Standard Unit: {UnitStandard.Name}]";
    }
}

public class DimensionalityMass : Dimensionality
{
    public override string Name => "Mass";

    // 实现 Symbol 属性
    public override string Symbol => "M";
    // 单例实例
    public static readonly DimensionalityMass Instance = new DimensionalityMass();

    // 私有构造函数，确保无法通过外部实例化
    private DimensionalityMass() : base(UnitMass.kg)
    {
        AddUnits(UnitMass.g, UnitMass.lb);
    }
}

// 定义质量类
public class Mass : Dimensionality
{

    public Quantity Quantity { get; private set; }  // 存储长度的物理量
    public override string Name => "Mass";
    public override string Symbol => "M";

    // 重载构造函数，允许直接接收数值和单位，生成 Quantity
    public Mass(double value, Unit unit) : base(UnitMass.kg)
    {
        AddUnits(UnitMass.g, UnitMass.lb);
        Quantity = new Quantity(value, unit);
    }
    public Mass() : base(UnitMass.kg)
    {
        AddUnits(UnitMass.g, UnitMass.lb);
    }
}

// 定义常用质量单位
public static class UnitMass
{
    public static readonly Unit kg = new Unit("Kilogram", "kg", 1.0, DimensionalityMass.Instance);
    public static readonly Unit g = new Unit("Gram", "g", 0.001, DimensionalityMass.Instance);
    public static readonly Unit lb = new Unit("Pound", "lb", 0.453592, DimensionalityMass.Instance);
}

public class DimensionalityLength : Dimensionality
{
    public override string Name => "Length";
    public override string Symbol => "L";
    public static readonly DimensionalityLength Instance = new DimensionalityLength();

    private DimensionalityLength() : base(UnitLength.m)
    {
        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
    }
}

public class Length : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Length";
    public override string Symbol => "L";

    public Length(double value, Unit unit) : base(UnitLength.m)
    {
        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
        Quantity = new Quantity(value, unit);
    }

    public Length() : base(UnitLength.m)
    {
        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
    }
}

public static class UnitLength
{
    public static readonly Unit m = new Unit("Meter", "m", 1.0, DimensionalityLength.Instance);
    public static readonly Unit km = new Unit("Kilometer", "km", 1000.0, DimensionalityLength.Instance);
    public static readonly Unit cm = new Unit("Centimeter", "cm", 0.01, DimensionalityLength.Instance);
    public static readonly Unit mi = new Unit("Mile", "mi", 1609.34, DimensionalityLength.Instance);
}


public class DimensionalityTime : Dimensionality
{
    public override string Name => "Time";
    public override string Symbol => "T";
    public static readonly DimensionalityTime Instance = new DimensionalityTime();

    private DimensionalityTime() : base(UnitTime.s)
    {
        AddUnits(UnitTime.min, UnitTime.hr);
    }
}

public class Time : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Time";
    public override string Symbol => "T";

    public Time(double value, Unit unit) : base(UnitTime.s)
    {
        AddUnits(UnitTime.min, UnitTime.hr);
        Quantity = new Quantity(value, unit);
    }

    public Time() : base(UnitTime.s)
    {
        AddUnits(UnitTime.min, UnitTime.hr);
    }
}

public static class UnitTime
{
    public static readonly Unit s = new Unit("Second", "s", 1.0, DimensionalityTime.Instance);
    public static readonly Unit min = new Unit("Minute", "min", 60.0, DimensionalityTime.Instance);
    public static readonly Unit hr = new Unit("Hour", "h", 3600.0, DimensionalityTime.Instance);
}

public class DimensionalityElectricCurrent : Dimensionality
{
    public override string Name => "Electric Current";
    public override string Symbol => "I";
    public static readonly DimensionalityElectricCurrent Instance = new DimensionalityElectricCurrent();

    private DimensionalityElectricCurrent() : base(UnitElectricCurrent.A)
    {
        AddUnits(UnitElectricCurrent.A);
    }
}

public class ElectricCurrent : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Electric Current";
    public override string Symbol => "I";

    public ElectricCurrent(double value, Unit unit) : base(UnitElectricCurrent.A)
    {
        Quantity = new Quantity(value, unit);
    }
}

public static class UnitElectricCurrent
{
    public static readonly Unit A = new Unit("Ampere", "A", 1.0, DimensionalityElectricCurrent.Instance);
}


// 定义温度类，重写 ConvertTo 方法处理特殊的温度转换
// 定义温度类，重写 ConvertTo 方法处理特殊的温度转换
public class Temperature : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Temperature";
    public override string Symbol => "Θ";

    // 重载构造函数
    public Temperature(double value, Unit unit) : base(UnitTemperature.K)
    {
        AddUnits(UnitTemperature.C, UnitTemperature.F);
        Quantity = new Quantity(value, unit);
    }

    // 重写 ConvertTo 方法，特殊处理温度的转换
    public override Quantity ConvertTo(Quantity quantity, Unit targetUnit)
    {
        if (quantity == null)
            throw new ArgumentNullException(nameof(quantity));

        if (!ListUnit.Contains(quantity.Unit))
        {
            throw new ArgumentException($"Unit {quantity.Unit.Name} is not recognized for Temperature.");
        }

        if (!ListUnit.Contains(targetUnit))
        {
            throw new ArgumentException($"Target unit {targetUnit.Name} is not recognized for Temperature.");
        }

        double convertedValue;

        // 温度的特殊转换逻辑
        if (quantity.Unit.Equals(UnitTemperature.K)) // 开尔文到目标单位
        {
            convertedValue = ConvertKelvinToTarget(quantity.Value, targetUnit);
        }
        else if (quantity.Unit.Equals(UnitTemperature.C)) // 摄氏度到目标单位
        {
            double valueInK = quantity.Value + 273.15; // 先转换为开尔文
            convertedValue = ConvertKelvinToTarget(valueInK, targetUnit);
        }
        else if (quantity.Unit.Equals(UnitTemperature.F)) // 华氏度到目标单位
        {
            double valueInK = (quantity.Value + 459.67) * 5.0 / 9.0; // 先转换为开尔文
            convertedValue = ConvertKelvinToTarget(valueInK, targetUnit);
        }
        else
        {
            throw new NotSupportedException($"Conversion from {quantity.Unit.Name} is not supported.");
        }

        return new Quantity(convertedValue, targetUnit);
    }

    // 从开尔文转换为目标单位
    private double ConvertKelvinToTarget(double valueInK, Unit targetUnit)
    {
        if (targetUnit.Equals(UnitTemperature.K)) // 开尔文
        {
            return valueInK;
        }
        else if (targetUnit.Equals(UnitTemperature.C)) // 摄氏度
        {
            return valueInK - 273.15;
        }
        else if (targetUnit.Equals(UnitTemperature.F)) // 华氏度
        {
            return valueInK * 9.0 / 5.0 - 459.67;
        }
        else
        {
            throw new NotSupportedException($"Conversion to {targetUnit.Name} is not supported.");
        }
    }
}

// 定义常用温度单位
public static class UnitTemperature
{
    public static readonly Unit K = new Unit("Kelvin", "K", 1.0, DimensionalityTemperature.Instance);
    public static readonly Unit C = new Unit("Celsius", "°C", 1.0, DimensionalityTemperature.Instance);
    public static readonly Unit F = new Unit("Fahrenheit", "°F", 5.0 / 9.0, DimensionalityTemperature.Instance);
}

// 定义温度量纲类
public class DimensionalityTemperature : Dimensionality
{
    public override string Name => "Temperature";
    public override string Symbol => "Θ";
    public static readonly DimensionalityTemperature Instance = new DimensionalityTemperature();

    private DimensionalityTemperature() : base(UnitTemperature.K)
    {
        AddUnits(UnitTemperature.C, UnitTemperature.F);
    }
}

// 定义物质的量量纲类
public class DimensionalityAmountOfSubstance : Dimensionality
{
    public override string Name => "Amount of Substance";
    public override string Symbol => "N";
    public static readonly DimensionalityAmountOfSubstance Instance = new DimensionalityAmountOfSubstance();

    private DimensionalityAmountOfSubstance() : base(UnitAmountOfSubstance.mol)
    {
        // 未来如果有其他物质的量单位，可以在这里添加
    }
}

// 定义物质的量类
public class AmountOfSubstance : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Amount of Substance";
    public override string Symbol => "N";

    // 重载构造函数
    public AmountOfSubstance(double value, Unit unit) : base(UnitAmountOfSubstance.mol)
    {
        Quantity = new Quantity(value, unit);
    }

    public AmountOfSubstance() : base(UnitAmountOfSubstance.mol)
    {
        // 如果需要默认构造函数
    }
}

// 定义常用物质的量单位
public static class UnitAmountOfSubstance
{
    public static readonly Unit mol = new Unit("Mole", "mol", 1.0, DimensionalityAmountOfSubstance.Instance);
}

// 定义光强量纲类
public class DimensionalityLuminousIntensity : Dimensionality
{
    public override string Name => "Luminous Intensity";
    public override string Symbol => "J";
    public static readonly DimensionalityLuminousIntensity Instance = new DimensionalityLuminousIntensity();

    private DimensionalityLuminousIntensity() : base(UnitLuminousIntensity.cd)
    {
        // 未来如果有其他光强单位，可以在这里添加
    }
}

// 定义光强类
public class LuminousIntensity : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Luminous Intensity";
    public override string Symbol => "J";

    // 重载构造函数
    public LuminousIntensity(double value, Unit unit) : base(UnitLuminousIntensity.cd)
    {
        Quantity = new Quantity(value, unit);
    }

    public LuminousIntensity() : base(UnitLuminousIntensity.cd)
    {
        // 如果需要默认构造函数
    }
}

// 定义常用光强单位
public static class UnitLuminousIntensity
{
    public static readonly Unit cd = new Unit("Candela", "cd", 1.0, DimensionalityLuminousIntensity.Instance);
}

// 定义电荷类
public class DimensionalityCharge : Dimensionality
{
    public override string Name => "Charge";
    public override string Symbol => "Q";
    public static readonly DimensionalityCharge Instance = new DimensionalityCharge();

    private DimensionalityCharge() : base(UnitCharge.C)
    {
        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
    }
}

public class Charge : Dimensionality
{
    public Quantity Quantity { get; private set; }

    public override string Name => "Charge";
    public override string Symbol => "Q";

    public Charge(double value, Unit unit) : base(UnitCharge.C)
    {
        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
        Quantity = new Quantity(value, unit);
    }

    public Charge() : base(UnitCharge.C)
    {
        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
    }
}

public static class UnitCharge
{
    public static readonly Unit C = new Unit("Coulomb", "C", 1.0, DimensionalityCharge.Instance);
    public static readonly Unit mC = new Unit("Millicoulomb", "mC", 1e-3, DimensionalityCharge.Instance);
    public static readonly Unit µC = new Unit("Microcoulomb", "µC", 1e-6, DimensionalityCharge.Instance);
    public static readonly Unit nC = new Unit("Nanocoulomb", "nC", 1e-9, DimensionalityCharge.Instance);
}


// 定义常量类
public static class Constant
{
    public static class Mass
    {
        public static readonly Quantity Neutron = new Quantity(1.67492749804e-27, UnitMass.kg);
        public static readonly Quantity Proton = new Quantity(1.67262192369e-27, UnitMass.kg);
        public static readonly Quantity Electron = new Quantity(9.1093837015e-31, UnitMass.kg);
    }

    public static class Charge
    {
        public static readonly Quantity Electron = new Quantity(-1.602176634e-19, UnitCharge.C);
        public static readonly Quantity Proton = new Quantity(1.602176634e-19, UnitCharge.C);
    }

    public static class Length
    {
        public static readonly Quantity Planck = new Quantity(1.616255e-35, UnitLength.m);
    }
}
