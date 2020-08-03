using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.Text m_rankText;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
