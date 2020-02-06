using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TicTacAgent : Agent
{
    public Player player;
    public GridController gridController;


    public void PlayTurn()
    {
        RequestDecision();
    }

    public override void CollectObservations()
    {
        int[] observations = gridController.GridValues();
        for (int i = 0; i < observations.Length; i++)
        {

            if (player == Player.Player1) //O
            {
                if (observations[i] == 1)
                {
                    AddVectorObs(1);
                }
                else if (observations[i] == 2)
                {
                    AddVectorObs(-1);
                }
                else
                {
                    AddVectorObs(0);
                }
            }
            else if (player == Player.Player2)//X
            {
                if (observations[i] == 1)
                {
                    AddVectorObs(-1);
                }
                else if (observations[i] == 2)
                {
                    AddVectorObs(1);
                }
                else
                {
                    AddVectorObs(0);
                }
            }
            
            if (observations[i] != 0)
            {
                SetActionMask(0, i);
            }
        }
    }

    public override void AgentAction(float[] vectorAction)
    {
        //implement actions here
        int move = Mathf.FloorToInt(vectorAction[0]);
    }


    public override void AgentReset()
    {
        //currently agent does not require any specific resetting
        base.AgentReset();
    }


    public override float[] Heuristic()
    {
        return base.Heuristic();
    }
    public enum Player { Player1, Player2 };
}
