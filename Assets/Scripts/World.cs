using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World _instance = new World();
    private static GameObject[] _barnSpots;

    static World()
    {
        _barnSpots = GameObject.FindGameObjectsWithTag("barn");
    }

    private World() { }
    public static World Instance { get { return _instance; } }
    public static GameObject[] BarnSpots() { return _barnSpots; }
}
