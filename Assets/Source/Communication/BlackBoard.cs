using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlackBoard {

	public string id_FactionName;
	public Manager_Collision man_Collision;




	//List of Units
	List<Actor> units;
	//List of Buildings
	List<Building> buildings;

	// Use this for initialization
	public BlackBoard (string faction) 
	{
		this.id_FactionName= faction;
		this.units = new List<Actor>();
		this.buildings= new List<Building>();
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




}
