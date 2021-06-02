using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyof3direction : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
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
          if (anim == null || fire1 == null || fire2 == null || fire3 == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
          else
          {
              fire1.SetActive(false);
              fire2.SetActive(false);
              fire3.SetActive(false);
          }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        if(currentState.IsName("idle")){
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
    public void attack(){
        GameObject g1=Instantiate(fire1);
        g1.transform.SetParent(transform);
        g1.transform.position=fire1.transform.position;
        g1.SetActive(true);

        GameObject g2=Instantiate(fire2);
        g2.transform.SetParent(transform);
        g2.transform.position=fire2.transform.position;
        g2.SetActive(true);

        GameObject g3=Instantiate(fire3);
        g3.transform.SetParent(transform);
        g3.transform.position=fire3.transform.position;
        g3.SetActive(true);
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
