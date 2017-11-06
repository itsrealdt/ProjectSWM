using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 0, -1.1f);
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
        Debug.LogWarning(initialPos);
    }


    IEnumerator DisappearEffectCO()
    {
        while (true)
        {
            zAxis = 0.01f;
            this.gameObject.transform.localPosition += new Vector3(0, 0, zAxis);
            yield return null;
        }
    }
}
