using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject score_object = null; // Textオブジェクト
    public int score_num = 0; // スコア変数
    public int scoreRanking1;
    public int scoreRanking2;
    public int scoreRanking3;

    // 初期化時の処理
    void Start()
    {
        // スコアのロード
        score_num = PlayerPrefs.GetInt("SCORE", 0);
        scoreRanking1 = PlayerPrefs.GetInt("Score",0);
        scoreRanking2 = PlayerPrefs.GetInt("Score", 0);
        scoreRanking3 = PlayerPrefs.GetInt("Score", 0);
    }
    // 削除時の処理
    void OnDestroy()
    {
        // スコアを保存
        PlayerPrefs.SetInt("SCORE", score_num);
        PlayerPrefs.SetInt("SCORE", scoreRanking1);
        PlayerPrefs.SetInt("SCORE", scoreRanking2);
        PlayerPrefs.SetInt("SCORE", scoreRanking3);
        PlayerPrefs.Save();
    }
    // 更新
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        //score_text.text = "Score:" + score_num;
        //score_num += 1; // とりあえず1加算し続けてみる
        score_text.text = "1," + scoreRanking1+"\n" 
            + "2," + scoreRanking2 + "\n"
            + "3," + scoreRanking3;
    }
}