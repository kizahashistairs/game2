using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biyoon : MonoBehaviour
{
    public GameObject hantei;

    private Vector3 size =new Vector3 (1.0f,1.0f,1.0f);
    private float length =1.0f;
    private bool hookedornot=false;
    private bool clicked=false;

    public float nobiruspeed =140.0f;
    public float maxlength =100.0f;
    public hookcheck hookcheck;
    [Header("ターゲットオブジェクト（マウスカーソル）")] public GameObject target;
    public Quaternion toCursor;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マウスカーソルの方向に向く処理
        Vector3 diff = (target.transform.position - this.transform.position);
        toCursor = Quaternion.FromToRotation (Vector3.up, diff);

        //var pos = Camera.main.WorldToScreenPoint (transform.localPosition);
        //var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos );
        //クリックの受け取り
        if (Input.GetMouseButtonDown(0)){clicked=true;}

        //クリックされたら伸ばす
        
        if (Input.GetMouseButton(0)&&clicked){
            if(!hookcheck.isHooked){
                length+=Time.deltaTime*nobiruspeed;
                this.transform.localScale=new Vector3 (1.0f,length,1.0f);
                hantei.transform.localScale=new Vector3 (1.0f,1.0f/length,1.0f);
            }
            if(length>maxlength){
                clicked=false;
            }
        }
        else {
            length=1.0f;
            this.transform.localScale=new Vector3 (1.0f,length,1.0f);
            hantei.transform.localScale=new Vector3 (1.0f,1.0f,1.0f);
            transform.localRotation=toCursor;
            clicked=false;
            hookcheck.modosu();
        }
    }
    }
