using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI用
[RequireComponent(typeof(ScoreManager))]
public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_scoreText;
    [SerializeField] TextMeshProUGUI m_rankText;
    ScoreManager m_scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        m_scoreManager = GetComponent<ScoreManager>();
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
        //順位管理
        var ranking = new List<int>();
        ranking.Add(m_scoreManager.scoreRanking1);
        ranking.Add(m_scoreManager.scoreRanking2);
        ranking.Add(m_scoreManager.scoreRanking3);
        foreach (var item in ranking)
        {
            if (item < resultHitpoint)//スコアが高かったら
            {
                //ranking[?] = resultHitpoint;
                break;
            }
        }
        ranking.RemoveAt(4);//四番目は削除
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
