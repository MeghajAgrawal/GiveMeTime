using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public static Transform[] nodes;

    void Awake()
    {
        nodes = new Transform[transform.childCount];
        for (int i = 0 ; i < nodes.Length; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
    }
}
