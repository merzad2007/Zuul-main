using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room balkon = new Room("in the balkon");




		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);
		office.AddExit("up", balkon);

		balkon.AddExit("down", office);

		// Create your Items here
		Item knife = new Item(5, "not sord");
		Item pistol = new Item(, "headshot");
		Item shield = new Item(10, "protection");

		// And add them to the Rooms
		pub.Chest.Put("knife", knife);
		theatre.Chest.Put("pistol", pistol);
		lab.Chest.Put("shield", shield);

		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();


		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			if (!player.IsAlive())
			{
				Console.WriteLine("You have lost all your health. Game over!");
				break;
			}
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Look();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
		}

		return wantToQuit;
	}



	// ######################################
	// implementations of user commands:
	// ######################################
	private void Look()
	{
		if (player.CurrentRoom.Chest.TotalWeight() <= 0)
		{
			Console.WriteLine("The chest is empty.");
		}
		else
		{
			player.CurrentRoom.Chest.GetItems();
		}
	}


	private void PrintStatus()
	{
		Console.WriteLine("Your health is: " + player.Health);
		Console.WriteLine(player.CurrentRoom.GetLongDescription());


	}

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}


	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}

		string itemName = command.SecondWord;
		bool success = player.TakeFromChest(itemName);

		if (success)
		{
			Console.WriteLine("You have taken the " + itemName);
		}
	}

	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}

		string itemName = command.SecondWord;
		bool success = player.DropToChest(itemName);

		if (success)
		{
			Console.WriteLine("You have dropped the " + itemName);
		}
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Go where?");
			return;
		}



		string direction = command.SecondWord;
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.Damage(10); // Reduce health by 10 per move
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		Console.WriteLine("Your health is now: " + player.Health);
	}
}
