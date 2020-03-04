﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;
using System;

[System.Serializable]
public class TTPlayerState
{
    public int playerIndex;
    public TicTacAgent agentScript;
}
public class GridController : MonoBehaviour
{
    [HideInInspector]
    public List<TTPlayerState> playerStates = new List<TTPlayerState>();

    public GameObject Grid;
    GridElement[] gridElements;
    public TicTacAgent PlayerOh;
    //public NaivePlayer PlayerOh;
    //public TicTacAgent PlayerEx;
    public NaivePlayer PlayerEx;
    int turn;

    public TMP_Text Xwins;
    public TMP_Text Owins;
    public TMP_Text Draws;

    int xwins;
    int owins;
    int draws;


    private void Awake()
    {
        Academy.Instance.OnEnvironmentReset += GridReset;
    }

    private void Start()
    {
        turn = 1;
        gridElements = Grid.transform.GetComponentsInChildren<GridElement>(); 
        GridReset();
        CheckTurn();

        xwins = 0;
        owins = 0;
        draws = 0;
    }

    private void CheckTurn()
    {
        if (turn == 1)
        {
            PlayerOh.PlayTurn();
        }
        else
        {
            PlayerEx.PlayTurn();
        }
    }

    public void EndTurn()
    {
        turn *= -1;

        int count = 0;
        bool someoneWon = false;

        foreach (var element in gridElements)
        {
            if (element.State == 0)
            {
                ++count;
            }
        }

        if (gridElements[0].State == gridElements[1].State && gridElements[1].State == gridElements[2].State && gridElements[0].State != 0)
        {
            AssignReward(gridElements[0].State);
            someoneWon = true;
        }
        else if (gridElements[3].State == gridElements[4].State && gridElements[4].State == gridElements[5].State && gridElements[3].State != 0)
        {
            AssignReward(gridElements[3].State);
            someoneWon = true;
        }
        else if (gridElements[6].State == gridElements[7].State && gridElements[7].State == gridElements[8].State && gridElements[6].State != 0)
        {
            AssignReward(gridElements[6].State);
            someoneWon = true;
        }
        else if (gridElements[0].State == gridElements[3].State && gridElements[3].State == gridElements[6].State && gridElements[0].State != 0)
        {
            AssignReward(gridElements[0].State);
            someoneWon = true;
        }
        else if (gridElements[1].State == gridElements[4].State && gridElements[4].State == gridElements[7].State && gridElements[1].State != 0)
        {
            AssignReward(gridElements[1].State);
            someoneWon = true;
        }
        else if (gridElements[2].State == gridElements[5].State && gridElements[5].State == gridElements[8].State && gridElements[2].State != 0)
        {
            AssignReward(gridElements[2].State);
            someoneWon = true;
        }
        else if (gridElements[0].State == gridElements[4].State && gridElements[4].State == gridElements[8].State && gridElements[0].State != 0)
        {
            AssignReward(gridElements[0].State);
            someoneWon = true;
        }
        else if (gridElements[2].State == gridElements[4].State && gridElements[4].State == gridElements[6].State && gridElements[2].State != 0)
        {
            AssignReward(gridElements[2].State);
            someoneWon = true;
        }

        if (someoneWon == false && count == 0)
        {
            foreach (var ps in playerStates)
            {
                ps.agentScript.AddReward(-0.2f);
            }

            foreach (var ps in playerStates)
            {
                ps.agentScript.Done();  //all agents need to be reset
            }

            draws++;
            UpdateCanvas();
            //BruteForce Naive
            PlayerEx.AddReward(-0.2f);
            PlayerEx.Done();
            PlayerEx.Reset();

            turn = 1;//ayyy minimaxonly 
        }

        

        CheckTurn();
    }

    private void AssignReward(int state)
    {

        foreach (var ps in playerStates)
        {
            if ((int)ps.agentScript.team == (state-1))
            {
                ps.agentScript.AddReward(1);
            }
            else
            {
                ps.agentScript.AddReward(-1);
            }
            
        }

        if (state == 1)
        {
            owins++;
        }
        else if (state == 2)
        {
            xwins++;
        }

        UpdateCanvas();

        //BruteForce Naive
        if ((int)PlayerEx.team == (state - 1))
        {
            PlayerEx.AddReward(1);
        }
        else
        {
            PlayerEx.AddReward(-1);
        }
        PlayerEx.Done();
        //Plus minimax
        PlayerEx.Reset();

        foreach (var ps in playerStates)
        {
            ps.agentScript.Done();  //all agents need to be reset
        }

        turn = 1;//ayyminimaxonly
        
    }

    private void UpdateCanvas()
    {
        Xwins.text = "X Wins : " + xwins;
        Owins.text = "O Wins : " + owins;
        Draws.text = "Draws : " + draws;
    }

    public void GridReset()
    {
        
        //Reset the Grid here
        foreach (var element in gridElements)
        {
            element.ResetState();
        }
    }

    public int[] GridValues()
    {
        int[] gridValues = new int[9];
        int i = 0;
        foreach (var element in gridElements)
        {
            gridValues[i++] = element.State;
            
        }

        return gridValues;
    }

    public void SetGridElement(int index, int val)
    {
        gridElements[index].SetState(val);
    }

    
}
