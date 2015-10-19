// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class PowerPlant : MonoBehaviour, IComparable
{
	public int baseCost;
	public int power;
	public int materialCost;
	[HideInInspector]
	public int materialStock;

	[HideInInspector]
	public bool purchased;

	private GameObject miniCardObj = null;
	private PlantCardMiniView miniViewData = null;
	public Type type;
	
	public enum Type
	{
		Coal,
		Oil,
		Hybrid,
		Garbage,
		Uranium,
		Clean,
		Step3
	};
	
	public int CompareTo( object pp) {
		return baseCost.CompareTo (((PowerPlant)pp).baseCost);
	}
	
	public void Start() {
		purchased = false;
		miniCardObj = Instantiate(GameObject.Find ("PlantMiniView"));
		if (miniCardObj == null)
			print ("Can't Find PlantMiniView object");
		else
			miniCardObj.transform.parent = transform;
	}

	public bool CanStockMoreMaterial() {
		return (materialStock < 2 * materialCost);
	}

	public GameObject MiniCardObj {
		get {
			return miniCardObj;
		}
	}

	public void Hide() {
		transform.position = new Vector3(100,100,0);
		miniCardObj.transform.position = new Vector3(100,100,0);
	}

	public bool AddMaterial() {
		if (CanStockMoreMaterial ()) {
			materialStock++;
			return true;
		}
		return false;
	}

	public int RunPlant() {
		if(materialCost > materialStock)
			return 0;

		materialStock -= materialCost;
		return power;
	}

	public override string ToString() {
		string info = "cost:" + baseCost + " power:" + power + " materialCost:" + materialCost + " type:" + type;
		return info;
	}
	public void OnMouseOver() {
		if (GameState.instance.CurrentState == GameState.State.BuyPlants) {
			if (Input.GetMouseButtonDown (0)) {
				GameState.instance.PowerplantShop.SetSelectedPlant(this);
			}
		}
	}

	public void Update() {
		if (miniCardObj != null) {
			if (miniViewData == null) {
				miniViewData = miniCardObj.GetComponent<PlantCardMiniView> ();
				if(miniViewData != null)
					miniViewData.Setup(this);
			}
		}
	}
	public void OnGUI() {
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		screenPoint.y = Screen.height - screenPoint.y;
		GUI.Label(new Rect(screenPoint.x-25, screenPoint.y-25,200,100),gameObject.name + "\nCost:" +materialCost +"\nPwr:"+power);
	}
}

