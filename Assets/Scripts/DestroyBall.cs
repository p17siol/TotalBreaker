using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private int cnt = 0;
    //private int spawnNum;
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ball")
        {
            cnt++;
            col.gameObject.GetComponent<Ball>().ReadyToDestroy();
            Debug.Log("Ball destroyed" + cnt);
            Destroy(col.gameObject);
        }
        Debug.Log("Balls destroyed = " + cnt);

    }
}
