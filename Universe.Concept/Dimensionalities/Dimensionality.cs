namespace Universe.Concept.Dimensionalities;

public abstract class Dimensionality
{
    public abstract string Name { get; }
    public abstract string Symbol { get; }
    public Unit UnitStandard { get; set; }  // 标准单位
    public List<Unit> ListUnit { get; set; } = new List<Unit>();

    // 可选的描述方法
    public virtual string GetDescription()
    {
        return $"{Name} ({Symbol})";
    }

    // 获取该量纲下所有单位的描述
    public string GetAllUnits()
    {
        return string.Join(", ", ListUnit.Select(u => u.ToString()));
    }
}

public class Unit
{
    public string Name { get; set; }   // 单位名称
    public string Symbol { get; set; } // 单位符号
    public double ConversionFactor { get; set; } // 转换为标准单位的系数

    public Unit(string name, string symbol, double conversionFactor)
    {
        Name = name;
        Symbol = symbol;
        ConversionFactor = conversionFactor;
    }

    public override string ToString()
    {
        return $"{Name} ({Symbol})";
    }
}

public class Length : Dimensionality
{
    public override string Name => "Length";
    public override string Symbol => "L";

    public Length()
    {
        Unit meter = new Unit("Meter", "m", 1.0);
        ListUnit.Add(meter);
        ListUnit.Add(new Unit("Kilometer", "km", 1000.0));
        ListUnit.Add(new Unit("Centimeter", "cm", 0.01));
        ListUnit.Add(new Unit("Mile", "mi", 1609.34));

        UnitStandard = meter;  // 设置标准单位
    }
}

public class Mass : Dimensionality
{
    public override string Name => "Mass";
    public override string Symbol => "M";

    public Mass()
    {
        Unit kilogram = new Unit("Kilogram", "kg", 1.0);
        ListUnit.Add(kilogram);
        ListUnit.Add(new Unit("Gram", "g", 0.001));
        ListUnit.Add(new Unit("Pound", "lb", 0.453592));

        UnitStandard = kilogram;  // 设置标准单位
    }
}

public class Time : Dimensionality
{
    public override string Name => "Time";
    public override string Symbol => "T";

    public Time()
    {
        Unit second = new Unit("Second", "s", 1.0);
        ListUnit.Add(second);
        ListUnit.Add(new Unit("Minute", "min", 60.0));
        ListUnit.Add(new Unit("Hour", "h", 3600.0));

        UnitStandard = second;  // 设置标准单位
    }
}

public class ElectricCurrent : Dimensionality
{
    public override string Name => "Electric Current";
    public override string Symbol => "I";

    public ElectricCurrent()
    {
        Unit ampere = new Unit("Ampere", "A", 1.0);
        ListUnit.Add(ampere);

        UnitStandard = ampere;  // 设置标准单位
    }
}

public class Temperature : Dimensionality
{
    public override string Name => "Temperature";
    public override string Symbol => "Θ";

    public Temperature()
    {
        Unit kelvin = new Unit("Kelvin", "K", 1.0);
        ListUnit.Add(kelvin);
        ListUnit.Add(new Unit("Celsius", "°C", 1.0));
        ListUnit.Add(new Unit("Fahrenheit", "°F", 5.0 / 9.0));

        UnitStandard = kelvin;  // 设置标准单位
    }
}

public class AmountOfSubstance : Dimensionality
{
    public override string Name => "Amount of Substance";
    public override string Symbol => "N";

    public AmountOfSubstance()
    {
        Unit mole = new Unit("Mole", "mol", 1.0);
        ListUnit.Add(mole);

        UnitStandard = mole;  // 设置标准单位
    }
}

public class LuminousIntensity : Dimensionality
{
    public override string Name => "Luminous Intensity";
    public override string Symbol => "J";

    public LuminousIntensity()
    {
        Unit candela = new Unit("Candela", "cd", 1.0);
        ListUnit.Add(candela);

        UnitStandard = candela;  // 设置标准单位
    }
}
public class Charge : Dimensionality
{
    public override string Name => "Charge";
    public override string Symbol => "Q";

    public Charge()
    {
        Unit coulomb = new Unit("Coulomb", "C", 1.0);  // 库仑为标准单位
        ListUnit.Add(coulomb);
        ListUnit.Add(new Unit("Millicoulomb", "mC", 1e-3));
        ListUnit.Add(new Unit("Microcoulomb", "µC", 1e-6));
        ListUnit.Add(new Unit("Nanocoulomb", "nC", 1e-9));

        UnitStandard = coulomb;  // 设置标准单位为库仑
    }
}

