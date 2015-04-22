using UnityEngine;
using System.Collections;

public class World {

	int max_Length;
	
	//private FlowField ff_Pathfinder;  <- Might not be needed
	public Manager_Collision man_Collision;
	public World(int dimensionLength)
	{
		this.max_Length= dimensionLength;
		//this.ff_Pathfinder = new FlowField(dimensionLength);
	
	}



	//
	public void Debug_Draw()
	{}


}
