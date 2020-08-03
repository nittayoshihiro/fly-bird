using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D m_rd2; //変数として使う
    [SerializeField] int power = 5;//飛ぶ力
    [SerializeField] GameObject m_downEffect;　//やられたエフェクト
    public AudioClip sound1; //AudioClip 変数にする
    AudioSource audioSource; //AudioSource 変数にする
    [SerializeField] int point; //ポイントを入れる
    
    // Start is called before the first frame update
    void Start()
    {
        m_rd2 = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))　//クリックされたら
        {
            Debug.Log("タッチされました");
            m_rd2.velocity = Vector2.up * power; //力を加える
            audioSource.PlayOneShot(sound1);　//サウンドを流す（効果音）
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block") //Blockをタグがついていたら
        {
            Die();　//破棄する
        }
        
        if (collision.gameObject.tag == "Score")　//Scoreをタグがついていたら
       　{
            GameObject name = GameObject.Find("GameManager"); //GameManagerという名のゲームオブジェクトを探す
            if (name)
            {
                GameManager getC = name.GetComponent<GameManager>(); //GameManagerという名のゲームオブジェクトからGameManagerのコンポーネント
                if (getC)
                {
                    getC.AddScore(point); //ポイント追加
                }
            }
        }

    }

    /// <summary>
    /// やられた時に呼び出す
    /// </summary>
    void Die()
    {
        // GameManager にやられたことを知らせる
        GameObject gameManagerObject = GameObject.Find("GameManager");　//GameManagerという名のゲームオブジェクトを探す
        if (gameManagerObject)
        {
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();　//GameManagerという名のゲームオブジェクトからGameManagerのコンポーネント
            if (gameManager)
            {
                gameManager.PlayerDead();　//playerを消す
            }
        }

        // 爆発エフェクトを置く
        if (m_downEffect)
        {
            Instantiate(m_downEffect, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);   // オブジェクトを破棄する
    }
}
