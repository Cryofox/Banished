using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class House : Building {

	public House ()
	{
		// 1x1
		
	}
	protected override void Setup_Building()
	{
		this.name="House";
		
		this.collisionBox = new BoundingBox(10,8);		
		//Houses can have 4 People assigned to them
		maxWorkers=4;
		assignedUnits= new List<Actor>();
	}
	protected override void Load_Model()
	{
		model=	Resources.Load<GameObject>("GameObject/house");
	}



}
