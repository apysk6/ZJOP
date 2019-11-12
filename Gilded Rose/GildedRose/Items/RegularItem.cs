namespace csharp.Items
{
    public class RegularItem : Item
    {
        public RegularItem(string name, int sellIn, int quality) : base (name, sellIn, quality) { }

        public override Item UpdateQuality()
        {
            SellIn -= 1;

            if (Quality > 0)
            {
                return SellIn < 0 ? new RegularItem(Name, SellIn, Quality - 2) 
                    : new RegularItem(Name, SellIn, Quality - 1);
            }

            return new RegularItem(Name, SellIn - 1, Quality);
        }
    }
}
