using UnityEngine;
using MLAgents;
using System;

public class GridController : MonoBehaviour
{
    public GameObject Grid;
    Transform[,] gridPositions;

    private void Awake()
    {
        Academy.Instance.OnEnvironmentReset += GridReset;
    }

    private void Start()
    {
        gridPositions = new Transform[3, 3];
        GridReset();
    }

    private void GridReset()
    {
        //Reset the Grid here
        throw new NotImplementedException();
    }
}
