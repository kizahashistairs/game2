using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPbar : MonoBehaviour
{
    
    private Slider bar;
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
        if(bar.value>currentHP){
            bar.value-=10*HPheri*Time.deltaTime;
            }
        else if(bar.value!=currentHP){
            bar.value=currentHP;
        }
        else if(timer>0.8){
            if(cg.alpha>0){
                cg.alpha-=Time.deltaTime;
            }
            else{
                this.gameObject.SetActive(false);
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
        
        this.gameObject.SetActive(false);
    }
    public void GetDamage(int damage){
        currentHP-=damage;
        this.gameObject.SetActive(true);
        timer=0;
        cg.alpha=1;
        HPheri=damage;
    }
}
