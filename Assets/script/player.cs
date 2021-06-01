using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("弾丸")]public GameObject bullet;
    [Header("ターゲットオブジェクト（マウスカーソル）")] public GameObject target;
    [Header("フックの部分")] public GameObject hook;
    [Header("床判定")]public GroundCheck g;
    [Header("銃を撃った時のSE")]public AudioClip ShotSE;
    public Vector3 toCursor;
    public bool isDown=false;
    [Header("ジャンプ力")]public float jumpryoku=3.0f;
    [Header("移動速度b")]public float speed=1.0f;
    [Header("フックの引力")]public float hookpower=3.0f;
    [Header("フックによる加速表現")]public AnimationCurve Hookspeedcurve;

    private Rigidbody2D rb=null; 
    private Animator anim=null;
    private hookshot h=null;
    //private LineRenderer line;//線を結ぶためのやつ
    [Header("マウスカーソルの方向")]private Quaternion rot;
    public bool pOnGround=false;
    private float hookTimer=0.0f;

    // Start is called before the first frame update
    void Start()
    {
                    GameManager.instance.PlaySE(ShotSE);
        //コンポーネントを捕まえる
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        h=hook.GetComponent<hookshot>(); 
        //line=GetComponent<LineRenderer>();


        //エラー処理
        if(bullet==null){Debug.Log("弾丸が設定されていないぞ");}
        if(anim==null){Debug.Log("animが設定されていないぞ");}
    }

    // Update is called once per frame
    void FixedUpdate(){
        //マウスカーソルの方向を取得
        toCursor = (target.transform.position - this.transform.position);
        toCursor.Normalize();
        rot = Quaternion.FromToRotation (Vector3.up, toCursor);

        //hookが引っ掛かっているときの移動
        if(h.isHooked){
            pOnGround=false;
            hookTimer+=Time.deltaTime;
            Vector2 hookForce=h.hookedposition-this.transform.position;
            Vector2 yoko=new Vector2 (Input.GetAxis("Horizontal"),0f);
            if(hookForce.magnitude<1.0f){
                yoko/=6;
                rb.AddForce(-0.7f*Physics.gravity);
                hookForce+=new Vector2 (Input.GetAxis("Horizontal")/6-hookForce.x*0.8f,hookForce.y/2);
                rb.velocity=0.95f*rb.velocity;
                rb.AddForce(5*hookForce);
                }
            else if(target.transform.position.y - this.transform.position.y<1.0f){
                rb.AddForce(-0.5f*Physics.gravity);
                hookForce+=new Vector2 (Input.GetAxis("Horizontal")/4-hookForce.x/2,hookForce.y/4);
                rb.velocity+=hookpower*hookForce.normalized*Hookspeedcurve.Evaluate(hookTimer)/2;
                yoko/=4;
            }
            else{
                hookForce+=new Vector2 (Input.GetAxis("Horizontal")/4-hookForce.x/4,hookForce.y/4);
                rb.velocity+=hookpower*hookForce.normalized*Hookspeedcurve.Evaluate(hookTimer)/2;
                yoko/=4;
                }
            Debug.Log(hookForce);
            rb.AddForce(yoko);
            //rb.AddForce(hookForce*hookpower*Hookspeedcurve.Evaluate(hookTimer));
            }
        
        
        //hookに引っ掛かっていないときの移動
        else{
        if(Input.GetAxis("Horizontal")>0){
            Vector2 idouForce=new Vector2 (Input.GetAxis("Horizontal")*speed,0);
            if(rb.velocity.x>1.5&&rb.velocity.x<=2){rb.velocity=new Vector2(2,rb.velocity.y);}
            else if(rb.velocity.x>2){rb.AddForce(idouForce/8);}
            else {rb.AddForce(idouForce);}
        }
        if(Input.GetAxis("Horizontal")<0){
            Vector2 idouForce=new Vector2 (Input.GetAxis("Horizontal")*speed,0);
            if(rb.velocity.x>-2&&rb.velocity.x<-1.5){rb.velocity=new Vector2(-2,rb.velocity.y);}
            else if(rb.velocity.x<-2){rb.AddForce(idouForce/8);}
            else {rb.AddForce(idouForce);}
        }
        }
    }
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)){
            hookTimer=0.0f;   
        }
        /// 銃弾を飛ばす
        if(Input.GetMouseButtonDown(1)){
            shotb(rot);
            GameManager.instance.PlaySE(ShotSE);
        }
        if(Input.GetMouseButtonDown(0)){
            shoth(rot);
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
    void shotb(Quaternion r){
        GameObject g = Instantiate(bullet);
        
        g.transform.localRotation=r;
        g.transform.position=this.transform.position;
        g.SetActive(true);
    }
    void shoth(Quaternion r){
        hook.transform.localRotation=r;
        hook.transform.position=this.transform.position;
        hook.SetActive(true);
        h.shot(toCursor,this.transform.position);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag =="sokushi"){
            Debug.Log("死");
            //playSE(yarareSE);
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
