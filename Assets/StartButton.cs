using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        GameManager.instance.stageNum=1;
        SceneManager.LoadScene("Stage_"+GameManager.instance.stageNum);
    }
}
