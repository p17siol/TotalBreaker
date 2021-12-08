using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int spawnNum;
    [SerializeField] private float spawnDelay;
    
    private Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mousePostoWorld = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePostoWorld;
    }
    // Start is called before the first frame update
    private void Start()
    {
       /* Debug.Log("Game 's running!!!!");
        if (Input.GetMouseButtonDown(0))
        {
            SpawnNextBurst();
            //FindObjectOfType<DestroyBall>().DeleteAll();
        }*/
    }

    public bool IsBurstActive()
    {
        //FindObjectOfType<DestroyBall>().DeleteAll();
        Ball[] balls = FindObjectsOfType<Ball>();
        Debug.Log("balls left= " + balls.Length);
        return balls.Length > 0;
    }

    //Spawns next burst
    public void SpawnNextBurst()
    {
        Debug.Log("Start is working");
        if (!IsBurstActive())
        {
            List<GameObject> balls = BallsBurst(spawnNum);
            Debug.Log("balls = " + balls.Count);
            StartCoroutine(EnableBurst(balls));
        }
    }

    //private List<GameObject> BallsBurst(int spawnNum)
    private List<GameObject> BallsBurst(int spawnNum)
    {
        List<GameObject> balls = new List<GameObject>();
        for (int i = 0; i < spawnNum; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
            balls.Add(ball);
        }
        return balls;
    }

    IEnumerator EnableBurst(List<GameObject> balls) 
    {
        //FindObjectOfType<DestroyBall>().DeleteAll();
        int a = balls.Count;
        Vector2 dir = (GetMousePosition() - (Vector2)transform.position).normalized;
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Ball>().Fire = dir;
            a--;
            Debug.Log("balls left = " + a);
            yield return new WaitForSeconds(spawnDelay);  
        }
        //FindObjectOfType<DestroyBall>().DeleteAll();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //FindObjectOfType<DestroyBall>().DeleteAll();
            Debug.Log("button pressed!");
            SpawnNextBurst();
            //FindObjectOfType<DestroyBall>().DeleteAll();
        }
    }
}
