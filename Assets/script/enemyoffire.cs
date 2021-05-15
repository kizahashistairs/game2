using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyoffire : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private SpriteRenderer sr =null;
    private Rigidbody2D rb=null;
    private ObjectCollision oc =null;
    private BoxCollider2D col =null;
    private bool isDead=false;
    // Start is called before the first frame update
    void Start()
    {
        sr =GetComponent<SpriteRenderer>();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        oc =GetComponent<ObjectCollision>();
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
            if(timer>interval){
            anim.SetTrigger("attack");
            timer=0.0f;
            }
            else{
                timer+=Time.deltaTime;
            }
        }
        if(oc.playerStepOn){
            if(!isDead){
                //GameManager.instance.PlaySE(yarareSE);
                anim.Play("enemy_yarareta");
                rb.velocity=new Vector2(0,0);
                isDead=true;
                col.enabled=false;
                Destroy(gameObject, 1.5f);

            }
        }
    }
    public void attack(){
        GameObject g=Instantiate(fire);
        g.transform.SetParent(transform);
        g.transform.position=fire.transform.position;
        g.SetActive(true);
        //Debug.Log("attack");
    }
}
