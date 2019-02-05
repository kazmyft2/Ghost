using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    public GameObject fadeImage;
    public Image image;
    float x = 140, y = 80;
    bool fadeOn = false;

    void Start()
    {
        image.enabled = false;
    }

    public void OnStartButtonClicked()
    {
        image.enabled = true;
        StartCoroutine(Fade());

    }

    IEnumerator Fade(){
        fadeOn = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        if(x > 14)
        {
            if (fadeOn)
            {
                fadeImage.gameObject.transform.localScale = new Vector3(x -= 1.4f, y -= 0.8f, 0);
            }
        }

    }


    public void OnRankingButtonClicked()
    {
        SceneManager.LoadScene("RankingView");
    }

    public void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("OP");
    }

    public void OnTutorialButtonClicked()
    {
        SceneManager.LoadScene("TutorialView");
    }
}
