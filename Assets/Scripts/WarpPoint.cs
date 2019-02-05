using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarpPoint : MonoBehaviour 
{
    public GameObject fadeImage;
    public Image image;
    public AudioSource audioSource;
    float x = 140, y = 80;
    bool fadeOn;

    void Start()
    {
        fadeOn = false;
        image.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        image.enabled = true;//イメージをON
        fadeOn = true;//Updateでフェードスタート
        StartCoroutine(Fade(other));
    }

    IEnumerator Fade(Collider other)
    {
        audioSource.Play();//ワープ音再生

        yield return new WaitForSeconds(3.0f);//フェードがいい感じになるまで待機してからワープ

        if(gameObject.tag == "B1")//1Fへワープ
        {
            other.gameObject.transform.position = new Vector3(0.03f, 258.8f, 164.1f);
        }
        else if (gameObject.tag == "1F")//B1へワープ
        {
            other.gameObject.transform.position = new Vector3(0f, -36.5f, -160.0f);
        }

        image.enabled = false;//初期化
        fadeOn = false;//初期化
        fadeImage.gameObject.transform.localScale = new Vector3(140f, 80f, 0f);//イメージサイズを初期値にリセット

    }

    void Update()
    {
        if (x > 10)
        {
            if (fadeOn) fadeImage.gameObject.transform.localScale = new Vector3(x -= 2.8f, y -= 1.6f, 0f);
        }
    }
}