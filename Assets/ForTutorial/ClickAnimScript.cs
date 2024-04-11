using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimScript : MonoBehaviour
{
    
    [Header("クリックしている間この画像に")][SerializeField] private Sprite ChangedSprite;
    private Sprite DefSprite;//デフォルトの画像
    private SpriteRenderer SR;
    
    
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        DefSprite = SR.sprite;
        if(ChangedSprite == null){
            Debug.Log("変更先の画像が設定されていません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SR.sprite = ChangedSprite;
        }

        if(Input.GetMouseButtonUp(0)){
            SR.sprite = DefSprite;
        }
    }
}
