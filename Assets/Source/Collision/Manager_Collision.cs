using UnityEngine;
using System.Collections;

public class Manager_Collision 
{

	//Divide Area into Sections

	//World needs to be divided into equal Zones

	public static int divCount=20; 
	public static int dimension=10000;

	//Sector Size = 50

	/*
		if divCount=10;
		& dimension =1000
		
		sectors = 100x100 = 10,000 area.

		dimensionArea = 1000x1000 = 1 000, 000
	*/
	Sector[][] sectors;
	public Manager_Collision()
	{
		//Initialize Sectors
		sectors = new Sector[divCount][];
		for(int x=0;x<divCount;x++)
		{
			sectors[x]= new Sector[divCount];
			for(int y=0;y<divCount;y++)
				sectors[x][y]=new Sector();
		}
	}



	//Static Objects Stored in SStatic List, they are only Added/Removed upon
	//construction/deconstruction

	//Dynamic Objects stored in Dynamic List, these guys update their position every frame.

	//Once Actors are placed inside a sector, there isn't a need for constant
	//removal + insert, simply check each sector that is not empty, and checks its units.

	public void Initial_Addition(Actor unit)
	{
		// Calculate the unit's sector using it's Center location
		Vector3 point = unit.unitPosition;

		float xSector = point.x/ (float)divCount;
		float ySector = point.z/ (float)divCount;
	}

	//Each Unit will call the Collision Manager, 

}
