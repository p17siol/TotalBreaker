using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private int cnt = 0;
    private int spawnNum;
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

/*    public void GetSpawnNum(int value)
    {
        spawnNum = value;
        //Debug.Log("htiPoints = " + hitPoints);
    }*/
    
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Debug.Log("Ball destroyed forn not colliding anymore");
            Destroy(coll.gameObject);
        }
    }

    /*public void DeleteAll()
    {
        int counter = 0;
        Debug.Log("deleteAll executed");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (!ball.GetComponent<Ball>().isColliding)
            {
                counter++;
                Destroy(ball);
            }
        }
        Debug.Log("Xebarkes deleted = " + cnt);
    }*/

}
