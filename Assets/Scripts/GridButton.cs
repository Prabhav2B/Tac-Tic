using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButton : MonoBehaviour
{
    public int position;
    NaivePlayer nv;

    private void Start()
    {
        nv = FindObjectOfType<NaivePlayer>();
    }

    public void PlayerMove()
    {
        nv.MovePosition = position;
        nv.PlayerMove = true;
    }
}
