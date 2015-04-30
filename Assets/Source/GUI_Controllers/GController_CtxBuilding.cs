using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This class is marked static so any functionality can directly use it.
//Since there is only one for Buildings it makes sense to mark it static
public static class GController_CtxBuilding {

	public static Manager_BlackBoard man_BlackBoards;

	static UILabel lbl_MaxWorkers;
	static UILabel lbl_CurWorkers;
	static UILabel lbl_Name;
	static Building selectedBuilding;
	static bool wasButtonPressed;

	public static void Initialize(Manager_BlackBoard man_Board)
	{
		//Acquire the Text Components we will modify
		man_BlackBoards = man_Board;
		lbl_MaxWorkers=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_MaxWorkers").GetComponent<UILabel>();
		lbl_CurWorkers=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_CurrentWorkers").GetComponent<UILabel>();		
		lbl_Name=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_Name").GetComponent<UILabel>();		
		wasButtonPressed=false;
	}

	public static void Update_GTXInfo(Building building)
	{
		//In the event a button was pressed reset to false and break out.
		//Otherwise we refocus the CTX
		// if(wasButtonPressed==true)
		// {
		// 	wasButtonPressed=false;
		// 	return;
		// }
		//Update the Onscreen GUI
		if(building==null)
		{
			lbl_MaxWorkers.text= "??";
			lbl_CurWorkers.text= "??";
			lbl_Name.text="??";	
		}
		else
		{
			lbl_MaxWorkers.text= (building.maxWorkers).ToString();
			lbl_CurWorkers.text= (building.assignedUnits.Count).ToString();
			lbl_Name.text=(building.name);
		}
			selectedBuilding=building;
	}
	public static void AddUnit()
	{
		if(selectedBuilding!=null)
			man_BlackBoards.AssignUnit(Logic_Controller.playerFaction,selectedBuilding);

		//Refresh Gui
		Update_GTXInfo(selectedBuilding);
	}
	public static void RemoveUnit()
	{
		if(selectedBuilding!=null)
			man_BlackBoards.DeAssignUnit(Logic_Controller.playerFaction,selectedBuilding);

		//Refresh Gui
		Update_GTXInfo(selectedBuilding);
	}
	public static void Print_Inventory()
	{
		if(selectedBuilding.inventory==null)
			return;
			
		List<string> resources;
		resources = selectedBuilding.inventory.Get_All_Resources();
		EventLog.Log_Message("Inventory----");
		for(int i=0;i<resources.Count;i++)
		{
			string message= "["+resources[i]+"]["+selectedBuilding.inventory.CheckResourceAmount(resources[i])+"]";
			EventLog.Log_Message(message);
		}
	}


}
