﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Rock : Building {

	public Rock ()
	{
	
	}
	protected virtual void Setup_Building()
	{
		this.collisionBox = new BoundingBox(2,2);		
	}
	
	protected override void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Rock");
	}
}
