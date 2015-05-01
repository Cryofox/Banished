using UnityEngine;
using System.Collections;

//core class, vampires, ghosts, people, elves etc all Inherit from Actor
public class Actor : Steering
{
	//To Speed up Sector Updates
	//Store the Last Sector we've been in
	public Vector3 lastSector;

	public BlackBoard blackBoard;

	public Job job;

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
		maxSpeed=2.5f;
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

		Arrive(target.position);
		if(Vector3.Distance(position, target.position) < harvestRange)
		{	
			//Wait untill Harvest Time is reached?
			//Harvest Max available
			int minAmount = Mathf.Min( inventory.CheckAvailableRoom(resourceType),5);
			target.inventory.RequestResourceAmount(resourceType,minAmount);
		
			//After each pull check if it's inventory is empty. If it is destroy the Tree.
			//This same logic will be used on herbs and rocks
			if(target.inventory.Get_Available_Resource()=="None")
				target.Destroy();
		}
		
	}

	public void Store_Inventory()
	{
		myState= Actor_State.Storing;
		//Store Logic
		//Find Nearest Non-Full Storage


	}

	public void Idle()
	{
		myState= Actor_State.Idle;
		Wander();

	}


		//Player States
	enum Actor_State{Idle, Harvesting, Storing, Panicked, Alerted};
	Actor_State myState= Actor_State.Idle;
}
