using System;
using Vuforia;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    public Action<bool> delLoadMarker;
    public Action<DetectionLevel> delDetectionMarker;

    public GameObject objectToMove;
    public bool effect;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    protected virtual void OnTrackingFound()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Animation UI" || scene.name == "Merge_23-10")
            delLoadMarker(true);
        else
            TrackingFound();
    }

    protected virtual void OnTrackingLost()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Animation UI"|| scene.name == "Merge_23-10")
        {
            delDetectionMarker(DetectionLevel.Lost);
            TrackingLost();
        }
        TrackingLost();

    }

    public void TrackingFound()
    {
        Debug.LogWarning("Sono dentro TrackingFound");

      

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        //CameraDevice.Instance.SetFlashTorchMode(true); //inserito il comando di attivazione del flash

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        //Attiva l'effetto di apparizione 
        if (effect)
        {
            objectToMove.GetComponent<MovePlaneEffect>().ActivateEffect();
        }
    }

    public void TrackingLost()
    {
        Debug.LogWarning("Sono dentro STrackingLost");

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //CameraDevice.Instance.SetFlashTorchMode(true); //inserito il comando di attivazione del flash
        //PilotEnabler pilotElements = FindObjectOfType<PilotEnabler>();
        //pilotElements.OnThisTrackingLost();

        //MotoEnabler motoElements = FindObjectOfType<MotoEnabler>();
        //motoElements.OnThisTrackingLost();

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        //Resetta la posizione del quad per l'effetto di apparizione
        if (effect)
        {
            objectToMove.transform.position = objectToMove.GetComponent<MovePlaneEffect>().startPos;
        }

    }
}