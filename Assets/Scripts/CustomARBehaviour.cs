using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectionLevel
{
    Acquisition,
    Tracked,
    Lost
}
public class CustomARBehaviour : MonoBehaviour
{
    public GameObject loadingIcon, detectionText;
    private DefaultTrackableEventHandler refDTEH;

    private void Awake()
    {
        refDTEH = FindObjectOfType<DefaultTrackableEventHandler>();
        refDTEH.delLoadMarker = LoadMarker;
        refDTEH.delDetectionMarker = SetTextDetectionMarker;
    }

    private void LoadMarker(bool _on)
    {
        Debug.LogWarning("Sono dentro LoadMarker");
        StartCoroutine(LoadMarkerCO());
    }

    private IEnumerator LoadMarkerCO()
    {
        Debug.LogWarning("Sono dentro LoadMarkerCO");

        while (loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount < 1)
        {
            loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount += .025f;
            SetTextDetectionMarker(DetectionLevel.Acquisition);
            yield return null;
        }
        //Far partire qui il metodo OnTrackinFound()
        refDTEH.TrackingFound();
        SetTextDetectionMarker(DetectionLevel.Tracked);
        loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
    }

    public void SetTextDetectionMarker(DetectionLevel _detectionLevel)
    {
        Debug.LogWarning("Sono dentro SetTextDetectionMarker");

        switch (_detectionLevel)
        {
            case DetectionLevel.Acquisition:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Acquisizione marker in corso...";
                break;
            case DetectionLevel.Tracked:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Tieni bene inquadrato il marker";
                break;
            case DetectionLevel.Lost:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "Inquadra il marker";
                break;
        }
    }

    public void ActiveLoadingIcon(bool _on)
    {
        loadingIcon.SetActive(_on);
    }
}