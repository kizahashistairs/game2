﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePoint : MonoBehaviour
{
    [Header("コンティニュー番号")] public int respawnNum;
    [Header("音")] public AudioClip se;
    [Header("プレイヤー判定")] public PlayerTriggerCheck trigger;
    [Header("スピード")] public float speed =2.0f;
    [Header("取得アニメーション")] public AnimationCurve curve;

    private bool on;
    private float timer=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(trigger==null||se==null){
            Debug.Log("インスペクターの設定が足りません");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.isOn&& !on){
            GameManager.instance.respawnnum=respawnNum;
            GameManager.instance.PlaySE(se);
            on =true;
        }   

        if(on){
            if(timer<1.0f){
                transform.localScale=Vector3.one * curve.Evaluate(timer);
                timer+=speed*Time.deltaTime;
            }
            else{
                transform.localScale=Vector3.one * curve.Evaluate(1.0f);
                gameObject.SetActive(false);
                on=false;
            }
        }
    }
}
