using System;
using Vuforia;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    public Action<bool> delLoadMarker;
    public Action<DetectionLevel> delDetectionMarker;
    public Action<bool> delStartEffect;
    public GameObject motoObject;
    public GameObject motoHolo;
    public GameObject[] objectWithEffect;

    public float scaleDev;

    private float xValue = 0f;
    private float yValue = -90f;
    private float zValue = 0f;

    //private Vector3 initialScaleMoto;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        delDetectionMarker(DetectionLevel.Lost);
        ResetScaleRot();
        //initialScaleMoto = motoObject.transform.localScale;
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
            
            //COMMENTATO PER FARE DELLE PROVE!!! CAUSA UN ERRORE ALL'AVVIO!!!
            //OnTrackingLost();
        }
    }

    protected virtual void OnTrackingFound()
    {
        Scene scene = SceneManager.GetActiveScene();

        //if (scene.name == "Merge_02-11" || scene.name == "Merge_23-10")
        delLoadMarker(true);

        //TrackingFound();
    }

    protected virtual void OnTrackingLost()
    {
        Scene scene = SceneManager.GetActiveScene();

        //if (scene.name == "Merge_02-11"|| scene.name == "Merge_23-10" )
        //{
        //    delDetectionMarker(DetectionLevel.Lost);
        //    TrackingLost();
        //}
        delDetectionMarker(DetectionLevel.Lost);
        TrackingLost();
    }

    public void TrackingFound()
    {
        //Debug.LogWarning("Sono dentro TrackingFound");

        //bSwitch.interactable = true;

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

        ////Attiva l'effetto di apparizione 
        //for (int i = 0; i < objectWithEffect.Length; i++)
        //{
        //    objectWithEffect[i].GetComponent<ClipShaderConfig>().ActivateEffect();
        //}
        motoHolo.SetActive(false);
        delStartEffect(true);
    }

    public void TrackingLost()
    {
        //Debug.LogWarning("Sono dentro TrackingLost");

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //CameraDevice.Instance.SetFlashTorchMode(true); //inserito il comando di attivazione del flash

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

        ////Resetta la x per resettare l'effetto di apparizione
        //for (int i = 0; i < objectWithEffect.Length; i++)
        //{
        //    objectWithEffect[i].GetComponent<ClipShaderConfig>().ResetPos();
        //}

        motoHolo.SetActive(true);
        delStartEffect(false);
        ResetScaleRot();
    }

    public void ResetScaleRot ()
    {
        motoObject.transform.localScale = new Vector3(scaleDev, scaleDev, scaleDev);
        motoObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        motoObject.transform.Rotate(xValue, yValue, zValue);
    }
}