using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_body : MonoBehaviour
{
    [SerializeField]
    [Header("本体に弾があたった時のダメージ")]private int hitdmg=3;
    [SerializeField] private enemy_boss boss_main;
    [SerializeField]
    [Header("弾があたった時のSE")]private AudioClip hitSE;
    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            Destroy(collision.gameObject);
            boss_main.GetDamage(hitdmg);
            if(hitSE!=null){
                GameManager.instance.PlaySE(hitSE);
            }
        }
    }
}
