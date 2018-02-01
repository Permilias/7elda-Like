﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite portrait;

    [TextArea(1, 10)]
    public string[] sentences;

}