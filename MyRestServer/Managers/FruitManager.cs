using JsonFruit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestServer.Managers
{
    public class FruitManager
    {
        private static List<Fruit> _fruits = new List<Fruit>()
        { 
            new Fruit("Banana"), 
            new Fruit("Apple"),
            new Fruit("Rotten banana"),
            new Fruit("Good apple")
        };

        public List<Fruit> GetAll(string substring)
        {
            List<Fruit> result = new List<Fruit>(_fruits);
            if (substring != null)
            {
                result = result.FindAll(fruit => fruit.typeOfFruit.Contains(substring,StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }
    }
}
