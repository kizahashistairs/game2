using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;
    [Header("スコア")]public int score;//実装するかは未定
    [Header("ステージ番号")]public int stageNum=0;
    public int missnum=0;
    public int respawnnum;
    [HideInInspector] public bool isStageClear;

    private AudioSource audioSource=null;
    // Start is called before the first frame update
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
        audioSource=GetComponent<AudioSource>();
    }
    public void plusmiss(){
        if (missnum<99){missnum++;}
    }
    public void PlaySE(AudioClip clip){
        if(audioSource!=null){
            audioSource.PlayOneShot(clip);
        }
        else{
            Debug.Log("オーディオソースが設定されていません");
        }
    }
}
