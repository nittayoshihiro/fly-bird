using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;  // Cinemachine を使うため

public class PlayerController : MonoBehaviour
{
    [SerializeField] int power = 5;//飛ぶ力
    [SerializeField] GameObject m_downEffect;　//やられたエフェクト
    [SerializeField] int point; //ポイントを入れる
    [SerializeField] bool invincible = true; //無敵(off)
    public AudioClip Fiy_sound; //AudioClip 変数にする
    public AudioClip Crash_sound; //AudioClip 変数にする
    public AudioClip Score_sound; //AudioClip 変数にする
    AudioSource audioSource; //AudioSource 変数にする
    Rigidbody2D m_rd2; //変数として使う
    public bool m_pole = true;//オブジェクトの移動on,off
    public bool m_flyVoice = false;
    // Start is called before the first frame update
    void Start()
    {
        m_rd2 = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        //Virtual Camera がプレイヤーを見るように設定する
        CinemachineVirtualCamera vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        if (vCam)
        {
            vCam.Follow = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)||m_flyVoice)　//クリックされたら
        {
            Debug.Log("タッチされました");
            m_rd2.velocity = Vector2.up * power; //力を加える
            audioSource.PlayOneShot(Fiy_sound);　//サウンドを流す（効果音）
            m_flyVoice = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Crash")
        {
            audioSource.PlayOneShot(Crash_sound);　//サウンドを流す（効果音）
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pole") //Poleをタグがついていたら
        {
            Die();　//破棄する
        }

        if (collision.gameObject.tag == "Score")　//Scoreをタグがついていたら
       　{
            audioSource.PlayOneShot(Score_sound);
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

    /// <summary>やられた時に呼び出す</summary>
    void Die()
    {
        if (!invincible)
        {
            // GameManager にやられたことを知らせる
            GameObject gameManagerObject = GameObject.Find("GameManager"); //GameManagerという名のゲームオブジェクトを探す
            if (gameManagerObject)
            {
                GameManager gameManager = gameManagerObject.GetComponent<GameManager>(); //GameManagerという名のゲームオブジェクトからGameManagerのコンポーネント
                if (gameManager)
                {
                    gameManager.PlayerDead(); //playerを消す
                }
            }
            m_pole = false;
            // 爆発エフェクトを置く
            if (m_downEffect)
            {
                Instantiate(m_downEffect, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);   // オブジェクトを破棄する
        }
        
    }
}
