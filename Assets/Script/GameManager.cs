using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体を管理するクラス。
/// EnemyGenerator と同じ GameObject にアタッチする必要がある。
/// </summary>
[RequireComponent(typeof(EnemyGenerator),typeof(PlayerController))]
public class GameManager : MonoBehaviour
{
    /// <summary>得点</summary>
    int m_score;
    /// <summary>自機のプレハブを指定する</summary>
    [SerializeField] GameObject m_playerPrefab;
    /// <summary>ゲームの初期化が終わってからゲームが始まるまでの待ち時間</summary>
    [SerializeField] float m_waitTimeUntilGameStarts = 5f;
    /// <summary>自機がやられてからゲームの再初期化をするまでの待ち時間</summary>
    [SerializeField] float m_waitTimeAfterPlayerDeath = 3f;
    /// <summary>ゲームオーバー後に遷移するシーン（タイトル画面）のシーン名</summary>
    [SerializeField] string m_titleSceneName = "Title";
    /// <summary>EnemyGenerator を保持しておく変数</summary>
    EnemyGenerator m_enemyGenerator;
    /// <summary>スコア表示用 Text</summary>
    [SerializeField] UnityEngine.UI.Text m_scoreText;
    //スコア移動法
    public static int scorepoint = 0;
    /// <summary>タイマー</summary>
    float m_timer;
    //[SerializeField] UnityEngine.UI.Text m_conbo;
    /// <summary>ゲームの状態</summary>
    // int m_status = 0;    // 0: ゲーム初期化前, 1: ゲーム初期化済み、ゲーム開始前, 2: ゲーム中, 3: プレイヤーがやられた
    // ↑ Enum で表現するように変更する
    GameState m_status = GameState.NonInitialized;
    //private object Initiate;
    //public static int hitpoint = 0;
    // getter
    public static int getHitPoint()
    {
        return scorepoint;
    }

    void Start()
    {
        // EnemyGenerator を取得しておき、まずは敵の生成をしない。
        m_enemyGenerator = GetComponent<EnemyGenerator>();
        m_enemyGenerator.StopGeneration();
        AddScore(0);
        scorepoint = 0;
    }

    void Update()
    {
        //hitpoint = m_score;

        // if (m_status == 0)  // 初期化前
        switch (m_status)   // ゲームの状態によって処理をわける
        {
            case GameState.NonInitialized:
                Debug.Log("Initialize.");
                Instantiate(m_playerPrefab);    // プレイヤーを生成する
                m_status = GameState.Initialized;   // ステータスを初期化済みにする
                break;
            // }
            // else if (m_status == 1) // 初期化済み、開始前
            // {
            case GameState.Initialized:
                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeUntilGameStarts)    // 待つ
                {
                    m_timer = 0f;   // タイマーをリセットする
                    m_status = GameState.InGame;   // ステータスをゲーム中にする
                    m_enemyGenerator.StartGeneration(); // 敵の生成を開始する
                }
                break;
            // }
            // else if (m_status == 3) // プレイヤーがやられた
            // {
            case GameState.PlayerDead:
                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeAfterPlayerDeath)   // 待つ
                {
                    GameOver(); // 残機がもうない場合はゲームオーバーにする
                }
                break;
        }

    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        Debug.Log("スコア");
            m_score += score;
            scorepoint += score;
        m_scoreText.text = "Score: " + m_score.ToString("d0");
    }

    /// <summary>
    /// プレイヤーがやられた時、外部から呼ばれる関数
    /// </summary>
    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_enemyGenerator.StopGeneration();  // 敵の生成を止める
        m_status = GameState.PlayerDead;   // ステータスをプレイヤーがやられた状態に更新する
    }
    /// <summary>
    /// シーン上にある敵と敵の弾を消す
    /// </summary>
    void ClearScene()
    {
        // 敵を消す
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Block");
        foreach (var go in goArray)
        {
            Destroy(go);
        }
    }

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    void GameOver()
    {
        //Time.timeScale = 0;
        Debug.Log("Game over. Return to title scene.");
        Initiate.Fade(m_titleSceneName, Color.black, 1.0f); // タイトル画面に戻る
    }
}

enum GameState
{
    /// <summary>ゲーム初期化前</summary>
    NonInitialized,
    /// <summary>ゲーム初期化済み、ゲーム開始前</summary>
    Initialized,
    /// <summary>ゲーム中</summary>
    InGame,
    /// <summary>プレイヤーがやられた</summary>
    PlayerDead,
    /// <summary>プレイヤーがやられた</summary>
    Result,
}
