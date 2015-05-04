using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//core class, vampires, ghosts, people, elves etc all Inherit from Actor
public class Actor : Steering
{
	//To Speed up Sector Updates
	//Store the Last Sector we've been in
	public Vector3 lastSector;

	public BlackBoard blackBoard;

	public Job job;

	//They have a Ring
	protected GameObject ring;

	public Vector3 unitPosition
	{
		get
		{
			//New Reference created each call
			Vector3 vector = new Vector3(position.x, 0,position.z);
			return vector;
		}
	}

	/*Protected Variables
	-----------------------------
	protected Vector3 position;
	protected float maxSpeed=10;
	protected float maxForce=0.5f;
	==============================
	*/

	GameObject model;

	public Inventory inventory;

	//Persistant Information
	public BoundingBox collisionBox;


	public Actor(Vector3 startPosition)
	{
		maxSpeed=5f; //2.5f default
		// 1x1
		this.collisionBox = new BoundingBox(1,1);		
		this.position=startPosition;
		this.lastSector= new Vector3(0,0); //This gets auto Updated anyways :)

		inventory = new Inventory(5,5); //25 Carry
	}


	public void Spawn_Actor()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("Generic_Actor");

		//Spawn our model
		model=GameObject.Instantiate(model,position, Quaternion.Euler(facing)) as GameObject;


		ring =	Resources.Load<GameObject>("GameObject/Circle");
		ring =	GameObject.Instantiate(ring,position, Quaternion.identity) as GameObject;
		ring.GetComponent<Renderer>().sharedMaterial= Resources.Load<Material>("Materials/Ring");
		ring.transform.localScale = new Vector3( 2,1, 2);
		ring.transform.parent = model.transform;

		ring.SetActive(false);
	}
	//Update Sector ever 0.5 seconds
	float updateSector=0.5f;
	float timePassed;
	public void Update_Actor(float timeElapsed)
	{
		timePassed+=timeElapsed;
		//For now we Assume the Actor is always Wandering/Idle
		Update_Steering(timeElapsed);



		//Ai Brain Logic....
			//Behaviour to do, just wander
			// if(!Avoid_Bounds())
			// Wander();
			//In Order to steer away from each other Set an avoidance radius= 2x bounding box, velocity = Max while inside it

		//If we don't have a job. Just Wander around;
		if(job==null)
			Idle();
		else 
			job.Work(this);


		Avoid_Bounds();
		/////////////////////


		//Update Sector?
		if(timePassed>updateSector)
		{
			timePassed=0;
			blackBoard.UpdateSector(this);
		}
		blackBoard.CheckCollision(this);

		//Apply new Position location + facing direction
		model.transform.position=position;

		if(facing!=Vector3.zero)
			model.transform.rotation= Quaternion.LookRotation(facing);
	}

	public bool ContainsJob()
	{
		if(job==null)
			return false;

		return true;
	}

	public void AssignJob(Job job)
	{	
		this.job=job; 
		job.AddEssentials(blackBoard, blackBoard.man_Collision);
	}





//Simplified Order Requests
	float harvestRate;
	public void Harvest_From(Building target, float harvestRange, string resourceType)
	{
		myState= Actor_State.Harvesting;
		// Collector Logic
		// **1. Check if a Collectible object exists within Assigned building Range
			//**This is done by the Job

		// **2. If an Object does exist, Seek it untill you are within X range of it.
			//**We now order the Unit to Harvest
		// 3. Once in X Range start "Harvesting the Tree"
		// 4. Once inventory is full, drop at nearest storage.
		// 5. Repeat from Step 1.


		//2 Seek it
		//Check distance
		//Target Position is harvest Range from
		Vector3 goal = target.position + new Vector3(harvestRange/5,0,0);

		Arrive(goal);


		if(Vector3.Distance(position, target.position) < harvestRange)
		{	
			//Wait untill Harvest Time is reached?
			//Harvest Max available
			int minAmount = Mathf.Min( inventory.CheckAvailableRoom(resourceType),5);
			int amountPulled = target.inventory.RequestResourceAmount(resourceType,minAmount);
			inventory.InsertResourceAmount(resourceType,amountPulled);

			//After each pull check if it's inventory is empty. If it is destroy the Tree.
			//This same logic will be used on herbs and rocks
			if(target.inventory.Get_Available_Resource()=="None")
				target.Destroy();
		}
	}

	//Go to Building
	public void Convert_At(Building target, List<Resource_Amount> inputResources, List<Resource_Amount> outputResources)
	{
		myState= Actor_State.Converting;
		// Collector Logic
		// **1. Check if a Collectible object exists within Assigned building Range
			//**This is done by the Job

		// **2. If an Object does exist, Seek it untill you are within X range of it.
			//**We now order the Unit to Harvest
		// 3. Once in X Range start "Harvesting the Tree"
		// 4. Once inventory is full, drop at nearest storage.
		// 5. Repeat from Step 1.


		//2 Seek it
		//Check distance
		//Target Position is harvest Range from


		Arrive(target.position);

		//If we're at target
		if(Vector3.Distance(position, target.position) < 1)
		{	

			//Remove the amount of input resources needed for one item
			for(int i=0;i<inputResources.Count;i++)
			{
				inventory.RequestResourceAmount(inputResources[i].resourceName,inputResources[i].amount);
			}

			for(int i=0;i<outputResources.Count;i++)
			{
				inventory.InsertResourceAmount(outputResources[i].resourceName,outputResources[i].amount);
			}

		}
	}	

	public void Store_Inventory(Building target)
	{
		myState= Actor_State.Storing;
		//Store Logic
		//Find Nearest Non-Full Storage

		Arrive(target.position);


		if(Vector3.Distance(position, target.position) < 1)
		{	
			//Empty as much shit as you can in the storage
			List<string> resources= inventory.Get_All_Resources();

			for(int i=0;i<resources.Count;i++)
			{
				int largestAmount = Mathf.Min( inventory.CheckResourceAmount(resources[i]),target.inventory.CheckAvailableRoom(resources[i]));
				
				inventory.RequestResourceAmount(resources[i],largestAmount);
				target.inventory.InsertResourceAmount(resources[i],largestAmount);
			}
		}
	}

	//A storage is passed here, simply pull the resource needed
	//Here we only pull the amount needed for one item
	public void Grab_Inventory(Building target, string resourceName, int amount)
	{
		myState= Actor_State.Retrieving;
		//Store Logic
		//Find Nearest Non-Full Storage
		Arrive(target.position);

		if(Vector3.Distance(position, target.position) < 1)
		{	
			EventLog.Log_Message("TargetName:"+target.name);
			int largestAmount = Mathf.Min( amount,target.inventory.CheckResourceAmount(resourceName));
			int amountPulled = target.inventory.RequestResourceAmount(resourceName,largestAmount);
			inventory.InsertResourceAmount(resourceName,amountPulled);
		}	
	}

	public void Idle()
	{
		myState= Actor_State.Idle;
		Wander();
	}


		//Player States
	enum Actor_State{Idle, Harvesting, Storing, Converting, Retrieving, Panicked, Alerted};
	Actor_State myState= Actor_State.Idle;


	public string Get_State()
	{
		return myState.ToString();
	}

	public string Get_Job()
	{
		if(job==null)
			return "None";
		else	
			return job.jobName;
	}


	public void Highlight()
	{
		ring.SetActive(true);
	}
	public void Darken()
	{
		ring.SetActive(false);
	}

}
