using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaivePlayer : MonoBehaviour
{
    public enum Team
    {
        Oh = 0,
        Ex = 1
    }

    public enum PlayMode
    {
        Player=0,
        Random=1,
        MinMax=2
    }

    public GridController gridController;
    public PlayMode playmode = PlayMode.Player;
    public Team team = Team.Ex;
    public MinMax minMax;

    bool playerMove = false;
    public bool PlayerMove { get{ return playerMove; } set{ playerMove = value; }}

    int movePosition;
    public int MovePosition { get { return movePosition; } set { movePosition = value; } }

    [HideInInspector]
    public int lastMove;


    float reward;

    public void PlayTurn()
    {
        if ((int)playmode == 0)
        {
            StartCoroutine("WaitForPlayerAction");
        }
        else if ((int)playmode == 1)
        {
            float[] move = NaiveDecision();
            NaiveAction(move);
        }
        else if ((int)playmode == 2)
        {
            float[] move = MinMax();
            NaiveAction(move);
        }
        
    }

    private float[] MinMax()
    {
        float nextMove = (float)minMax.NextMove(lastMove);
        float[] move = { nextMove };

        return (move);
    }

    public float[] NaiveDecision()
    {
        
        int[] observations = gridController.GridValues();
        List<int> unoccupiedElements = new List<int>();
        for (int i = 0; i < observations.Length; i++)
        {
            if (observations[i] == 0)
            {
                unoccupiedElements.Add(i);
            }
        }

        float randomMove = (float)unoccupiedElements[UnityEngine.Random.Range(0, unoccupiedElements.Count)];
        float[] move = { randomMove };

        return (move);  
    }

    public void NaiveAction(float[] vectorAction)
    {

        //implement actions here
        int moveOnPosition = Mathf.FloorToInt(vectorAction[0]);

        if (team == Team.Oh)
        {
            gridController.SetGridElement(moveOnPosition, 1);
        }
        else
        {
            gridController.SetGridElement(moveOnPosition, 2);
        }

        gridController.EndTurn();
    }

    public void AddReward(float value)
    {
        reward += value;
    }

    public void Done()
    {
        reward = 0;
        gridController.GridReset();
    }

    public void Reset()
    {
        minMax.Reset();
    }
    public IEnumerator WaitForPlayerAction()
    {
        while (true)
        {
            if (PlayerMove == true)
            {
                PlayerMove = false;
                float[] move = { MovePosition };
                NaiveAction(move);

                break;
            }
            yield return null;
        }
    }
}
