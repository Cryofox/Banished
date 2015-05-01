using UnityEngine;
using System.Collections;

public class Gui_Hover : MonoBehaviour {
	
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
		_isOver=isOver;
		// EventLog.Log_Message("GUI_Generic Mouse is:"+_isOver);
	}
	

}
