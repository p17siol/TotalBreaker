using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayToBox : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform parentObject;
    //Start Position indicates where the first block should spawn.
    [SerializeField] private Vector2 startPos;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
     
    //private int[,] array2D = new intMakeArray();

    private int[,] MakeArray()
    {
        int height = rows;        /*Number of rows*/
        int width = columns;      /*Number of columns*/

        // Upper limit of hit range per team
        //int redTeam = 100; int greenTeam = 120; int blueTeam = 140; int purpleTeam = 160; int orangeTeam = 180;
        int redTeam = 60; int greenTeam = 80; int blueTeam = 100; int purpleTeam = 120; int orangeTeam = 140;

        // Percentage that defines the lower limit of hits range per team
        float percentage = 0.15f;

        System.Random rand = new System.Random();

        // Calculate lower limit of hits per team
        int TeamLow(int team, float percent)
        {
            int a = team - (int)(team * percent);
            return a;
        }

        // Calculate random number of boxes per line
        int BoxNumLine(int w)
        {
            //absolute min number of boxes per line
            int minInit = (int)(w / 2) + 1;
            int threshMax = w + 1; //max limit fop upper range
            int threshMin = minInit + 2; //min limit for lower range

            //Select a value randomly between minInit and min threshold
            int minNum = rand.Next(minInit, threshMin + 1);
            // Select a value randomly from higher values
            int maxNum = rand.Next(width - 2, threshMax);

            int boxNum = rand.Next(minNum, maxNum);
            return boxNum;
        }

        // Calculate random initial position of boxes
        int BoxLoc(int w, int boxNumber)
        {
            // Select a value randomly between 0 and width - boxnumber
            int boxStart = rand.Next(0, (w - boxNumber) + 1);
            return boxStart;
        }

        // Gets values used for calculation of placing special boxes
        int[] BoxSpecial(int boxes, int pos, int team, int hits, int line)
        {
            int[] tmpspecial = { boxes, pos, team, hits, line };
            return tmpspecial;
        }

        // Calculates random position of a special box in a row
        int PosSpecial(int boxes, int pos)
        {
            //System.Random rand = new System.Random();
            int position = rand.Next(pos, (boxes + pos));
            return position;
        }

        //Set color teamss
        int[] teamsList = { 0, 1, 2, 3, 4 };
        
        // 2D array to associate team with colors 
        int[,] teamsComplete = { { 0, redTeam }, { 1, greenTeam }, { 2, blueTeam }, { 3, purpleTeam }, { 4, orangeTeam } };

        // Create a zero matrix with dimensions of board
        int[,] pista = new int[height, width];
        for (int j = 0; j < height; j++)
            for (int k = 0; k < width; k++)
                pista[j, k] = 0;

        //System.Random rand = new System.Random();
        int randBoxNum; int boxPos; int randTeam; int randHits;
        int[] current = { 0, 0, 0, 0, 0 }; int[] special = { 0, 0, 0, 0, 0 };

        for (int x = 0; x < height; x++)
        {
            // Get color team
            randTeam = rand.Next(0, teamsList.Length);
            // Get number of boxes
            randBoxNum = BoxNumLine(width);
            // Get position of boxes
            boxPos = BoxLoc(width, randBoxNum);
            // Get number of hits
            randHits = rand.Next(TeamLow(teamsComplete[randTeam, 1], percentage), teamsComplete[randTeam, 1]);
            
            // Place boxes randomly to available positions 
            for (int y = 0; y < width; y++)
            {
                if (y >= boxPos && y < (randBoxNum + boxPos))
                    pista[x, y] = randHits;
            }

            // Check if must add a special box
            if (randTeam > (teamsList.Length - 3))
            {
                // Check if previous new and current lines are high color teams 
                special = BoxSpecial(randBoxNum, boxPos, randTeam, randHits, x);
                if (current[2] > (teamsList.Length - 3))
                {
                    // Get a random position to put special box
                    int a = PosSpecial(randBoxNum, boxPos);
                    pista[x, a] = 22;
                }
                // Update current value
                current = special;
            }
            else
            {
                // Update current value
                current = BoxSpecial(randBoxNum, boxPos, randTeam, randHits, x);
            }
        }
        return pista;
    }
    
    // Start is called before the first frame update
       
    void Start()
    {
        ArrayToObjects(MakeArray());
    }

    public void ArrayToObjects(int[,] array)
    {
        //Calculate block width and height
        float blockWidth = blockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float blockHeight = blockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        //Next Position indicates where the next block should spawn
        Vector2 nextPos = startPos;
        //Color currentColor =new Color();
                
        //Check all array positions:
        for (int i = array.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                //If array position value is greater than 0: Spawn Object as child of parentObj
                if (array[i, j] > 0)
                {
                    //currentColor = BoxTeamColor(array[i, j]);
                    GameObject block = Instantiate(blockPrefab, nextPos, transform.rotation, parentObject);
                    //Set blocks hitpoints equal to array value
                    block.GetComponent<Box>().SetTeamColor(BoxTeamColor(array[i, j]));
                    block.GetComponent<Box>().SetHitPoints(array[i, j]);
                }
                //Calculate next X spawn position
                nextPos.x += blockWidth;
            }
            //Calculate next Y spawn position
            nextPos.y += blockHeight;
            nextPos.x = startPos.x;
        }
    }

    // Set Box Initial Color
    public Color32 BoxTeamColor(int hitsState)
    {
        int teamColor;
        if (hitsState == 22)
            teamColor = 1;
        else if (hitsState <= 60)
            teamColor = 2;
        else if (hitsState <= 80)
            teamColor = 3;
        else if (hitsState <= 100)
            teamColor = 4;
        else if (hitsState <= 120)
            teamColor = 5;
        else 
            teamColor = 6;

        Color32 boxColor = new Color32();
        Debug.Log("teamColor = " + teamColor);
        switch (teamColor)
        {
            case 1:
                boxColor = new Color32(140, 120, 30, 255);
                break;
            case 2:
                boxColor = new Color32(222, 52, 52, 255);
                break;
            case 3:
                boxColor = new Color32(36, 142, 36, 255);
                break;
            case 4:
                boxColor = new Color32(71, 71, 226, 255);
                break;
            case 5:
                boxColor = new Color32(128, 31, 128, 255);
                break;
            case 6:
                boxColor = new Color32(193, 104, 14, 255);
                break;
            
        }

        return boxColor;
    }

    //Move Blocks Parent down
    public void NextLevel()
    {
        parentObject.position -= new Vector3(0, blockPrefab.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
    }
}
