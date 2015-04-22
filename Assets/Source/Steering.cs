using UnityEngine;
using System.Collections;

public class Steering {

	//Persistant Values
	Vector3 position;
	Vector3 velocity;
	Vector3 acceleration;


	//Vector3 targetPosition;
	float maxSpeed=10;
	float maxForce=0.5f;
	////////////////////
	//The direction we are facing, a normalized Vector
	Vector3 facing;

	//Our Current Rotation to append new Rotations onto
	float rotation;

	public Steering()
	{
		//Here we Set our targetPosition to -1,-1 (An Illegal Move)
		//targetPosition= new Vector3(-1,-1);
		acceleration= Vector3.zero;
		velocity= Vector3.zero;
		//Position will be set by the Actor Class
		//Target Position gets overridden by Actor Class
	}



	// Update_Logic is called once per frame
	//This calculates and corrects the movement based on proximity
	void Update_Logic () 
	{
		//Every Frame
		velocity+=acceleration;

		if( velocity.magnitude!=0)
		{
			facing = Vector3.Normalize(velocity);
		}



		//Limit Velocity to max speed
		if( velocity.magnitude > maxSpeed)
		{
			//Normalize the Vector
			velocity =   Vector3.Normalize(velocity);

			//Multiply by Max Value
			velocity *= maxSpeed;
		}


		//Add the velocity to the Position
		position+= (velocity * Time.deltaTime);

		//Zero out the 3rd dimension, only for Demo purposes
		position= new Vector3(transform.position.x,0, transform.position.z);


		//Reset Acceleration
		acceleration*=0;

		//What do we want to do?
		

		// Seek(); 
		// Avoid();
		// Avoid_T2();
		//Seek Arrival
		// Arrive();
		// Wander();
		//FollowPath();
	}

	void ApplyForce(Vector3 force)
	{
		acceleration += force;
	}

	void Seek(Vector3 seekLocation)
	{
		Vector3 desiredVector= (Vector3.Normalize( seekLocation -  position ))* maxSpeed;
		Vector3 steer = ( desiredVector - velocity);

		//Limit to Max Force
		if( steer.magnitude > maxForce)
		{
			//Normalize the Vector
			steer =   Vector3.Normalize(steer);

			//Multiply by Max Value
			steer *= maxForce;
		}
		ApplyForce(steer);
	}


	void Avoid(Vector3 avoidLocation)
	{
		Vector3 desiredVector= (Vector3.Normalize( position - avoidLocation ))* maxSpeed;
		Vector3 steer = ( desiredVector - velocity);

		//Limit to Max Force
		if( steer.magnitude > maxForce)
		{
			//Normalize the Vector
			steer =   Vector3.Normalize(steer);

			//Multiply by Max Value
			steer *= maxForce;
		}
		ApplyForce(steer);
	}

	void Arrive(Vector3 seekLocation) 
	{
		Vector3 desiredVector= ( seekLocation- position);

		float radius=10;
		// Debug.Log("DesMag:"+desiredVector.magnitude);
		float distance =  desiredVector.magnitude; 
		desiredVector =   Vector3.Normalize(desiredVector);

		//Limit Speed
		if( distance < radius)
		{
			//Multiply by Max Value
			desiredVector *=   (maxSpeed* (distance/radius));
			Debug.Log("Applying Brakes!" + desiredVector);
		}	
		else
			desiredVector*=maxSpeed;

		Vector3 steer = ( desiredVector - velocity);
		//Limit to Max Force
		if( steer.magnitude > maxForce)
		{
			//Normalize the Vector
			steer =   Vector3.Normalize(steer);

			//Multiply by Max Value
			steer *= maxForce;
		}

		ApplyForce(steer);
	}
}
