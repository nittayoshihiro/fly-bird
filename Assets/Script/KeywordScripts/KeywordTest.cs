﻿using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
[RequireComponent(typeof(PlayerController))]
public class KeywordTest : MonoBehaviour
{
    PlayerController m_playerController;
    private KeywordController keyCon;
    private string[][] keywords;
    // Use this for initialization
    void Start()
    {
        keywords = new string[2][];
        keywords[0] = new string[] { "とべ", "フライ"};//ひらがなでもカタカナでもいい
        keywords[1] = new string[] { "おちろ", "ホール" };

        keyCon = new KeywordController(keywords, true);//keywordControllerのインスタンスを作成
        keyCon.SetKeywords();//KeywordRecognizerにkeywordsを設定する
        keyCon.StartRecognizing(0);//シーン中で音声認識を始めたいときに呼び出す
        keyCon.StartRecognizing(1);

        m_playerController = GetComponent < PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (keyCon.hasRecognized[0])//設定したKeywords[0]の単語らが認識されたらtrueになる
        {
            Debug.Log("keyword[0] was recognized");
            Fry();
            keyCon.hasRecognized[0] = false;
        }
        if (keyCon.hasRecognized[1])
        {
            Debug.Log("keyword[1] was recognized");
            Fall();
            keyCon.hasRecognized[1] = false;
        }

    }

    void Fry()
    {
        m_playerController.Fly();
    }

    void Fall()
    {
        
    }
}
