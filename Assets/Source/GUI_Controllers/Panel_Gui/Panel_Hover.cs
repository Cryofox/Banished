using UnityEngine;
using System.Collections;

//This class creates a panel for main mouse check.
//if mouse enters inner button, this script
//checks each child and returns true if any true, otherwise false.
//It requires each child with it's own collider have a Gui_Hover script attached.
public class Panel_Hover : MonoBehaviour {
	
	public bool isMouseOver
	{
		get
		{
			return _isOver;
		}
	}

	private bool _isOver;
	void OnHover( bool isOver )
	{
		// if(isOver==true)
		// {
		// 	_isOver=true;
		// 	EventLog.Log_Message("Mouse is:"+ _isOver);
		// 	return;
		// }
		// else
		// {
		// 	foreach (Transform child in transform)
		// 	{
		// 		Gui_Hover gh = child.gameObject.GetComponent<Gui_Hover>();
		// 		if(gh!=null && gh.isMouseOver)
		// 		{
		// 			_isOver=true;
		// 			EventLog.Log_Message("_Mouse is:"+ _isOver);
		// 			return;
		// 		}
		// 		else
		// 		{
		// 			EventLog.Log_Message("Not sure about:"+ child.gameObject.name);					
		// 			//Could be a Panel_Hover
		// 			Panel_Hover ph = child.gameObject.GetComponent<Panel_Hover>();
		// 			if(ph!=null && ph.checkHover())
		// 			{
		// 				_isOver=true;
		// 				return;
		// 			}
		// 		}
		// 	}
		// }
		//If any child says true we set to true otherwise false.
	// controller.tooltipAnchor.SetActive(true);
	// else
	// controller.tooltipAnchor.SetActive(false);
		_isOver=isOver ;
		//EventLog.Log_Message("Mouse is:"+ _isOver);
		return;
	}

	public bool checkHover()
	{
		// //Use BoxCollider for fast checking
		// BoxCollider bc = GetComponent<BoxCollider>();

		// float minX=  bc.center.x - (bc.size.x/2);
		// float maxX=  bc.center.x + (bc.size.x/2);
		// float minY=  bc.center.y - (bc.size.y/2);
		// float maxY=  bc.center.y + (bc.size.y/2);


		// EventLog.Log_Message("Mouse is:"+Input.mousePosition);
		// EventLog.Log_Message("Minx is:"+minX);
		// EventLog.Log_Message("Maxx is:"+maxX);
		// EventLog.Log_Message("Miny is:"+minY);
		// EventLog.Log_Message("Maxy is:"+maxY);		

		// if(Input.mousePosition.x > minX &&
		//    Input.mousePosition.x < maxX &&
		//    Input.mousePosition.y > minY &&
		//    Input.mousePosition.y < maxY)
		// 	return true;

		// return false;	
		// if(_isOver==true)
		// 	return true;
		
			foreach (Transform child in transform)
			{
				Gui_Hover gh = child.gameObject.GetComponent<Gui_Hover>();
				if(gh!=null && gh.isMouseOver)
				{
					_isOver=true;
					// EventLog.Log_Message("_Mouse is:"+ _isOver);
					return true;
				}
				else
				{
					// EventLog.Log_Message("Inner-Not sure about:"+ child.gameObject.name);			
					//Could be a Panel_Hover
					Panel_Hover ph = child.gameObject.GetComponent<Panel_Hover>();
					if(ph!=null && ph.checkHover())
					{
						//_isOver=true;
						// EventLog.Log_Message("_Mouse is:"+ _isOver);
						return true;
					}
					else
					{
						// EventLog.Log_Message("Inner-Not a Panel:"+ child.gameObject.name);	
					}
				}
			}
			// EventLog.Log_Message("_Mouse is: False");
			return false;	
		
	}
}
