using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireshot : MonoBehaviour
{
    [Header("スピード")]public float speed =3.0f;
    [Header("サイズ")]public float size =1.0f;
    [Header("最大距離")]public float maxDistance=100f;
    [Header("標的（多分プレイヤー）")]public GameObject target;
    public float mukix =10.0f;
    public float mukiy =10.0f;
    [Header("動き方のモード：速度を取得したいことがある場合はチェック")]public bool mode1 =false;
    [Header("何かの方向へ向かうかどうか")]public bool aim=false;
    private Vector3 defaultPos;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
          rb = GetComponent<Rigidbody2D>();
          if(rb == null)
          {
              Debug.Log("設定が足りません");
              Destroy(this.gameObject);
          }
          defaultPos = transform.position;

          
          if(aim){
              //向きを取得
              if(target==null){
                  Debug.Log("標的未設定");
              }
              var houkou=target.transform.position-this.transform.position;
              houkou.Normalize();
              mukix=houkou.x;
              mukiy=houkou.y;
              if(mukix>0){
                  transform.localScale = new Vector3(-size, size, size);
              }
          }
     }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);
        if(d>maxDistance){
            Destroy(this.gameObject);
        }
        else{
            if(mode1){rb.velocity=(mukiy*Vector3.up+mukix* Vector3.right)* speed;}
            else{rb.MovePosition(transform.position += (mukiy*Vector3.up+mukix* Vector3.right) * Time.deltaTime * speed);}
        }
    }
}
