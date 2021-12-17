using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] SpriteRenderer blockPrefab;
    [SerializeField] SpriteRenderer borderPrefab;
    [SerializeField] TextMeshProUGUI hitText;

    //private int initHitPoints;
    private int hitPoints;
    private int teamColor;
    private Color32 blockColor;
    private Color32 borderColor;
    
    // Start is called before the first frame update
    void Start()
    {
        borderColor = borderPrefab.color;
        blockPrefab.color = blockColor;
        //initHitPoints = hitPoints;
        hitText.text = hitPoints.ToString();
    }
    
    public void SetTeamColor(Color colorValue)
    {
        blockColor = colorValue;
        Debug.Log("blockColor = " + blockColor);
    }
    public void SetHitPoints(int value)
    {
        hitPoints = value;
        Debug.Log("hitPoints = " + hitPoints);
    }

    private void Flashing()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        for (int i = 255; i >= 55; i -= 40)
        {
            borderColor.a = (byte)i;
            blockColor.a = (byte)i;
            borderPrefab.color = borderColor;
            blockPrefab.color = blockColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeIn()
    {
        for (int j = 55; j <= 255; j += 40)
        {
            borderColor.a = (byte)j;
            blockColor.a = (byte)j;
            borderPrefab.color = borderColor;
            blockPrefab.color = blockColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When target is hit
        if (collision.gameObject.tag == "Ball")
        {
            if (hitPoints < 2)
                Destroy(gameObject);
            else
            {
                Debug.Log("Box hited!!" + hitPoints);
                hitPoints--;
                Flashing();
                hitText.text = hitPoints.ToString();
            }
        }
    }   
    
}
