namespace csharp.Items
{
    public class AgedBrie : Item
    {
        public AgedBrie(string name, int sellIn, int quality) : base(name, sellIn, quality) { }

        public override Item UpdateQuality()
        {
            return Quality < 50 ? new AgedBrie(Name, SellIn - 1, Quality + 1) 
                : new AgedBrie(Name, SellIn - 1, Quality);
        }
    }
}
