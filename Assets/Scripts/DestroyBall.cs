using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private int destroyedBalls = 0;
    Spawner spawner;
    ArrayToBox moveDown;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        moveDown = FindObjectOfType<ArrayToBox>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ball")
        {
            destroyedBalls++;
            Debug.Log("Ball destroyed" + destroyedBalls);
            Destroy(col.gameObject);
        }
        Debug.Log("Balls destroyed = " + destroyedBalls);
    }

    void Update()
    {
        if (destroyedBalls >= spawner.GetSpawnNum())
        {
            moveDown.NextLevel();
                destroyedBalls = 0;
        }
    }
}
