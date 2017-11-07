using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisappearEffect : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 0, -1.1f);
    private DefaultTrackableEventHandler refDTEH;
    private ActiveAR refAAR;
    private float zAxis;

    private void Awake()
    {
        refDTEH = FindObjectOfType<DefaultTrackableEventHandler>();
        refAAR = FindObjectOfType<ActiveAR>();
        refDTEH.delStartEffect = StartDisappearEffect;
    }

    public void StartDisappearEffect(bool _on)
    {
        if (_on)
        {
            StartCoroutine(DisappearEffectCO());
        }
        else
        {
            ResetInitialPos();
            StopAllCoroutines();
        }
    }

    public void ResetInitialPos()
    {
        transform.localPosition = initialPos;
    }

    IEnumerator DisappearEffectCO()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Final_Fissa")
            zAxis = 0.03f;
        else
            zAxis = 0.05f;

        // Riferimento ad ActiveAR che fa sparire il bottone (nel caso fosse visibile)
        // aspetta 5 sec e poi lo fa ricomparire
        refAAR.ResetAttivaAR(false);

        while (true)
        {
            this.gameObject.transform.localPosition += new Vector3(0, 0, zAxis);
            yield return null;
        }
    }
}
