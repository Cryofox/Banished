using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SawMill : Building  {
	//Units assigned to Storages are responsible for dispersing goods
	//to where they are most needed
	public SawMill ()
	{
		// 1x1
	}
	protected override void Setup_Building()
	{
		this.name="SawMill";
		this.collisionBox = new BoundingBox(4,4);		

		//Houses can have 4 People assigned to them
		maxWorkers=2;
		assignedUnits= new List<Actor>();
	}

	protected override void Load_Model()
	{
		model=	Resources.Load<GameObject>("GameObject/house");
	}
//Converter(List<Resource_Amount> inputResources, List<Resource_Amount> outputResources, Building building): base(building)

	public override Job LoadJob()
	{
		List<Resource_Amount> resources_Input =new List<Resource_Amount>();
		List<Resource_Amount> resources_Output=new List<Resource_Amount>();

	 	resources_Input.Add(new Resource_Amount("wood",2));
		resources_Output.Add(new Resource_Amount("plank",1));

		return new Converter(resources_Input,resources_Output,this);
	}

}
