using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlackBoard {

	public string id_FactionName;


	//List of Buildings


	//List of Units
	List<Actor> units;

	// Use this for initialization
	public BlackBoard (string faction) 
	{
		this.id_FactionName= faction;
		this.units = new List<Actor>();
	}

	//Adds the Character to the BlackBoard and Spawns the Unit in the Game World
	public void AddActor(Actor unit)
	{
		//No Duplicate searching (Slow Down)
		units.Add(unit);
		unit.Spawn_Actor();
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

}
