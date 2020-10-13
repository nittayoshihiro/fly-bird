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
    [SerializeField] Text m_rankingText;
    [SerializeField] GameObject m_enter;
    [SerializeField] GameObject m_inputFineld;
    [SerializeField] GameObject m_panel;
    /// <summary>名前入れ</summary>
    [SerializeField] InputField m_nameSpece;
    public GameObject score_object = null; // Textオブジェクト
    List<RankingData> dataList = new List<RankingData>();//ランキング用リスト
    RankingData[] newList;
    int resultHitpoint; //ポイント結果

    //ランキングデータ
    [System.Serializable]
    public class RankingData
    {
        [SerializeField]
        public string playerName;
        [SerializeField]
        public int score;

        public RankingData(string name, int score)
        {
            this.score = score;
            this.playerName = name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        m_enter.SetActive(false);
        m_inputFineld.SetActive(false);

        resultHitpoint = GameManager.getHitPoint();　//GameManagerからgetHitPointを持ってくる
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

        //保存先に何もない時
        RankingData one = new RankingData("---", 0);
        RankingData two = new RankingData("---", 0);
        RankingData three = new RankingData("---", 0);

        string savePath = "/save1.json";

        if (System.IO.Directory.Exists(Application.dataPath))
        {
            if (!System.IO.File.Exists(Application.dataPath + savePath))//データがなかったら
            {
                //セーブ
                StreamWriter fistWriter1;
                string fistJsonstr1 = JsonUtility.ToJson(one);
                fistWriter1 = new StreamWriter(Application.dataPath + "/save1.json", false);
                fistWriter1.Write(fistJsonstr1);
                fistWriter1.Flush();
                fistWriter1.Close();

                //セーブ
                StreamWriter fistWriter2;
                string fistJsonstr2 = JsonUtility.ToJson(two);
                fistWriter2 = new StreamWriter(Application.dataPath + "/save2.json", false);
                fistWriter2.Write(fistJsonstr2);
                fistWriter2.Flush();
                fistWriter2.Close();

                //セーブ
                StreamWriter fistWriter3;
                string fistJsonstr3 = JsonUtility.ToJson(three);
                fistWriter3 = new StreamWriter(Application.dataPath + "/save3.json", false);
                fistWriter3.Write(fistJsonstr3);
                fistWriter3.Flush();
                fistWriter3.Close();

            }
        }
        

        //ロード処理
        string datastr1;
        StreamReader reader1;
        reader1 = new StreamReader(Application.dataPath + "/save1.json");
        datastr1 = reader1.ReadToEnd();
        reader1.Close();
        one = JsonUtility.FromJson<RankingData>(datastr1);

        //ロード処理
        StreamReader reader2;
        reader2 = new StreamReader(Application.dataPath + "/save2.json");
        string datastr2 = reader2.ReadToEnd();
        reader2.Close();
        two = JsonUtility.FromJson<RankingData>(datastr2);

        //ロード処理
        StreamReader reader3;
        reader3 = new StreamReader(Application.dataPath + "/save3.json");
        string datastr3 = reader3.ReadToEnd();
        reader3.Close();
        three = JsonUtility.FromJson<RankingData>(datastr3);

        dataList.Add(one);
        dataList.Add(two);
        dataList.Add(three);

        //ランキング圏内だったら名前を聞く
        if (dataList[2].score < resultHitpoint)
        {
            m_panel.SetActive(false);
            //名前を聞く
            m_enter.SetActive(true);
            m_inputFineld.SetActive(true);

        }

        string json = JsonUtility.ToJson(dataList);
        Debug.Log(json);
        json = JsonHelper.ToJson<RankingData>(dataList.ToArray());
        Debug.Log(json);
       　newList = JsonHelper.FromJson<RankingData>(json);
        //ランキングを表示
        m_rankingText.text = "1." + newList[0].playerName + newList[0].score
            + "\n2." + newList[1].playerName + newList[1].score
            + "\n3." + newList[2].playerName + newList[2].score;

       

    }

    public void OnClickEvent()
    {

        RakingDateSort();

        string json = JsonUtility.ToJson(dataList);
        Debug.Log(json);
        json = JsonHelper.ToJson<RankingData>(dataList.ToArray());
        Debug.Log(json);
        newList = JsonHelper.FromJson<RankingData>(json);
        //ランキングを表示
        m_rankingText.text = "1." + newList[0].playerName + newList[0].score
            + "\n2." + newList[1].playerName + newList[1].score
            + "\n3." + newList[2].playerName + newList[2].score;

        //セーブ
        StreamWriter writer1;
        string jsonstr1 = JsonUtility.ToJson(newList[0]);
        writer1 = new StreamWriter(Application.dataPath + "/save1.json", false);
        writer1.Write(jsonstr1);
        writer1.Flush();
        writer1.Close();

        //セーブ
        StreamWriter writer2;
        string jsonstr2 = JsonUtility.ToJson(newList[1]);
        writer2 = new StreamWriter(Application.dataPath + "/save2.json", false);
        writer2.Write(jsonstr2);
        writer2.Flush();
        writer2.Close();

        //セーブ
        StreamWriter writer3;
        string jsonstr3 = JsonUtility.ToJson(newList[2]);
        writer3 = new StreamWriter(Application.dataPath + "/save3.json", false);
        writer3.Write(jsonstr3);
        writer3.Flush();
        writer3.Close();

        m_enter.SetActive(false);
        m_inputFineld.SetActive(false);
        m_panel.SetActive(true);
    }

    void RakingDateSort()
    {
        RankingData newPlayer = new RankingData(m_nameSpece.text, resultHitpoint);

        if (resultHitpoint >= dataList[0].score)
        {
            dataList.Insert(0,newPlayer);
        }
        else if (resultHitpoint >= dataList[1].score)
        {
            dataList.Insert(1,newPlayer);
        }
        else if (resultHitpoint >= dataList[2].score)
        {
            dataList.Insert(2,newPlayer);
        }
        dataList.RemoveAt(3);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
