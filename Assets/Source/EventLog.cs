using UnityEngine;
using System.Collections;

public static class EventLog{

	//Here we simply print to Console
	public static void Log_Message(string message)
	{
		UnityEngine.Debug.Log(message);
	}


	public static void Draw_Square(Vector3 botLeft, Vector3 topRight, Color color)
	{
		//Bottom Line
		UnityEngine.Debug.DrawLine(botLeft, new Vector3(topRight.x,0,botLeft.z), color);
		UnityEngine.Debug.DrawLine(botLeft, new Vector3(botLeft.x,0,topRight.z), color);

		//Top Line
		UnityEngine.Debug.DrawLine(topRight, new Vector3(botLeft.x,0,topRight.z), color);
		
		UnityEngine.Debug.DrawLine(topRight, new Vector3(topRight.x,0,botLeft.z), color);		

	}
}
