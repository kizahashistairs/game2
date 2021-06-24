using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    [Header("拡大縮小のアニメーションカーブ")] public AnimationCurve curve;
    [Header("ステージコントローラー")] public StageControl ctrl;
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
                transform.localScale=3*Vector3.one*curve.Evaluate(timer);
                timer+=Time.deltaTime;
            }
            else{
                transform.localScale=3*Vector3.one;
                timer+=Time.deltaTime;
                if(timer>1.5f){
                ctrl.gotitle();
                //ctrl.ChangeStage(GameManager.instance.stageNum+1);
                comp = true;
                }
            }
        }
    }
}
