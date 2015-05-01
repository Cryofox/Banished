using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory 
{

	private int maxSlots;
	private int maxSlotAmnt;
	

	string[] resource_Name;
	int[]	 resource_Amount;

	public Inventory(int slots, int maxPerSlot)
	{
		maxSlots	= slots;
		maxSlotAmnt	= maxPerSlot;

		resource_Amount= new int[slots];
		resource_Name = new string[slots];

		//Initialize Arrays
		for(int i=0;i<slots;i++)
		{
			resource_Amount[i]=0;
			resource_Name[i]="None";	
		}
	}

	//This will Return an amount of resource available for extraction
	//and modify the inventory by removing this amount from the inventory aswell
	public int RequestResourceAmount(string resourceType, int amountRequested)
	{
		int currentAmount=0;
		for(int i=0; i< maxSlots && currentAmount<amountRequested;i++)
		{
			if(resource_Name[i]==resourceType)
			{
				//Calculate the amount remaining
				int remaining= amountRequested-currentAmount;

				if(resource_Amount[i]<=remaining)
				{
					currentAmount+= resource_Amount[i];
					//Not enough or Just enough resources, so we empty
					//this slot in the inventory
					resource_Amount[i]=0;
					resource_Name[i]="None"; 
				}
				else
				{
					currentAmount+= remaining;
					//Since there was an overbundance 
					//all that is needed is to subtract the remaining amount needed
					resource_Amount[i]-= remaining;
				}
			}
		}
		//If the requested amount is attainable we have it, otherwise
		//we get what we get.
		return currentAmount;
	}

	//Returns the amount that could not be inserted into
	//the inventory
	public int InsertResourceAmount(string resourceType, int amountToPush)
	{
		int currentAmount=amountToPush;
		for(int i=0; i< maxSlots && currentAmount>0;i++)
		{
			if(resource_Name[i]==resourceType)
			{
				//Calculate the amount remaining
				int remaining= maxSlotAmnt-resource_Amount[i];
				//Amount we are pushing onto this slot
				resource_Amount[i]+=remaining;
				//Amount remaining to be pushed
				currentAmount-= remaining;
			}
			else if( resource_Name[i]=="None")
			{
				resource_Name[i]=resourceType;

				if(currentAmount<maxSlotAmnt)
				{
					resource_Amount[i]=currentAmount;
					currentAmount=0;
				}
				else
				{
					resource_Amount[i]=maxSlotAmnt;
					currentAmount-=maxSlotAmnt;					
				}
			}
		}
		//If the requested amount is attainable we have it, otherwise
		//we get what we get.

		//Returns the amount that could not be pushed.
		//Useful incase an error occured and resources could not get pushed.
		//Safety measure to prevent resource loss.
		return currentAmount;
	}



	//Returns True/False if a certain resource exists in this inventory
	public bool ContainsResource(string resourceName)
	{
		for(int i=0; i< maxSlots;i++)
		{
			if(resource_Name[i]==resourceName)
				return true;
		}		
		return false;
	}


	//Returns the amount of room available for a resource type
	public int CheckAvailableRoom(string resourceName)
	{
		int roomAvailable=0;
		for(int i=0; i< maxSlots;i++)
		{
			if(resource_Name[i]==resourceName)
			{
				roomAvailable += (maxSlotAmnt-resource_Amount[i]);
			}
			else if(resource_Name[i]=="None")
				roomAvailable+= maxSlotAmnt;
		}		
		return roomAvailable;
	}

	//Returns the amount of items of a type of resource
	public int CheckResourceAmount(string resourceName)
	{
		int occupiedRoom=0;
		for(int i=0; i< maxSlots;i++)
		{
			if(resource_Name[i]==resourceName)
			{
				occupiedRoom += (resource_Amount[i]);
			}
		}		
		return occupiedRoom;
	}


	//Returns the First resource type stored
	//This function can be called multiple times to determine if 
	// a unit/building contains any resources in it that need to be manipulated
	public string Get_Available_Resource()
	{
		for(int i=0; i< maxSlots;i++)
			if(resource_Name[i]!="None")
				return resource_Name[i];

		return "None";
	}

	public bool isFull()
	{
		int occupiedRoom=0;
		for(int i=0; i< maxSlots;i++)
		{
			if(resource_Name[i]=="None")
				return false;

			if(resource_Amount[i]< maxSlotAmnt)
				return false;
			
		}		
		return true;

	}

	public List<string> Get_All_Resources()
	{
		List<string> items= new List<string>();
		for(int i=0; i< maxSlots;i++)
			if(resource_Name[i]!="None")
				if(!items.Contains(resource_Name[i]))
					items.Add(resource_Name[i]);

		return items;	
	}



}
