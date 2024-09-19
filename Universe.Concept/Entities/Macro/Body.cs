



namespace Universe.Concept.Entities.Macro
{
    public abstract class Body : Entity
    {
        // 允许宏观物体修改 Name
        public override string Name { get; protected set; }
        protected Body(string name, List<Dimensionality> dimensions = null)
            : base(name, dimensions) // 将 name 和 dimensions 传递给基类构造函数
        {
        }
    }
}
