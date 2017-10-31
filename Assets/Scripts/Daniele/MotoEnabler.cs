using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotoEnabler : MonoBehaviour {

    public GameObject moto1;
    public GameObject moto2;
    public bool isEnable;
	
	public void SwitchMoto()
    {


        if (!isEnable)
        {
            moto1.SetActive(false);
            moto2.transform.position = moto1.transform.position;
            moto2.transform.localScale = moto1.transform.localScale;
            moto2.transform.rotation = moto1.transform.rotation;
            moto2.SetActive(true);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button_ON");
            isEnable = true;
        }
        else
            OnThisTrackingLost();
    

        }

    public void OnThisTrackingLost()
    {
            moto1.SetActive(true);
            moto2.SetActive(false);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button_OFF");
            isEnable = false;
    }



}
