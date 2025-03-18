class Inventory
{

    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();


    }

    // methods
    public bool Put(string itemName, Item item)
    {

        if (item.Weight > FreeWeight())
        {
            Console.WriteLine("You can't carry that much weight!");
            return false;
        }
        items.Add(itemName, item);
        return true;
    }
    public Item Get(string itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            Console.WriteLine("There is no " + itemName + " in the chest!");
            return null;
        }

        Item item = items[itemName];
        items.Remove(itemName);

        return item;
    }



    // methods
    public int TotalWeight()
    {
        int totalWeight = 0;
        foreach (var item in items.Values)
        {
            totalWeight += item.Weight;
        }
        return totalWeight;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public void GetItems()
    {
        Console.WriteLine("Items:");
        foreach (var itemName in items.Keys)
        {
            Console.WriteLine(itemName);
        }
    }
}
