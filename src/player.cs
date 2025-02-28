class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    // public Game game;
    private int health;
    private Inventory backpack;

    private bool alive = true;
    // constructor

    public Inventory Backpack
    {
        get { return backpack; }
    }

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
        if (health > 100)
        {
            health = 100;
        }
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
    }
    public bool DropToChest(string itemName)
    {
        Item pItem = backpack.Get(itemName);
        backpack.Get(itemName);
        CurrentRoom.Chest.Put(itemName, pItem);
        Console.WriteLine($"you have dropped {itemName}");
        return false;
    }

    public void Use(Command command)
    {
        if (command.SecondWord == null)
        {
            Console.WriteLine("use what?");
            return;
        }
        if (backpack.Items.ContainsKey(command.SecondWord))
        {
            switch (command.SecondWord)
            {
                case "cheese":
                    Heal(50);
                    backpack.Items.Remove(command.SecondWord);
                    Console.WriteLine("You healed 50 hp while eating the cheese!");
                    break;
            }
        }
        else
        {
            Console.WriteLine($"there is no {command.SecondWord} to use");
        }
    }
}
