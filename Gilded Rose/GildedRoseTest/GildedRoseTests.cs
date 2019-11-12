using System;
using System.Collections.Generic;
using csharp;
using csharp.Items;
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
            List<Item> items = new List<Item>();
            items.Add(new RegularItem("+5 Dexterity Vest", 10, 20));
            items.Add(new AgedBrie("Aged Brie", 2, 0));
            items.Add(new RegularItem("Elixir of the Mongoose", 5, 7));
            items.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 80));
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 15, 20));
            items.Add(new Conjured("Conjured Mana Cake", 3, 6));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            List<Item> expectedItemsAfterDay = new List<Item>();
            expectedItemsAfterDay.Add(new RegularItem("+5 Dexterity Vest", 9, 19));
            expectedItemsAfterDay.Add(new AgedBrie("Aged Brie", 1, 1));
            expectedItemsAfterDay.Add(new RegularItem("Elixir of the Mongoose", 4, 6));
            expectedItemsAfterDay.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 80));
            expectedItemsAfterDay.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 14, 21));
            expectedItemsAfterDay.Add(new Conjured("Conjured Mana Cake", 2, 4));

            Assert.AreEqual(expectedItemsAfterDay[0], items[0]);
            Assert.AreEqual(expectedItemsAfterDay[1], items[1]);
            Assert.AreEqual(expectedItemsAfterDay[2], items[2]);
            Assert.AreEqual(expectedItemsAfterDay[3], items[3]);
            Assert.AreEqual(expectedItemsAfterDay[4], items[4]);
            Assert.AreEqual(expectedItemsAfterDay[5], items[5]);
        }

        // The Quality of an item is never negative.
        [TestMethod]
        public void QualityNotNegative()
        {
            List<Item> items = new List<Item>();
            items.Add(new RegularItem("+5 Dexterity Vest", 10, 0));
            items.Add(new AgedBrie("Aged Brie", 2, 0));
            items.Add(new RegularItem("Elixir of the Mongoose", 5, 0));
            items.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 0));
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 15, 0));
            items.Add(new Conjured("Conjured Mana Cake", 3, 0));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.IsTrue(items[0].Quality > -1);
            Assert.IsTrue(items[1].Quality > -1);
            Assert.IsTrue(items[2].Quality > -1);
            Assert.IsTrue(items[3].Quality > -1);
            Assert.IsTrue(items[4].Quality > -1);
            Assert.IsTrue(items[5].Quality > -1);
        }

        // "Aged Brie" actually increases in Quality the older it gets.
        [TestMethod]
        public void AgedBrieIncreaseQualityInTime()
        {
            List<Item> items = new List<Item>();
            items.Add(new AgedBrie("Aged Brie", 5, 5));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();
            store.UpdateQuality();
            store.UpdateQuality();

            Assert.IsTrue(items[0].Quality > 5);
        }

        // Once the sell by date has passed, Quality degrades twice as fast.
        [TestMethod]
        public void SellDatePassed()
        {
            List<Item> items = new List<Item>();
            items.Add(new RegularItem("+5 Dexterity Vest", 0, 15));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(13, items[0].Quality);
        }

        // The Quality of an item is never more than 50.
        [TestMethod]
        public void QualityNoMoreThanFifty()
        {
            List<Item> items = new List<Item>();
            items.Add(new RegularItem("+5 Dexterity Vest", 10, 50));
            items.Add(new AgedBrie("Aged Brie", 2, 50));
            items.Add(new RegularItem("Elixir of the Mongoose", 5, 50));
            items.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 50));
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 15, 50));
            items.Add(new Conjured("Conjured Mana Cake", 3, 50));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.IsTrue(items[0].Quality < 51);
            Assert.IsTrue(items[1].Quality < 51);
            Assert.IsTrue(items[2].Quality < 51);
            Assert.IsTrue(items[3].Quality < 51);
            Assert.IsTrue(items[4].Quality < 51);
            Assert.IsTrue(items[5].Quality < 51);
        }

        // “Sulfuras”, being a legendary item, never has to be sold or decreases in Quality
        [TestMethod]
        public void SulfurasNeverDecreaseQuality()
        {
            List<Item> items = new List<Item>();
            items.Add(new Sulfuras("Sulfuras, Hand of Ragnaros", 0, 50));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();
            store.UpdateQuality();
            store.UpdateQuality();

            Assert.AreEqual(50, items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as it's SellIn value approaches; Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert.
        [TestMethod]
        public void BackstageIncreaseQuality()
        {
            List<Item> items = new List<Item>();
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 15, 20));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(21, items[0].Quality);
        }

        [TestMethod]
        public void BackstageIncreaseQualityTenDays()
        {
            List<Item> items = new List<Item>();
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 10, 20));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(22, items[0].Quality);
        }

        [TestMethod]
        public void BackstageIncreaseQualityFiveDays()
        {
            List<Item> items = new List<Item>();
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 5, 20));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(23, items[0].Quality);
        }

        [TestMethod]
        public void BackstageDropsQualityAfterConcert()
        {
            List<Item> items = new List<Item>();
            items.Add(new BackstagePass("Backstage passes to a TAFKAL80ETC concert", 0, 20));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(0, items[0].Quality);
        }

        // "Conjured" items degrade in Quality twice as fast as normal items.
        [TestMethod]
        public void ConjuredItemsDecreaseQualityTwiceOnSellIn()
        {
            List<Item> items = new List<Item>();
            items.Add(new Conjured("Conjured Mana", 5, 6));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(4, items[0].Quality);
        }

        [TestMethod]
        public void ConjuredItemsDecreaseQualityTwiceAfterSellIn()
        {
            List<Item> items = new List<Item>();
            items.Add(new Conjured("Conjured Mana", 0, 6));

            GildedRose store = new GildedRose(items);
            store.UpdateQuality();

            Assert.AreEqual(2, items[0].Quality);
        }
    }
}