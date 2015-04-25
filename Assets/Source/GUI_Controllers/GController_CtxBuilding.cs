using UnityEngine;
using System.Collections;
//This class is marked static so any functionality can directly use it.
//Since there is only one for Buildings it makes sense to mark it static
public static class GController_CtxBuilding {

	public static Logic_Controller logic_Cont;

	static UILabel lbl_MaxWorkers;
	static UILabel lbl_CurWorkers;
	static UILabel lbl_Name;

	public static void Initialize(Logic_Controller lgc)
	{
		//Acquire the Text Components we will modify
		logic_Cont = lgc;
		lbl_MaxWorkers=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_MaxWorkers").GetComponent<UILabel>();
		lbl_CurWorkers=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_CurrentWorkers").GetComponent<UILabel>();		
		lbl_Name=GameObject.Find("UI Root/Camera/Panel_Main/Pnl_CtxSen_Building/Panel/Lbl_Name").GetComponent<UILabel>();		
	}

	public static void Update_GTXInfo(Building building)
	{
		//Update the Onscreen GUI
		if(building==null)
		{
			lbl_MaxWorkers.text= "??";
			lbl_CurWorkers.text= "??";
			lbl_Name.text="??";
			return;		
		}
		lbl_MaxWorkers.text= (building.maxWorkers).ToString();
		lbl_CurWorkers.text= (building.assignedUnits.Count).ToString();
		lbl_Name.text=(building.name);
	}



}
