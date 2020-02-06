using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class GridController : MonoBehaviour
{
    public GameObject Grid;
    GridElement[] gridElements;
    public TicTacAgent Player1;
    public TicTacAgent Player2;
    int turn;

    private void Awake()
    {
        //Academy.Instance.OnEnvironmentReset += GridReset;
    }

    private void Start()
    {
        turn = 1;
        gridElements = Grid.transform.GetComponentsInChildren<GridElement>(); 
        GridReset();
        CheckTurn();
    }

    private void CheckTurn()
    {
        if (turn == 1)
        {
            Player1.PlayTurn();
        }
        else
        {
            Player2.PlayTurn();
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
                count++;
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
            Player1.AddReward(-0.2f);
            Player1.Done();
            Player2.AddReward(-0.2f);
            Player2.Done();
        }
        

        CheckTurn();
    }

    private void AssignReward(int state)
    {
        if (state == 1)
        {
            Player1.AddReward(1f);
            Player1.Done();
            Player2.AddReward(-1f);
            Player2.Done();
        }
        else if (state == 2)
        {
            Player1.AddReward(-1f);
            Player1.Done();
            Player2.AddReward(1f);
            Player2.Done();
        }
        
    }

    public void GridReset()
    {
        //Reset the Grid here
        foreach (var element in gridElements)
        {
            element.gameObject.GetComponent<GridElement>().ResetState();
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
