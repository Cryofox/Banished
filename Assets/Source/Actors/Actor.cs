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




	//Persistant Information
	BoundingBox collisionBox;

	public Actor()
	{
		// 1x1
		collisionBox = new BoundingBox(1,1);

	}
	public void Spawn_Actor()
	{}

	public void Update_Actor(float timeElapsed)
	{
		
		//For now we Assume the Actor is always Wandering/Idle
		Update_Steering(timeElapsed);
		Wander();


	}




}
