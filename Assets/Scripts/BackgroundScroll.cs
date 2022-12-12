using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Vector2 _curOffset = Vector2.zero;
    private Material _material;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void SetOffset(float x, float y)
    {
        _curOffset.x = x;
        _curOffset.y = y;
        _material.mainTextureOffset = _curOffset;
    }
}
