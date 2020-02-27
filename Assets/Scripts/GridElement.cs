using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    int state;
    public int State { get { return state; } set { state = value; } }
    GameObject visualStateO;
    GameObject visualStateX;


    void Start()
    {
        visualStateO = this.transform.GetChild(0).gameObject;
        visualStateX = this.transform.GetChild(1).gameObject;
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
            visualStateO.SetActive(false);
            visualStateX.SetActive(false);
        }
        else if (state == 1)
        {
            visualStateO.SetActive(true);
        }
        else if (state == 2)
        {
            visualStateX.SetActive(true);
        }
    }
}
