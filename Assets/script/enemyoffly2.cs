using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyoffly2 : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
    [Header("移動方法")]public int pattern; //1=左に移動, 2=右に移動
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private SpriteRenderer sr =null;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    private bool isDead=false;
    Vector3 objPosition; // オブジェクトの位置を記録
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
          
          // 最初に置かれた場所を代入 
        objPosition = this.transform.position;
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
        if(pattern==1){//左に移動
            this.transform.position = new Vector3(-Time.time + objPosition.x, objPosition.y, objPosition.z );
        }else if(pattern==2){//右に移動
            this.transform.position = new Vector3(Time.time * 2.0f + objPosition.x, objPosition.y, objPosition.z );
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
