﻿using UnityEngine;
using System.Collections;

public class Building 
{


	//Persistant Information
	public BoundingBox collisionBox;


	public Vector3 position;
	GameObject model;


	//Building Can't have a startPosition because that would imply it was Placed
	public Building()
	{
		// 1x1
		this.collisionBox = new BoundingBox(1,1);		
	}
	//Select Ghost
	public void Select_Ghost()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Tree");
		//Spawn our model
		model=GameObject.Instantiate(model,position, Quaternion.identity) as GameObject;
	}
	//DeSelect Ghost
	public void DeSelect_Ghost()
	{
		Object.Destroy(model);
	}	


	public void Set_Position(Vector3 reqPosition)
	{
		position= reqPosition;
		model.transform.position=position;
	}

}

