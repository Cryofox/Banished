using UnityEngine;
using System.Collections;

public class Tree : Building {

	public Tree ()
	{
		// 1x1
		this.collisionBox = new BoundingBox(2,2);		
	}

	protected override void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Tree");
	}
}
