using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	//Thsese curves may be used for controlling the Camera for
	// a smooth curve in type effect when zooming.
	public AnimationCurve zoomCurve;
	public AnimationCurve distanceOffsetCurve;	

	//Player States
	enum Player_State{Idle, Placing_Building};

	Player_State current_state;
	//Grab the references from Logic_Controller
	public Logic_Controller logic_Cont;
	Building building;

	void Start ()
	{
		building= null;
		current_state= Player_State.Idle;
		//Initialization Code
		logic_Cont=GameObject.Find("Logic_Controller").GetComponent<Logic_Controller>();
	}




	//Here we check the Player's State and Cycle through the possible 
	//options the player wishes to do.
	void Update()
	{
		if(current_state== Player_State.Idle)
			if (Input.GetKeyDown (KeyCode.B))
			{
				current_state= Player_State.Placing_Building;
			}

		//We are Placing a Building
		if(current_state==Player_State.Placing_Building)
		{
			if(Input.GetKeyDown(KeyCode.T))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new Building();
				building.Select_Ghost();
				EventLog.Log_Message("Placing Building");
			}

			//Exit Placement State, go back to Idle
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				current_state= Player_State.Idle;
			}		

			if(building!=null)
			{	
				building.Set_Position(GetWorldPosition());	
				//0=Left, 1 =Right, 2 = Middle
				if(Input.GetMouseButtonDown(0))
				{
					logic_Cont.man_Collisions.Place_Building(building);
					logic_Cont.man_BlackBoards.AddBuilding(building, Logic_Controller.playerFaction);
					building=null;
				}
			}



		}


	}




	//Returns the World position the mouse is pointing to.
	//This is useful when placing buildings

	Vector3 lastPosition;
	//Also useful for checking on characters
	Vector3 GetWorldPosition()
	{

        //Selecting Objects
        Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
        if(screenRect.Contains(Input.mousePosition))
        {
	 		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//Get Distance from Origin to Ground based on direction
   
			float distance = -(ray.origin.y/ ray.direction.y);

	        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
    		lastPosition=  (ray.origin+ (ray.direction*distance) );
	    }

	    return lastPosition;
	}




/*
Legend:
B= Build
	T= Tree, Debug Only...Default Building is a Tree...because that's what I've placed so far.


Escape = Back
*/





}
