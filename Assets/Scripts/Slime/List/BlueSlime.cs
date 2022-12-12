using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlime : Slime
{
    public override void Push()
    {
        ObjectPool.instance.Push(gameObject,ObjectType.BLUE_SLIME);
    }
}
