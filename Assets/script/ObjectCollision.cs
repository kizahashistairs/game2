using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("プレイヤーが踏んだ時に跳ねる高さ")]public float boundHeight;
    
    [HideInInspector] public bool playerStepOn;//このオブジェクトをプレイヤーが踏んだかどうか
}
