using UnityEngine;
using System.Collections;

public class Drag_Selection {
	
	GameObject sprite;
	UISprite uisprite;
	Rect rect;
	bool isVisible=false;
	public Drag_Selection()
	{
		//Initialize the Rectangle
		sprite= GameObject.Find("UI Root/Camera/Panel_Main/Sprite");
		uisprite= sprite.GetComponent<UISprite>();
		if(uisprite==null)
			EventLog.Log_Message("Error!");
		rect = new Rect(-1,-1,-1,-1);
	}
	//1280 & 720 are the UIRoot's aspect width.
	public void Start_Draw(Vector3 mousepos)
	{
		mousepos.x = mousepos.x / Screen.width   	* 1280;
		mousepos.y = mousepos.y / Screen.height		* 720;
		//Reset the Rectangle
		//rect = new Rect(-1,-1,-1,-1);
		// EventLog.Log_Message("Starting Draw");
		initialMouse=mousepos;



		isVisible=true;
	}
	Vector3 initialMouse;
	public void Update_Draw(Vector3 mousepos)
	{
		//Get Mouse pos percentage.
		mousepos.x = mousepos.x / Screen.width   	* 1280;
		mousepos.y = mousepos.y / Screen.height		* 720;

		// old_y= rect.yMin;
		// EventLog.Log_Message("Update Draw");
		float xMin = Mathf.Min(initialMouse.x, mousepos.x);
		float xMax = Mathf.Max(initialMouse.x, mousepos.x);
		float yMin = Mathf.Min(initialMouse.y, mousepos.y);
		float yMax = Mathf.Max(initialMouse.y, mousepos.y);

		// rect.xMin= xMin;
		// rect.xMax= xMax;
		// rect.yMin= yMin;
		// rect.yMax= yMax;

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
