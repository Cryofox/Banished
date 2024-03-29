﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class is used to Update Logic of All Units within the game
public class Manager_BlackBoard
{
	Manager_Collision man_Collision;
	//Makes more Sense using a HashTable
	
	//ADT for Managing Boards

	//Used for O(1) Storage/Retrieval
	Hashtable hasht_Boards;
	//Used for Iteration when needed
	List<BlackBoard> list_Boards;
	


	public Manager_BlackBoard(Manager_Collision manCol)
	{
		this.man_Collision= manCol;

		hasht_Boards = new Hashtable();
		list_Boards = new List<BlackBoard>();
	}

	//This is how Actors get Added to the Game
	public void AddActor(Actor unit, string factionName)
	{
		//Here we add the Actor to the specified Faction
		Add_Faction(factionName); //Ensure faction exists

		((BlackBoard)hasht_Boards[factionName]).AddActor(unit); //Add Actor to Board
		//Insert our new Unit inside the Collision Field
		//man_Collision.Initial_Addition(unit);
	}

	//This is how Buildings get Added to the Game
	
	public void AddBuilding(Building building, string factionName)
	{
		//Here we add the Actor to the specified Faction
		Add_Faction(factionName); //Ensure faction exists

		((BlackBoard)hasht_Boards[factionName]).AddBuilding(building); //Add Actor to Board

		//Used to be able to skip some checks
		building.faction= ((BlackBoard)hasht_Boards[factionName]); 
		
	}


	//Create the BlackBoard to manage this Factions Content
	public void Add_Faction(string factionName)
	{
		//The Faction is already stored, we can Exit this function
		if(hasht_Boards.ContainsKey(factionName))
			return;		

		BlackBoard bboard = new BlackBoard(factionName,this);
		bboard.man_Collision = man_Collision;
		hasht_Boards.Add(factionName, bboard);
		list_Boards.Add(bboard);

		EventLog.Log_Message("Faction:"+factionName+" has been added to Database");
	}



	public void Remove_Faction(string factionName)
	{
		//The Faction is already stored, we can Exit this function
		if(! hasht_Boards.ContainsKey(factionName))
			return;			

		BlackBoard bboard = (BlackBoard)hasht_Boards[factionName];
		hasht_Boards[factionName]=null;

		for(int i=0;i<list_Boards.Count;i++)
		{
			//C# doesn't require .equals for Strings like java does.
			if(list_Boards[i].id_FactionName == factionName) 
			{
				list_Boards[i]=null;

				//Call a dispose method?				
				return; //Break out of routine
			}
		}
	}

	//Iterate through black boards
	public void Update_Boards(float timeElapsed)
	{
		for(int i=0;i<list_Boards.Count;i++)
		{
			list_Boards[i].UpdateActors(timeElapsed);
		}
	}


	public void AssignUnit(string factionName, Building building)
	{
		((BlackBoard)hasht_Boards[factionName]).AssignUnit(building);
	}

	public void DeAssignUnit(string factionName, Building building)
	{
		((BlackBoard)hasht_Boards[factionName]).DeAssignUnit(building);
	}

}
