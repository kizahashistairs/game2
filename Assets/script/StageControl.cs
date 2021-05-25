using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StageControl : MonoBehaviour
{
    [Header("ゲームオブジェクト")]public GameObject playerobj;
    [Header("コンティニュー位置")]public GameObject[] continuepoint;
    //[Header("ステージクリアSE")]public AudioClip stageclearSE;
    public GameObject bgmplayer;
    //private AudioSource bgm=null;        
    private player p;
    private int nextStageNum;
    private bool StartStageChange=false;
    private bool doSceneChange = false;
    private bool doClear=false;
    // Start is called before the first frame update
    void Start()
    {
        //bgm=bgmplayer.GetComponent<AudioSource>();
      if(playerobj !=null&&continuepoint!=null&&continuepoint.Length>0){
          
          p=playerobj.GetComponent<player>();
          playerobj.transform.position=continuepoint[0].transform.position;
          if(p==null){
              Debug.Log("プレイヤーじゃないものがアタッチされているよ");
              }
      }
      else{
          Debug.Log("設定が足りないよ");
      }
    }

    // Update is called once per frame
    
    void Update()
    {
        if(p!=null&& p.isDownDone())//プレイヤーからコンティニューを受け取る
        {
            if(continuepoint.Length>GameManager.instance.respawnnum){
                playerobj.transform.position=continuepoint[GameManager.instance.respawnnum].transform.position;
                p.ContinuePlayer();
            }
            else{
                Debug.Log("コンティニューポイントの設定が足りないよ");
            }
        }
        if (false)//ステージクリア判定がtrueになったら、にする予定
        {
            StageClear();
            doClear = true;
        }
        ///<summary>
        /// ステージの切り替え
        /// </summary>
        if(StartStageChange){
            if(false){
                GameManager.instance.stageNum = nextStageNum;
            }
            GameManager.instance.isStageClear = false;
            SceneManager.LoadScene("stage_" + nextStageNum);
            GameManager.instance.stageNum = nextStageNum;
            doSceneChange = true;
        }

    }
    /// <summary>
    /// ステージを切り替えます
    /// </summary>
    /// <param name="num"></param>
    public void ChangeStage(int num){
            nextStageNum =num;
            //fade.StartFadeOut();フェードアウト演出欲しい
            StartStageChange=true;
    }
    public void StageClear(){
        GameManager.instance.respawnnum=0;
        //GameManager.instance.isStageClear=true;
        //bgm.Stop();
        //GameManager.instance.PlaySE(stageclearSE);
    }
}
