using UnityEngine;
using System.Collections;

public class Job  {

	protected Building workPlace;
	protected Building storage;
	protected BlackBoard bboard;
	protected Manager_Collision man_Collision;
	protected float range = 4;

	public string jobName;

	public Job(Building building)
	{
		workPlace = building;
		jobName= "Default Job";
	}
	//BlackBoard and collision detection in case references need to be made from this class
	public void AddEssentials(BlackBoard blackboard, Manager_Collision man_Col)
	{
		bboard = blackboard;
		man_Collision = man_Col;
	}

	//Our behaviour is Simple when Working
	//Working Range= 5;



	public virtual void Work(Actor unit)
	{
		unit.Idle();
	}


}
