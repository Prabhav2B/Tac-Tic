using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TicTacAgent : Agent
{

    [Header("Specific to tac-tic")]
    public Player player;
    public GridController gridController;


    public void PlayTurn()
    {
        if (Academy.Instance.IsCommunicatorOn)
        {
            RequestDecision();
        }
    }

    public override void CollectObservations()
    {
        int[] observations = gridController.GridValues();
        List<int> maskedElements = new List<int>();
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
                maskedElements.Add(i);
            }
        }

        string str = "";

        if(maskedElements!=null)
        {
            //SetActionMask(0, maskedElements.ToArray());
            foreach (var maskedElement in maskedElements)
            {
                SetActionMask(0, maskedElement);
                str += maskedElement;
            }
        }
        else
        {
            Debug.Log("Null Mask");
        }

        str += this.gameObject.name;
        Debug.Log(str);
    }

    public override void AgentAction(float[] vectorAction)
    {
        Debug.Log("taking action now");
        //implement actions here
        int moveOnPosition = Mathf.FloorToInt(vectorAction[0]);

        if (player == Player.Player1)
        {
            gridController.SetGridElement(moveOnPosition, 1);
        }
        else
        {
            gridController.SetGridElement(moveOnPosition, 2);
        }

        gridController.EndTurn();
    }


    public override void AgentReset()
    {
        Debug.Log("Reset yo!");
        gridController.GridReset();
    }


    public override float[] Heuristic()
    {
        return base.Heuristic();
    }
    public enum Player { Player1, Player2 };
}
