using UnityEngine;
using System.Collections;

public class FlowField {

	//2d array of bytes

	//Absolute Values
	byte[][] traversableTerrain;

	public FlowField(int dimension)
	{
		this.traversableTerrain= new byte[dimension][];

		for(int x=0;x<dimension;x++)
		{
			traversableTerrain[x]= new byte[dimension];
			for(int y=0;y<dimension;y++)
				traversableTerrain[x][y]=0; // Cost =0; 
		}
	}


	//A Heirarchy is needed to -speed up- algorithm.




}
