using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyofkeiroshitei : MonoBehaviour
{
    [Header("攻撃オブジェクト")] public GameObject fire;
    [Header("攻撃間隔")]public float interval;
    [Header("ライフ")]public int life=1;
    [Header("移動経路")]public string route;
    //[Header("yarareSE")]public AudioClip yarareSE;
    


    private Animator anim;
    private float timer;
    private SpriteRenderer sr =null;
    private Rigidbody2D rb=null;
    private BoxCollider2D col =null;
    private bool isDead=false;
    Vector3 objPosition; // オブジェクトの位置を記録
    int index = 0;
    int now = 1;
    char[] houkou = new char[100];
    int[] num = new int[100];
    int length = 0;//配列の長さ
    int reverse = 1;//逆方向に移動
    int stop = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr =GetComponent<SpriteRenderer>();
        rb =GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        col =GetComponent<BoxCollider2D>();
        int i;
        
          if (anim == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
          
          // 最初に置かれた場所を代入 
        objPosition = this.transform.position;
        string[] arr =  route.Split(',');//「,」で配列を区切る
        
        for (i=0; i<arr.Length;i++){
            length++;
            char s1 = arr[i][0];//方向を取得
            string s2 = arr[i].Substring(1);//数値を取得
            houkou[i] = s1;
            num[i] = int.Parse(s2)*10;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo currentState =anim.GetCurrentAnimatorStateInfo(0);
        
        
        if(life<=0){
            anim.Play("enemy_yarareta");
            rb.velocity=new Vector2(0,0);
            isDead=true;
            col.enabled=false;
            Destroy(gameObject, 1.5f);
        }
        //移動させる 
        if(houkou[index]=='u'){//上に移動
            this.transform.position += new Vector3(0, 0.05f * reverse,0);
        }else if(houkou[index]=='d'){//下に移動
            this.transform.position += new Vector3(0, -0.05f * reverse, 0);
        }else if(houkou[index]=='l'){//左に移動
            this.transform.position += new Vector3(-0.05f * reverse, 0, 0);
        }else if(houkou[index]=='r'){//右に移動
            this.transform.position += new Vector3(0.05f * reverse, 0, 0);
        }
        if (now<num[index]){
            now++;
        }else {
            now=1;
            if (index<length-1&&index>0){
                index+=reverse;
            }else if(index==length-1){
                reverse=-1;
                if(stop==0){
                    stop=1;
                }else{
                    stop=0;
                    index--;
                }
            }else if(index==0){
                reverse=1;
                if(stop==0){
                    stop=1;
                }else{
                    stop=0;
                    index++;
                }
            }
            Debug.Log(index);
        }
    }
    /*
    r2,u3,l2
    
    i0,r1,s1
    r2
    i1,r1,s0
    u3
    i2,r1,s0
    l2
    i2,r-1,s1
    l-2
    i1,r-1,s0
    u-3
    i0,r-1,s0
    r-2
    i0,r1,s1
    r2
    */
    
    private void OnTriggerEnter2D(Collider2D collision) {
        //弾に当たったらライフがへる
        if(collision.tag=="yourbullet"){
            life--;
            //playSE(弾が当たったSE)
        }
    }
}
