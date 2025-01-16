using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandleTurnsOff : MonoBehaviour
{
    public int candleState;
    public float candleRemain;
    public float candleTotal;
    public int candleMinimum;

    // Start is called before the first frame update
    void Start()
    {
        //If candleState are defined as 1, it will be on 
        candleState = 1;
        candleTotal = 100;
        candleMinimum = 0;
        candleRemain = candleTotal/3;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || candleState != 0)
        {
            candleState = 0;
        }

        if (Input.GetKey(KeyCode.Z) && candleState == 0 && candleRemain <= 0)
        {
            candleState = 1;
        }

        if (candleRemain >= candleMinimum && candleState != 0)
        {
            candleRemain -= Time.deltaTime;
        }
    }
}
