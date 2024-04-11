using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class barrier : MonoBehaviour
{
    [Header("触れたら消えるやつ")] public string[] barriertag={"sokushi","sokushihookable","sokushienemy"};//触れたら死ぬやつ
    public AudioClip barrierSE;
    void OnTriggerEnter2D(Collider2D other) {
        if(barriertag.Contains(other.tag)){
            other.gameObject.SetActive(false);
            GameManager.instance.PlaySE(barrierSE);
        }
    }
}
