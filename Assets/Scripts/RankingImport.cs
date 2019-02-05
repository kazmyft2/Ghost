using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;

public class RankingImport : MonoBehaviour {
    public Text[] Ranking = new Text[10];

    public string ServerAddress = "kazmyft2.php.xdomain.jp/selecttest.php";  //selecttest.phpを指定　今回のアドレスはlocalhost


    // Use this for initialization
    void Start () {
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
            int rank = 0;
            foreach (IDictionary data in user_list)
            {
                rank++;
                var user_name = (string)data["NAME"];
                var score = (string)data["SCORE"];

                float user_score = float.Parse(score);

                switch (rank)
                {
                    case 1:
                        Ranking[0].text = user_score + " / " + user_name;
                        break;
                    case 2:
                        Ranking[1].text = user_score + " / " + user_name;
                        break;
                    case 3:
                        Ranking[2].text = user_score + " / " + user_name;
                        break;
                    case 4:
                        Ranking[3].text = user_score + " / " + user_name;
                        break;
                    case 5:
                        Ranking[4].text = user_score + " / " + user_name;
                        break;
                    case 6:
                        Ranking[5].text = user_score + " / " + user_name;
                        break;
                    case 7:
                        Ranking[6].text = user_score + " / " + user_name;
                        break;
                    case 8:
                        Ranking[7].text = user_score + " / " + user_name;
                        break;
                    case 9:
                        Ranking[8].text = user_score + " / " + user_name;
                        break;
                    case 10:
                        Ranking[9].text = user_score + " / " + user_name;
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
                //
                break;
            }
        }
        yield return null;
    }
   

}
