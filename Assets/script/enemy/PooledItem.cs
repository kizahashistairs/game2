using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//オブジェクトプールから呼び出すやつ用のインターフェース

public interface PooledItem
{
    void PassPool(ref Queue<GameObject> pool);
} 
