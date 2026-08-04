using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        private readonly IList<Item> Items;

        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];

                if (item.Name == "B-DAWG Keychain")
                {
                    continue;
                }

                GetStrategy(item.Name).Update(item);
            }
        }

        private static IItemUpdateStrategy GetStrategy(string itemName)
        {
            if (itemName.StartsWith("Backstage passes"))
            {
                return new BackstagePassStrategy();
            }

            if (itemName == "Good Wine")
            {
                return new GoodWineStrategy();
            }

            if (itemName == "Duplicate Code" || itemName == "Long Methods" || itemName == "Ugly Variable Names")
            {
                return new ConjuredStrategy();
            }

            return new StandardItemStrategy();
        }

        private interface IItemUpdateStrategy
        {
            void Update(Item item);
        }

        private abstract class ItemUpdateStrategyBase : IItemUpdateStrategy
        {
            public void Update(Item item)
            {
                item.SellIn = item.SellIn - 1;
                Apply(item);
            }

            protected abstract void Apply(Item item);
        }

        private sealed class BackstagePassStrategy : ItemUpdateStrategyBase
        {
            protected override void Apply(Item item)
            {
                if (item.SellIn < 0)
                {
                    item.Quality = 0;
                }
                else
                {
                    var qualityIncrease = item.SellIn < 5 ? 3 : item.SellIn < 10 ? 2 : 1;
                    item.Quality = System.Math.Min(50, item.Quality + qualityIncrease);
                }
            }
        }

        private sealed class GoodWineStrategy : ItemUpdateStrategyBase
        {
            protected override void Apply(Item item)
            {
                item.Quality = System.Math.Min(50, item.Quality + 1);
            }
        }

        private sealed class ConjuredStrategy : ItemUpdateStrategyBase
        {
            protected override void Apply(Item item)
            {
                var qualityDecrease = item.SellIn < 0 ? 4 : 2;
                item.Quality = System.Math.Max(0, item.Quality - qualityDecrease);
            }
        }

        private sealed class StandardItemStrategy : ItemUpdateStrategyBase
        {
            protected override void Apply(Item item)
            {
                var qualityDecrease = item.SellIn < 0 ? 2 : 1;
                item.Quality = System.Math.Max(0, item.Quality - qualityDecrease);
            }
        }
    }
}
