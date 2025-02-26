class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    // public Game game;
    private int health;
    private Inventory backpack;

    private bool alive = true;
    // constructor

    public Player()
    {
        CurrentRoom = null;
        health = 100;
        backpack = new Inventory(25);
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public bool Alive
    {
        get
        {
            if (health <= 0)
            {
                alive = false;
            }
            return alive;
        }
        set { alive = value; }
    }
    //methods
    public void Damage(int amount)
    {
        health -= amount;
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public void IsAlive()
    {
        if (health <= 0)
        {
            alive = false;
        }
    }

    public bool TakeFromChest(string itemName)
    {
        Item pItem = CurrentRoom.Chest.Get(itemName);
        if (backpack.FreeWeight() - pItem.Weight >= 0)
        {
            backpack.Put(itemName, pItem);
            return true;
        }
        else
        {
            Console.WriteLine("youre backpack is full");
            DropToChest(itemName);
            return false;
        }
        // TODO implement:
        // Remove the Item from the Room
        // Put it in your backpack.
        // Inspect returned values.
        // Communicate to the user what's happening.
        // If the item doesn't fit your backpack, put it back in the chest.
        // Return true/false for success/failure
    }
    public bool DropToChest(string itemName)
    {
        Item pItem = backpack.Get(itemName);
        backpack.Get(itemName);
        CurrentRoom.Chest.Put(itemName, pItem);
        Console.WriteLine("you have dropped the item");
        // TODO implement:
        // Remove Item from your inventory.
        // Add the Item to the Room
        // Inspect returned values
        // Communicate to the user what's happening
        // Return true/false for success/failure
        return false;
    }

}
