using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GhostCountChecker : MonoBehaviour {

    Camera cam;//カメラについてるBGMを最後止めるため
    GameObject[] tagObjects;
    public Text GhostCount;
    public static float endTime;
    public Text GameOverText;

    float timer = 0.0f;
    float interval = 2.0f;

    void Start()
    {
        tagObjects = GameObject.FindGameObjectsWithTag("Ghost");
        cam = Camera.main;
    }
    // ゴーストの数をtagでカウント
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > interval)
        {
            Check("Ghost");
            timer = 0;
        }
    }

    //シーン上のタグが付いたオブジェクトを数える
    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        int num = tagObjects.Length;
        GhostCount.text = "" + num;
        if (tagObjects.Length == 0)
        {

            GameOverText.text = "CLEAR";
            GameOverText.color = Color.Lerp(GameOverText.color, Color.white, Time.deltaTime);
            StartCoroutine(Interval());
            endTime = Timer.totalTime;
        }
    }
    IEnumerator Interval()
    {
        cam.GetComponent<AudioSource>().enabled = false;
        GhostCount.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("ScoreView");
    }
}
