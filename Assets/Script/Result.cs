using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //TextMeshProUGUI用
using System.IO; //StreamWriterを使うためのシステム

public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_scoreText;
    [SerializeField] TextMeshProUGUI m_rankText;
    /// <summary>名前入れ</summary>
    [SerializeField] InputField inputArea;
    public GameObject score_object = null; // Textオブジェクト
    string ranking;
    //プレイヤーデータ
    [System.Serializable]
    public class PlayerData
    {
        public int resultScore;
        public string playerName;
    }
    //ランキングデータ
    [System.Serializable]
    public class RankingDate
    {
        List<PlayerData> ranking = new List<PlayerData>(){};
    }
    RankingDate m_rankingDate = new RankingDate();//ランキングデータ

    [System.Serializable]
    public class RankingData
    {
        [SerializeField]
        string playerName;
        [SerializeField]
        int score;

        public RankingData(string name, int score)
        {
            this.score = score;
            this.playerName = name;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        int resultHitpoint = GameManager.getHitPoint();　//GameManagerからgetHitPointを持ってくる
        Debug.Log("スコア");
        m_scoreText.text = ("Score:\n") + resultHitpoint.ToString("0"); //Score:改行をしてresultHitpointを入れる
        if (resultHitpoint >= 500)
        {
            m_rankText.text = "S";
        }
        else if (resultHitpoint >= 250)
        {
            m_rankText.text = "A";
        }
        else if (resultHitpoint >= 100)
        {
            m_rankText.text = "B";
        }
        else
        {
            m_rankText.text = "C";
        }
        //テスト
        RankingData a = new RankingData("Nitta", 150);
        RankingData b = new RankingData("Kato",200);
        RankingData c = new RankingData("Suzuki", 300);
        string d = JsonUtility.ToJson(a);
        Debug.Log(d);

        List<RankingData> dataList = new List<RankingData>();
        dataList.Add(a);
        dataList.Add(b);
        dataList.Add(c);
        string json = JsonUtility.ToJson(dataList);
        Debug.Log(json);
        json = JsonHelper.ToJson<RankingData>(dataList.ToArray());
        Debug.Log(json);
        RankingData[] newList = JsonHelper.FromJson<RankingData>(json);


        //今までの順位を持ってくる
        string readText = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/save" + ranking + ".json");
        readText = reader.ReadToEnd();
        reader.Close();

        m_rankingDate = JsonUtility.FromJson<RankingDate>(readText);



        // オブジェクトからTextコンポーネントを取得
        TextMeshProUGUI score_text = score_object.GetComponent<TextMeshProUGUI>();
        score_text.text = "1,"  + "\n" + "2," +  "\n" + "3," ;
        //ランキングを保存
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(m_rankingDate);
        writer = new StreamWriter(Application.dataPath + "/save" +ranking + ".json", false);
    }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
