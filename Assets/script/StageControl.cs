using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class StageControl : MonoBehaviour
{
    [Header("ゲームオブジェクト")]public GameObject playerobj;
    [Header("コンティニュー位置")]public GameObject[] continuepoint;
    [Header("ステージクリア")] public GameObject stageclearobj;
    [Header("フェード")]public fadeimage fade;
    [Header("タイマー")]public timer mytimer;
    [Header("クリア時に消すアイテム")]public GameObject[] kesuitem;
    [Header("ステージクリア判定")]public PlayerTriggerCheck stagecleartrigger;
    [Header("ステージクリアSE")]public AudioClip stageclearSE;
    public GameObject bgmplayer;
    //[SerializeField] private Text cleartimetext;
    private AudioSource bgm=null;        
    private player p;
    private int nextStageNum;
    private bool startFade=false;
    private bool doSceneChange = false;
    private bool doClear=false;
    
    
    [SerializeField]
    [Header("クリアしたらタイトルに戻るか")]private bool totitle=false;
    // Start is called before the first frame update
    void Start()
    {
        //bgm=bgmplayer.GetComponent<AudioSource>();
      if(playerobj !=null&&continuepoint!=null&&continuepoint.Length>0){
          bgm=bgmplayer.GetComponent<AudioSource>();
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
        if(Input.GetKeyDown("t")){
            gotitle();
        }
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
        if (stagecleartrigger != null && stagecleartrigger.isOn && !doClear)
        {
            StageClear();
            doClear = true;
        }
        ///<summary>
        /// フェードアウトしたらステージの切り替え
        /// </summary>
        
        if(fade!=null&&startFade&&!doSceneChange){
            Debug.Log("a");
            if(fade.IsFadeOutComp()){
            GameManager.instance.stageNum = nextStageNum;
            
            GameManager.instance.isStageClear = false;
            if(totitle){
                SceneManager.LoadScene("title");
                totitle=false;
                GameManager.instance.stageNum = 1;
            }
            else{SceneManager.LoadScene("stage_" + nextStageNum);}
            GameManager.instance.stageNum = nextStageNum;
            doSceneChange = true;
            }
            
        }

    }
    
    /// <summary>
    /// ステージを切り替えます
    ///クリアエフェクト完了後にクリアエフェクトで以下を呼ぶ
    /// </summary>
    /// <param name="num"></param>
    public void toNextStage(){
        if(totitle){
            gotitle();
        }
        else{
            ChangeStage(GameManager.instance.stageNum+1);
        }
    }
    public void ChangeStage(int num){
        if(fade!=null){
            nextStageNum =num;
            fade.StartFadeOut();
            startFade=true;
            mytimer.timereset();
        }
    }
    public void gotitle(){
        if(fade!=null){
            totitle=true;
            fade.StartFadeOut();
            startFade=true;
        }
    }

    public void StageClear(){
        GameManager.instance.respawnnum=0;
        p.stopplayer();
        stageclearobj.SetActive(true);
        foreach(GameObject i in kesuitem){
            i.SetActive(false);
        }
        GameManager.instance.isStageClear=true;
        bgm.Stop();
        GameManager.instance.PlaySE(stageclearSE);

        //クリアタイムを保存
        
        mytimer.SetGoalTime();
        
        
    }
}
