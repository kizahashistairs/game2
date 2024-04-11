using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
    [Header("スコア")]public int score;//実装するかは未定
    [Header("ステージ番号")]public int stageNum;
    public int missnum=0;
    public int respawnnum;
    private bool isstageclear=false;
    [HideInInspector] public bool isStageClear{
         get{
            return isstageclear;
        }
        set{
            isstageclear = value;
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }
       
    }
    private bool slowmode = false;

    private AudioSource audioSource=null;
    private AudioSource ShortSESource;//途中でキャンセルしたいSE用のaudiosource

    private void Awake() {
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        ShortSESource = gameObject.AddComponent<AudioSource>();
        if(audioSource != null && ShortSESource != null){
            ShortSESource.volume = audioSource.volume;//SEの音量は一緒にする
        }
        
    }
    private void Update(){
        
    }
    public void plusmiss(){
        if (missnum<99){missnum++;}
    }
    public void PlaySE(AudioClip clip, float SEVolume = 1.0f){
        if(audioSource!=null){
            audioSource.PlayOneShot(clip, SEVolume);
        }
        else{
            Debug.Log("オーディオソースが設定されていません");
        }
    }
    public void PlayShortSE(AudioClip clip, float SEVolume = 1.0f){
        if(ShortSESource!=null){
            ShortSESource.PlayOneShot(clip , SEVolume);
        }
        else{
            Debug.Log("オーディオソースが設定されていません");
        }
    }
    public void StopShortSE(){
         ShortSESource.Stop();
    }
}
