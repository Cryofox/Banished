using UnityEngine;
using System.Collections;

//core class, vampires, ghosts, people, elves etc all Inherit from Actor
public class Actor : Steering
{
	//To Speed up Sector Updates
	//Store the Last Sector we've been in
	public Vector3 lastSector;

	public BlackBoard blackBoard;



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



	//Persistant Information
	public BoundingBox collisionBox;


	public Actor(Vector3 startPosition)
	{

		// 1x1
		this.collisionBox = new BoundingBox(1,1);		
		this.position=startPosition;
		this.lastSector= new Vector3(0,0); //This gets auto Updated anyways :)
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

		//Behaviour to do, just wander
		// if(!Avoid_Bounds())
		Wander();
		Avoid_Bounds();
		//In Order to steer away from each other Set an avoidance radius= 2x bounding box, velocity = Max while inside it



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




}
