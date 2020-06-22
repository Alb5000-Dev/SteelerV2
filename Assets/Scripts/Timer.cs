using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float thiefTime;
    public float ownerTime;

    public float timeLeft;

    public Text timer;

    public IEnumerator timerCoroutine(float timeToGo)
    {
        timeLeft = timeToGo;
        while (timeLeft >= 0)
        {
            yield return new WaitForEndOfFrame();
            timeLeft -= Time.unscaledDeltaTime;
            timer.text = Mathf.Round(timeLeft).ToString();
        }
        timer.text = "0";
        if (timeToGo == ownerTime)
        {
            //Changement de personnage
        }
        else
        {
            //Fin de Game
            timer.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(timerCoroutine(ownerTime));
        }
    }
}
