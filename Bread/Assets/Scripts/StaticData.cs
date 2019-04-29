using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    private static string PLAYER_NAME = "";

    public static string Name
    {
        get
        {
            return PLAYER_NAME;
        }
        set
        {
            PLAYER_NAME = value;
        }
    }

    public static float NetWorth { get; set; }
}
