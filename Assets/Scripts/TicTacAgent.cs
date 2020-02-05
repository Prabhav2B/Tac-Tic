using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TicTacAgent : Agent
{
    
   

    public Player player;
    public List<GameObject> grid;
    public override void AgentAction(float[] vectorAction)
    {
        //implement actions here
    }

    public override void CollectObservations()
    {
        foreach (var gridposition in grid)
        {
            AddVectorObs(gridposition);
        }
        
        //set masks
    }

    public override void AgentReset()
    {
        //currently agent does not require any specific resetting
        base.AgentReset();
    }

    public enum Player { Player1, Player2 };
}
