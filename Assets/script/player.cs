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
    [Header("ジャンプ力")]public float jumpryoku=3.0f;
    [Header("フックの引力")]public float hookpower=3.0f;
    [Header("フックによる加速表現")]public AnimationCurve Hookspeedcurve;

    private Rigidbody2D rb=null; 
    private Animator anim=null;
    private bool pOnGround=false;
    private float hookTimer=0.0f;

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
            hookTimer+=Time.deltaTime;
            Vector2 hookForce=h.hookedposition-this.transform.position;
            rb.AddForce(hookForce*hookpower*Hookspeedcurve.Evaluate(hookTimer));
            Debug.Log(hookForce);
        }

        if(Input.GetMouseButtonDown(1)){
            shot(rot);
            hookTimer=0.0f;
        }
        pOnGround=g.OnGround();//接地判定
        /// <summary>
        /// ジャンプキー入力
        /// </summary>
        /// <value></value>
        if(Input.GetKeyDown("w")&&pOnGround){
            Jump();
        }
        // 自滅用
        if(Input.GetKey(KeyCode.Escape))RecieveDamage();
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
    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump(){
        rb.velocity=new Vector2(rb.velocity.x,jumpryoku);
        //PlaySE("JumpSE");
    }
    public void ContinuePlayer(){
        isDown=false;
        anim.Play("player");
        //GameManager.instance.PlaySE(respawnSE);
    }
}
