using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3 : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int spawnNum;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;
    private Vector2 dir;
    ArrayToBox moveDown;
    private Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mousePostoWorld = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePostoWorld;
    }
    // Start is called before the first frame update

    private int cnt = 0;

    private void Start()
    {
        moveDown = FindObjectOfType<ArrayToBox>();
    }

    public int GetSpawnNum()
    {
        return spawnNum;
    }

    public bool IsBurstActive()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        Debug.Log("balls left= " + balls.Length);
        return balls.Length > 0;
    }

    void SpawnObject()
    {
        GameObject currentBall = Instantiate(ballPrefab, transform.position, transform.rotation);
        currentBall.GetComponent<Ball>().Fire = dir;
        cnt++;
        
        if (cnt == spawnNum)
        {
            cnt = 0;
            CancelInvoke("SpawnObject");
            if(IsBurstActive())
            { 
                moveDown.NextLevel();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsBurstActive())
            {
                dir = (GetMousePosition() - (Vector2)transform.position).normalized;
                InvokeRepeating("SpawnObject", spawnDelay, spawnTime);
            }
            
        }

    }
}
