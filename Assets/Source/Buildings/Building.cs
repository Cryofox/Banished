using UnityEngine;
using System.Collections;

public class Building 
{


	//Persistant Information
	public BoundingBox collisionBox;


	public Vector3 position;
	protected GameObject model;


	//Building Can't have a startPosition because that would imply it was Placed
	public Building()
	{
		// 1x1
		this.collisionBox = new BoundingBox(1,1);		
	}


	protected virtual void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Tree");
	}

	//Select Ghost
	public void Select_Ghost()
	{
		Load_Model();
		//Spawn our model
		model=GameObject.Instantiate(model,position, Quaternion.identity) as GameObject;
	}
	//DeSelect Ghost
	public void DeSelect_Ghost()
	{
		Object.Destroy(model);
	}	

	public void Rotate_Right()
	{
		//You add rotations by multiplying quaternions
		Quaternion object_rotation= Quaternion.identity;
		object_rotation.eulerAngles= new Vector3(0,90,0);
		model.transform.rotation *= object_rotation;
		collisionBox.Rotate();
	}

	public void Rotate_Left()
	{
		//You add rotations by multiplying quaternions
		Quaternion object_rotation= Quaternion.identity;
		object_rotation.eulerAngles= new Vector3(0,-90,0);
		model.transform.rotation *= object_rotation;
		collisionBox.Rotate();
	}

	public void Set_Position(Vector3 reqPosition)
	{
		position= reqPosition;
		model.transform.position=position;		
	}

}

