using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    private Vector3 initialPos;
    private DefaultTrackableEventHandler refDTEH;
    private float zAxis;

    private void Awake()
    {
        refDTEH = FindObjectOfType<DefaultTrackableEventHandler>();
        refDTEH.delStartEffect = StartDisappearEffect;
    }

    public void StartDisappearEffect(bool _on)
    {
        if (_on)
        {
            StartCoroutine(DisappearEffectCO());
        }
        else
        {
            ResetInitialPos();
            StopAllCoroutines();
        }
    }

    public void ResetInitialPos()
    {
        this.transform.localPosition = initialPos;
        Debug.Log("Posizione del ClipPlane resettata");
        Debug.LogWarning(initialPos);
    }


    IEnumerator DisappearEffectCO()
    {
        initialPos = this.transform.localPosition;

        while (true)//this.transform.forward != initialTrans.forward *2)
        {
            zAxis = -0.5f;
            //this.gameObject.transform.right += new Vector3(1f, 0f, 0f);
            this.gameObject.transform.position += new Vector3(zAxis, 0, 0); //new Vector3(0f, 0f, 1f);
            yield return null;
        }
    }
}
