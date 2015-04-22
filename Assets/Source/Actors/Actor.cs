using UnityEngine;
using System.Collections;

//core class, vampires, ghosts, people, elves etc all Inherit from Actor
public class Actor : Steering
{
	/*Protected Variables
	-----------------------------
	protected Vector3 position;
	protected float maxSpeed=10;
	protected float maxForce=0.5f;
	==============================
	*/
	GameObject model;



	//Persistant Information
	BoundingBox collisionBox;

	public Actor(Vector3 startPosition)
	{

		// 1x1
		this.collisionBox = new BoundingBox(1,1);		
		this.position=startPosition;
	}








	public void Spawn_Actor()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("Generic_Actor");

		//Spawn our model
		model=GameObject.Instantiate(model,position, Quaternion.Euler(facing)) as GameObject;
	}

	public void Update_Actor(float timeElapsed)
	{
		//For now we Assume the Actor is always Wandering/Idle
		Update_Steering(timeElapsed);
		Wander();

		model.transform.position=position;
		model.transform.rotation= Quaternion.Euler(facing);
	}




}
