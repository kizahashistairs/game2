using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour
{
    public Text timeText;
    [SerializeField] private Text BestTimeText;
    [SerializeField] private Text RenewBestTime;
    [SerializeField] private Text cleartimetext;
    public bool run=true;
    private float seconds=0.0f;
    private int minutes=0;
    // Start is called before the first frame update
    void Start()
    {
        timeText=this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isStageClear){
        seconds+=Time.deltaTime;
        if(seconds>=60){
            minutes++;
            seconds-=60.0f;
        }
        }
        timeText.text="Time: " + minutes.ToString() + "分" + ((float)seconds).ToString("00.00")+"秒";
    }

    public void SetGoalTime(){
       var besttime = PlayerPrefs.GetFloat("StageTime"+GameManager.instance.stageNum.ToString(), 9999.9f);
        BestTimeText.text = "ベストタイム: " + PlayerPrefs.GetString ("Stage"+GameManager.instance.stageNum.ToString(), "-");
        if(goaltimescore()<besttime){
            Debug.Log(goaltimescore());
            Debug.Log("best"+ besttime.ToString());
            PlayerPrefs.SetFloat("StageTime"+GameManager.instance.stageNum.ToString(), goaltimescore());
            PlayerPrefs.SetString("Stage"+GameManager.instance.stageNum.ToString(), goaltime());
            PlayerPrefs.Save();
            RenewBestTime.enabled = true;
        }
        else{
             RenewBestTime.enabled = false;
        }
        if(cleartimetext!=null){
            cleartimetext.text="クリアタイム:"+goaltime();
        } 
        else{
            Debug.Log("クリアタイムを表示するテキストがアタッチされていません");
        }
    }
    

    public string goaltime(){
        return(minutes.ToString() + "分" + ((float)seconds).ToString("00.00")+"秒");
    }
    public float goaltimescore(){
        return(((float)minutes*60.0f) + seconds);
    }
    public void timereset(){
        seconds=0.0f;
        minutes=0;
    }
}