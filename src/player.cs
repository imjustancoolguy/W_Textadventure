class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    // public Game game;
    private int health;
    private Inventory backpack;

    private double currency;

    private bool win = false;


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
        currency = 0;
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public double Currency
    {
        get { return currency; }
        set { currency = value; }
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

    public bool IsAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool Checkwin()
    {
        if (win == true)
        {
            return true;
        }
        else
        {
            return false;
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
            CurrentRoom.Chest.Put(itemName, pItem);
            return false;
        }
    }
    public bool DropToChest(string itemName)
    {
        Item pItem = backpack.Get(itemName);
        if (pItem == null)
        {
            // CurrentRoom.Chest.Put(itemName, pItem);
            return false;
        }
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
                case "BigCheese":
                    Heal(1250);
                    backpack.Items.Remove(command.SecondWord);
                    Console.WriteLine("You healed 50 hp while eating the cheese!");
                    break;
                case "teleportationCrystal":
                    win = true;
                    break;
                

            }
        }
        else
        {
            Console.WriteLine($"there is no {command.SecondWord} to use");
        }
    }

}
