using UnityEngine;
using System.Collections;
using System;
//This struct is used when an amount of a certain resource is requestted
public struct Resource_Amount
{
	public string resourceName;
	public int amount;
	public Resource_Amount (string rN, int amnt) {
		resourceName = rN;
		amount=amnt;
     }
}
