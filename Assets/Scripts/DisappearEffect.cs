using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    private Vector3 initialPos;
    private DefaultTrackableEventHandler refDTEH;
    private float zAxis;
    private bool isColliding = true;

    private void Awake()
    {
        refDTEH = FindObjectOfType<DefaultTrackableEventHandler>();
        refDTEH.delStartEffect = StartDisappearEffect;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name != this.gameObject.name)
        {
            isColliding = false;
            Debug.LogWarning("Sono fuori dall collider");
        }
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
        isColliding = true;
        Debug.LogWarning(initialPos);
    }


    IEnumerator DisappearEffectCO()
    {
        initialPos = new Vector3(0, 0, -1.1f);

        while (isColliding)
        {
            zAxis = 0.01f;
            this.gameObject.transform.localPosition += new Vector3(0, 0, zAxis); //new Vector3(0f, 0f, 1f);
            yield return null;
        }
    }


}
