﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LandPlatform", menuName = "ShellCore/LandPlatform", order = 3)]
public class LandPlatform : ScriptableObject
{

    [System.Serializable]
    public struct Platform
    {
        public int type;
        public int rotation;
    }

    [System.Serializable]
    public struct PlatformRow
    {
        public Platform[] platformRow;
    }

    public float spriteSize;
    public PlatformRow[] platformRows; 
    // why tf can't I make a 2D serializable
    // you can flatten it to one dimentional array first
}