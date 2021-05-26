using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyofouhuku : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
    [Header("移動方法")]public int pattern; //1=縦移動, 2=横移動, 3=円運動
    [Header("移動速度")]public float speed;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private SpriteRenderer sr =null;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    private bool isDead=false;
    // Start is called before the first frame update
    void Start()
    {
        sr =GetComponent<SpriteRenderer>();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        col =GetComponent<BoxCollider2D>();
          if (anim == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        
        if(life<=0){
            anim.Play("enemy_yarareta");
            rb.velocity=new Vector2(0,0);
            isDead=true;
            col.enabled=false;
            Destroy(gameObject, 1.5f);
        }
        // Sinを使って移動させる 
        if(pattern==1){//縦移動
            rb.velocity = new Vector2(0, Mathf.Sin(Time.time) * 1.0f * speed);
        }else if(pattern==2){//横移動
            rb.velocity = new Vector2(Mathf.Sin(Time.time) * 1.0f * speed, 0);
        }else if(pattern==3){//円運動
            rb.velocity = new Vector2(Mathf.Cos(Time.time) * 1.0f * speed, Mathf.Sin(Time.time) * 1.0f * speed);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            life--;
            //playSE(弾が当たったSE)
        }
    }
}
