using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCamera : MonoBehaviour
{
    public GameObject markerAR;
    private Vector3 delta;
    private float distance;

    public void CalculateDistance()
    {
        StartCoroutine(CalculateDistanceCO());
    }

    private IEnumerator CalculateDistanceCO()
    {
        while (true)
        {
            delta = this.transform.position - markerAR.transform.position;
            distance = delta.sqrMagnitude;
            Debug.Log("Distance to marker: " + distance / 100 + " cm");
            yield return new WaitForSeconds(1.5f);
        }
    }
}
