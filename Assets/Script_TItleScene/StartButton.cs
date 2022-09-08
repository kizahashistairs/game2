using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private bool clickedthis=false;
    public fadeimage fade;
    [Header("押されたときのSE")]public AudioClip buttonSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(clickedthis&&fade.IsFadeOutComp()){
            SceneManager.LoadScene("Stage_"+GameManager.instance.stageNum);
        }
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClickstart()
    {
        if(!clickedthis){
        GameManager.instance.PlaySE(buttonSE);
        clickedthis=true;
        fade.StartFadeOut();
        }
    
    }
        public void OnClicktut()
    {
        if(!clickedthis){
        GameManager.instance.PlaySE(buttonSE);
        GameManager.instance.stageNum=0;
        clickedthis=true;
        fade.StartFadeOut();
        }
    
    }
}
