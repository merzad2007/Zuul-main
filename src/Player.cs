
using System.ComponentModel;

class Player
{
    // fields


    public int Health { get; private set; }
    // auto property
    public Room CurrentRoom { get; set; }

    private Inventory backpack; // player's inventory

    // constructor
    public Player()
    {
        CurrentRoom = null;
        Health = 100;
        backpack = new Inventory(25);
    }

    public Inventory Backpack{ get { return backpack; } }

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

    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("There is no " + itemName + " in the chest!");
            return false;
        }
        if (item.Weight > backpack.FreeWeight())
        {
            Console.WriteLine("You can't carry that much weight!");
            CurrentRoom.Chest.Put(itemName, item);
            return false;
        }

        bool success = backpack.Put(itemName, item);
        if (!success)
        {
            CurrentRoom.Chest.Put(itemName, item);
        }
        return true;
    }

    public bool DropToChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("There is no " + itemName + " in the inventory!");
            return false;
        }

        CurrentRoom.Chest.Put(itemName, item);
        Console.WriteLine("You have dropped the " + itemName);
        return true;
    }
}
