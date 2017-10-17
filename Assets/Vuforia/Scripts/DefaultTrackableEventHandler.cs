/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections;
using Vuforia;

public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    // Roba aggiunta da David per calcolo distanza, vedi OnTrackingFound()
    private RaycastCamera refRC;
    public GameObject loadingIcon, detectionText;
    private void Awake()
    {
        refRC = FindObjectOfType<RaycastCamera>();
    }

    #region PRIVATE_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
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

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    protected virtual void OnTrackingFound()
    {
        StartCoroutine(LoadMarkerCO());
    }

    protected virtual void OnTrackingLost()
    {
        ActiveLoadingIcon(true);
        TrackingLost();
        SetTextDetectionMarker(-1);
    }

    #endregion // PRIVATE_METHODS

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
        ActiveLoadingIcon(false);
        loadingIcon.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
    }
    private void TrackingFound()
    {
        refRC.CalculateDistance();

        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }
    private void TrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        
        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
    private void ActiveLoadingIcon(bool _on)
    {
        loadingIcon.SetActive(_on);
    }
    private void SetTextDetectionMarker(sbyte _detectionLevel)
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