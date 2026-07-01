using UnityEngine;
using System.Collections.Generic;

public static class GameData
{
    public static HashSet<string> collectedObjects =
    new HashSet<string>();

    public static AtmosphereState currentAtmosphere = AtmosphereState.Collapsed;
}