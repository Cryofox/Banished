using UnityEngine;
using System.Collections;

public class BoundingBox {

	//Save Calculation Time
	public float horizontal_halfOffset;
	public float vertical_halfOffset;


	public BoundingBox(float dimensionWidth, float dimensionHeight)
	{
		horizontal_halfOffset = dimensionWidth/2;
		vertical_halfOffset = dimensionHeight/2;
	}


}
