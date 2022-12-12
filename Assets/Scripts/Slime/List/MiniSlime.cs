using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime : Slime
{
    public override void Push()
    {
        ObjectPool.instance.Push(gameObject,ObjectType.MINI_SLIME);
    }
}
