using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlackBoard {

	public string id_FactionName;
	public Manager_Collision man_Collision;

	Manager_BlackBoard man_BlackBoard;


	//List of Units
	List<Actor> units;
	//List of Buildings
	List<Building> buildings;

	// Use this for initialization
	public BlackBoard (string faction, Manager_BlackBoard man_BB) 
	{
		this.id_FactionName= faction;
		this.units = new List<Actor>();
		this.buildings= new List<Building>();
		this.man_BlackBoard= man_BB;
	}

	//Adds the Character to the BlackBoard and Spawns the Unit in the Game World
	public void AddActor(Actor unit)
	{
		//No Duplicate searching (Slow Down)
		units.Add(unit);
		unit.blackBoard= this; //Give the actor our reference for Sector Calls
		unit.Spawn_Actor();
	}

	//Adds the Character to the BlackBoard and Spawns the Unit in the Game World
	public void AddBuilding(Building building)
	{
		//No Duplicate searching (Slow Down)
		buildings.Add(building);
	}



	//Adds the Character to the BlackBoard and Spawns the Unit in the Game World
	public void RemoveActor(Actor unit)
	{
		//To remove the actor we need to Kill it.

	}

	//Adds the Character to the BlackBoard and Spawns the Unit in the Game World
	public void RemoveBuilding(Building building)
	{
		//To remove the building we need to Kill it.
		for(int i=0;i<buildings.Count;i++)
		{
			if(buildings[i]==building)
			{
				buildings.RemoveAt(i);
				return;
			}
		}		
	}


	//Add Building

	//Update all Actors ruled by this Board
	public void UpdateActors(float timeElapsed)
	{
		for(int i=0;i< units.Count;i++)
		{
			units[i].Update_Actor(timeElapsed);
		}

	}

	//Callbacks from Actor to BlackBoard
	public void UpdateSector(Actor unit)
	{
		//Call update sector
		man_Collision.Update_Sector(unit);
	}

	//Callbacks from Actor to BlackBoard
	public void CheckCollision(Actor unit)
	{
		//Call update sector
		man_Collision.Collision_Check(unit);
	}



	public void AssignUnit(Building building)
	{
		//Find a unit without a job.
		//Attemp to assign it to the building
		for(int i=0;i<units.Count;i++)
		{
			if(!units[i].ContainsJob())
			{
				building.AssignUnit(units[i]);	
				return;
			}
		}
	}

	public void DeAssignUnit(Building building)
	{
		building.DeAssignUnit();	
	}


//Internal Calls from Managed Objects

	//This function is called from the Destroyed Building
	public void DestroyBuilding(Building self)
	{
		//Since called internally, assume all recordchecking is done


		//Remove the building from Sector
		man_Collision.Remove_Building(self); //Remove from Sector
		RemoveBuilding(self); // Remove from this Board
	}



	public Building FindNearestStorage(Actor unit)
	{
		//Cycle through all Buildings owned by faction
		Building storage=null;
		float distance=0;
		string firstResource = unit.inventory.Get_Available_Resource();
		for(int i=0;i<buildings.Count;i++)
		{
			if(buildings[i].name=="Storage")
			{
				if(storage==null)
				{	
					//If it has room for (A) resource then do eet.
					if(buildings[i].inventory.CheckAvailableRoom(firstResource)>0)
					{
						storage=buildings[i];
						distance= Vector3.Distance(unit.unitPosition, buildings[i].position);
					}
				}
				else 
				{
					float temp = Vector3.Distance(unit.unitPosition, buildings[i].position);
					if(temp<distance)
					{
						if(buildings[i].inventory.CheckAvailableRoom(firstResource)>0)
						{
							storage=buildings[i];
							distance= temp;
						}
					}
				}
			}
		}

		//Future (Check Sectors, and slowly branch outward from unit's location)
		return storage;
	}


}
