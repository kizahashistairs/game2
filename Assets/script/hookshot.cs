using System.Collections;
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
    private string[] hookable={"ground","hookable","sokushihookable"};//hookをかけられるタグ
    private string[] cannothook={"cannothook"};//hookをかけられるタグ

    private Vector3 defaultPos;
    private Vector2 defaultspeed;
    private Rigidbody2D rb;
    private Rigidbody2D prb;
    private GameObject hookedObject;
    public Rigidbody2D hookedrb;
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
        if (!GameManager.instance.isStageClear&&!p.isDown&&Input.GetMouseButton(0)){
            if(!isHooked){
                timer+=Time.deltaTime; 
                var kansei=prb.velocity/2;
                rb.velocity=defaultspeed+kansei;
            }
            if(isHooked){
                //hookをひっかけているものが動くなら、フックも動かす
                if(hookedrb==null){rb.velocity=new Vector2 (0,0);}
                else{
                    Debug.Log(hookedrb.velocity);
                    rb.velocity=hookedrb.velocity;
                    hookedposition=this.transform.position;
                    }
                if(hookedObject==null){modosu();}
                }
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
                        hookedObject=collision.gameObject;
                        if(hookedObject.GetComponent<Rigidbody2D>()!=null){
                            hookedrb=hookedObject.GetComponent<Rigidbody2D>();
                        }
                        isHooked =true;
                        }
                    
                }
                else if(cannothook.Contains(collision.tag)){
                    modosu();
                }
    }
    //フックを戻すメソッド
    public void modosu(){
        timer=0.0f;
        saki.SetActive(false);
        isHooked=false;
        line.enabled=false;
        hookedrb=null;
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
