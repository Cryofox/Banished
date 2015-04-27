using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ArcheryRange : Building  {
	//Units assigned to Storages are responsible for dispersing goods
	//to where they are most needed
	public ArcheryRange()
	{
		// 1x1
	}
	protected override void Setup_Building()
	{
		this.name="ArcheryRange";
		this.collisionBox = new BoundingBox(10,10);		

		//Houses can have 4 People assigned to them
		maxWorkers=2;
		assignedUnits= new List<Actor>();
	}

	protected override void Load_Model()
	{
		model=	Resources.Load<GameObject>("GameObject/house");
	}


}
