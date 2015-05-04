using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	//Gui Panel_Ctx
	Panel_Hover pnH_CtxBuild;
	Panel_Hover pnH_CtxUnit;	

	Panel_Hover pnH_RoleManager;

	UILabel lbl_DebugState;
	//Thsese curves may be used for controlling the Camera for
	// a smooth curve in type effect when zooming.
	public AnimationCurve zoomCurve;
	public AnimationCurve distanceOffsetCurve;	

//	GController_CtxBuilding ctx_BuildMan;
	static UILabel lbl_MaxWorkers;

	//Player States
	enum Player_State{Idle, Placing_Building};

	Player_State current_state;
	//Grab the references from Logic_Controller
	public Logic_Controller logic_Cont;
	Building building;

	Drag_Selection dragSelect;

	void Start ()
	{
		building= null;
		current_state= Player_State.Idle;
		//Initialization Code
		logic_Cont=GameObject.Find("Logic_Controller").GetComponent<Logic_Controller>();
		pnH_CtxBuild=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building").GetComponent<Panel_Hover>();
		pnH_CtxUnit=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Unit").GetComponent<Panel_Hover>();
		

		pnH_RoleManager=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_RoleManager").GetComponent<Panel_Hover>();
		lbl_DebugState=GameObject.Find("UI Root/Camera/Panel_Main/Lbl_DebugState").GetComponent<UILabel>();
		
		dragSelect = new Drag_Selection();
		//Setup Ctx
		//ctx_BuildMan = new GController_CtxBuilding(logic_Cont);
		GController_CtxBuilding.Initialize(logic_Cont.man_BlackBoards);
		GController_CtxUnit.Initialize(logic_Cont.man_BlackBoards);		
	}




	//Here we check the Player's State and Cycle through the possible 
	//options the player wishes to do.

	Vector3 oldMouse;
	void Update()
	{
		if(current_state== Player_State.Idle)
		{
			if (Input.GetKeyDown (KeyCode.B))
			{
				current_state= Player_State.Placing_Building;
			}
			//If left clicking in the world in Idle mode
	        // Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
       		// if(screenRect.Contains(Input.mousePosition))
       		// {
				if(Input.GetMouseButtonDown(0))
				{

					//Only perform task if mouse is not over GUI element
					//Perform the Nested Check + Check if the panel itself is being hovered
					if(MouseOnGui())
						return;

					oldMouse= Input.mousePosition;
					//Not on Gui Therefore Start Drag Selection
					dragSelect.Start_Draw(GetWorldPosition(oldMouse));


					//Check if something collides with the placement before placing
					//Check if building can be placed, and if so place it. 
					//Otherwise we don't

					//The Code below is only for single click
					
				}
				//If the mouse "CLICKED" position should be same
				else if(Input.GetMouseButtonUp(0))
				{
					dragSelect.End_Draw();
					if(Input.mousePosition== oldMouse)
					{
						Building selectedBuild=logic_Cont.man_Collisions.Collision_GetBuilding(GetWorldPosition(Input.mousePosition));
						GController_CtxBuilding.Update_GTXInfo(selectedBuild);

						Actor selectedUnit=logic_Cont.man_Collisions.Collision_GetUnit(GetWorldPosition(Input.mousePosition));
						GController_CtxUnit.Update_GTXInfo(selectedUnit);	
					}			
				}
				// else if(Input.GetMouseButton(0))
				// {
				dragSelect.Update_Draw(GetWorldPosition(Input.mousePosition));				
				dragSelect.Draw();
				// }


			// }
		}

		//We are Placing a Building
		else if(current_state==Player_State.Placing_Building)
		{
				// Trees and Rocks
			// if(Input.GetKeyDown(KeyCode.T))
			// {
			// 	//Setup Tree for Placement
			// 	if(building!=null)
			// 	{
			// 		building.DeSelect_Ghost();
			// 		building=null;
			// 	}
			// 	building= new Tree();
			// 	building.Select_Ghost();
			// 	EventLog.Log_Message("Placing Building");
			// }
			// if(Input.GetKeyDown(KeyCode.R))
			// {
			// 	//Setup Tree for Placement
			// 	if(building!=null)
			// 	{
			// 		building.DeSelect_Ghost();
			// 		building=null;
			// 	}
			// 	building= new Rock();
			// 	building.Select_Ghost();
			// 	EventLog.Log_Message("Placing Building");
			// }

// House
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
			}
// Storage
			if(Input.GetKeyDown(KeyCode.C))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new Storage();
				building.Select_Ghost();
			}		
// Foresting Station
			if(Input.GetKeyDown(KeyCode.F))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new Foresting_Station();
				building.Select_Ghost();
			}		
// Sawmill
			if(Input.GetKeyDown(KeyCode.S))
			{
				//Setup Tree for Placement
				if(building!=null)
				{
					building.DeSelect_Ghost();
					building=null;
				}
				building= new SawMill();
				building.Select_Ghost();
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

				building.Set_Position(SnapVector(GetWorldPosition(Input.mousePosition)));	
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
		lbl_DebugState.text = current_state.ToString();
		MoveView();
		//Regardless State we move the camera
	}



	void MoveView()
	{
		int up=0;
		int right=0;
		if(Input.GetKey(KeyCode.W))
			up+=1;
		if(Input.GetKey(KeyCode.S))
			up-=1;
		if(Input.GetKey(KeyCode.D))
			right+=1;
		if(Input.GetKey(KeyCode.A))
			right-=1;

		Camera.main.transform.position += new Vector3(right,0,up);
	}



	//Returns the World position the mouse is pointing to.
	//This is useful when placing buildings

	Vector3 lastPosition;
	//Also useful for checking on characters
	Vector3 GetWorldPosition(Vector3 input)
	{
        //Selecting Objects
        // Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
        // if(screenRect.Contains(input))
        // {
 		Ray ray = Camera.main.ScreenPointToRay(input);

		//Get Distance from Origin to Ground based on direction

		float distance = -(ray.origin.y/ ray.direction.y);

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
		lastPosition=  (ray.origin+ (ray.direction*distance) );
	    // }

	    return lastPosition;
	}

	Vector3 SnapVector(Vector3 pos)
	{
		pos.x= (int)pos.x;
		pos.y= (int)pos.y;
		pos.z= (int)pos.z;
		return pos;
	}


	bool MouseOnGui()
	{
		if(pnH_CtxBuild.isMouseOver  || pnH_CtxBuild.checkHover())
			return true;
			
		if(pnH_CtxUnit.isMouseOver  || pnH_CtxUnit.checkHover())
			return true;		
	
		if(pnH_RoleManager.isMouseOver || pnH_RoleManager.checkHover())
			return true;
		return false;
	}
/*
Legend:
WSAD = Camera Move

B= Build
	T= Tree, Debug Only...Default Building is a Tree...because that's what I've placed so far.
	R= Rock

	H=House
	C=Storage
	F=Forest Station
	S=Sawmill

Escape = Back
*/



	//Gui Buttons Pressed

}
