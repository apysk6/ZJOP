using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        public List<Item> Items { get; set; }

        public GildedRose(List<Item> items)
        {
            Items = items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[i] = Items[i].UpdateQuality();
            }
        }
    }
}
