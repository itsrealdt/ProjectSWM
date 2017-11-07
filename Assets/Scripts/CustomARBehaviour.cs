using System.Collections;
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
        StartCoroutine(LoadMarkerCO());
    }

    private IEnumerator LoadMarkerCO()
    {

        while (loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount < 1)
        {
            loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount += .03f;
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
        switch (_detectionLevel)
        {
            case DetectionLevel.Acquisition:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "ACQUISIZIONE IN CORSO...";
                break;
            case DetectionLevel.Tracked:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "";
                break;
            case DetectionLevel.Lost:
                detectionText.GetComponent<UnityEngine.UI.Text>().text = "INQUADRA IL MARKER";
                break;
        }
    }

    public void ActiveLoadingIcon(bool _on)
    {
        loadingIcon.SetActive(_on);
    }
}