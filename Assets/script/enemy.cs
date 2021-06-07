using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //[Header("攻撃オブジェクト")] public GameObject fire;
    //[Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
    [Header("死んでからどれくらいで消えるか")]public float deathtime=1.5f;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    private bool isDead=false;
    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        //anim =GetComponent<Animator>();
        col =GetComponent<BoxCollider2D>();
          if (rb == null )
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        
        if(life<=0){
            //anim.Play("enemy_yarareta");
            rb.velocity=new Vector2(0,0);
            isDead=true;
            col.enabled=false;
            Destroy(gameObject, deathtime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            Destroy(collision.gameObject);
            life--;
            //playSE(弾が当たったSE)
        }
    }
}

