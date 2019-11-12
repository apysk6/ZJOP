namespace csharp.Items
{
    public class Conjured : Item
    {
        public Conjured(string name, int sellIn, int quality) : base(name, sellIn, quality) { }

        public override Item UpdateQuality()
        {
            SellIn -= 1;

            if (Quality > 0)
            {
                return SellIn < 0 ? new RegularItem(Name, SellIn, Quality - 4)
                    : new RegularItem(Name, SellIn, Quality - 2);
            }

            return new RegularItem(Name, SellIn - 1, Quality);
        }
    }
}
