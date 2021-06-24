using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeimage : MonoBehaviour
{
    [Header("最初からフェードインが完了しているかどうか")] public bool firstfadeincomp;
    private Image img=null;
    private int framecount=0;
    private float timer=0.0f;
    private bool fadein =false;
    private bool fadeout =false;
    private bool compfadein=false;
    private bool compfadeout=false;

    public void StartFadeIn(){
        if(fadein||fadeout){
            return;
        }
        fadein=true;
        compfadein=false;
        timer=0.0f;
        img.color=new Color(1,1,1,1);
        img.fillAmount=1;
        img.raycastTarget=true;
    }
    ///<summary>
    ///  フェードインが完了したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsFadeInComp(){
        return compfadein;
    }

    public void StartFadeOut(){
        if(fadein||fadeout){
            return;
        }
        fadeout=true;
        compfadeout=false;
        timer=0.0f;
        img.color=new Color(1,1,1,0);
        img.fillAmount=0;
        img.raycastTarget=true;
    }

    ///<summary>
    ///  フェードインが完了したかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsFadeOutComp(){
        return compfadeout;
    }
    // Start is called before the first frame update
    void Start()
    {
     img=GetComponent<Image>();   
     if(firstfadeincomp){
         FadeInComp();
     }
     else{
         StartFadeIn();
     }
    }

    // Update is called once per frame
    void Update()
    {
        if(framecount>2){
            if(fadein){
            FadeInupdate();
        }
        else if(fadeout){
            FadeOutupdate();
        }
            
    }
    ++framecount;
    }
    private void FadeInupdate(){
        if(timer <1){
                img.color=new Color(1,1,1,1-timer);
                img.fillAmount =1-timer;
            }
            //フェード終了
            else{
                FadeInComp();
            }
            timer+=Time.deltaTime;
    }
    private void FadeOutupdate(){
        if(timer <1){
                img.color=new Color(1,1,1,timer);
                img.fillAmount =timer;
            }
            //フェード終了
            else{
                img.color=new Color(1,1,1,1);
                img.fillAmount =1;
                if(timer>1.2f){FadeOutComp();}
            }
            timer+=Time.deltaTime;
    }
     private void FadeInComp(){
        img.color=new Color(1,1,1,0);
                img.fillAmount=0;
                img.raycastTarget=false;
                timer = 0.0f;
                fadein = false;
                compfadein=true;
    } 
    private void FadeOutComp(){
        img.color=new Color(1,1,1,1);
                img.fillAmount=1;
                img.raycastTarget=false;
                timer = 0.0f;
                fadeout = false;
                compfadeout=true;
    }   
}
