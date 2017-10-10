using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotEnabler : MonoBehaviour {

    public GameObject pilot;
    public bool isEnable;
	
	public void SwitchPilot ()
    {
       

		if (!isEnable)
        {
            pilot.SetActive(true);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("pilotEnabled");
            isEnable = true;
        }
        else
        {
            pilot.SetActive(false);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("pilotDisabled");
            isEnable = false;
        }
	}

    public void OnThisTrackingLost()
    {
        pilot.SetActive(false);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("pilotDisabled");
            isEnable = false;
    }



}
