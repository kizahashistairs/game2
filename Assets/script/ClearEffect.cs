﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    [Header("拡大縮小のアニメーションカーブ")] public AnimationCurve curve;
    [Header("ステージコントローラー")] public StageControl ctrl;
    [Header("クリア後タイトルに戻るかどうか")] public bool finishornot;
    private bool comp = false;     
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(!comp){
            if(timer<1.0f){
                transform.localScale=2.2f*Vector3.one*curve.Evaluate(timer);
                timer+=Time.deltaTime;
            }
            else{
                transform.localScale=2.2f*Vector3.one;
                timer+=Time.deltaTime;
                if(timer>1.5f){
                //if(finishornot){ctrl.gotitle();}
                //else{ctrl.ChangeStage(GameManager.instance.stageNum+1);}
                comp = true;
                }
            }
        }
    }
}
