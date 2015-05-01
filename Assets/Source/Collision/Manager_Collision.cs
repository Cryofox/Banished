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
	int segmentLength;// = dimension/divCount;
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
		segmentLength = dimension/divCount;
	}

	//This is called once during initial placement
	//Returns True if the Object can be placed, and places it into the sector
	//Otherwise it returns False and no additions get amde
	public bool Place_Building(Building building)
	{
		int segmentLength = dimension/divCount;
		// Calculate the unit's sector using it's Center location
		Vector3 point = building.position;

		int xSector = (int)(point.x)/ segmentLength;
		int ySector = (int)(point.z)/ segmentLength;


		//Check if the Object can be placed here First
		//If no collisions occur, the building is placed
		if(!sectors[xSector][ySector].checkCollision(building))
		{
			sectors[xSector][ySector].AddBuilding(building);
			return true;
		}

		return false;
	}


	//This is called by units and their location gets updated each call
	public void Update_Sector(Actor unit)
	{
		int segmentLength = dimension/divCount;
		// Calculate the unit's sector using it's Center location
		Vector3 point = unit.unitPosition;

		int xSector = (int)(point.x)/ segmentLength;
		int ySector = (int)(point.z)/ segmentLength;

		//if(xSector<0 || xSector> dimension)
		//{xSector=0;}
		//if(ySector<0 || ySector> dimension)
		//{ySector=0;}

		if(unit.lastSector.x!= xSector || unit.lastSector.z != ySector)
		{
			//Remove Unit from Last Sector
			sectors[ (int)unit.lastSector.x][(int)unit.lastSector.z].RemoveUnit(unit);
			unit.lastSector= new Vector3(xSector,0,ySector);
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

		// float tL_x 		= unit.unitPosition.x + unit.collisionBox.horizontal_halfOffset;
		// float tL_y		= unit.unitPosition.z + unit.collisionBox.vertical_halfOffset;

		int bl_xSector = (int)(bL_x)/ segmentLength;
		int bl_ySector = (int)(bL_y)/ segmentLength;

		// int tl_xSector = (int)(tL_x)/ segmentLength;
		// int tl_ySector = (int)(tL_y)/ segmentLength;

		//Atm only check current Square, no adjacent squares
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


	//Now That Sector information is maintained, a unit can cross examined for collision
	public Building Collision_GetBuilding(Vector3 point)
	{
		// int segmentLength = dimension/divCount;
		// Calculate the unit's sector using it's Center location
		int xSector = (int)(point.x)/ segmentLength;
		int ySector = (int)(point.z)/ segmentLength;

		//Check if the Object can be placed here First
		//If no collisions occur, the building is placed
		return sectors[xSector][ySector].Collision_GetBuilding(point);
	}

	public Actor Collision_GetUnit(Vector3 point)
	{
		// int segmentLength = dimension/divCount;
		// Calculate the unit's sector using it's Center location
		int xSector = (int)(point.x)/ segmentLength;
		int ySector = (int)(point.z)/ segmentLength;

		//Check if the Object can be placed here First
		//If no collisions occur, the building is placed
		return sectors[xSector][ySector].Collision_GetUnit(point);
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

	public void Remove_Building(Building building)
	{
		int xSector = (int)(building.position.x)/ segmentLength;
		int ySector = (int)(building.position.z)/ segmentLength;		

		sectors[xSector][ySector].RemoveBuilding(building);
	}

	public void Remove_Building(Actor unit)
	{
		int xSector = (int)(unit.unitPosition.x)/ segmentLength;
		int ySector = (int)(unit.unitPosition.z)/ segmentLength;		

		sectors[xSector][ySector].RemoveUnit(unit);
	}












	//This will find tree objects from a position within range
	//It will return the first "Valid" tree available.
	//Valid Means: it's within range 

	//Problem: Will always return the same tree for X actors requesting from
	//same building.
	public Building FindinRange_Tree(Vector3 position, float distance)
	{
		//Step 1 calculate all sectors that Intersect

		//Calculate Origin Position
		int sectorX_origin= (int)(position.x) / segmentLength;
		int sectorY_origin= (int)(position.z) / segmentLength;

		int newSectorX = (int)(position.x-distance)/ segmentLength;
		int newSectorY = (int)(position.x-distance)/ segmentLength;

		//Check Center
		Building tree = sectors[sectorX_origin][sectorY_origin].Find_Static_InRange("Tree",position,distance);
		
		if(tree!=null)
			return tree;
		if(newSectorX < sectorX_origin && newSectorY < sectorY_origin)
		{
			//bottom Left corner is hit aswell meaning minimum 3 checks

			//Check Left
			if(tree!=null)
			
				return tree;
			//Check Bottom
			if(tree!=null)
				return tree;
			//Check Bottom Left
			if(tree!=null)
				return tree;
		}
		else if(newSectorX < sectorX_origin)
		{
			//Only check Left
			if(tree!=null)
				return tree;
		}
		else if(newSectorY < sectorY_origin)
		{
			//Only check Bottom
			if(tree!=null)
				return tree;
		}

		//Perform same check for opposite side
		newSectorX = (int)(position.x+distance)/ segmentLength;
		newSectorY = (int)(position.x+distance)/ segmentLength;

		if(newSectorX > sectorX_origin && newSectorY > sectorY_origin)
		{
			//Top Right corner is hit aswell meaning minimum 3 checks

			//Check Right
			if(tree!=null)
				return tree;
			//Check Top
			if(tree!=null)
				return tree;
			//Check Top Right
			if(tree!=null)
				return tree;
		}
		else if(newSectorX > sectorX_origin)
		{
			//Only check Right
			if(tree!=null)
				return tree;
		}
		else if(newSectorY > sectorY_origin)
		{
			//Only check Top
			if(tree!=null)
				return tree;
		}



		return null;
	}




}
