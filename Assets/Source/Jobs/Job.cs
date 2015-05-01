using UnityEngine;
using System.Collections;

public class Job  {

	Building workPlace;
	BlackBoard bboard;
	Manager_Collision man_Collision;
	public Job(Building building)
	{
		workPlace = building;
	}
	//BlackBoard and collision detection in case references need to be made from this class
	public void AddEssentials(BlackBoard blackboard, Manager_Collision man_Col)
	{
		bboard = blackboard;
		man_Collision = man_Col;
	}

	//Our behaviour is Simple when Working
	//Working Range= 5;

	float range = 4;
	public virtual void Work(Actor unit)
	{
		// Collector Logic
		// 1. Check if a Collectible object exists within Assigned building Range
		// 2. If an Object does exist, Seek it untill you are within X range of it.
		// 3. Once in X Range start "Harvesting the Tree"
		// 4. Once inventory is full, drop at nearest storage.
		// 5. Repeat from Step 1.

		//Here we assume this job is collector
		Building targetCollect = man_Collision.FindinRange_Tree(workPlace.position, workPlace.range);

		//If there are no "Trees", check if our inventory has items in it
		if(targetCollect==null)
		{
			if(unit.inventory.Get_Available_Resource()!="None")
				unit.Store_Inventory();			
		}
		//Not Null
		else
		{
			//Is our Actor's inventory full?
			string resourceType = targetCollect.inventory.Get_Available_Resource();

			//Does the actor have room for this resource?
			if(unit.inventory.CheckAvailableRoom(resourceType)==0)
			{
				//The Job is to Store the Item
				unit.Store_Inventory();
			}

			//Trees exist and we have room in the inventory
			unit.Harvest_From(targetCollect, range, resourceType);
		}
	}


}
