using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    private Animator animPanel;

    private void Awake()
    {
        animPanel = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //&& animPanel.GetFloat("SpeedParam") != 1)
        {
            Debug.Log("Premuto R e dovrebbe partire animation");
            animPanel.SetTrigger("ForwardTrigger");
        }
        if (Input.GetKeyDown(KeyCode.S)) //&& animPanel.GetFloat("SpeedParam") != -1)
        {
            Debug.Log("Premuto S e dovrebbe tornare indietro");
            animPanel.SetTrigger("BackwardTrigger");
        }
    }

    public void MovePanelMeth(string _trigger)
    {
        Debug.Log("Premuto R e dovrebbe partire animation");
        animPanel.SetTrigger(_trigger);
    }
}
