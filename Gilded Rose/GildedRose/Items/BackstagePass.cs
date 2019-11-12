namespace csharp.Items
{
    public class BackstagePass : Item
    {
        public BackstagePass(string name, int sellIn, int quality) : base(name, sellIn, quality) { }

        public override Item UpdateQuality()
        {
            SellIn -= 1;
       
            if (Quality < 50)
            {
                if (SellIn < 0)
                    return new BackstagePass(Name, SellIn, 0);
                if (SellIn < 6)
                    return new BackstagePass(Name, SellIn, Quality + 3);
                if (SellIn < 11)
                    return new BackstagePass(Name, SellIn, Quality + 2);
              
                return new BackstagePass(Name, SellIn, Quality + 1);
            }

            return new BackstagePass(Name, SellIn, Quality);
        }
    }
}
