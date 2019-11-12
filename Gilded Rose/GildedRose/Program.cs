using System;
using System.Collections.Generic;
using csharp.Items;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Item> gildedRosesItems = new List<Item>();
            gildedRosesItems.Add(new RegularItem("+5 Dexterity Vest", 10, 20));
            gildedRosesItems.Add(new AgedBrie("Aged Brie", 2, 0));
            gildedRosesItems.Add(new RegularItem("Elixir of the Mongoose", 5, 7));
            gildedRosesItems.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 80));
            gildedRosesItems.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", -1, 80));
            gildedRosesItems.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 15, 20));
            gildedRosesItems.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 10, 49));
            gildedRosesItems.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 5, 49));

            GildedRose gildedRoseStore = new GildedRose(gildedRosesItems);


            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < gildedRosesItems.Count; j++)
                {
                    System.Console.WriteLine(gildedRosesItems[j]);
                }
                Console.WriteLine("");
                gildedRoseStore.UpdateQuality();
            }

            Console.ReadLine();
        }
    }
}
