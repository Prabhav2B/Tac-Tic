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
        visualStateX = this.transform.GetChild(0).gameObject;
        visualStateY = this.transform.GetChild(1).gameObject;
        ResetState();
        Refresh();
    }

    public void ResetState()
    {
        State = 0;
        Refresh();
        
    }

    public void SetState(int val)
    {
        State = val;
        Refresh();
    }

    void Refresh()
    {
        if (state == 0)
        {
            visualStateX.SetActive(false);
            visualStateY.SetActive(false);
        }
        else if (state == 1)
        {
            visualStateX.SetActive(true);
        }
        else if (state == 2)
        {
            visualStateY.SetActive(true);
        }
    }
}
