using UnityEngine;
using System.Collections;

public class Manager_Collision 
{

	//Divide Area into Sections

	//World needs to be divided into equal Zones

	public static int divCount=100; 
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

	// public void Initial_Addition(Actor unit)
	// {
	// 	int segmentLength = dimension/divCount;
	// 	// Calculate the unit's sector using it's Center location
	// 	Vector3 point = unit.unitPosition;

	// 	int xSector = (int)(point.x)/ segmentLength;
	// 	int ySector = (int)(point.z)/ segmentLength;

	// 	if(xSector<0 || xSector> dimension)
	// 	{xSector=0;}
	// 	if(ySector<0 || ySector> dimension)
	// 	{ySector=0;}

	// 	sectors[xSector][ySector].Initial_AddUnit(unit);
	// }

	public void Update_Sector(Actor unit)
	{
		int segmentLength = dimension/divCount;
		// Calculate the unit's sector using it's Center location
		Vector3 point = unit.unitPosition;

		int xSector = (int)(point.x)/ segmentLength;
		int ySector = (int)(point.z)/ segmentLength;

		if(xSector<0 || xSector> dimension)
		{xSector=0;}
		if(ySector<0 || ySector> dimension)
		{ySector=0;}

		if(unit.lastSector.x!= xSector || unit.lastSector.z != ySector)
		{
			//Remove Unit from Last Sector
			sectors[ (int)unit.lastSector.x][(int)unit.lastSector.z].RemoveUnit(unit);
			sectors[xSector][ySector].AddUnit(unit);
		}
	}

	//Now That Sector information is maintained, a unit can cross examined for collision
	public void Collision_Check(Actor unit)
	{
		//For Each Corner of the Object check what sectors they belong in and 
		//Cross Examine those entities for collision
		float bL_x 		= unit.unitPosition.x - unit.collisionBox.horizontal_halfOffset;
		float bL_y		= unit.unitPosition.z - unit.collisionBox.vertical_halfOffset;

		float tL_x 		= unit.unitPosition.x + unit.collisionBox.horizontal_halfOffset;
		float tL_y		= unit.unitPosition.z + unit.collisionBox.vertical_halfOffset;

		//We now check the Sectors of these locations
		int segmentLength = dimension/divCount;



		int bl_xSector = (int)(bL_x)/ segmentLength;
		int bl_ySector = (int)(bL_y)/ segmentLength;

		int tl_xSector = (int)(tL_x)/ segmentLength;
		int tl_ySector = (int)(tL_y)/ segmentLength;
		//Remove Unit from Last Sector

		// //4 Checks are needed :/
		// if(bl_xSector < tl_xSector && bl_ySector< tl_ySector)
		// {


		// }

		// //2 Checks Left and Right
		// if(bl_xSector < tl_xSector && bl_ySector == tl_ySector)
		// {

		// }	
		// //2 Checks Up and Down	
		// if(bl_xSector == tl_xSector && bl_ySector < tl_ySector)
		// {
		// 	//Bottom Cell
		// 	sectors[bl_xSector][bl_ySector].checkCollision(unit);

		// }		
		// //Otherwise we only need to check our Square :)
		// else
		// {
			//Current Cell
			sectors[bl_xSector][bl_ySector].checkCollision(unit);
		// }

	}

	//Each Unit will call the Collision Manager, 


	public void Debug_DrawZones()
	{
		int segmentLength = dimension/divCount;
		for(int x=0;x<divCount;x++)
		{
			for(int y=0;y<divCount;y++)
			{
				Vector3 botLeft  =new  Vector3((x*segmentLength),0,(y*segmentLength));
				Vector3 topRight =new  Vector3(((x+1)*segmentLength),0,((y+1)*segmentLength));
				EventLog.Draw_Square(botLeft,topRight, Color.yellow);
				sectors[x][y].Debug_DrawEntities();
			}
		}		
	}


}
