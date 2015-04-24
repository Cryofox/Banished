using UnityEngine;
using System.Collections;

public class House : Building {

	public House ()
	{
		// 1x1
		this.collisionBox = new BoundingBox(10,8);		
	}

	protected override void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/house");
	}
}
