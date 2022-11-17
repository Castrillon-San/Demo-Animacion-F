using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReach
{
    public Vector3 healthPosition
    {
        get;
        set;
    }

    public bool grab
    {
        get;
        set;
    }
}
