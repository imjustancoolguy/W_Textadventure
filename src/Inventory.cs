class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    // constructor
    public Dictionary<string, Item> Items
    {
        get { return items; }
    }

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }


    // methods
    public string PrintItems()
    {
        string item = "Look you found ";
        foreach (string name in items.Keys)
        {
            item += name + " ";
            item += items[name].Weight + "KG";
            item += ", ";
        }
        return item;
    }

    public string PrintInvItems()
    {
        string item = "";
        if (items.Count == 0)
        {
            item = "There are no items in your inventory";
        }
        else
        {
            foreach (string name in items.Keys)
            {
                item += name;
                item += ", ";
            }
        }
        return item;
    }
    public bool Put(string itemName, Item item)
    {
        if (item.Weight > maxWeight)
        {
            Console.WriteLine("This item is too heavy!");
            return false;
        }
        else
        {
            items.Add(itemName, item);
            return true;
        }
    }
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item gottenitem = items[itemName];
            items.Remove(itemName);
            return gottenitem;
        }
        return null;
    }

    // methods
    public int TotalWeight()
    {
        int total = 0;
        foreach (KeyValuePair<string, Item> entry in items)
        {
            total += entry.Value.Weight;
        }
        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

}
