using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Converter : Job 
{
	List<Resource_Amount> resources_Input;
	List<Resource_Amount> resources_Output;
	public Converter(List<Resource_Amount> inputResources, List<Resource_Amount> outputResources, Building building): base(building)
	{
		resources_Input = inputResources;
		resources_Output= outputResources;
	}

	Building input_Storage;


	//This job requires the most back and forth (Polling storage for items, going to conversion shop, Converting Resource, Storing Converted Resource. Repeat)

	public override void Work(Actor unit)
	{
		// Collector Logic
		// 1. Check if a Collectible object exists within Assigned building Range
		// 2. If an Object does exist, Seek it untill you are within X range of it.
		// 3. Once in X Range start "Harvesting the Tree"
		// 4. Once inventory is full, drop at nearest storage.
		// 5. Repeat from Step 1.

		//Here we assume this job is collector


		//Check if inventory is full ( empty if so)
		//If multiple items are used they are all stored when one conversion is made. So checking for the existance
		//of 1 is on par with all.
		if(unit.inventory.isFull() || unit.inventory.ContainsResource(resources_Output[0].resourceName))
		{
			//First time it's called we assign
			if(storage==null)
				storage=bboard.FindNearestStorage(unit);

			if(storage==null || storage.inventory.isFull())
				unit.Idle();
			else
				unit.Store_Inventory(storage);		
		}	

		//Not Null
		else
		{
			//Need to check if we have the required amount of input resources to create 
			//the output
			for(int i=0;i<resources_Input.Count;i++)
			{

				//During this stage we attempt to acquire a storage location that has the input resource(s) we need
				//If the unit has less than the required amount of a certain resource type. We look for a storage
				if(unit.inventory.CheckResourceAmount(resources_Input[i].resourceName)<resources_Input[i].amount)
				{
					//This is a resource we lack so we must Find the nearest storage that has this item in stock (any amount)
					if(input_Storage==null)
					{
						input_Storage=bboard.FindNearestStorage_With(unit,resources_Input[i].resourceName);

						//Grab Inventory/Idle
						if(input_Storage!=null)
							unit.Grab_Inventory(input_Storage,resources_Input[i].resourceName, resources_Input[i].amount);

						else
							unit.Idle();

						return;	
					}
					//We have an input location, check if it still contains the resource we want
					else
					{
						//If an input storage exists, check if it still has the item we want.
						if(input_Storage.inventory.ContainsResource(resources_Input[i].resourceName)==false)
						{
							input_Storage=bboard.FindNearestStorage_With(unit,resources_Input[i].resourceName);

							//Grab Inventory/Idle
							if(input_Storage!=null)
								unit.Grab_Inventory(input_Storage,resources_Input[i].resourceName, resources_Input[i].amount);
							
							else
								unit.Idle();

						}
						//If it still has the resource then get it
						else
							unit.Grab_Inventory(input_Storage,resources_Input[i].resourceName, resources_Input[i].amount);

						return;
					}
				}
			}
			//If this branch of code is reached its safe to assume that the unit has the necessary input resources
			unit.Convert_At(workPlace,resources_Input, resources_Output);
		}
	}
}
