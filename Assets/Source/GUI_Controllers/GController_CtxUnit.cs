using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This class is marked static so any functionality can directly use it.
//Since there is only one for Buildings it makes sense to mark it static
public static class GController_CtxUnit {

	public static Manager_BlackBoard man_BlackBoards;

	static UILabel lbl_Job;
	static UILabel lbl_Doing;
	static UILabel lbl_Name;
	static Actor selectedUnit;
	public static void Initialize(Manager_BlackBoard man_Board)
	{
		//Acquire the Text Components we will modify
		man_BlackBoards = man_Board;
		lbl_Job=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Unit/Panel/Lbl_Job").GetComponent<UILabel>();
		lbl_Doing=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Unit/Panel/Lbl_Doing").GetComponent<UILabel>();
		lbl_Name=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Unit/Panel/Lbl_Name").GetComponent<UILabel>();

	}

	public static void Update_GTXInfo(Actor unit)
	{
		//In the event a button was pressed reset to false and break out.
		//Otherwise we refocus the CTX
		// if(wasButtonPressed==true)
		// {
		// 	wasButtonPressed=false;
		// 	return;
		// }
		//Update the Onscreen GUI
		if(unit==null)
		{
			lbl_Job.text= "Job:??";
			lbl_Doing.text= "Doing:??";
			lbl_Name.text="Name:??";
		}
		else
		{
			lbl_Job.text 	= "Job:"+unit.Get_Job();
			lbl_Doing.text 	= "Doing:"+unit.Get_State();
			lbl_Name.text 	= "Name: MissingNo";	
		}

		if(selectedUnit!=null)
			selectedUnit.Darken();

		selectedUnit= unit;

		if(selectedUnit!=null)	
			selectedUnit.Highlight();
	}

	//Updates based on selected unit
	public static void Generic_Update()
	{
		if(selectedUnit==null)
		{
			lbl_Job.text= "Job:??";
			lbl_Doing.text= "Doing:??";
			lbl_Name.text="Name:??";
		}
		else
		{
			lbl_Job.text 	= "Job:"+selectedUnit.Get_Job();
			lbl_Doing.text 	= "Doing:"+selectedUnit.Get_State();
			lbl_Name.text 	= "Name: MissingNo";	
		}
	}

	public static void Print_Inventory()
	{
		if(selectedUnit ==null || selectedUnit.inventory==null)
			return;
			
		List<string> resources;
		resources = selectedUnit.inventory.Get_All_Resources();
		EventLog.Log_Message("Inventory----");
		for(int i=0;i<resources.Count;i++)
		{
			string message= "["+resources[i]+"]["+selectedUnit.inventory.CheckResourceAmount(resources[i])+"]";
			EventLog.Log_Message(message);
		}
	}

}
