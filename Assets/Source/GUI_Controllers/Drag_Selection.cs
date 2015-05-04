using UnityEngine;
using System.Collections;

public class Drag_Selection {
	
	GameObject sprite;
	UISprite uisprite;
	Rect rect;
	bool isVisible=false;

	GameObject debugOnly_Start;
	GameObject debugOnly_End;

	public Drag_Selection()
	{
		//Initialize the Rectangle
		sprite= GameObject.Find("UI Root/Camera/Panel_Main/Sprite");

		debugOnly_End = Resources.Load<GameObject>("GameObject/Capsule");
		debugOnly_End = GameObject.Instantiate(debugOnly_End, Vector3.zero,Quaternion.identity) as GameObject;
		debugOnly_Start = Resources.Load<GameObject>("GameObject/Capsule");
		debugOnly_Start = GameObject.Instantiate(debugOnly_Start, Vector3.zero,Quaternion.identity) as GameObject;



		uisprite= sprite.GetComponent<UISprite>();

		rect = new Rect(-1,-1,-1,-1);
	}
	//1280 & 720 are the UIRoot's aspect width.

	//Should be drawn by giving the "World Pos" + Camera_Pos. And drawing the
	//Selection based on that.

	// public void Start_Draw(Vector3 mousepos, Vector3 camera_Pos)
	// {
	// 	cam_Pos= camera_Pos;

	// 	mousepos.x = mousepos.x / Screen.width   	* 1280;
	// 	mousepos.y = mousepos.y / Screen.height		* 720;
	// 	//Reset the Rectangle
	// 	//rect = new Rect(-1,-1,-1,-1);
	// 	// EventLog.Log_Message("Starting Draw");
	// 	initialMouse=mousepos;

	// 	isVisible=true;
	// }

	public void Start_Draw(Vector3 mouseWorldpos)
	{

		// debugOnly_Start.transform.position =  mouseWorldpos;
		// cam_Pos = cameraWorld_Pos;

		// //Calculate where on the Screen it is based on the camera_Pos

		// //1-1 Mapping now.

		// //We know the Cameras Position, we know the Mouse Pos.

		// //What's left is to determine the Cameras BotLeft Corner, and TopRight. Calculations can then be converted from there.
		// EventLog.Log_Message("Initial Mouse:"+ mousepos);

		// mousepos.x = mousepos.x / Screen.width   	* 1280;
		// mousepos.y = mousepos.y / Screen.height		* 720;
		// //Reset the Rectangle
		// //rect = new Rect(-1,-1,-1,-1);
		// // EventLog.Log_Message("Starting Draw");
		// initialMouse= mousepos;

		// EventLog.Log_Message("MousePos:"+ mousepos);
		// EventLog.Log_Message("Converted Mouse:"+ mousepos);

		isVisible=true;
		world_Mouse = mouseWorldpos;
	}
	float ratioX;
	Vector3 initialMouse;
	Vector3 cam_Pos;
	Vector3 world_Mouse;
	//+ converted Mouse
	public void Update_Draw(Vector3 mouseWorldpos)
	{

		//Convert from world to Screen.
		Vector3 mousepos =  Camera.main.WorldToScreenPoint(mouseWorldpos);
	
		Vector3 fixedMouse;


		fixedMouse = Camera.main.WorldToScreenPoint(world_Mouse);
		// EventLog.Log_Message("Mouse Converted Pos:"+ fixedMouse);
		//Calculate fixedMouse from WorldMousePos.

		fixedMouse.x = fixedMouse.x / Screen.width   	* 1280;
		fixedMouse.y = fixedMouse.y / Screen.height		* 720;
		//Get Mouse pos percentage.

		// EventLog.Log_Message("Difference:"+ (camera_Pos-cam_Pos));

		//Convert the Mouseposition from the Screen to the Aspect Ratio
		mousepos.x = mousepos.x / Screen.width   	* 1280;
		mousepos.y = mousepos.y / Screen.height		* 720;

		// old_y= rect.yMin;
		// EventLog.Log_Message("Update Draw");
		float xMin = Mathf.Min(fixedMouse.x, mousepos.x);
		float xMax = Mathf.Max(fixedMouse.x, mousepos.x);
		float yMin = Mathf.Min(fixedMouse.y, mousepos.y);
		float yMax = Mathf.Max(fixedMouse.y, mousepos.y);

		//Create the Rectangle on Screen
		rect= Rect.MinMaxRect(xMin,yMin,xMax, yMax);
		//Update Widget Properties
		uisprite.width = (int)rect.width;
		uisprite.height= (int)rect.height;





		sprite.transform.localPosition=new Vector2( rect.center.x- (1280/2),rect.center.y  - (720/2));	
	}

	public void End_Draw()
	{
		// EventLog.Log_Message("End Draw");
		isVisible=false;
	}

	//This is called Every time 
	public void Draw()
	{
		if(!isVisible)
		{
			sprite.SetActive(false);
			return;
		}
		else
		{
			sprite.SetActive(true);
			return;			
		}
		
	}


}
