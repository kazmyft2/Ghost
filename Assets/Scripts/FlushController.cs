using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour 
{
    //画面を赤くフラッシュさせるためのキャンバス
    Image img;
    bool gameover = false;

    void Start()
    {
        //通常は透明にしておく
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    public void GameOver()
    {
        gameover = true;
    }

    //ダメージを受けた時
    public void Flush()
    {
        this.img.color = new Color(0.5f, 0f, 0f, 0.7f);//第4引数はフラッシュの戻る時間
    }

    void Update()
    {
        //ゲームオーバー時以外は透明に戻す
        if(!gameover){
            this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }
    }
}