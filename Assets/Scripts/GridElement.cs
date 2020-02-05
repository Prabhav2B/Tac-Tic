using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    int state;
    public int State { get { return state; } set { state = value; } }
    GameObject visualStateX;
    GameObject visualStateY;

    void Start()
    {
        ResetState();
    }

    void ResetState()
    {
        State = 0;
    }

    void SetState(int val)
    {
        State = val;        
    }

    void Refresh()
    {
        if (State == 1)
        { 
            
        }
        else if (State == 2)
        { 
        
        }
    }
}
