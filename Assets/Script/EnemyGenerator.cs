using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するコンポーネント
/// 適当なオブジェクトに追加して使う
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>敵として生成するプレハブの配列</summary>
    [SerializeField] GameObject[] m_enemyPrefabs;
    /// <summary>ランダム変数</summary>
    int ram;
    /// <summary>敵のプレハブの配列に使うインデックス</summary>
    int m_index;
    /// <summary>オンにしておくと、実行と同時に生成を始める</summary>
    [SerializeField] bool m_playOnStart = true;
    /// <summary>動作中フラグ</summary>
    bool m_isWorking;
    /// <summary>何秒ごとに生成k</summary>
    [SerializeField] int EnemyTime = 5;
    float m_Time;

    void Start()
    {
        // フラグがオンなら生成を始める
        if (m_playOnStart)
        {
            StartGeneration();
        }
        m_index = 0;
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > EnemyTime)　//何秒(EnemyTime)ごとに実行
        {
            ram = Random.Range(0, 3);//0から3のランダム
            Debug.Log(ram);
            switch (ram)
            {
                case 0:
                    if (m_index != 0)//インデックスが最小の場合変更しない
                    {
                        m_index--;
                    }
                    break;
                case 1:
                    if (m_index == 0)
                    {
                        m_index++;
                    }
                    break;
                case 2:
                    if (m_index != m_enemyPrefabs.Length)//インデックスが最大の場合変更しない
                    {
                        m_index++;
                    }
                    break;
              
            }
            GameObject go = Instantiate(m_enemyPrefabs[m_index], gameObject.transform.position, m_enemyPrefabs[m_index].transform.rotation);    // 配列からプレハブを一つインスタンス化する
            go.transform.SetParent(this.transform); // インスタンス化したオブジェクトを自分の子供にする
            m_Time = 0;
        }
    }

    /// <summary>敵の生成を開始する</summary>
    public void StartGeneration()
    {
        // フラグを立てる
        m_isWorking = true;
    }

    /// <summary>子オブジェクトを全て破棄する</summary>
    //void DestroyChildren()
    //{
    //    Transform[] txes = transform.GetComponentsInChildren<Transform>();
    //    foreach (var tx in txes)
    //    {
    //        if (tx != this.transform)
    //        {
    //            Destroy(tx.gameObject);
    //        }
    //    }
    //}

    /// <summary>敵の生成を停止する</summary>
    public void StopGeneration()
    {
        // フラグを倒す
        m_isWorking = false;
    }
}
