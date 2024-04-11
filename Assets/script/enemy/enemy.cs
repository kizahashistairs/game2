using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //[Header("攻撃オブジェクト")] public GameObject fire;
    //[Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life;
    [Header("死んでからどれくらいで消えるか")]public float deathtime=1.5f;
    [Header("HPバー")]public HPbar HP;
    [SerializeField]
    [Header("弾があたった時のSE")]private AudioClip hitSE;
    [SerializeField]
    [Header("何かによって生成される敵かどうか")]private bool isGenerated;
    private int life_default;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    // Start is called before the first frame update
    void Start()
    {
        life_default = life;
        rb =GetComponent<Rigidbody2D>();
        //anim =GetComponent<Animator>();
        col =GetComponent<BoxCollider2D>();
        HP.SetMaxHP((float)life);
          if (rb == null )
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
    }

    // Update is called once per frame
    void OnEnable()
    {
        HP.SetMaxHP((float)life);
    }
    void FixedUpdate()
    {
        //AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        
        if(life<=0){
            //anim.Play("enemy_yarareta");
            if(isGenerated){
                this.gameObject.SetActive(false);
                life = life_default;
            }
            else{
                rb.velocity=new Vector2(0,0);
                col.enabled=false;
                Destroy(gameObject, deathtime);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            Destroy(collision.gameObject);
            life--;
            HP.GetDamage(1);
            if(hitSE!=null){
                GameManager.instance.PlaySE(hitSE);
            }
        }
    }
}

