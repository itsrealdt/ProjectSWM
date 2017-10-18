using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomARBehaviour : MonoBehaviour
{
    public GameObject loadingIcon, detectionText;
    private RaycastCamera refRC;

    private void Awake()
    {
        refRC = FindObjectOfType<RaycastCamera>();
    }

    private IEnumerator LoadMarkerCO()
    {
        while (loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount < 1)
        {
            loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount += .025f;
            SetTextDetectionMarker(0);
            yield return null;
            //yield return new WaitForSeconds(.1f);
        }
        TrackingFound();
        SetTextDetectionMarker(1);
        //ActiveLoadingIcon(false);
        loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
    }
    public void ActiveLoadingIcon(bool _on)
    {
        loadingIcon.SetActive(_on);
    }
    public void SetTextDetectionMarker(sbyte _detectionLevel)
    {
        switch (_detectionLevel)
        {
            case 0:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Acquisizione marker in corso...";
                break;
            case 1:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Tieni il marker inquadrato";
                break;
            case -1:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Inquadra il marker";
                break;
        }
    }
}
