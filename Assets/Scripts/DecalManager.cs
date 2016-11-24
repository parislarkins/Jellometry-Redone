﻿using UnityEngine;
using System.Collections.Generic;

public class DecalManager : MonoBehaviour
{
    private readonly List<GameObject> _decals = new List<GameObject>();

    private int MaxDecals = 30;

    public void AddDecal(GameObject decal)
    {
        if (_decals.Count > MaxDecals)
        {
            Destroy(_decals[0]);
            _decals.RemoveAt(0);
        }

        _decals.Add(decal);
    }

    public List<GameObject> GetDecals()
    {
        return _decals;
    }
}
