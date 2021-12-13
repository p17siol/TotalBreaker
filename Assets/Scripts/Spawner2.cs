using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int spawnNum;
    //[SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;
    // Start is called before the first frame update


    private Vector2 GetMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 mousePostoWorld = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePostoWorld;
    }
    public bool IsBurstActive()
    {
        //FindObjectOfType<DestroyBall>().DeleteAll();
        Ball[] balls = FindObjectsOfType<Ball>();
        Debug.Log("balls left= " + balls.Length);
        return balls.Length > 0;
    }

    IEnumerator SpawnBalls()
    {
        Vector2 dir = (GetMousePosition() - (Vector2)transform.position).normalized;
        for (int i = 0; i < spawnNum; i++)
        {
            //Vector2 dir = (GetMousePosition() - (Vector2)transform.position).normalized;
            GameObject ball = Instantiate(ballPrefab,transform.position,transform.rotation);
            ball.GetComponent<Ball>().Fire = dir;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsBurstActive())
            {
                StartCoroutine(SpawnBalls());
            }
        }
    }
}
