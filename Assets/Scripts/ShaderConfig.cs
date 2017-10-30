using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderConfig : MonoBehaviour {


    public GameObject ObjectPos;
    public float FlowEdge;
    public float EdgeSize;

    public Color E_Color;
    public float Dissolve;
    public float Noise;
    private Material[] _M;


	// Use this for initialization
	void Start () {
        _M = GetComponent<Renderer>().materials;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var _refMat in _M)
        {
            _refMat.SetVector("_GetPos", ObjectPos.transform.position);
            _refMat.SetFloat("_FlowEdge", FlowEdge);
            _refMat.SetFloat("_Size", EdgeSize);
            _refMat.SetColor("_EdgeColor", E_Color);
            _refMat.SetFloat("_Dissolve", Dissolve);
            _refMat.SetVector("_GetRot", ObjectPos.transform.rotation.eulerAngles);
            _refMat.SetFloat("_Noise", Noise);
        }
	}
}
