using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{

    Camera cam;
    public float startingHealth = 100f;                 // 開始時のプレイヤーの体力の値
    public Slider slider;                               // 現在のプレイヤーの体力を示すスライダー
    public Image fillImage;                             // スライダーの Image コンポーネント
    public Color fullHealthColor = Color.blue;          // 体力が満タンのときの体力バーの色
    public Color zeroHealthColor = Color.red;           // 体力が 0 になったときの体力バーの色
    public FlushController flush;                       // ダメージを受けた時に画面をフラッシュさせる
    public Text GameOverText;
    public GameObject player;

    private float currentHealth;                        // 現在のプレイヤーの体力値
    private bool dead;                                  // プレイヤーの体力値が 0 を下回ったかどうか


    private void OnEnable()
    {
        //cam = Camera.main;
        // スタート時の体力
        currentHealth = startingHealth;
        dead = false;

        // 体力スライダーの値と色を更新
        SetHealthUI();
    }


    public void TakeDamage()
    {
        // 受けたダメージに基づいて現在の体力を削減
        currentHealth -= 10;
        player.GetComponent<PlayerMove>().Stan();

        flush.GetComponent<FlushController>().Flush();

        // 適切な UI 要素に変更
        SetHealthUI();

        // 現在の体力が 0 を下回り、かつ、まだ登録されていなければ、 OnDeath を呼び出します。
        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // スライダーに適切な値を設定
        slider.value = currentHealth;

        // 開始時に対する現在の体力のパーセントに基づいて、選択した色でバーを満たします。
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }


    private void OnDeath()
    {
        // フラグを設定して、この関数が1 度しか呼び出されないようにします。
        dead = true;
        flush.GetComponent<FlushController>().GameOver();
        GameOverText.text = "DEAD";
        StartCoroutine(Interval());
    }

    IEnumerator Interval(){
        cam.GetComponent<AudioSource>().enabled = false;
        GameOverText.GetComponent<AudioSource>().Play();
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = GameOverText.color;
            c.a = f;
            GameOverText.color = c;
        }
        yield return new WaitForSeconds(5.0f);
        ReturnOp();
    }

    void ReturnOp(){
        SceneManager.LoadScene("OP");
    }
}
