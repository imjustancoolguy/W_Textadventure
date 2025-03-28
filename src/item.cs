class Item
{
    // fields
    public int Weight { get; }
    public string Description { get; }
    public string IName { get; }
    public int Price { get; }
    // constructor
    public Item(string iName, string description, int weight, int price)
    {
        Weight = weight;
        Description = description;
        IName = iName;
        Price = price;
    }
}