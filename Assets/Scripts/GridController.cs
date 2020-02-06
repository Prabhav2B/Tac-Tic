using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;


public class GridController : MonoBehaviour
{
    public GameObject Grid;
    GridElement[] gridElements;

    private void Awake()
    {
        Academy.Instance.OnEnvironmentReset += GridReset;
    }

    private void Start()
    {
        gridElements = Grid.transform.GetComponentsInChildren<GridElement>(); 
        GridReset();
    }

    private void GridReset()
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
}
