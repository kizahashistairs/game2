using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_boss : MonoBehaviour
{
    //[Header("攻撃オブジェクト")] public GameObject fire;
    //[Header("攻撃間隔")]public float interval;
    
    [Header("ライフ")]public int life;
    [SerializeField][Header("標的（多分プレイヤー）")] private GameObject target;
    [SerializeField]
    [Header("bossのbody")]private CircleCollider2D bodycol;
    [SerializeField]
    [Header("bossのlegs")]private PolygonCollider2D legscol;
    [SerializeField]
    [Header("攻撃オブジェクト")] private GameObject fire;
    [Header("死んでからどれくらいで消えるか")]public float deathtime=1.5f;
    [Header("HPバー")]public HPbar HP;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject clearobj;
    //[SerializeField]
    //[Header("弾があたった時のSE")]private AudioClip hitSE;
    
    [Header("死んだときのSE")]public AudioClip downSE;
    


    private float timer;
    private Rigidbody2D rb=null;
    private int rndx,speedx;
    private int MoveDirection = -1;//右に移動するか左に移動するか
    private float deathtimer = 0.0f;//死んでから時間を数える
    
    private bool isDead=false;
    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        //anim =GetComponent<Animator>();
        HP.SetMaxHP((float)life);
          if (rb == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if(life<=0){
            if(!isDead){
                rb.simulated=false;
                anim.Play("boss_down");
                GameManager.instance.PlaySE(downSE);
            }
            isDead = true;
        }
    }


    public void DeathDone(){
        clearobj.SetActive(true);
        Destroy(gameObject);
    }

    public void GetDamage(int dmg){
        life-=dmg;
        HP.GetDamage(dmg);
    }
    public void Jump(){
        rndx = Random.Range(2, 6);
        speedx=rndx; 
        rb.velocity=new Vector2(MoveDirection * speedx,12);
        
    }

    public void flipx(){
            MoveDirection *= -1;
            rb.velocity=new Vector2(MoveDirection * speedx,rb.velocity.y);
    }
    public void attack(){
        GameObject g=Instantiate(fire);
        g.transform.SetParent(transform);
        g.transform.position=fire.transform.position;
        g.SetActive(true);
        Debug.Log("attack");
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            Destroy(collision.gameObject);
            HP.GetDamage(hitdmg);
            if(hitSE!=null){
                GameManager.instance.PlaySE(hitSE);
            }
        }
    }
    */
}