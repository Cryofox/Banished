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

	//Game starts you off with 10 people anyways
	int debug_Test=1000;
	void Initialize_World()
	{
		//Create the Collision Handler
		//World dimensions currently Stored inside here
		man_Collisions= new Manager_Collision();

		//Create the Unit Faction Handler
		man_BlackBoards= new Manager_BlackBoard(man_Collisions);
		//Spawn 10 "Player Faction" units
		for(int i=0;i<debug_Test;i++)
		{
			man_BlackBoards.AddActor(new Actor(new Vector3(500,0,500)),playerFaction);
		}

		EventLog.Log_Message("World successfully Initialized");
	}



	//This is the Function that does all the Heavy Lifting
	void Update_Routine(float timeElapsed)
	{
		man_BlackBoards.Update_Boards(timeElapsed);
		man_Collisions.Debug_DrawZones();
	}

	void AutoSave_Routine()
	{
		//Call Garbage Collector

	}

}
