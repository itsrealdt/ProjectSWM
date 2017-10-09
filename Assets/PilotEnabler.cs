using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotEnabler : MonoBehaviour {

    public GameObject pilot;
    private bool isEnable;
	
	public void SwitchPilot ()
    {
        Debug.LogWarning("mi stai pigiando");

		if (!isEnable)
        {
            pilot.SetActive(true);
            //this.GetComponent<Image>().sprite = Resources.Load<Sprite>("pilotEnabled");
            isEnable = true;
        }
        else
        {
            pilot.SetActive(false);
            //this.GetComponent<Image>().sprite = Resources.Load<Sprite>("pilotDisabled");
            isEnable = false;
        }
	}
	
	
	
}
