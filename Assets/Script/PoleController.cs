using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoleController : MonoBehaviour
{
    float deathtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BC_Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        GameObject name = GameObject.Find("Player(Clone)"); //Playerという名のゲームオブジェクトを探す
        if (name)
        {
            //Debug.Log(name);
            PlayerController get = name.GetComponent<PlayerController>(); //Playerrという名のゲームオブジェクトからPlayerControllerのコンポーネント
            if (get)
            {
                //Debug.Log(get.m_pole);
                if (get.m_pole)
                {
                    this.gameObject.transform.Translate(-0.03f, 0, 0); //X 軸に移動
                    deathtime += Time.deltaTime;
                    if (deathtime > 6)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
