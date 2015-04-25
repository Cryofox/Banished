using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	//Thsese curves may be used for controlling the Camera for
	// a smooth curve in type effect when zooming.
	public AnimationCurve zoomCurve;
	public AnimationCurve distanceOffsetCurve;	

//	GController_CtxBuilding ctx_BuildMan;



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

		//Setup Ctx
		//ctx_BuildMan = new GController_CtxBuilding(logic_Cont);
		GController_CtxBuilding.Initialize(logic_Cont);
	}




	//Here we check the Player's State and Cycle through the possible 
	//options the player wishes to do.
	void Update()
	{
		if(current_state== Player_State.Idle)
		{
			if (Input.GetKeyDown (KeyCode.B))
			{
				current_state= Player_State.Placing_Building;
			}
			//If left clicking in the world in Idle mode
			if(Input.GetMouseButtonDown(0))
			{
				//Check if something collides with the placement before placing

				//Check if building can be placed, and if so place it. 
				//Otherwise we don't
				Building selectedBuild=logic_Cont.man_Collisions.Collision_GetBuilding(GetWorldPosition());
				GController_CtxBuilding.Update_GTXInfo(selectedBuild);
			}

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
				building= new Tree();
				building.Select_Ghost();
				EventLog.Log_Message("Placing Building");
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new Rock();
				building.Select_Ghost();
				EventLog.Log_Message("Placing Building");
			}

			if(Input.GetKeyDown(KeyCode.H))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new House();
				building.Select_Ghost();
				EventLog.Log_Message("Placing Building");
			}



			//Exit Placement State, go back to Idle
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				current_state= Player_State.Idle;
			}		
			//When a building is highlighted
			if(building!=null)
			{	
				building.Set_Position(GetWorldPosition());	
				//0=Left, 1 =Right, 2 = Middle
				if(Input.GetMouseButtonDown(0))
				{
					//Check if something collides with the placement before placing

					//Check if building can be placed, and if so place it. 
					//Otherwise we don't
					if(logic_Cont.man_Collisions.Place_Building(building))
					{
						logic_Cont.man_BlackBoards.AddBuilding(building, Logic_Controller.playerFaction);
						building=null;
					}
				}

				else if(Input.GetKeyDown(KeyCode.Period))
				{
					building.Rotate_Right();
				}

				else if(Input.GetKeyDown(KeyCode.Comma))
				{
					building.Rotate_Left();
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
	R= Rock

Escape = Back
*/



	//Gui Buttons Pressed

}
