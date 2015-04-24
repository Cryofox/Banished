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


	//Swap H with V
	public void Rotate()
	{
		float temp = horizontal_halfOffset;
		horizontal_halfOffset= vertical_halfOffset;
		vertical_halfOffset= temp;
	}

}
