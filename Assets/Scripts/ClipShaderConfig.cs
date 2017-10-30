using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipShaderConfig : MonoBehaviour {


    public Vector3 pos;
    public Vector3 startPos;
    public Vector3 endPos;
    private Vector3 securityPos = new Vector3(-10000f, 0f, 0f);
    public float moveSeconds;

    private Material[] _M;


	// Use this for initialization
	void Start () {
        _M = GetComponent<Renderer>().materials;
        //ActivateEffect();
	}


    public void ActivateEffect()
    {
        StartCoroutine(MoveOverSeconds(endPos, moveSeconds));
    }

    public void ResetPos()
    {
        foreach (var _refMat in _M)
        {
            _refMat.SetVector("_PlanePoint", startPos);
        }
    }
    
    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        //Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            pos = Vector3.Lerp(startPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            foreach (var _refMat in _M)
            {
                _refMat.SetVector("_PlanePoint", pos);
            }
            
            yield return new WaitForEndOfFrame();
        }
        foreach (var _refMat in _M)
        {
            _refMat.SetVector("_PlanePoint", securityPos);
        }

    }

}
