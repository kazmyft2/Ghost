using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;
    public static float totalTime;
    int seconds;
	
	// Update is called once per frame
	void Update () 
    {
        totalTime += Time.deltaTime;
        seconds = (int)totalTime;
        timerText.text = seconds.ToString();
	}
}
