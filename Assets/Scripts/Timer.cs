using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public float thiefTime;
    public float ownerTime;

    public float timeLeft;

    public GameObject timer;

    public IEnumerator timerCoroutine(float timeToGo)
    {
        timeLeft = timeToGo;
        while (timeLeft >= 0)
        {
            yield return new WaitForEndOfFrame();
            timeLeft -= Time.unscaledDeltaTime;
            timer.GetComponent<TextMeshProUGUI>().text = Mathf.Round(timeLeft).ToString();
        }
        timer.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
