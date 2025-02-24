class Player
{
// auto property
    public Room currentRoom { get; set; }
    public Game game;
    private int health;
    private bool alive = true;
// constructor

    public  int Health
    {
        get { return health; }
        set { health = value; }
    }
    public bool  Alive
    {
        get { 
        if (health <= 0)
        {
            alive = false;
        }
            return alive; }
        set { alive = value; }
    }

    public void Damage(int amount)
    {
        health-=amount;
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

    public Player()
    {
        currentRoom = null;
        health = 100;
    }
}
