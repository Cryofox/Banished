using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Tree : Building {

	public Tree ()
	{
		this.name="Tree";
	}
	protected override void Setup_Building()
	{
		this.collisionBox = new BoundingBox(2,2);	
		inventory= new Inventory(1,200);  
		inventory.InsertResourceAmount("wood",50);	
	}

	protected override void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Tree");
	}
}
