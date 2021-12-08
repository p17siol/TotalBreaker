using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float xMin = 11.911f;
    private float xMax = 23.009f;
    private float yMin = 0.73f;
    private float yMax = 16.254f;
    private int cnt = 0;
    private void Update()
    {
        if (transform.position.x > xMax || transform.position.x < xMin
                || transform.position.y > yMax || transform.position.y < yMin)
        {
            Destroy(this);
            cnt++;
            Debug.Log(cnt + "out of bounds destroyed!!!");
        }
    }
    
}
