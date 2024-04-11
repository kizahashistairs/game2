using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyoffire : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life;
    [Header("HPバー")]public HPbar HP;
    [SerializeField]
    [Header("弾があたった時のSE")]private AudioClip hitSE;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    [SerializeField]private Animator anim;
    private float timer;
    private SpriteRenderer sr =null;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    
    // 以下オブジェクトプール
    private Queue<GameObject> firePool = new Queue<GameObject>();

    private GameObject CreateNewObject(GameObject pref, Queue<GameObject> pool)
    {
        var newObj = Instantiate(pref);
        newObj.name = pref.name + (pool.Count + 1);
        //newObj.transform.position = defaultPos_fire;
        pool.Enqueue(newObj);
        var component_fireshot = newObj.GetComponent<PooledItem>();
        component_fireshot.PassPool(ref firePool);
        return newObj;
    }

    public Queue<GameObject> CreatePool(GameObject pref, int maxCount)
    {
        Queue<GameObject> pool = new Queue<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject(pref, pool);
            newObj.SetActive(false);
        }
        return pool;
    }

    public GameObject GetObject(GameObject pref, Queue<GameObject> pool)
    {
        // 全て使用中だったら新しく作って返す
        if(pool.Count == 0){
            CreateNewObject(pref, pool);
        }
        
        var obj = pool.Dequeue();
        return obj;
    }
    void Start()
    {
        var component_fireshot = fire.GetComponent<PooledItem>();
        component_fireshot.PassPool(ref firePool);
        firePool = CreatePool(fire, 2);
        //firePool.Enqueue(fire);
        sr =GetComponent<SpriteRenderer>();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        col =GetComponent<BoxCollider2D>();
        HP.SetMaxHP((float)life);
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
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        
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
            col.enabled=false;
            Destroy(gameObject, 1.5f);
        }
    }
    //弾を放出
    public void attack(){
        //GameObject g=Instantiate(fire);
        GameObject g = GetObject(fire, firePool);
        g.transform.SetParent(transform);
        g.transform.position = this.transform.position;
        g.SetActive(true);
        //Debug.Log("attack");
    }
    //弾に当たったら
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
