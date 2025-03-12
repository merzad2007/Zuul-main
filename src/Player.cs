
class Player
{
    // fields
    public int Health { get; private set; }
    // auto property
    public Room CurrentRoom { get; set; }
    

    // constructor
    public Player()
    {
        CurrentRoom = null;
        Health = 100;

    }

    // methods
    public int Damage(int amount) // player loses some health
    {
        this.Health -= amount;
        if (this.Health < 0) this.Health = 0;
        return this.Health;
    }


    public int Heal(int amount) // player's health restores
    {
        this.Health += amount;
        if (this.Health > 100) this.Health = 100;
        return this.Health;
    }
    public bool IsAlive() // checks whether the player is alive or not
    {
        return this.Health > 0;

    }


    public string GetStatus()
    {
        string str = "You have ";
        str += this.Health;
        str += ".\n";
        return str;
    }


}