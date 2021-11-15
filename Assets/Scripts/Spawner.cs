using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawning;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;
    [SerializeField] private bool stopSpawning = false;

    private Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mousePostoWorld = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePostoWorld;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    void SpawnObject()
    {
        Vector2 dir = (GetMousePosition() - (Vector2)transform.position).normalized;
        GameObject currentBall = Instantiate(spawning, transform.position, transform.rotation);
        //Vector2 dir1 = dir;
        currentBall.GetComponent<Ball>().Fire = dir;
        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }
}
