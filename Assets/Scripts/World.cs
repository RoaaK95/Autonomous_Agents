using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World _instance = new World();
    private static GameObject[] _hideSpots;

    static World()
    {
        _hideSpots= GameObject.FindGameObjectsWithTag("hide");
    }

    private World() { }
    public static World Instance { get { return _instance; } }
    public  GameObject[] HideSpots() { return _hideSpots; }
}
