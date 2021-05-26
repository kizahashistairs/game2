using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyofippou : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
    [Header("移動方法")]public int pattern; //1=上に移動, 2=下に移動, 3=左に移動, 4=右に移動
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
        //移動させる 
        if(pattern==1){//上に移動
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(0, 1.0f * speed);
        }else if(pattern==2){//下に移動
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(0, -1.0f * speed);
        }else if(pattern==3){//左に移動
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(-1.0f * speed, 0);
        }else if(pattern==4){//右に移動
            transform.localScale = new Vector2(-1, 1);
            rb.velocity = new Vector2(1.0f * speed, 0);
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
