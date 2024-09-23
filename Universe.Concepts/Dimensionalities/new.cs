using System;
using System.Collections.Generic;

namespace Universe.Concepts.Dimensionalities
{
    // 定义一个惰性初始化的 LazyUnit 类
    public class LazyUnit
    {
        private readonly Lazy<Unit> _lazyUnit;

        public LazyUnit(Func<Unit> unitFactory)
        {
            _lazyUnit = new Lazy<Unit>(unitFactory);
        }

        // 隐式转换为 Unit
        public static implicit operator Unit(LazyUnit lazyUnit)
        {
            return lazyUnit._lazyUnit.Value;
        }
    }

    // 定义质量单位
    public static class UnitMass
    {
        public static readonly LazyUnit kg = new LazyUnit(() => new Unit("Kilogram", "kg", 1.0, DimensionalityMass.Instance));
        public static readonly LazyUnit g = new LazyUnit(() => new Unit("Gram", "g", 0.001, DimensionalityMass.Instance));
        public static readonly LazyUnit lb = new LazyUnit(() => new Unit("Pound", "lb", 0.453592, DimensionalityMass.Instance));
    }

    // 维度接口
    public interface IDimensionality
    {
        string Name { get; }
        string Symbol { get; }
        Unit UnitStandard { get; }
        Quantity ConvertTo(Quantity quantity, Unit targetUnit);
    }

    // 抽象维度类
    public abstract class Dimensionality : CascadeConcept, IDimensionality
    {
        public abstract string Name { get; }
        public abstract string Symbol { get; }
        public abstract Unit UnitStandard { get; }

        public virtual Quantity ConvertTo(Quantity quantity, Unit targetUnit)
        {
            return UnitConversionService.ConvertTo(quantity, targetUnit);
        }
    }

    // 单位类
    public class Unit : Concept, IEquatable<Unit>
    {
        public string Name { get; }
        public string Symbol { get; }
        public double ConversionFactor { get; }
        public IDimensionality Dimensionality { get; }

        public Unit(string name, string symbol, double conversionFactor, IDimensionality dimensionality)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            ConversionFactor = conversionFactor;
            Dimensionality = dimensionality ?? throw new ArgumentNullException(nameof(dimensionality));
        }

        public override string ToString() => $"{Name} ({Symbol})";

        public bool Equals(Unit other)
        {
            if (other is null) return false;
            return Name == other.Name &&
                   Symbol == other.Symbol &&
                   ConversionFactor == other.ConversionFactor &&
                   Dimensionality.Equals(other.Dimensionality);
        }

        public override int GetHashCode() => HashCode.Combine(Name, Symbol, ConversionFactor, Dimensionality);
    }

    // 质量维度类
    public class DimensionalityMass : Dimensionality
    {
        public override string Name => "Mass";
        public override string Symbol => "M";

        // 直接初始化实例，避免使用 Lazy
        public static readonly DimensionalityMass Instance = new DimensionalityMass();

        // 使用 Lazy<Unit> 来延迟初始化 UnitStandard
        private readonly Lazy<Unit> _unitStandard = new Lazy<Unit>(() => UnitMass.kg);
        public override Unit UnitStandard => _unitStandard.Value;

        private DimensionalityMass()
        {
            // 构造函数中不再设置 UnitStandard
        }
    }

    // 单位转换服务
    public static class UnitConversionService
    {
        public static Quantity ConvertTo(Quantity quantity, Unit targetUnit)
        {
            if (quantity.Unit == targetUnit)
                return new Quantity(quantity.Value, targetUnit);

            if (quantity.Unit.Dimensionality != targetUnit.Dimensionality)
            {
                throw new InvalidOperationException($"无法在不同维度之间转换：{quantity.Unit.Dimensionality.Name} 和 {targetUnit.Dimensionality.Name}。");
            }

            // 使用转换因子进行转换
            double baseValue = quantity.Value * quantity.Unit.ConversionFactor;
            double targetValue = baseValue / targetUnit.ConversionFactor;

            return new Quantity(targetValue, targetUnit);
        }
    }

    // 量纲类
    public class Quantity : CascadeConcept
    {
        private double _value;
        public double Value
        {
            get => _value;
            private set
            {
                if (_value != value)
                {
                    var oldValue = _value;
                    _value = value;

                    // 触发属性变化和级联事件
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        private Unit _unit;
        public Unit Unit
        {
            get => _unit;
            private set
            {
                if (_unit != value)
                {
                    _unit = value;
                    OnPropertyChanged(nameof(Unit));
                }
            }
        }

        public Quantity(double value, Unit unit)
        {
            _value = value;
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));

            // 订阅子对象的属性变化事件
            SubscribeToChildProperties();
        }

        public Quantity ConvertTo(Unit targetUnit)
        {
            if (targetUnit == null) throw new ArgumentNullException(nameof(targetUnit));

            var convertedQuantity = Unit.Dimensionality.ConvertTo(this, targetUnit);

            // 更新 Value 和 Unit
            Value = convertedQuantity.Value;
            Unit = convertedQuantity.Unit;

            return this;
        }

        public override string ToString() => $"{Value} {Unit.Symbol}";
    }

    // 质量类
    public class Mass : CascadeConcept
    {
        private Quantity _quantity;
        public Quantity Quantity
        {
            get => _quantity;
            private set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public Mass(double value, Unit unit)
        {
            if (unit.Dimensionality != DimensionalityMass.Instance)
                throw new ArgumentException("无效的质量单位。", nameof(unit));

            Quantity = new Quantity(value, unit);

            // 订阅子对象的属性变化事件
            SubscribeToChildProperties();
        }

        public void ConvertTo(Unit targetUnit)
        {
            if (targetUnit.Dimensionality != DimensionalityMass.Instance)
                throw new ArgumentException("无效的目标质量单位。", nameof(targetUnit));

            Quantity.ConvertTo(targetUnit);
        }

        public override string ToString() => Quantity.ToString();
    }
}
