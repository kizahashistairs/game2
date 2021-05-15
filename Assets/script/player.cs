using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("弾丸")]public GameObject bullet;
    [Header("ターゲットオブジェクト（マウスカーソル）")] public GameObject target;
    [Header("フックの部分")] public hookcheck h;
    public Vector3 toCursor;

    private Rigidbody2D rb=null; 

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントを捕まえる
        rb = GetComponent<Rigidbody2D>();


        //エラー処理
        if(bullet==null){Debug.Log("弾丸が設定されていないぞ");}
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
}
