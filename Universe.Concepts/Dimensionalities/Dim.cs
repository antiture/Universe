

//namespace Universe.Concept.Dimensionalities;


//public class DimensionalityLength : Dimensionality
//{
//    public override string Name => "Length";
//    public override string Symbol => "L";
//    public static readonly DimensionalityLength Instance = new DimensionalityLength();

//    private DimensionalityLength() : base(UnitLength.m)
//    {
//        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
//    }
//}

//public class Length : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Length";
//    public override string Symbol => "L";

//    public Length(double value, Unit unit) : base(UnitLength.m)
//    {
//        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
//        Quantity = new Quantity(value, unit);
//    }

//    public Length() : base(UnitLength.m)
//    {
//        AddUnits(UnitLength.km, UnitLength.cm, UnitLength.mi);
//    }
//}

//public static class UnitLength
//{
//    public static readonly Unit m = new Unit("Meter", "m", 1.0, DimensionalityLength.Instance);
//    public static readonly Unit km = new Unit("Kilometer", "km", 1000.0, DimensionalityLength.Instance);
//    public static readonly Unit cm = new Unit("Centimeter", "cm", 0.01, DimensionalityLength.Instance);
//    public static readonly Unit mi = new Unit("Mile", "mi", 1609.34, DimensionalityLength.Instance);
//}


//public class DimensionalityTime : Dimensionality
//{
//    public override string Name => "Time";
//    public override string Symbol => "T";
//    public static readonly DimensionalityTime Instance = new DimensionalityTime();

//    private DimensionalityTime() : base(UnitTime.s)
//    {
//        AddUnits(UnitTime.min, UnitTime.hr);
//    }
//}

//public class Time : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Time";
//    public override string Symbol => "T";

//    public Time(double value, Unit unit) : base(UnitTime.s)
//    {
//        AddUnits(UnitTime.min, UnitTime.hr);
//        Quantity = new Quantity(value, unit);
//    }

//    public Time() : base(UnitTime.s)
//    {
//        AddUnits(UnitTime.min, UnitTime.hr);
//    }
//}

//public static class UnitTime
//{
//    public static readonly Unit s = new Unit("Second", "s", 1.0, DimensionalityTime.Instance);
//    public static readonly Unit min = new Unit("Minute", "min", 60.0, DimensionalityTime.Instance);
//    public static readonly Unit hr = new Unit("Hour", "h", 3600.0, DimensionalityTime.Instance);
//}

//public class DimensionalityElectricCurrent : Dimensionality
//{
//    public override string Name => "Electric Current";
//    public override string Symbol => "I";
//    public static readonly DimensionalityElectricCurrent Instance = new DimensionalityElectricCurrent();

//    private DimensionalityElectricCurrent() : base(UnitElectricCurrent.A)
//    {
//        AddUnits(UnitElectricCurrent.A);
//    }
//}

//public class ElectricCurrent : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Electric Current";
//    public override string Symbol => "I";

//    public ElectricCurrent(double value, Unit unit) : base(UnitElectricCurrent.A)
//    {
//        Quantity = new Quantity(value, unit);
//    }
//}

//public static class UnitElectricCurrent
//{
//    public static readonly Unit A = new Unit("Ampere", "A", 1.0, DimensionalityElectricCurrent.Instance);
//}

//// 定义温度类，重写 ConvertTo 方法处理特殊的温度转换
//public class Temperature : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Temperature";
//    public override string Symbol => "Θ";

//    // 重载构造函数
//    public Temperature(double value, Unit unit) : base(UnitTemperature.K)
//    {
//        AddUnits(UnitTemperature.C, UnitTemperature.F);
//        Quantity = new Quantity(value, unit);
//    }

//    // 重写 ConvertTo 方法，特殊处理温度的转换
//    public override Quantity ConvertTo(Quantity quantity, Unit targetUnit)
//    {
//        if (quantity == null)
//            throw new ArgumentNullException(nameof(quantity));

//        if (!ListUnit.Contains(quantity.Unit))
//        {
//            throw new ArgumentException($"Unit {quantity.Unit.Name} is not recognized for Temperature.");
//        }

//        if (!ListUnit.Contains(targetUnit))
//        {
//            throw new ArgumentException($"Target unit {targetUnit.Name} is not recognized for Temperature.");
//        }

//        double convertedValue;

//        // 温度的特殊转换逻辑
//        if (quantity.Unit.Equals(UnitTemperature.K)) // 开尔文到目标单位
//        {
//            convertedValue = ConvertKelvinToTarget(quantity.Value, targetUnit);
//        }
//        else if (quantity.Unit.Equals(UnitTemperature.C)) // 摄氏度到目标单位
//        {
//            double valueInK = quantity.Value + 273.15; // 先转换为开尔文
//            convertedValue = ConvertKelvinToTarget(valueInK, targetUnit);
//        }
//        else if (quantity.Unit.Equals(UnitTemperature.F)) // 华氏度到目标单位
//        {
//            double valueInK = (quantity.Value + 459.67) * 5.0 / 9.0; // 先转换为开尔文
//            convertedValue = ConvertKelvinToTarget(valueInK, targetUnit);
//        }
//        else
//        {
//            throw new NotSupportedException($"Conversion from {quantity.Unit.Name} is not supported.");
//        }

//        return new Quantity(convertedValue, targetUnit);
//    }

//    // 从开尔文转换为目标单位
//    private double ConvertKelvinToTarget(double valueInK, Unit targetUnit)
//    {
//        if (targetUnit.Equals(UnitTemperature.K)) // 开尔文
//        {
//            return valueInK;
//        }
//        else if (targetUnit.Equals(UnitTemperature.C)) // 摄氏度
//        {
//            return valueInK - 273.15;
//        }
//        else if (targetUnit.Equals(UnitTemperature.F)) // 华氏度
//        {
//            return valueInK * 9.0 / 5.0 - 459.67;
//        }
//        else
//        {
//            throw new NotSupportedException($"Conversion to {targetUnit.Name} is not supported.");
//        }
//    }
//}

//// 定义常用温度单位
//public static class UnitTemperature
//{
//    public static readonly Unit K = new Unit("Kelvin", "K", 1.0, DimensionalityTemperature.Instance);
//    public static readonly Unit C = new Unit("Celsius", "°C", 1.0, DimensionalityTemperature.Instance);
//    public static readonly Unit F = new Unit("Fahrenheit", "°F", 5.0 / 9.0, DimensionalityTemperature.Instance);
//}

//// 定义温度量纲类
//public class DimensionalityTemperature : Dimensionality
//{
//    public override string Name => "Temperature";
//    public override string Symbol => "Θ";
//    public static readonly DimensionalityTemperature Instance = new DimensionalityTemperature();

//    private DimensionalityTemperature() : base(UnitTemperature.K)
//    {
//        AddUnits(UnitTemperature.C, UnitTemperature.F);
//    }
//}

//// 定义物质的量量纲类
//public class DimensionalityAmountOfSubstance : Dimensionality
//{
//    public override string Name => "Amount of Substance";
//    public override string Symbol => "N";
//    public static readonly DimensionalityAmountOfSubstance Instance = new DimensionalityAmountOfSubstance();

//    private DimensionalityAmountOfSubstance() : base(UnitAmountOfSubstance.mol)
//    {
//        // 未来如果有其他物质的量单位，可以在这里添加
//    }
//}

//// 定义物质的量类
//public class AmountOfSubstance : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Amount of Substance";
//    public override string Symbol => "N";

//    // 重载构造函数
//    public AmountOfSubstance(double value, Unit unit) : base(UnitAmountOfSubstance.mol)
//    {
//        Quantity = new Quantity(value, unit);
//    }

//    public AmountOfSubstance() : base(UnitAmountOfSubstance.mol)
//    {
//        // 如果需要默认构造函数
//    }
//}

//// 定义常用物质的量单位
//public static class UnitAmountOfSubstance
//{
//    public static readonly Unit mol = new Unit("Mole", "mol", 1.0, DimensionalityAmountOfSubstance.Instance);
//}

//// 定义光强量纲类
//public class DimensionalityLuminousIntensity : Dimensionality
//{
//    public override string Name => "Luminous Intensity";
//    public override string Symbol => "J";
//    public static readonly DimensionalityLuminousIntensity Instance = new DimensionalityLuminousIntensity();

//    private DimensionalityLuminousIntensity() : base(UnitLuminousIntensity.cd)
//    {
//        // 未来如果有其他光强单位，可以在这里添加
//    }
//}

//// 定义光强类
//public class LuminousIntensity : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Luminous Intensity";
//    public override string Symbol => "J";

//    // 重载构造函数
//    public LuminousIntensity(double value, Unit unit) : base(UnitLuminousIntensity.cd)
//    {
//        Quantity = new Quantity(value, unit);
//    }

//    public LuminousIntensity() : base(UnitLuminousIntensity.cd)
//    {
//        // 如果需要默认构造函数
//    }
//}

//// 定义常用光强单位
//public static class UnitLuminousIntensity
//{
//    public static readonly Unit cd = new Unit("Candela", "cd", 1.0, DimensionalityLuminousIntensity.Instance);
//}

//// 定义电荷类
//public class DimensionalityCharge : Dimensionality
//{
//    public override string Name => "Charge";
//    public override string Symbol => "Q";
//    public static readonly DimensionalityCharge Instance = new DimensionalityCharge();

//    private DimensionalityCharge() : base(UnitCharge.C)
//    {
//        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
//    }
//}

//public class Charge : Dimensionality
//{
//    public Quantity Quantity { get; private set; }

//    public override string Name => "Charge";
//    public override string Symbol => "Q";

//    public Charge(double value, Unit unit) : base(UnitCharge.C)
//    {
//        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
//        Quantity = new Quantity(value, unit);
//    }

//    public Charge() : base(UnitCharge.C)
//    {
//        AddUnits(UnitCharge.mC, UnitCharge.µC, UnitCharge.nC);
//    }
//}

//public static class UnitCharge
//{
//    public static readonly Unit C = new Unit("Coulomb", "C", 1.0, DimensionalityCharge.Instance);
//    public static readonly Unit mC = new Unit("Millicoulomb", "mC", 1e-3, DimensionalityCharge.Instance);
//    public static readonly Unit µC = new Unit("Microcoulomb", "µC", 1e-6, DimensionalityCharge.Instance);
//    public static readonly Unit nC = new Unit("Nanocoulomb", "nC", 1e-9, DimensionalityCharge.Instance);
//}
