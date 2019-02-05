using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;
using UnityEngine.SceneManagement;

public class SendResult : MonoBehaviour {

    InputField input;
    public Text scoreText;                          //スコア覧を取得
    public Text rankText;                          //ランク覧を取得
    public string ServerAddress = "kazmyft2.php.xdomain.jp/rankingAll.php";  //selecttest.phpを指定　今回のアドレスはlocalhost
    string textCheck;//入力チェックよう
    int ResultScore = (int)GhostCountChecker.endTime;//クリア時のスコアを入れる

    /*
     * 最初にDBから最新ランク10位までをもらう 
     */

    void Start()
    {
        input = GetComponent<InputField>();
        scoreText = scoreText.GetComponent<Text>();
        rankText = rankText.GetComponent<Text>();
        
        StartCoroutine(Post(ServerAddress));
        
    }

    private IEnumerator Post(string url)
    {

        WWW www = new WWW(url);
        yield return StartCoroutine(CheckTimeOut(www, 3f)); //TimeOutSecond = 3s;


        if (www.error != null)
        {
            Debug.Log("HttpPost NG: " + www.error);
            //そもそも接続ができていないとき

        }
        else if (www.isDone)
        {
            //送られてきたデータをテキストに反映
            string json_code = www.text;
            IList user_list = (IList)Json.Deserialize(json_code); //jsonをIList型にキャスト
            int rankAll = 0;
            
            foreach (IDictionary data in user_list)
            {
                rankAll++;
                var score = (string)data["SCORE"];
                float user_score = float.Parse(score);
                
                if (ResultScore < user_score)
                {
                    rankText.text = ""+ rankAll;
                    scoreText.text = "" + ResultScore;
                    break;
                }
            }
        }
    }

    private IEnumerator CheckTimeOut(WWW www, float timeout)
    {
        float requestTime = Time.time;

        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
                yield return null;
            else
            {
                Debug.Log("TimeOut");  //タイムアウト
                //タイムアウト処理
                //
                break;
            }
        }
        yield return null;
    }

    /*
     * 以下、PHPへデータをPOST 
     */

   public void SubmitButton_push()
   {
       StartCoroutine("post_data");
   }

   private IEnumerator post_data()
   {
        textCheck = input.text;//入力された名前をstringに

        if (string.IsNullOrEmpty(textCheck))//入力された名前がnullもしくは空文字ならデフォルトで登録
        {
            textCheck = "NONAME";
        }

        string sendUrl = "kazmyft2.php.xdomain.jp/sendNewDate.php";
        WWWForm form = new WWWForm();
        form.AddField("name", textCheck); //Input Fieldから渡された名前
        form.AddField("score", ResultScore); //クリアTime


        WWW post = new WWW(sendUrl, form);
        
        yield return post;
        SceneManager.LoadScene("OP");//タイトル画面に戻る
    }
}
