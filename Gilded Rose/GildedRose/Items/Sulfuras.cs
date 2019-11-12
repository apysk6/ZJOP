namespace csharp.Items
{
    public class Sulfuras : Item
    {
        public Sulfuras(string name, int sellIn, int quality) : base(name, sellIn, quality) { }

        public override Item UpdateQuality()
        {
            return this;
        }
    }
}
