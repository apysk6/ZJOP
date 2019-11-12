using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void DefaultDayPassed()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            List<Item> expectedItemsAfterDay = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 9, Quality = 19},
                new Item {Name = "Aged Brie", SellIn = 1, Quality = 1},
                new Item {Name = "Elixir of the Mongoose", SellIn = 4, Quality = 6},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 14, Quality = 21},
                new Item {Name = "Conjured Mana Cake", SellIn = 2, Quality = 4}
            };

            Assert.AreEqual(expectedItemsAfterDay[0], Items[0]);
            Assert.AreEqual(expectedItemsAfterDay[1], Items[1]);
            Assert.AreEqual(expectedItemsAfterDay[2], Items[2]);
            Assert.AreEqual(expectedItemsAfterDay[3], Items[3]);
            Assert.AreEqual(expectedItemsAfterDay[4], Items[4]);
            Assert.AreEqual(expectedItemsAfterDay[5], Items[5]);
        }
    }
}