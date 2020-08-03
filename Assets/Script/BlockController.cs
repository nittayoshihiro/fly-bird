using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    float deathtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BC_Start");
        //StartCoroutine(Move())
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(-0.03f, 0, 0); //X 軸に移動
        deathtime += Time.deltaTime;
        if (deathtime > 6)
        {
            Destroy(this.gameObject);
        }
    }
    /*IEnumerator Move()
    {
        float deathtime = 0;
        //Debug.Log("コルーチン入った");
        while (deathtime < 3)
        {
            deathtime += Time.deltaTime;
            //Debug.Log("ループゾーン");
            this.gameObject.transform.Translate(-0.05f, 0, 0); //X 軸に移動
            yield return null;
        }
        Destroy(this.gameObject);
    }*/
}
