using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    private GameObject resetButton;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        resetButton = GameObject.FindGameObjectWithTag("ResetButton");
    }

    public void ResetScene()
    {
        StartCoroutine(ChangeSceneCO());
    }
    
    private IEnumerator ChangeSceneCO()
    {
        SceneManager.LoadScene(1);
        yield return null;
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}

//public class MovePanel : MonoBehaviour
//{
//    private Vector3 initialPos = Vector3.zero;
//    private Vector3 originalPos;
//    public float speedMove = 25f;
//    private bool isMoving;

//    public void MovePanelMeth(GameObject _go)
//    {
//        if (!isMoving)
//        {
//            // Prendo qui la posizione originale dell'oggetto in modo da porterlo poi
//            // far tornare in posizione con ReturnInPosition solo se non si sta muovendo
//            originalPos = _go.GetComponent<RectTransform>().localPosition;
//            StartCoroutine(MovePanelCO(_go));
//        }
//    }

//    private IEnumerator MovePanelCO(GameObject _go)
//    {
//        while (_go.GetComponent<RectTransform>().localPosition != initialPos)
//        {
//            isMoving = true;
//            _go.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(_go.GetComponent<RectTransform>().localPosition, initialPos, speedMove);
//            yield return null;
//        }
//        isMoving = false;
//    }

//    public void ReturnInPosition(GameObject _go)
//    {
//        StartCoroutine(ReturnPanelCO(_go));
//    }

//    private IEnumerator ReturnPanelCO(GameObject _go)
//    {
//        while (_go.GetComponent<RectTransform>().localPosition != originalPos)
//        {
//            isMoving = true;
//            _go.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(_go.GetComponent<RectTransform>().localPosition, originalPos, speedMove);
//            yield return null;
//        }
//        isMoving = false;
//    }
//}