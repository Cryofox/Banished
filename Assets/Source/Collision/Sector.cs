using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sector
{

	List<Actor> dynamicEntities;
	public Sector()
	{
		dynamicEntities= new List<Actor>();
	}

	public void AddUnit(Actor unit)
	{
		for(int i=0;i<dynamicEntities.Count;i++)
		{
			if(dynamicEntities[i]==unit)
			{
				return;
			}
		}
		dynamicEntities.Add(unit);
	}

	public void RemoveUnit(Actor unit)
	{
		dynamicEntities.Add(unit);
		for(int i=0;i<dynamicEntities.Count;i++)
		{
			if(dynamicEntities[i]==unit)
			{
				dynamicEntities.RemoveAt(i);
				return;
			}
		}
	}	

	public void Debug_DrawEntities()
	{
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
/*
r1.maxX > r2.minX &&
r1.minX < r2.maxX &&
r1.maxY > r2.minY &&
r1.minY < r2.maxY
*/		
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
		}

		return false;
	}
}
