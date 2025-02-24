class Player
{
// auto property
    public Room currentRoom { get; set; }
    private int health;
// constructor

    public void damage(int amount)
    {
        health-=amount;
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public void IsAlive()
    {
        if (health >= 0)
        {
            
        }
    }
    public Player()
    {
        currentRoom = null;
        health = 100;
    }
}
