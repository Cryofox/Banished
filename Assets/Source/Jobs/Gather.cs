using UnityEngine;
using System.Collections;

public class Gather : Job {
	string collectionBuilding_Name;
	public Gather(string cbN ,Building building): base(building)
	{
		collectionBuilding_Name= cbN;
	}

	public override void Work(Actor unit)
	{
		// Collector Logic
		// 1. Check if a Collectible object exists within Assigned building Range
		// 2. If an Object does exist, Seek it untill you are within X range of it.
		// 3. Once in X Range start "Harvesting the Tree"
		// 4. Once inventory is full, drop at nearest storage.
		// 5. Repeat from Step 1.

		//Here we assume this job is collector
		Building targetCollect = man_Collision.FindinRange_Building(workPlace.position, workPlace.range, collectionBuilding_Name);

		//If there are no "Trees", check if our inventory has items in it
		if(targetCollect==null)
		{
			if(unit.inventory.Get_Available_Resource()!="None")
			{
				//First time it's called we assign
				if(storage==null)
					storage=bboard.FindNearestStorage(unit);


				if(storage==null || storage.inventory.isFull())
					unit.Idle();
				else
					unit.Store_Inventory(storage);		
			}	
		}
		//Not Null
		else
		{
			//Is our Actor's inventory full?
			string resourceType = targetCollect.inventory.Get_Available_Resource();

			//Does the actor have room for this resource?
			if(unit.inventory.CheckAvailableRoom(resourceType)==0)
			{
				//First time it's called we assign
				if(storage==null)
					storage=bboard.FindNearestStorage(unit);

				if(storage==null || storage.inventory.isFull())
					unit.Idle();
				else
					unit.Store_Inventory(storage);	
			}
			//Trees exist and we have room in the inventory
			else
			{	
				storage=null;//Reset Storage to re-poll when inventory full
				unit.Harvest_From(targetCollect, range, resourceType);
			}
		}
	}
}
