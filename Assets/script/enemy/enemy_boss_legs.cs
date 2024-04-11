using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_boss_legs : MonoBehaviour
{
    private PolygonCollider2D legscol;
    [SerializeField] private enemy_boss boss_main;
    [SerializeField][Header("足に弾が当たった時のダメージ")] private int legdmg=1;
     public bool isjumping=false;
    [SerializeField]
    [Header("弾があたった時のSE")]private AudioClip hitSE;
    private Animator anim=null;
    // Start is called before the first frame update
    void Start()
    {
        legscol =GetComponent<PolygonCollider2D>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            Destroy(collision.gameObject);
            boss_main.GetDamage(legdmg);
            if(hitSE!=null){
                GameManager.instance.PlaySE(hitSE);
            }
        }
        if(collision.tag=="hookshot"){
            collision.gameObject.SendMessage("modosu");
        }
        if(collision.tag=="wall"){
            boss_main.flipx();
            Debug.Log("flip");
        }
        
    }

    //以下アニメーションから呼び出す用の関数
    public void jmp_boss(){
        boss_main.Jump();
    }
    public void atk_boss(){
        boss_main.attack();
        Debug.Log("atk");
    }
    public void DeathDone_boss(){
        boss_main.DeathDone();
    }
}
