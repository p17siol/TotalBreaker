using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int power;
    private Vector2 fire;
    public bool isColliding = true;
    // Start is called before the first frame update
    
    public void ReadyToDestroy()
    {
        isColliding = false;
    }

    public Vector2 Fire
    {
        get { return fire; }
        set { fire = value; }
    }

    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(fire * power, ForceMode2D.Impulse);
    }

}