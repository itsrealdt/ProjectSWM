using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    private RectTransform initialPos;

    private void Awake()
    {
        initialPos = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(MovePanelCO());   
        }
    }

    private IEnumerator MovePanelCO()
    {
        while (true)
        {
            this.GetComponent<RectTransform>().position -= new Vector3(100, 0, 0);
            yield return null;
        }
    }
}
