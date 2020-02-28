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


    public GridController gridController;
    public bool playerControlled = false;
    public Team team = Team.Ex;

    float reward;

    public void PlayTurn()
    {
        if (!playerControlled)
        {
            float[] move = NaiveDecision();
            NaiveAction(move);
        }
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

        float randomMove = (float)unoccupiedElements[Random.Range(0, unoccupiedElements.Count)];
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
}
