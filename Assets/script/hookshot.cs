﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class hookshot : MonoBehaviour
{
    public bool isHooked =false;
    public Vector3 hookedposition;
    public GameObject saki;
    [Header("スピード")]public float speed =7.5f;
    [Header("最大距離")]public float maxtime=0.8f;
    public player p;
    [Header("hookable")]private string[] hookable={"ground","hookable"};

    private Vector3 defaultPos;
    private Vector2 defaultspeed;
    private Rigidbody2D rb;
    private Rigidbody2D prb;
    private LineRenderer line;//線を結ぶためのやつ
    private float timer=0.0f;
    private bool clicked=true;
    // Start is called before the first frame update
    void Start()
    {
          rb = GetComponent<Rigidbody2D>();
          prb = p.GetComponent<Rigidbody2D>();
          line=GetComponent<LineRenderer>();
          line.enabled=true;
          line.SetPosition(0, this.transform.position);
          line.SetPosition(1, this.transform.position);
          if(rb == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
          defaultPos = transform.position;
     }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonUp(0)) {modosu();}
    }
    void FixedUpdate()
    {
        //線でプレイヤーと結ぶ
        if(clicked){
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, p.transform.position);
        }
        if (!p.isDown&&Input.GetMouseButton(0)){
            if(!isHooked){
                timer+=Time.deltaTime; 
                var kansei=prb.velocity/2;
                rb.velocity=defaultspeed+kansei;
            }
            if(isHooked){rb.velocity=new Vector2 (0,0);}
            if(timer>maxtime){
                clicked=false;
                modosu();
            }
        }
        else {
            clicked=false;
            modosu();
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
                Debug.Log("c");
                if(hookable.Contains(collision.tag)){
                    if(!isHooked){
                        saki.SetActive(true);
                        saki.transform.position=this.transform.position;
                        hookedposition=this.transform.position;
                        isHooked =true;
                        }
                    
                }
    }
    //フックを戻すメソッド
    public void modosu(){
        timer=0.0f;
        saki.SetActive(false);
        isHooked=false;
        line.enabled=false;
        this.gameObject.SetActive(false);
        clicked=false;
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.transform.position);
    }
    public void shot(Vector3 muki,Vector3 pos){
        defaultspeed=muki*speed;
        rb.velocity=muki*speed;
        clicked=true;
        line.enabled=true;
        this.transform.position=pos;
        this.transform.localRotation=Quaternion.FromToRotation (Vector3.up, muki);
    }
}