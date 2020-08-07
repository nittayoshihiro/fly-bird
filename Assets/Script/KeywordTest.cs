using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class KeywordTest : MonoBehaviour
{
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
            Magic1();
            keyCon.hasRecognized[1] = false;
        }
        if (keyCon.hasRecognized[2])
        {
            Debug.Log("keyword[2] was recognized");
            Magic2();
            keyCon.hasRecognized[2] = false;
        }
        if (keyCon.hasRecognized[3])
        {
            Debug.Log("keyword[3] was recognized");
            shield();
            keyCon.hasRecognized[3] = false;
        }


    }

    void Fry()
    {
        
    }

    void Magic1()
    {
        
    }

    void Magic2()
    {

    }

    void shield()
    {

    }
}
