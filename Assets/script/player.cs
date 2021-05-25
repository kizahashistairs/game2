using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("弾丸")]public GameObject bullet;
    [Header("ターゲットオブジェクト（マウスカーソル）")] public GameObject target;
    [Header("フックの部分")] public hookcheck h;
    [Header("床判定")]public GroundCheck g;
    public Vector3 toCursor;
    public bool isDown=false;

    private Rigidbody2D rb=null; 
    private Animator anim=null;

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを捕まえる
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>(); 


        //エラー処理
        if(bullet==null){Debug.Log("弾丸が設定されていないぞ");}
        if(anim==null){Debug.Log("animが設定されていないぞ");}
    }

    // Update is called once per frame
    void Update()
    {
        //マウスカーソルの方向を取得
        toCursor = (target.transform.position - this.transform.position);
        Quaternion rot = Quaternion.FromToRotation (Vector3.up, toCursor);

        if(h.isHooked){
            Vector2 hookForce=h.hookedposition-this.transform.position;
            rb.AddForce(hookForce*1.2f);
            Debug.Log(hookForce);
        }

        if(Input.GetMouseButtonDown(1)){
            shot(rot);
        }
    }
    void shot(Quaternion r){
        GameObject g = Instantiate(bullet);
        
        g.transform.localRotation=r;
        g.transform.position=this.transform.position;
        g.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag =="sokushi"){
            Debug.Log("死");
            //playSE("yarareSE");
            RecieveDamage();
        }
    }

    private void RecieveDamage(){
        isDown=true;
        anim.Play("player_tiun");
        rb.velocity=new Vector2 (0,0);
    }
    public bool isDownDone(){
        if(anim!=null){
            AnimatorStateInfo currentState=anim.GetCurrentAnimatorStateInfo(0);
            if(currentState.IsName("player_tiun")){
                if(currentState.normalizedTime>=1){
                    return true;
                }
            }
        }
        return false;
    }
    public void ContinuePlayer(){
        isDown=false;
        anim.Play("player");
        //GameManager.instance.PlaySE(respawnSE);
    }
}
