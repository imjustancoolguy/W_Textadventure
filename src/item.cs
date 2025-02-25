class Item
{
    // fields
    public int Weight { get; }
    public string Description { get; }
    public string IName { get; }
    // constructor
    public Item(string iName, string description, int weight)
    {
        Weight = weight;
        Description = description;
        IName = iName;
    }
}