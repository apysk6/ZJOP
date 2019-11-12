namespace csharp
{
    public abstract class Item
    {
        protected string Name { get; set; }
        protected int SellIn { get; set; }
        public int Quality { get; set; }

        protected Item(string name, int sellIn, int quality)
        {
            Name = name;
            SellIn = sellIn;
            Quality = quality;
        }

        public abstract Item UpdateQuality();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Item comparableItem = (Item) obj;
            return Name.Equals(comparableItem.Name, System.StringComparison.CurrentCultureIgnoreCase) &&
                   SellIn == comparableItem.SellIn && Quality == comparableItem.Quality;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name + ", " + SellIn + ", " + Quality;
        }
    }
}