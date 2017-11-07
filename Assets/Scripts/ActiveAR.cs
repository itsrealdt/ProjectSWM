using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAR : MonoBehaviour
{
    public void ResetAttivaAR(bool _on)
    {
        StartCoroutine(ResetAttivaARCO(_on));
    }

    private IEnumerator ResetAttivaARCO(bool _on)
    {
        if (_on)
        {
            // Fa apparire il tasto Reload AR Button, aspetta X sec e poi lo fa scomparire
            GetComponent<Image>().enabled = !_on;
            GetComponentInChildren<Text>().enabled = !_on;
            yield return new WaitForSeconds(8f);
            GetComponent<Image>().enabled = _on;
            GetComponentInChildren<Text>().enabled = _on;
        }
        else
        {
            // Fa sparire il bottone (nel caso fosse visibile)
            // aspetta X sec e poi lo fa ricomparire
            GetComponent<Image>().enabled = _on;
            GetComponentInChildren<Text>().enabled = _on;
            yield return new WaitForSeconds(8f);
            GetComponent<Image>().enabled = !_on;
            GetComponentInChildren<Text>().enabled = !_on;
        }
    }
}
