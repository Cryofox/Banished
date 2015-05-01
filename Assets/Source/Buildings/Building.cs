using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Building 
{
	public BlackBoard faction;
	public string name;
	public int maxWorkers=0;
	public Inventory inventory;
	public List<Actor> assignedUnits;

	//The max amount of items able to be "harvested" from a resource
	public float harvestAmount=0;
	

	//Persistant Information
	public BoundingBox collisionBox;


	public Vector3 position;
	protected GameObject model;

	//Building Can't have a startPosition because that would imply it was Placed
	public Building()
	{
		name="DefaultBuilding";
		Setup_Building();
	}

	protected virtual void Setup_Building()
	{
		// 1x1
		this.collisionBox = new BoundingBox(1,1);	
		inventory= new Inventory(1,200);		
	}
	protected virtual void Load_Model()
	{
		//Here we use a Cube for the Actor
		model=	Resources.Load<GameObject>("GameObject/Generic_Tree");
	}

	//Add the unit to the building.
	//If the assignment fails this function returns false
	public void AssignUnit(Actor unit)
	{
		//If room doesn't exists for unit assignment
		if(assignedUnits==null || assignedUnits.Count>=maxWorkers)
		{
			return;
		}
		//Add unit to assigned units list
		assignedUnits.Add(unit);

		Job job  = LoadJob();
		//Be sure to overwrite this for each new Job
		unit.AssignJob(job);
	}

	public virtual Job LoadJob()
	{
		return new Job(this);
	}

	public void DeAssignUnit()
	{
		//We do not add the unit
		if(assignedUnits==null || assignedUnits.Count==0)
		{
			return;
		}

		Actor unit = assignedUnits[0];
		unit.job= null;
		assignedUnits.RemoveAt(0);
	}





	//Select Ghost
	public void Select_Ghost()
	{
		Load_Model();
		//Spawn our model
		model=GameObject.Instantiate(model,position, Quaternion.identity) as GameObject;
		Post_Instantiate();
	}

	public virtual void Post_Instantiate()
	{

	}



	//DeSelect Ghost
	public void DeSelect_Ghost()
	{
		Object.Destroy(model);
		Destroy_Extra();
	}	

	public virtual void Destroy_Extra()
	{}


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
	//Collector Buildings have a special Setup

	public float range {
		get{ return collect_Diameter;}
	}


	//They have a Ring
	protected GameObject ring;
	protected float collect_Diameter=100;


	public void Destroy()
	{
		//Destroy animation

		//Remove units assigned here
		if(assignedUnits!=null)
			while(assignedUnits.Count>0)
			{DeAssignUnit();}

		//Remove from BlackBoard and sector info
		faction.DestroyBuilding(this);

		Object.Destroy(ring);
		Object.Destroy(model);
	}
}

