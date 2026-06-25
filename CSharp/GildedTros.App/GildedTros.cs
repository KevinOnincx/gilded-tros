using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
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
                    item.Quality = 80;
                    continue;
                }

                item.SellIn = item.SellIn - 1;

                if (item.Name.StartsWith("Backstage passes"))
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
                else if (item.Name == "Good Wine")
                {
                    item.Quality = System.Math.Min(50, item.Quality + 1);
                }
                else if (item.Name == "Duplicate Code" || item.Name == "Long Methods" || item.Name == "Ugly Variable Names")
                {
                    var qualityDecrease = item.SellIn < 0 ? 4 : 2;
                    item.Quality = System.Math.Max(0, item.Quality - qualityDecrease);
                }
                else
                {
                    var qualityDecrease = item.SellIn < 0 ? 2 : 1;
                    item.Quality = System.Math.Max(0, item.Quality - qualityDecrease);
                }
            }
        }
    }
}
