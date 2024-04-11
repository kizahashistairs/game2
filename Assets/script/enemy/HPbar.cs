using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPbar : MonoBehaviour
{
    
    private Slider bar;
    [SerializeField]
    private Canvas mycanvas;
    [SerializeField]
    private bool forboss=false;//boss用の場合HPバーを隠さない
    private CanvasGroup cg; //
    private float timer=0;
    private float HPheri=0;
    private float currentHP=0;
    // Start is called before the first frame update
    void Start()
    {
        cg=GetComponent<CanvasGroup>();
        if(cg==null){
            Debug.Log("HPバーにキャバスグループが付いていません");
        }
        bar=GetComponent<Slider>();
        if(bar==null){
            Debug.Log("HPバーでないものにアタッチされています");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer+=Time.deltaTime;

        //HPをじりじり減らす
        if(bar.value>currentHP){
            bar.value-=10*HPheri*Time.deltaTime;
            }
        //HPの減りが間に合わないときの帳尻合わせ
        else if(bar.value!=currentHP){
            bar.value=currentHP;
        }

        //一定時間たったらHPバーを隠す処理
        if(!forboss){
            if(timer>0.8){
            if(cg.alpha>0){
                cg.alpha-=Time.deltaTime;
            }
            else{
                mycanvas.enabled=false;
                }
        }
        }
        
    }
    public void SetMaxHP(float MaxHP){
        if(bar==null){
            bar=GetComponent<Slider>();
        }
        if(cg==null){
            cg=GetComponent<CanvasGroup>();
        }
        currentHP=MaxHP;
        bar.maxValue=MaxHP;
        bar.value=MaxHP;
    }

    //ダメージをうける処理
    public void GetDamage(int damage){
        currentHP-=damage;
        mycanvas.enabled=true;
        timer=0;
        cg.alpha=1;
        HPheri=damage;
    }
}
