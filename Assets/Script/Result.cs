using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI用
[RequireComponent(typeof(ScoreManager))]
public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_scoreText;
    [SerializeField] TextMeshProUGUI m_rankText;
    [SerializeField] ScoreManager m_scoreManager;
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
        List<int> ranking = new List<int>();
        ranking.Add(m_scoreManager.scoreRanking1);
        ranking.Add(m_scoreManager.scoreRanking2);
        ranking.Add(m_scoreManager.scoreRanking3);
        ranking.Add(resultHitpoint);
        //ソート
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if((j > 0) && (ranking[j - 1] > ranking[j]))
                {
                    Swap( ranking[j - 1], ranking[j]);
                }
                else
                {
                    break;
                }
            }
        }
        ranking.RemoveAt(4);//四番目は削除

        m_scoreManager.scoreRanking1 += ranking[1];
        m_scoreManager.scoreRanking2 += ranking[2];
        m_scoreManager.scoreRanking3 += ranking[3];


    }
    void Swap(int x,  int y)
    {
        int tmp;
        tmp = x;
        x = y;
        y = tmp;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
