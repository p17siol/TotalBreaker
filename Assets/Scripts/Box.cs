using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitText = null;
    [SerializeField ]private int hitPoints;
    
    private int initHitPoints;
    // Start is called before the first frame update

    private void Start()
    {
        //initHitPoints = hitPoints;
        //hitText.text = hitPoints.ToString();
        
    }

    public void SetHitPoints(int value)
    {
        hitPoints = value;
        Debug.Log("htiPoints = " + hitPoints);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When target is hit
        if (collision.gameObject.tag == "Ball")
        {
            if (hitPoints == 0)
                Destroy(gameObject);
            else
            {
                Debug.Log("Box hited!!" + hitPoints);
                hitPoints--;
            }

        }
    }   
    /*void UpdateBox()
    {
     
    }*/
}
