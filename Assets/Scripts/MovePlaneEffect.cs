using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlaneEffect : MonoBehaviour {

    public GameObject objectToMove;
    public Vector3 startPos;
    public Vector3 endPos;
    public float moveSeconds;

    private void Start()
    {
        objectToMove = this.gameObject;
        startPos = this.gameObject.transform.position;
        //StartCoroutine(MoveOverSeconds(gameObject, endPos, moveSeconds));
        //StartCoroutine(MoveOverSpeed(gameObject, new Vector3(-45f, 70f, 20f), 5f));
    }

    public void ActivateEffect()
    {
        StartCoroutine(MoveOverSeconds(gameObject, endPos, moveSeconds));
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.localPosition = end;
    }
}
