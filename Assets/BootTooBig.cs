using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

public class BootTooBig : MonoBehaviour 
{
	public KMBombInfo bomb;
	public KMAudio Audio;

	//Logging
	static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

	public GameObject[] btns;
	public KMSelectable[] modules;
	public Material[] icons;

	public AudioSource sound;
	public AudioClip[] sounds;

	String[] buttonNames;
	int[] buttonSets;
	int[] buttonOrders;
	List<int> correctBtns;
	List<int> presses = new List<int>();

	static String[][] moduleNames = {
								new String[] { "Air Traffic Controller", "Alphabetical Order", "Antichamber", "Button Masher", "Caesar Cipher", "Calendar", "Color Generator", "Digital Cipher", "Double Color", "Dr. Doctor", "Filibuster", "Follow the Leader", "Gadgetron Vendor", "Maze Scrambler", "Melody Sequencer", "Microcontroller", "Mineseeker", "Minesweeper", "Modern Cipher", "Murder", "Playfair Cipher", "Point of Order", "Poker", "Radiator", "Random Number Generator", "Snooker", "Street Fighter", "The Dealmaker", "The Fidget Spinner", "The Hangover", "The Number", "The Number Cipher", "The Plunger", "The Time Keeper", "Unfair Cipher", "Prime Checker", "LED Solver"},
								new String[] { "Alchemy", "Astrology", "Binary Tree", "Blind Alley", "Colorful Insanity", "Cryptography", "Dragon Energy", "Four-Card Monte", "Graphic Memory", "Horrible Memory", "Laundry", "Mad Memory", "Memory", "Modules Against Humanity", "Pictionary", "Poetry", "Purgatory", "S.E.T.", "Shape Memory", "SYNC-125 [3]", "Turn The Key", "Vexillology", "Yahtzee", "Zoni"},
								new String[] { "Binary LEDs", "Bordered Keys", "Chord Qualities", "Colored Keys", "Cruel Piano Keys", "Disordered Keys", "Festive Piano Keys", "Letter Keys", "Misordered Keys", "Ordered Keys", "Piano Keys", "Recorded Keys", "Reordered Keys", "Tallordered Keys", "Turn The Keys", "Unordered Keys"},
								new String[] { "Bartending", "Color Decoding", "Cooking", "Digit String", "Forget Everything", "Free Parking", "Grid Matching", "Hunting", "Listening", "Painting", "Plumbing", "Probing", "Scripting", "Symbolic Colouring", "Wavetapping"},
								new String[] { "Big Circle", "Binary Puzzle", "Boggle", "Bomb Diffusal", "European Travel", "Game of Life Simple", "Hex To Decimal", "Light Cycle", "Marble Tumble", "Mastermind Simple", "Periodic Table", "Qwirkle", "Symbol Cycle", "The Triangle", "Word Scramble"},
								new String[] { "3D Maze", "Blind Maze", "Boolean Maze", "Catchphrase", "Factory Maze", "Hexamaze", "Maze", "Module Maze", "Morse-A-Maze", "Mouse In The Maze", "Polyhedral Maze", "Subways", "The Crystal Maze", "USA Maze"},
								new String[] { "Adjacent Letters", "Alphabet Numbers", "Blockbusters", "Graffiti Numbers", "Hidden Colors", "Hyperactive Numbers", "Lasers", "Manometers", "Numbers", "Resistors"},
								new String[] { "Button Sequence", "Christmas Presents", "Determinants", "Genetic Sequence", "Harmony Sequence", "Maintenance", "Motion Sense", "Simon_s Sequence", "Wire Sequence"},
								new String[] { "101 Dalmatians", "Answering Questions", "Bitwise Operations", "Daylight Directions", "Encrypted Equations", "Equations", "Functions", "Instructions", "Pigpen Rotations", "Constellations"},
								new String[] { "Broken Buttons", "Complicated Buttons", "Gryphons", "Logical Buttons", "Morse Buttons", "Rapid Buttons", "Simon Simons", "Spinning Buttons", "Triangle Buttons"},
								new String[] { "Blue Arrows", "Dominoes", "Green Arrows", "LEGOs", "Orange Arrows", "Purple Arrows", "Red Arrows", "Yellow Arrows"},
								new String[] { "Bamboozling Button", "Square Button", "The Button", "The Hexabutton", "The Plunger Button", "The Sun", "The Triangle Button", "X01"},
								new String[] { "Colour Code", "Morse Code", "QR Code", "Tap Code", "Ten-Button Color Code", "The Code", "Who_s That Monsplode"},
								new String[] { "Accumulation", "Creation", "LED Encryption", "Modulus Manipulation", "Morse Identification", "Neutralization", "Synchronization"},
								new String[] { "3D Tunnels", "Quintuples", "Raiding Temples", "Signals", "Simon Samples", "Simon Scrambles", "Sonic & Knuckles"},
								new String[] { "Boolean Keypad", "Complex Keypad", "Keypad", "Number Pad", "Round Keypad", "The Gamepad"},
								new String[] { "Colored Squares", "Decolored Squares", "Discolored Squares", "Divided Squares", "Uncolored Squares", "Varicolored Squares"},
								new String[] { "Complicated Wires", "Perplexing Wires", "Risky Wires", "Seven Wires", "Skinny Wires", "Wires"},
								new String[] { "Combination Lock", "Gridlock", "Rock-Paper-Scissors-Lizard-Spock", "Rubik_s Clock", "The Block", "The Clock"},
								new String[] { "Cursed Double-Oh", "DetoNATO", "Double-Oh", "Hot Potato", "Modulo", "Tic-Tac-Toe"},
								new String[] { "Baba is Who", "Kudosudoku", "Mega Man 2", "Shikaku", "The Screw", "Zoo"},
								new String[] { "Orientation Cube", "Pattern Cube", "Rubik_s Cube", "The Cube", "The Hypercube", "The Ultracube"},
								new String[] { "Color Math", "Emoji Math", "Fast Math", "LED Math", "Math"},
								new String[] { "Crazy Talk", "Insane Talk", "Krazy Talk", "Regular Crazy Talk"},
								new String[] { "Coordinates", "Foreign Exchange Rates", "Logic Gates", "Simon States"},
								new String[] { "Retirement", "Visual Impairment", "Waste Management", "Wire Placement"},
								new String[] { "Chess", "Colorful Madness", "Number Nimbleness", "The Witness"},
								new String[] { "Bases", "Forget This", "Tennis", "Tetris"},
								new String[] { "Hieroglyphics", "Mashematics", "Mazematics", "Morsematics", "The Matrix"},
								new String[] { "Color Morse", "Encrypted Morse", "Reverse Morse", "Transmitted Morse"},
								new String[] { "Algebra", "Alpha", "IKEA", "Mafia", "Forget Enigma"},
								new String[] { "Cryptic Password", "Extended Password", "Password", "Symbolic Password"},
								new String[] { "Lion_s Share", "Mystic Square", "The Stare"},
								new String[] { "aa", "Know Your Way", "X-Ray"},
								new String[] { "Refill that Beer!", "Souvenir", "The Sphere"},
								new String[] { "Equations X", "Flavor Text EX", "Press X"},
								new String[] { "Arithmelogic", "Logic", "Superlogic"},
								new String[] { "The Deck of Many Things", "Simon Sings", "Wingdings"},
								new String[] { "Faulty Sink", "Sink", "The Giant_s Drink"},
								new String[] { "Rotary Phone", "The iPhone", "Timezone"},
								new String[] { "Backgrounds", "Faulty Backgrounds", "Simon Sounds"},
								new String[] { "Cheap Checkout", "Lights Out", "Odd One Out"},
								new String[] { "Crackbox", "The Festive Jukebox", "The Jukebox"},
								new String[] { "Flags", "Maritime Flags"},
								new String[] { "Forget Them All", "Sueet Wall"},
								new String[] { "Boolean Venn Diagram", "Nonogram"},
								new String[] { "Elder Futhark", "Question Mark"},
								new String[] { "Burger Alarm", "Burglar Alarm"},
								new String[] { "Stained Glass", "Venting Gas"},
								new String[] { "Benedict Cumberbatch", "Color Match"},
								new String[] { "Colored Switches", "Switches"},
								new String[] { "Font Select", "Only Connect"},
								new String[] { "Simon Shrieks", "Simon Speaks"},
								new String[] { "Planets", "Symbolic Coordinates"},
								new String[] { "Human Resources", "Simon Says"},
								new String[] { "Button Grid", "LED Grid"},
								new String[] { "Pie", "Subscribe to PewDiePie"},
								new String[] { "Character Shift", "Shape Shift"},
								new String[] { "Left and Right", "Monsplode, Fight!"},
								new String[] { "Seven Deadly Sins", "Simon Spins"},
								new String[] { "Battleship", "Friendship"},
								new String[] { "Knob", "Needy Mrs Bob"},
								new String[] { "Character Codes", "Error Codes"},
								new String[] { "Passport Control", "The Troll"},
								new String[] { "The Necronomicon", "The Swan"},
								new String[] { "Mahjong", "Pong"},
								new String[] { "Digital Root", "Splitting The Loot", "Cruel Digital Root"},
								new String[] { "Broken Guitar Chords", "Guitar Chords"},
								new String[] { "Grocery Store", "Semaphore"},
								new String[] { "Forget Me Not", "Turtle Robot"},
								new String[] { "Silly Slots", "Skewed Slots"},
								new String[] { "Countdown", "Cruel Countdown"},
								new String[] { "Game of Life Cruel", "Mastermind Cruel"},
								new String[] { "Curriculum", "Stack_em"},
								new String[] { "Cruel Ten Seconds", "Ten Seconds"},
								new String[] { "Edgework", "Module Homework"},
								new String[] { "Calculus", "Greek Calculus"},
								new String[] { "FizzBuzz", "QuizBuzz"},
								new String[] { "Lucky Dice", "Connection Device"},
								new String[] { "Anagrams", "Tangrams", "Unrelated Anagrams"},
								new String[] { "A Mistake", "Adventure Game", "Alphabet", "Bitmaps", "Black Hole", "Blackjack", "Braille", "British Slang", "Brush Strokes", "Capacitor Discharge", "Challenge & Contact", "Coffeebucks", "Color Flash", "Command Prompt", "Connection Check", "Cookie Jars", "Crafting Table", "Draw", "Double Expert", "Encrypted Values", "English Test", "Find The Date", "Flashing Lights", "Flavor Text", "Flip The Coin", "Hogwarts", "Homophones", "HTTP Response", "Ice Cream", "Identity Parade", "Lightspeed", "Party Time", "Lombax Cubes", "Maze^3", "Micro-Modules", "Monsplode Trading Cards", "Morse War", "Mortal Kombat", "Pay Respects", "Perspective Pegs", "Rhythms", "Roman Art", "Safety Safe", "Schlag den Bomb", "Sea Shells", "Shapes And Bombs", "Simon Screams", "Simon Sends", "Simon Squawks", "Simon Stops", "Simon Stores", "Simon_s Stages", "Simon_s Star", "Skyrim", "Snap!", "Sonic the Hedgehog", "Speak English", "Sticky Notes", "Synonyms", "T-Words", "Tasha Squeals", "Tax Returns", "Terraria Quiz", "Text Field", "The Bulb", "The Digit", "The Jack-O_-Lantern", "The Jewel Vault", "The Labyrinth", "The London Underground", "The Moon", "The Radio", "The Stock Market", "The Stopwatch", "The Switch", "The Wire", "Third Base", "Toon Enough", "Two Bits", "Valves", "Web Design", "Westeros", "Who_s on First", "Wire Spaghetti", "Word Search"}
	};

	void Awake()
	{
		moduleId = moduleIdCounter++;
		GetComponent<KMBombModule>().OnActivate += Activate;

		modules[0].OnInteract += delegate () { PressButton(0); return false; };
		modules[1].OnInteract += delegate () { PressButton(1); return false; };
		modules[2].OnInteract += delegate () { PressButton(2); return false; };
		modules[3].OnInteract += delegate () { PressButton(3); return false; };
		modules[4].OnInteract += delegate () { PressButton(4); return false; };
		modules[5].OnInteract += delegate () { PressButton(5); return false; };
		modules[6].OnInteract += delegate () { PressButton(6); return false; };
		modules[7].OnInteract += delegate () { PressButton(7); return false; };
	}

	void Activate()
	{
		
	}

	void Start () 
	{
		for(int i = 0; i < moduleNames.Length; i++)
			Debug.Log(i + " " + moduleNames[i].Length);
		RandomizeButtons();
	}

	void PressButton(int n)
	{
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		modules[n].AddInteractionPunch(.5f);

		if(moduleSolved)
		{
			sound.clip = sounds.ToList().Find(x => x.name == buttonSets[n] + " " + (buttonOrders[n] + 1));
			sound.Play();
			return;
		}

		if(!presses.Exists(x => x == n))
		{
			presses.Add(n);
       		Debug.LogFormat("[Boot Too Big #{0}] Pressed button {1}.", moduleId, n + 1);
		}

		if(presses.Count() == 2)
		{
			if(presses.Exists(x => x == correctBtns.ElementAt(0)) && presses.Exists(x => x == correctBtns.ElementAt(1)))
			{
				sound.clip = sounds.ToList().Find(x => x.name == buttonSets[n] + " " + (buttonOrders[n] + 1));
				sound.Play();
				Debug.LogFormat("[Boot Too Big #{0}] Module solved.", moduleId);
				moduleSolved = true;
				GetComponent<KMBombModule>().HandlePass();
			}
			else
			{
				Debug.LogFormat("[Boot Too Big #{0}] Strike! Button presses do not match solution.", moduleId);
				presses = new List<int>();
				GetComponent<KMBombModule>().HandleStrike();
			}
		}
		else
		{
			sound.clip = sounds.ToList().Find(x => x.name == buttonSets[n] + " " + (buttonOrders[n] + 1));
			sound.Play();
		}
	}
	
	void RandomizeButtons()
	{
		int[] order = new int[] {0, 1, 2, 3, 4, 5, 6, 7}.ToList().OrderBy(x => rnd.Range(0, 1000)).ToArray();
		List<int> usedSets = new List<int>();
		buttonNames = new String[8];
		buttonSets = new int[8];
		buttonOrders = new int[8];
		correctBtns = new List<int>();

		int set = rnd.Range(0, moduleNames.Length - 1);
		String[] rhyme = moduleNames[set];
		int firstBtn = rnd.Range(0, rhyme.Length);
		int secondBtn;
		do { secondBtn = rnd.Range(0, rhyme.Length);} while (firstBtn == secondBtn);
		buttonNames[order[0]] = rhyme[firstBtn];
		buttonNames[order[1]] = rhyme[secondBtn];
		buttonSets[order[0]] = set;
		buttonSets[order[1]] = set;
		buttonOrders[order[0]] = firstBtn;
		buttonOrders[order[1]] = secondBtn;

		btns[order[0]].transform.Find("img").GetComponentInChildren<Renderer>().material = icons.ToList().Find(x => x.name == buttonNames[order[0]]);
		btns[order[1]].transform.Find("img").GetComponentInChildren<Renderer>().material = icons.ToList().Find(x => x.name == buttonNames[order[1]]);

		correctBtns.Add(order[0]);
		correctBtns.Add(order[1]);
		usedSets.Add(set);

		int orderN = rnd.Range(0, moduleNames[moduleNames.Length - 1].Length);
		buttonNames[order[2]] = moduleNames[moduleNames.Length - 1][orderN];
		btns[order[2]].transform.Find("img").GetComponentInChildren<Renderer>().material = icons.ToList().Find(x => x.name == buttonNames[order[2]]);
		buttonSets[order[2]] = moduleNames.Length - 1;
		buttonOrders[order[2]] = orderN;

		for(int i = 3; i < order.Length; i++)
		{
			do {set = rnd.Range(0, moduleNames.Length - 1);} while (usedSets.Exists( x => x == set));
			usedSets.Add(set);
			orderN = rnd.Range(0, moduleNames[set].Length);
			buttonNames[order[i]] = moduleNames[set][orderN];
			btns[order[i]].transform.Find("img").GetComponentInChildren<Renderer>().material = icons.ToList().Find(x => x.name == buttonNames[order[i]]);
			buttonSets[order[i]] = set;
			buttonOrders[order[i]] = orderN;
		}

		for(int i = 0; i < buttonNames.Length; i++)
       		Debug.LogFormat("[Boot Too Big #{0}] Button {1} is {2}.", moduleId, i + 1, buttonNames[i]);
	
		Debug.LogFormat("[Boot Too Big #{0}] Correct buttons are {1} and {2}.", moduleId, correctBtns.ElementAt(0) + 1, correctBtns.ElementAt(1) + 1);
	}
}
