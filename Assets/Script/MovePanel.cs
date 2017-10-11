using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanel : MonoBehaviour
{
    private Vector3 initialPos = Vector3.zero;
    private Vector3 originalPos;
    public float speedMove = 25f;

    public void MovePanelMeth(GameObject _go)
    {
        // Prendo qui la posizione originale dell'oggetto in modo da porterlo poi
        // far tornare in posizione con ReturnInPosition
        originalPos = _go.GetComponent<RectTransform>().localPosition;
        StartCoroutine(MovePanelCO(_go));
    }

    private IEnumerator MovePanelCO(GameObject _go)
    {
        while (_go.GetComponent<RectTransform>().localPosition != initialPos)
        {
            _go.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(_go.GetComponent<RectTransform>().localPosition, initialPos, speedMove);
            yield return null;
        }
    }

    public void ReturnInPosition(GameObject _go)
    {
        StartCoroutine(ReturnPanelCO(_go));
    }

    private IEnumerator ReturnPanelCO(GameObject _go)
    {
        while (_go.GetComponent<RectTransform>().localPosition != originalPos)
        {
            _go.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(_go.GetComponent<RectTransform>().localPosition, originalPos, speedMove);
            yield return null;
        }
    }
}
