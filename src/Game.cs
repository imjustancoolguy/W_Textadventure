using System;
using System.Runtime.InteropServices;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	// private Item item;

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
		Room spawn = new Room("The spawn point of the dungeon");
		Room guild = new Room("Welcome to the dungeons guild");
		Room gacha = new Room("We love gambling!");
		Room home = new Room("Welcome home!");
		Room shop = new Room("You are at the shop");
		Room gate = new Room("Welcome to the gate of the dungeon");
		Room dungeon = new Room("It is le dungeon");

		// Initialise room exits
		spawn.AddExit("east", shop);
		spawn.AddExit("south", gacha);
		spawn.AddExit("west", guild);
		spawn.AddExit("north", gate);

		gate.AddExit("east", home);
		gate.AddExit("south", spawn);
		gate.AddExit("north", dungeon);

		guild.AddExit("east", spawn);

		shop.AddExit("west", spawn);

		home.AddExit("west", gate);

		gacha.AddExit("north", spawn);

		// Create your Items here
		Item cheese = new Item("Cheese", "A very delicious piece of cheese", 1);
		Item bigCheese = new Item("BigCheese", "a big piece of cheese", 25);

		// And add them to the Rooms
		spawn.Chest.Put("cheese", cheese);
		spawn.Chest.Put("BigCheese", bigCheese);

		// Create new enemy's
		Enemy zombie = new Enemy(100, "zombie");

		// add enemy's to the room
		// dungeon.addEnemy("zombie", zombie);

		// Start game outside
		player.CurrentRoom = spawn;

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
		Console.WriteLine(player.CurrentRoom.Chest.PrintItems());
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
				if (command.SecondWord.Contains("next"))
				{

				}
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				if (player.CurrentRoom.Chest.Items.Count > 0)
				{
					Console.WriteLine(player.CurrentRoom.Chest.PrintItems());
				}
				// if (player.CurrentRoom.rEnemies.Count > 0)
				// {
				// 	Console.WriteLine(player.CurrentRoom.rEnemies.printEnemy());
				// }
				break;
			case "status":
				Status();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "inventory":
				Console.WriteLine(player.Backpack.PrintInvItems());
				break;
			case "use":
				player.Use(command);
				break;
		}

		return wantToQuit;
	}
	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}
		if (player.CurrentRoom.Chest.Items.ContainsKey(command.SecondWord))
		{
			if (player.TakeFromChest(command.SecondWord))
			{
				Console.WriteLine($"you took the {command.SecondWord}");
			}
		}
		else if (!player.CurrentRoom.Chest.Items.ContainsKey(command.SecondWord))
		{
			Console.WriteLine($"there is no {command.SecondWord} here");
		}
	}
	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}
		if (player.Backpack.Items.ContainsKey(command.SecondWord))
		{
			player.DropToChest(command.SecondWord); ;
		}
		else if (!player.Backpack.Items.ContainsKey(command.SecondWord))
		{
			Console.WriteLine($"there is no {command.SecondWord} in your backpack");
		}
	}

	private void Status()
	{
		Console.WriteLine("your health = " + player.Health);
		if (!player.Alive) { Console.WriteLine("You are dead"); }
		else { Console.WriteLine("You are alive"); }
		Console.WriteLine(player.Backpack.FreeWeight() + "KG free space");
	}


	// ######################################
	// implementations of user commands:
	// ######################################
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

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.Damage(5);
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}
