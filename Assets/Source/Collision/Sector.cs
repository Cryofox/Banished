using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sector
{

	List<Actor> dynamicEntities;
	public Sector()
	{
		dynamicEntities= new List<Actor>();

	}

	public void Initial_AddUnit(Actor unit)
	{
		dynamicEntities.Add(unit);
	}

	
}
