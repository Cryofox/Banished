using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Foresting_Station : Building  
{
	//Units assigned to Storages are responsible for dispersing goods
	//to where they are most needed
	public Foresting_Station ()
	{
		// 1x1
	}
	protected override void Setup_Building()
	{
		this.name="Foresting_Station";
		this.collisionBox = new BoundingBox(10,10);		

		//Houses can have 4 People assigned to them
		maxWorkers=2;
		assignedUnits= new List<Actor>();
	}

	protected override void Load_Model()
	{
		model=	Resources.Load<GameObject>("GameObject/house");
	}





	void Collector_Setup()
	{	
		ring =	Resources.Load<GameObject>("GameObject/Circle");
		ring =	GameObject.Instantiate(ring,position, Quaternion.identity) as GameObject;
		ring.transform.localScale = new Vector3( collect_Diameter,1, collect_Diameter);
		ring.transform.parent = model.transform;
	}


	public override void Post_Instantiate()
	{
		Collector_Setup();
	}
	public override void Destroy_Extra()
	{
		Object.Destroy(ring);
	}


	public override Job LoadJob()
	{
		return new Gather("Tree",this);
	}

}
