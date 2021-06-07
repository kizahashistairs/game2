using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyoffire : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
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
          if (anim == null || fire == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
          else
          {
              fire.SetActive(false);
          }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        if(currentState.IsName("idle")){
            //インターバルを置いて攻撃するアニメーションを再生
            if(timer>interval){
            anim.SetTrigger("attack");
            timer=0.0f;
            }
            else{
                timer+=Time.deltaTime;
            }
        }
        if(life<=0){
            anim.Play("enemy_yarareta");
            rb.velocity=new Vector2(0,0);
            isDead=true;
            col.enabled=false;
            Destroy(gameObject, 1.5f);
        }
    }
    //弾を放出
    public void attack(){
        GameObject g=Instantiate(fire);
        g.transform.SetParent(transform);
        g.transform.position=fire.transform.position;
        g.SetActive(true);
        //Debug.Log("attack");
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
