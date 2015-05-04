using UnityEngine;
using System.Collections;
//This Class inherits Mono as it needs to be triggered from within the game
public class Logic_Controller : MonoBehaviour {
	public static string playerFaction="MyFaction";



	//Persistent Values
	private float timeElapsed;

	//This resets to 0 when targetTime autosave is hit.
	private float timeElapsed_AutoSave;
	

	private float current_RealWorldTime;

	//Managers for key game Logic
	public Manager_BlackBoard man_BlackBoards;
	public Manager_Collision man_Collisions;

	// FlowField Manager
	// Collision Manager


	//Modifiable Parameters
	//Avoid Public Tag (when inheriting monobehaviour) as that caches into Unitys- UI, and overrides and code changes

	//Creates the number of BlackBoard Classes that need to be managed (1 Per Faction)
	int num_Factions=10;


	//Auto-Save Feature
	float targetTime_AutoSave;



	void Start ()
	{
		//Initialization Code
		Initialize_World();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Calculate the Time since last Frame
		//Here we assign unity's Time since last function call here
		timeElapsed = Time.deltaTime;
		
		timeElapsed_AutoSave+= timeElapsed;

		//We want to fix updates to a max of 1Second calls
		float tempTime;
		//Call Update while we're behind
		while(timeElapsed>0)
		{
			//Choose the smaller of the 2, this prevents overshooting passed times.
			tempTime= Mathf.Min(1,timeElapsed);
			Update_Routine(tempTime);
			timeElapsed-=tempTime;
		}

		if(timeElapsed_AutoSave>targetTime_AutoSave)
		{
			timeElapsed_AutoSave=0;
			AutoSave_Routine();
		}
	}

	//1k Units per faction? :)
	//10k = Lag

	//5k = Decent












	//This is the Function that does all the Heavy Lifting
	void Update_Routine(float timeElapsed)
	{
		man_BlackBoards.Update_Boards(timeElapsed);
		man_Collisions.Debug_DrawZones();
		GController_CtxUnit.Generic_Update();
	}

	void AutoSave_Routine()
	{
		//Call Garbage Collector

	}




	//Game starts you off with 10 people anyways
	int debug_Test=1000;


	//This function will setup the world with a random displacement of
	//trees rocks etc
	void Initialize_World()
	{
		//Create the Collision Handler
		//World dimensions currently Stored inside here
		man_Collisions= new Manager_Collision();

		//Create the Unit Faction Handler
		man_BlackBoards= new Manager_BlackBoard(man_Collisions);
		//Spawn 10 "Player Faction" units
		// for(int i=0;i<debug_Test;i++)
		// {
		// 	man_BlackBoards.AddActor(new Actor(new Vector3(500,0,500)),playerFaction);
		// }
		AddRandomTrees();
		AddRandomRocks();

		//Position camera and spawn player's units
		SetupPlayer();
		EventLog.Log_Message("World successfully Initialized");
	}

	//Add some Random Trees

	readonly int debugTreeCount=10000;
	readonly int debugRockCount=5000;
	void AddRandomTrees()
	{
		//Lets see what adding 1000 trees at random looks like
		//If we can place a building at this location, then we place it.
		//We also add it to Blackboard for consistency
		int treeCount=0;
		while(treeCount<debugTreeCount)
		{
			Building building= new Tree();
			bool isPlaced=false;
			while(!isPlaced)
			{
				//Randomize the X/Z
				float x= Random.Range(0,Manager_Collision.dimension);
				float y= Random.Range(0,Manager_Collision.dimension);
				//We need to create the GO
				//Select Ghost may be a poor name, but it was conjured for building 
				//placement via player
				building.Select_Ghost();
				building.Set_Position(new Vector3(x,0,y));			
				if(man_Collisions.Place_Building(building))
				{
					man_BlackBoards.AddBuilding(building, "Nature");
					treeCount++;
					isPlaced=true;
				}
			}
		}
	}

	void AddRandomRocks()
	{
		//Lets see what adding 1000 trees at random looks like
		//If we can place a building at this location, then we place it.
		//We also add it to Blackboard for consistency
		int rockCount=0;
		while(rockCount<debugRockCount)
		{
			Building building= new Rock();
			bool isPlaced=false;
			while(!isPlaced)
			{
				//Randomize the X/Z
				float x= Random.Range(0,Manager_Collision.dimension);
				float y= Random.Range(0,Manager_Collision.dimension);
				//We need to create the GO
				//Select Ghost may be a poor name, but it was conjured for building 
				//placement via player
				building.Select_Ghost();
				building.Set_Position(new Vector3(x,0,y));			
				if(man_Collisions.Place_Building(building))
				{
					man_BlackBoards.AddBuilding(building, "Nature");
					rockCount++;
					isPlaced=true;
				}
			}
		}
	}
	//Now That Sector information is maintained, a unit can cross examined for collision
	//Start with 10 units
	void SetupPlayer()
	{
		//Choose a start location
		bool isPlaced=false;
		Building building = new Storage();

		building.Select_Ghost();
		building.inventory.InsertResourceAmount("plank",200);
		building.inventory.InsertResourceAmount("wood",200);		
		while(!isPlaced)
		{
			//Randomize the X/Z
			float x= Random.Range(0,Manager_Collision.dimension);
			float y= Random.Range(0,Manager_Collision.dimension);
			//We need to create the GO
			//Select Ghost may be a poor name, but it was conjured for building 
			//placement via player

			Vector3 startPos=new Vector3(x,0,y);			
			

			building.Set_Position(startPos);

			if(man_Collisions.Place_Building(building))
			{
				man_BlackBoards.AddBuilding(building, Logic_Controller.playerFaction);
				for(int i=0;i<10;i++)
				{
					man_BlackBoards.AddActor(new Actor(startPos),playerFaction);
				}
				isPlaced=true;
				Camera.main.transform.position=new Vector3(x,Camera.main.transform.position.y,y);	
			}


			// if(man_Collisions.Collision_GetBuilding(startPos)==null)
			// {
			// for(int i=0;i<10;i++)
			// {
			// 	man_BlackBoards.AddActor(new Actor(startPos),playerFaction);
			// }
			// isPlaced=true;
			// Camera.main.transform.position=new Vector3(x,Camera.main.transform.position.y,y);

			// }
		}
		
	}
	//public Building Collision_GetBuilding(Vector3 point)




}
