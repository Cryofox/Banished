using UnityEngine;
using System.Collections;
//This Class inherits Mono as it needs to be triggered from within the game
public class Logic_Controller : MonoBehaviour {




	//Persistent Values
	private float timeElapsed;

	//This resets to 0 when targetTime autosave is hit.
	private float timeElapsed_AutoSave;
	

	private float current_RealWorldTime;

	//Managers for key game Logic
	private Manager_BlackBoard man_BlackBoards;
	// FlowField Manager
	// Collision Manager


	//Modifiable Parameters
	//Avoid Public Tag (when inheriting monobehaviour) as that caches into Unitys- UI, and overrides and code changes

	//Creates the number of BlackBoard Classes that need to be managed (1 Per Faction)
	int num_Factions=10;
	//Sets up the Square World dimensions to be used by the games Navigation and
	int world_SideLength; 

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

	void Initialize_World()
	{


	}



	//This is the Function that does all the Heavy Lifting
	void Update_Routine(float timeElapsed)
	{

	}

	void AutoSave_Routine()
	{
		//Call Garbage Collector

	}

}
