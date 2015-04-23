using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sector
{

	List<Actor> dynamicEntities;
	List<Building> staticEntities;
	public Sector()
	{
		dynamicEntities= new List<Actor>();
		staticEntities = new List<Building>();
	}

	public void AddUnit(Actor unit)
	{
		//Optimized, no duplicates are entered :)
		// for(int i=0;i<dynamicEntities.Count;i++)
		// {
		// 	if(dynamicEntities[i]==unit)
		// 	{
		// 		EventLog.Log_Message("Duplicate Found");
		// 		return;
		// 	}
		// }
		dynamicEntities.Add(unit);
	}


	public void RemoveUnit(Actor unit)
	{
		for(int i=0;i<dynamicEntities.Count;i++)
		{
			if(dynamicEntities[i]==unit)
			{
				dynamicEntities.RemoveAt(i);
				return;
			}
		}
	}	

	public void AddBuilding(Building building)
	{
		staticEntities.Add(building);
	}
	public void RemoveBuilding(Building building)
	{
		for(int i=0;i<staticEntities.Count;i++)
		{
			if(staticEntities[i]==building)
			{
				staticEntities.RemoveAt(i);
				return;
			}
		}
	}	

	public void Debug_DrawEntities()
	{
		for(int i=0;i<staticEntities.Count;i++)
		{
			Vector3 botLeft =  new Vector3(staticEntities[i].position.x- staticEntities[i].collisionBox.horizontal_halfOffset,0,staticEntities[i].position.z- staticEntities[i].collisionBox.vertical_halfOffset);
			Vector3 topRight = new Vector3(staticEntities[i].position.x+ staticEntities[i].collisionBox.horizontal_halfOffset,0,staticEntities[i].position.z+ staticEntities[i].collisionBox.vertical_halfOffset);
			EventLog.Draw_Square(botLeft,topRight, Color.blue);
		}	
		return;
		//EventLog.Log_Message("Sector Count:"+dynamicEntities.Count);
		for(int i=0;i<dynamicEntities.Count;i++)
		{
			Vector3 botLeft =  new Vector3(dynamicEntities[i].unitPosition.x- dynamicEntities[i].collisionBox.horizontal_halfOffset,0,dynamicEntities[i].unitPosition.z- dynamicEntities[i].collisionBox.vertical_halfOffset);
			Vector3 topRight = new Vector3(dynamicEntities[i].unitPosition.x+ dynamicEntities[i].collisionBox.horizontal_halfOffset,0,dynamicEntities[i].unitPosition.z+ dynamicEntities[i].collisionBox.vertical_halfOffset);
			EventLog.Draw_Square(botLeft,topRight, Color.blue);
		}
	}


	public bool checkCollision(Actor unit)
	{
		//Since these parameters are created in Manager_Collision, they could prob be passed in
		//Calculate the minmax of our unit
		float cur_minX= unit.unitPosition.x - unit.collisionBox.horizontal_halfOffset;
		float cur_maxX= unit.unitPosition.x + unit.collisionBox.horizontal_halfOffset;
		float cur_minY= unit.unitPosition.z - unit.collisionBox.vertical_halfOffset;
		float cur_maxY= unit.unitPosition.z + unit.collisionBox.vertical_halfOffset;



		for(int i=0;i<dynamicEntities.Count;i++)
		{
			if(dynamicEntities[i]!=unit)
			{
				//Repeat Calculations per unit
				float tar_minX= dynamicEntities[i].unitPosition.x - dynamicEntities[i].collisionBox.horizontal_halfOffset;
				float tar_maxX= dynamicEntities[i].unitPosition.x + dynamicEntities[i].collisionBox.horizontal_halfOffset;
				float tar_minY= dynamicEntities[i].unitPosition.z - dynamicEntities[i].collisionBox.vertical_halfOffset;
				float tar_maxY= dynamicEntities[i].unitPosition.z + dynamicEntities[i].collisionBox.vertical_halfOffset;		


				if(	cur_maxX > tar_minX && 
					cur_minX < tar_maxX &&
					cur_maxY > tar_minY &&
					cur_minY < tar_maxY	)
				{
					Vector3 botLeft =  new Vector3(cur_minX,0,cur_minY);
					Vector3 topRight=  new Vector3(cur_maxX,0,cur_maxY);
					EventLog.Draw_Square(botLeft,topRight, Color.red);
					//Here we know what unit has collided with our Actor so we can stop here 
					//For Projectiles, we may/maynot continue
					//EventLog.Log_Message("Collision!");
					return true;
				}

			}
		}

		for(int i=0;i<staticEntities.Count;i++)
		{
			//Repeat Calculations per unit
			float tar_minX= staticEntities[i].position.x - staticEntities[i].collisionBox.horizontal_halfOffset;
			float tar_maxX= staticEntities[i].position.x + staticEntities[i].collisionBox.horizontal_halfOffset;
			float tar_minY= staticEntities[i].position.z - staticEntities[i].collisionBox.vertical_halfOffset;
			float tar_maxY= staticEntities[i].position.z + staticEntities[i].collisionBox.vertical_halfOffset;		


			if(cur_maxX > tar_minX && 
			   cur_minX < tar_maxX &&
			   cur_maxY > tar_minY &&
			   cur_minY < tar_maxY)
			{
				Vector3 botLeft =  new Vector3(cur_minX,0,cur_minY);
				Vector3 topRight=  new Vector3(cur_maxX,0,cur_maxY);
				EventLog.Draw_Square(botLeft,topRight, Color.red);
				//Here we know what unit has collided with our Actor so we can stop here 
				//For Projectiles, we may/maynot continue
				//EventLog.Log_Message("Collision!");
				return true;
			}
		}
		return false;
	}
	public bool checkCollision(Building building)
	{
		//Since these parameters are created in Manager_Collision, they could prob be passed in
		//Calculate the minmax of our unit
		float cur_minX= building.position.x -  building.collisionBox.horizontal_halfOffset;
		float cur_maxX= building.position.x +  building.collisionBox.horizontal_halfOffset;
		float cur_minY= building.position.z -  building.collisionBox.vertical_halfOffset;
		float cur_maxY= building.position.z +  building.collisionBox.vertical_halfOffset;



		for(int i=0;i<dynamicEntities.Count;i++)
		{
				//Repeat Calculations per unit
				float tar_minX= dynamicEntities[i].unitPosition.x - dynamicEntities[i].collisionBox.horizontal_halfOffset;
				float tar_maxX= dynamicEntities[i].unitPosition.x + dynamicEntities[i].collisionBox.horizontal_halfOffset;
				float tar_minY= dynamicEntities[i].unitPosition.z - dynamicEntities[i].collisionBox.vertical_halfOffset;
				float tar_maxY= dynamicEntities[i].unitPosition.z + dynamicEntities[i].collisionBox.vertical_halfOffset;		


				if(cur_maxX > tar_minX && 
				   cur_minX < tar_maxX &&
				   cur_maxY > tar_minY &&
				   cur_minY < tar_maxY)
				{
					Vector3 botLeft =  new Vector3(cur_minX,0,cur_minY);
					Vector3 topRight=  new Vector3(cur_maxX,0,cur_maxY);
					EventLog.Draw_Square(botLeft,topRight, Color.red);
					//Here we know what unit has collided with our Actor so we can stop here 
					//For Projectiles, we may/maynot continue
					//EventLog.Log_Message("Collision!");
					return true;
				}
		}
		//No need to check for duplicates because the object is JUST being placed
		for(int i=0;i<staticEntities.Count;i++)
		{

			//Repeat Calculations per unit
			float tar_minX= staticEntities[i].position.x - staticEntities[i].collisionBox.horizontal_halfOffset;
			float tar_maxX= staticEntities[i].position.x + staticEntities[i].collisionBox.horizontal_halfOffset;
			float tar_minY= staticEntities[i].position.z - staticEntities[i].collisionBox.vertical_halfOffset;
			float tar_maxY= staticEntities[i].position.z + staticEntities[i].collisionBox.vertical_halfOffset;		


			if(cur_maxX > tar_minX && 
			   cur_minX < tar_maxX &&
			   cur_maxY > tar_minY &&
			   cur_minY < tar_maxY)
			{
				Vector3 botLeft =  new Vector3(cur_minX,0,cur_minY);
				Vector3 topRight=  new Vector3(cur_maxX,0,cur_maxY);
				EventLog.Draw_Square(botLeft,topRight, Color.red);
				//Here we know what unit has collided with our Actor so we can stop here 
				//For Projectiles, we may/maynot continue
				//EventLog.Log_Message("Collision!");
				return true;
			}
		}
		return false;
	}

}
