using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TicTacAgent : Agent
{
    public enum Team
    {
        Oh = 0,
        Ex = 1
    }

    [HideInInspector]
    public Team team;
    BehaviorParameters m_BehaviorParameters;
    int m_PlayerIndex;


    [Header("Specific to tac-tic")]
    public GridController gridController;
    public bool playerControlled = false;

    NaivePlayer nv;

    public void PlayTurn()
    {
        //StartCoroutine("OneSecondPause");      
        RequestDecision();
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        m_BehaviorParameters = gameObject.GetComponent<BehaviorParameters>();
        if (m_BehaviorParameters.m_TeamID == (int)Team.Ex)
        {
            team = Team.Ex;
        }
        else
        {
            team = Team.Oh;
        }

        var playerState = new TTPlayerState
        {
            agentScript = this,
        };

        gridController.playerStates.Add(playerState);
        m_PlayerIndex = gridController.playerStates.IndexOf(playerState);
        playerState.playerIndex = m_PlayerIndex;
        nv = FindObjectOfType<NaivePlayer>();
    }

    public override void CollectObservations()
    {
        int[] observations = gridController.GridValues();
        List<int> maskedElements = new List<int>();
        for (int i = 0; i < observations.Length; i++)
        { 
                if (observations[i] == 1)
                {
                    AddVectorObs(1); //Oh
                    maskedElements.Add(i);
                }
                else if (observations[i] == 2)
                {
                    AddVectorObs(-1); //Ex
                    maskedElements.Add(i);
                }
                else
                {
                    AddVectorObs(0);
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
        
    }

    public override void AgentAction(float[] vectorAction)
    {
       

        //foreach (var item in vectorAction)
        //{
        //    Debug.Log(item);
        //}


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

        if (nv != null && (int)nv.playmode == 2)
        {
            nv.lastMove = moveOnPosition;
        }

        gridController.EndTurn();
    }


    public override void AgentReset()
    {
        
        gridController.GridReset();
    }


    public override float[] Heuristic()
    {
        if (!playerControlled)
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

            float randomMove;



            if (unoccupiedElements.Count > 0)
            {
                randomMove = (float)unoccupiedElements[Random.Range(0, unoccupiedElements.Count)];
                float[] move = { randomMove };

                return (move);
            }
        }

        return (null);
    }

     IEnumerator OneSecondPause()
    {
        yield return new WaitForSeconds(1f);
        RequestDecision();
    }
}
