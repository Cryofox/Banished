using UnityEngine;
using System.Collections;

public class Steering {

	//Persistant Values

	//These are the values we want the Actor to have access to
	//as it might become useful
	protected Vector3 position;
	protected float maxSpeed=10;
	protected float maxForce=0.5f;

		//The direction we are facing, a normalized Vector
	protected Vector3 facing;

	//Persistent Values only needed for Steering Dynamics
	Vector3 velocity;
	Vector3 acceleration;
	//Our Current Rotation to append new Rotations onto
	float rotation;
	////////////////////
	private float halfSpeed;
	public Steering()
	{
		halfSpeed=maxSpeed/2;
		//Here we Set our targetPosition to -1,-1 (An Illegal Move)
		//targetPosition= new Vector3(-1,-1);
		acceleration= Vector3.zero;
		velocity= Vector3.zero;
		//Position will be set by the Actor Class
		//Target Position gets overridden by Actor Class
		facing = Vector3.zero;
	}


	float nextWander;
	Vector3 lastWander;
	// Update_Logic is called once per frame
	//This calculates and corrects the movement based on proximity
	protected void Update_Steering (float timeElapsed) 
	{
		nextWander+=timeElapsed;
		//Every Frame
		velocity+=acceleration;

		if( velocity.magnitude!=0)
		{
			facing = Vector3.Normalize(velocity);
			UnityEngine.Debug.DrawLine(position, (position+velocity), Color.green);
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
		position+= (velocity *  timeElapsed);

		//Zero out the 3rd dimension, only for Demo purposes
		position= new Vector3(position.x,0, position.z);


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

	protected void Seek(Vector3 seekLocation)
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
		UnityEngine.Debug.DrawLine(position, (position+steer), Color.blue);
	}

	//Perhaps provide Target to avoid?
	protected void Avoid(Vector3 avoidLocation)
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



	protected void Arrive(Vector3 seekLocation) 
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

	protected void Wander() 
	{
		if(nextWander>0.05f)
		{
			//Choose Distance from Face
			float distance_Center = 11;
			float wanderRadius = 5;

			float randomNum = Random.Range(-1.0f,1.0f);
			randomNum*=20;
			rotation+=randomNum;
			//Calculate Position



			float xVal = Mathf.Sin(  (Mathf.PI/180 * rotation) );
			float yVal = Mathf.Cos(  (Mathf.PI/180 * rotation) );

			// xVal *= 180/Mathf.PI;
			xVal*=wanderRadius;
			yVal*=wanderRadius;


			//Need to Reposition infront of Unit
			//Find Direction we are facing
			Vector3 circleCenter = facing;
			circleCenter *= distance_Center;
			circleCenter+= position;

			lastWander = new Vector3(circleCenter.x + xVal, 0, circleCenter.z+yVal);
			nextWander=0;
		}
		Seek(lastWander);
	}

	
	protected void Wander_2() 
	{

		//For Demo only
		//We Reposition our Target.
		if(nextWander>1.0f)
		{
			float randomX = Random.Range(-1.0f,1.0f);
			float randomY = Random.Range(-1.0f,1.0f);

			randomX*= Manager_Collision.dimension/4;
			randomY*= Manager_Collision.dimension/4;

			lastWander = new Vector3(randomX, 0, randomY); 
			nextWander=0;
		}
		Seek(lastWander);
		//UnityEngine.Debug.DrawLine(position, lastWander, Color.blue);
	}
	//Strength must be given to certain Characterstics to avoid
	// negation
	
	//Perhaps use inverse Arrival to tweak strength based on proximity to edge
	protected bool Avoid_Bounds()
	{
		Vector3 desiredVector= new Vector3(0,0, 0);
		float bufferZone=5;
		bool correctionNeeded=false;
		if(position.x-bufferZone<0)
		{
			desiredVector.x=maxSpeed;
			correctionNeeded=true;
		}
		else if(position.x+bufferZone>Manager_Collision.dimension)
		{
			desiredVector.x=-maxSpeed;
			correctionNeeded=true;
		}		
		if(position.z-bufferZone<0)
		{
			desiredVector.z=maxSpeed;
			correctionNeeded=true;
		}
		else if(position.z+bufferZone>Manager_Collision.dimension)
		{
			desiredVector.z=-maxSpeed;
			correctionNeeded=true;
		}

		if(!correctionNeeded)
			return correctionNeeded;



		desiredVector =   Vector3.Normalize(desiredVector);
		desiredVector *=  maxSpeed;

		Vector3 steer = ( desiredVector - velocity);
		//Limit to Max Force
		if( steer.magnitude > (maxForce + (maxForce*0.3f)))
		{
			//Normalize the Vector
			steer =   Vector3.Normalize(steer);

			//Multiply by Max Value
			steer *= (maxForce + (maxForce*0.3f));
		}

		ApplyForce(steer);
		UnityEngine.Debug.DrawLine(position, (position+steer), Color.red);
		
		return correctionNeeded;
	}
}
