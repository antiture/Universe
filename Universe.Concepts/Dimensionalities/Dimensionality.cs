

//namespace Universe.Concept.Dimensionalities;


//public class Unit : IEquatable<Unit>
//{
//    public string Name { get; }
//    public string Symbol { get; }
//    public double ConversionFactor { get; }
//    public IDimensionality Dimensionality { get; }

//    public Unit(string name, string symbol, double conversionFactor, IDimensionality dimensionality)
//    {
//        Name = name ?? throw new ArgumentNullException(nameof(name));
//        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
//        ConversionFactor = conversionFactor;
//        Dimensionality = dimensionality ?? throw new ArgumentNullException(nameof(dimensionality));
//    }

//    public override string ToString()
//    {
//        return $"{Name} ({Symbol})";
//    }

//    public bool Equals(Unit other)
//    {
//        if (other is null)
//            return false;

//        return Name == other.Name &&
//               Symbol == other.Symbol &&
//               ConversionFactor == other.ConversionFactor &&
//               Dimensionality.Equals(other.Dimensionality);
//    }

//    public override bool Equals(object obj)
//    {
//        return Equals(obj as Unit);
//    }

//    public override int GetHashCode()
//    {
//        return HashCode.Combine(Name, Symbol, ConversionFactor, Dimensionality);
//    }
//}

//public class UnitConversionService
//{
//    private static readonly Dictionary<(Unit, Unit), Func<double, double>> ConversionMap = new();

//    public static void RegisterConversion(Unit from, Unit to, Func<double, double> conversion)
//    {
//        ConversionMap[(from, to)] = conversion;
//    }

//    public static Quantity ConvertTo(Quantity quantity, Unit targetUnit)
//    {
//        if (quantity.Unit == targetUnit)
//            return new Quantity(quantity.Value, targetUnit);

//        if (ConversionMap.TryGetValue((quantity.Unit, targetUnit), out var conversion))
//        {
//            double convertedValue = conversion(quantity.Value);
//            return new Quantity(convertedValue, targetUnit);
//        }

//        throw new InvalidOperationException($"No conversion available from {quantity.Unit.Name} to {targetUnit.Name}.");
//    }
//}

//public class Quantity
//{
//    public double Value { get; set; }
//    public Unit Unit { get; set; }

//    public Quantity(double value, Unit unit)
//    {
//        Value = value;
//        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
//    }

//    public Quantity ConvertTo(Unit targetUnit)
//    {
//        if (targetUnit == null)
//            throw new ArgumentNullException(nameof(targetUnit));

//        return Unit.Dimensionality.ConvertTo(this, targetUnit);
//    }

//    public override string ToString()
//    {
//        return $"{Value} {Unit.Symbol}";
//    }

//    public static Quantity operator +(Quantity q1, Quantity q2)
//    {
//        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
//            throw new InvalidOperationException("Cannot add quantities of different dimensionalities.");

//        Quantity q2Converted = q2.ConvertTo(q1.Unit);
//        return new Quantity(q1.Value + q2Converted.Value, q1.Unit);
//    }

//    public static Quantity operator -(Quantity q1, Quantity q2)
//    {
//        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
//            throw new InvalidOperationException("Cannot subtract quantities of different dimensionalities.");

//        Quantity q2Converted = q2.ConvertTo(q1.Unit);
//        return new Quantity(q1.Value - q2Converted.Value, q1.Unit);
//    }
//}

//public interface IDimensionality
//{
//    string Name { get; }
//    string Symbol { get; }
//    Unit UnitStandard { get; }
//    HashSet<Unit> ListUnit { get; }

//    Quantity ConvertTo(Quantity quantity, Unit targetUnit);
//    Quantity ConvertToStandard(Quantity quantity);
//    string GetDescription();
//}

//public abstract class Dimensionality : IDimensionality
//{
//    public abstract string Name { get; }
//    public abstract string Symbol { get; }
//    public Unit UnitStandard { get; private set; }
//    public HashSet<Unit> ListUnit { get; } = new HashSet<Unit>();

//    // 构造函数不再直接接受标准单位，而是通过 InitStandardUnit 方法进行设置
//    protected Dimensionality()
//    {
//    }

//    // 延迟设置标准单位，避免构造函数中的循环引用
//    public void InitStandardUnit(Unit standardUnit)
//    {
//        UnitStandard = standardUnit ?? throw new ArgumentNullException(nameof(standardUnit));
//        ListUnit.Add(standardUnit);
//    }

//    protected void AddUnits(params Unit[] units)
//    {
//        foreach (var unit in units)
//        {
//            if (!ListUnit.Contains(unit))
//            {
//                ListUnit.Add(unit);
//            }
//        }
//    }

//    public virtual Quantity ConvertTo(Quantity quantity, Unit targetUnit)
//    {
//        return UnitConversionService.ConvertTo(quantity, targetUnit);
//    }

//    public Quantity ConvertToStandard(Quantity quantity)
//    {
//        return ConvertTo(quantity, UnitStandard);
//    }

//    public virtual string GetDescription()
//    {
//        return $"{Name} ({Symbol})";
//    }

//    public string GetAllUnits()
//    {
//        return string.Join(", ", ListUnit.Select(u => u.ToString()));
//    }

//    public override string ToString()
//    {
//        return $"{Name} ({Symbol}) [Standard Unit: {UnitStandard.Name}]";
//    }
//}

//public class DimensionalityMass : Dimensionality
//{
//    public override string Name => "Mass";
//    public override string Symbol => "M";
//    private static readonly Lazy<DimensionalityMass> _instance = new(() => new DimensionalityMass());
//    public static DimensionalityMass Instance => _instance.Value;

//    private DimensionalityMass()
//    {
//        InitStandardUnit(UnitMass.kg);
//        AddUnits(UnitMass.g, UnitMass.lb);
//    }
//}

//public static class UnitMass
//{
//    public static readonly Unit kg;
//    public static readonly Unit g;
//    public static readonly Unit lb;

//    static UnitMass()
//    {
//        kg = new Unit("Kilogram", "kg", 1.0, DimensionalityMass.Instance);
//        g = new Unit("Gram", "g", 0.001, DimensionalityMass.Instance);
//        lb = new Unit("Pound", "lb", 0.453592, DimensionalityMass.Instance);
//    }
//}
//public class Mass : Dimensionality
//{
//    public Quantity Quantity { get; private set; }  // 存储质量的物理量
//    public override string Name => "Mass";
//    public override string Symbol => "M";

//    // 重载构造函数，允许直接接收数值和单位，生成 Quantity
//    public Mass(double value, Unit unit)
//    {
//        InitStandardUnit(UnitMass.kg);  // 延迟初始化标准单位
//        AddUnits(UnitMass.g, UnitMass.lb);
//        Quantity = new Quantity(value, unit);
//    }

//    public Mass()
//    {
//        InitStandardUnit(UnitMass.kg);  // 延迟初始化标准单位
//        AddUnits(UnitMass.g, UnitMass.lb);
//    }
//}
////public class Unit : IEquatable<Unit>
////{
////    public string Name { get; }
////    public string Symbol { get; }
////    public double ConversionFactor { get; }
////    public IDimensionality Dimensionality { get; }

////    public Unit(string name, string symbol, double conversionFactor, IDimensionality dimensionality)
////    {
////        Name = name ?? throw new ArgumentNullException(nameof(name));
////        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
////        ConversionFactor = conversionFactor;
////        Dimensionality = dimensionality ?? throw new ArgumentNullException(nameof(dimensionality));
////    }

////    public override string ToString()
////    {
////        return $"{Name} ({Symbol})";
////    }

////    public bool Equals(Unit other)
////    {
////        if (other is null)
////            return false;

////        return Name == other.Name &&
////               Symbol == other.Symbol &&
////               ConversionFactor == other.ConversionFactor &&
////               Dimensionality.Equals(other.Dimensionality);
////    }

////    public override bool Equals(object obj)
////    {
////        return Equals(obj as Unit);
////    }

////    public override int GetHashCode()
////    {
////        return HashCode.Combine(Name, Symbol, ConversionFactor, Dimensionality);
////    }
////}

////// 1. 定义一个转换服务，处理单位间的转换
////public class UnitConversionService
////{
////    private static readonly Dictionary<(Unit, Unit), Func<double, double>> ConversionMap
////        = new Dictionary<(Unit, Unit), Func<double, double>>();

////    // 注册单位转换规则
////    public static void RegisterConversion(Unit from, Unit to, Func<double, double> conversion)
////    {
////        ConversionMap[(from, to)] = conversion;
////    }

////    // 执行单位转换
////    public static Quantity ConvertTo(Quantity quantity, Unit targetUnit)
////    {
////        if (quantity.Unit == targetUnit)
////            return new Quantity(quantity.Value, targetUnit);

////        if (ConversionMap.TryGetValue((quantity.Unit, targetUnit), out var conversion))
////        {
////            double convertedValue = conversion(quantity.Value);
////            return new Quantity(convertedValue, targetUnit);
////        }

////        throw new InvalidOperationException($"No conversion available from {quantity.Unit.Name} to {targetUnit.Name}.");
////    }
////}

////public class Quantity
////{
////    public double Value { get; set; }   // 数值
////    public Unit Unit { get; set; }      // 单位

////    public Quantity(double value, Unit unit)
////    {
////        Value = value;
////        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
////    }

////    // 转换到指定单位
////    public Quantity ConvertTo(Unit targetUnit)
////    {
////        if (targetUnit == null)
////            throw new ArgumentNullException(nameof(targetUnit));

////        // 使用所属的 Dimensionality 进行转换
////        return Unit.Dimensionality.ConvertTo(this, targetUnit);
////    }

////    public override string ToString()
////    {
////        return $"{Value} {Unit.Symbol}";
////    }
////    // 可选：实现运算符重载
////    public static Quantity operator +(Quantity q1, Quantity q2)
////    {
////        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
////            throw new InvalidOperationException("Cannot add quantities of different dimensionalities.");

////        // 将 q2 转换为 q1 的单位
////        Quantity q2Converted = q2.ConvertTo(q1.Unit);
////        return new Quantity(q1.Value + q2Converted.Value, q1.Unit);
////    }

////    public static Quantity operator -(Quantity q1, Quantity q2)
////    {
////        if (q1.Unit.Dimensionality != q2.Unit.Dimensionality)
////            throw new InvalidOperationException("Cannot subtract quantities of different dimensionalities.");

////        // 将 q2 转换为 q1 的单位
////        Quantity q2Converted = q2.ConvertTo(q1.Unit);
////        return new Quantity(q1.Value - q2Converted.Value, q1.Unit);
////    }
////}

////// 定义 IDimensionality 接口
////public interface IDimensionality
////{
////    string Name { get; }
////    string Symbol { get; }
////    Unit UnitStandard { get; }
////    HashSet<Unit> ListUnit { get; }

////    Quantity ConvertTo(Quantity quantity, Unit targetUnit);
////    Quantity ConvertToStandard(Quantity quantity);
////    string GetDescription();
////}

////public abstract class Dimensionality : IDimensionality
////{
////    public abstract string Name { get; }
////    public abstract string Symbol { get; }
////    public Unit UnitStandard { get; }
////    public HashSet<Unit> ListUnit { get; } = new HashSet<Unit>();

////    protected Dimensionality(Unit standardUnit)
////    {
////        UnitStandard = standardUnit ?? throw new ArgumentNullException(nameof(standardUnit));
////        ListUnit.Add(standardUnit);
////    }

////    protected void AddUnits(params Unit[] units)
////    {
////        foreach (var unit in units)
////        {
////            if (!ListUnit.Contains(unit))
////            {
////                ListUnit.Add(unit);
////            }
////        }
////    }

////    public virtual Quantity ConvertTo(Quantity quantity, Unit targetUnit)
////    {
////        // 调用 UnitConversionService 进行转换
////        return UnitConversionService.ConvertTo(quantity, targetUnit);
////    }

////    public Quantity ConvertToStandard(Quantity quantity)
////    {
////        return ConvertTo(quantity, UnitStandard);
////    }

////    public virtual string GetDescription()
////    {
////        return $"{Name} ({Symbol})";
////    }

////    public string GetAllUnits()
////    {
////        return string.Join(", ", ListUnit.Select(u => u.ToString()));
////    }

////    public override string ToString()
////    {
////        return $"{Name} ({Symbol}) [Standard Unit: {UnitStandard.Name}]";
////    }
////}


////public class DimensionalityMass : Dimensionality
////{
////    public override string Name => "Mass";

////    // 实现 Symbol 属性
////    public override string Symbol => "M";
////    // 使用 Lazy 来避免递归初始化
////    private static readonly Lazy<DimensionalityMass> _instance = new Lazy<DimensionalityMass>(() => new DimensionalityMass());

////    public static DimensionalityMass Instance => _instance.Value;

////    // 私有构造函数，确保无法通过外部实例化
////    private DimensionalityMass() : base(UnitMass.kg)
////    {
////        AddUnits(UnitMass.g, UnitMass.lb);
////    }
////}

//// 定义质量类
////public class Mass : Dimensionality
////{
////    public Quantity Quantity { get; private set; }  // 存储质量的物理量
////    public override string Name => "Mass";
////    public override string Symbol => "M";

////    // 重载构造函数，允许直接接收数值和单位，生成 Quantity
////    public Mass(double value, Unit unit)
////    {
////        InitStandardUnit(UnitMass.kg);  // 延迟初始化标准单位
////        AddUnits(UnitMass.g, UnitMass.lb);
////        Quantity = new Quantity(value, unit);
////    }

////    public Mass()
////    {
////        InitStandardUnit(UnitMass.kg);  // 延迟初始化标准单位
////        AddUnits(UnitMass.g, UnitMass.lb);
////    }
////}


////// 定义常用质量单位
////public static class UnitMass
////{
////    public static readonly Unit kg = new Unit("Kilogram", "kg", 1.0, DimensionalityMass.Instance);
////    public static readonly Unit g = new Unit("Gram", "g", 0.001, DimensionalityMass.Instance);
////    public static readonly Unit lb = new Unit("Pound", "lb", 0.453592, DimensionalityMass.Instance);
////}


//// 定义常量类
//public static class Constant
//{
//    public static class Mass
//    {
//        public static readonly Quantity Neutron = new Quantity(1.67492749804e-27, UnitMass.kg);
//        public static readonly Quantity Proton = new Quantity(1.67262192369e-27, UnitMass.kg);
//        public static readonly Quantity Electron = new Quantity(9.1093837015e-31, UnitMass.kg);
//    }

//    //public static class Charge
//    //{
//    //    public static readonly Quantity Electron = new Quantity(-1.602176634e-19, UnitCharge.C);
//    //    public static readonly Quantity Proton = new Quantity(1.602176634e-19, UnitCharge.C);
//    //}

//    //public static class Length
//    //{
//    //    public static readonly Quantity Planck = new Quantity(1.616255e-35, UnitLength.m);
//    //}
//}
