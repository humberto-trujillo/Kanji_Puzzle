﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }
}
