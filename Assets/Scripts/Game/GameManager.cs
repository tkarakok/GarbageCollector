using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> garbages;
    public Vector3 newPositionForGarbage;
    public Transform firstPosition;

    private int garbageCounter = 0;

    public int GarbageCounter { get => garbageCounter; set => garbageCounter = value; }
}
