using System;
using System.Collections.Generic;
using csharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRoseTest
{
    [TestClass]
    public class GildedRoseTests
    {
        // At the end of each day our system lowers both values for every item.
        [TestMethod]
        public void DefaultDayPassed()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

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

        // The Quality of an item is never negative.
        [TestMethod]
        public void QualityNotNegative()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 0},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 0},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 0},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 0}
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.IsTrue(Items[0].Quality > -1);
            Assert.IsTrue(Items[1].Quality > -1);
            Assert.IsTrue(Items[2].Quality > -1);
            Assert.IsTrue(Items[3].Quality > -1);
            Assert.IsTrue(Items[4].Quality > -1);
            Assert.IsTrue(Items[5].Quality > -1);
        }

        // "Aged Brie" actually increases in Quality the older it gets.
        [TestMethod]
        public void AgedBrieIncreaseQualityInTime()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Aged Brie", SellIn = 5, Quality = 5}
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();
            store.UpdateQuality();
            store.UpdateQuality();

            Assert.IsTrue(Items[0].Quality > 5);
        }

        // Once the sell by date has passed, Quality degrades twice as fast.
        [TestMethod]
        public void SellDatePassed()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 15},
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.AreEqual(13, Items[0].Quality);
        }

        // The Quality of an item is never more than 50.
        [TestMethod]
        public void QualityNoMoreThanFifty()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 50},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 50},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 50},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 50},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 50},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 50}
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.IsTrue(Items[0].Quality < 51);
            Assert.IsTrue(Items[1].Quality < 51);
            Assert.IsTrue(Items[2].Quality < 51);
            Assert.IsTrue(Items[3].Quality < 51);
            Assert.IsTrue(Items[4].Quality < 51);
            Assert.IsTrue(Items[5].Quality < 51);
        }

        // “Sulfuras”, being a legendary item, never has to be sold or decreases in Quality
        [TestMethod]
        public void SulfurasNeverDecreaseQuality()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 50}
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();
            store.UpdateQuality();
            store.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert.
        [TestMethod]
        public void BackstageIncreaseQuality()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.AreEqual(21, Items[0].Quality);
        }

        [TestMethod]
        public void BackstageIncreaseQualityTenDays()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20},
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.AreEqual(22, Items[0].Quality);
        }

        [TestMethod]
        public void BackstageIncreaseQualityFiveDays()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20},
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.AreEqual(23, Items[0].Quality);
        }

        [TestMethod]
        public void BackstageDropsQualityAfterConcert()
        {
            List<Item> Items = new List<Item>()
            {
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20},
            };

            GildedRose store = new GildedRose(Items);
            store.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }
    }
}