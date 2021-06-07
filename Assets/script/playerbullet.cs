using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class playerbullet : MonoBehaviour
{
    [Header("スピード")]public float speed =3.0f;
    [Header("最大距離")]public float maxDistance=100f;
    public player p;
    [Header("当たったら消える対象")]public string[] kabe={"ground","sokushihookable"};

    private Vector3 defaultPos;
    private Vector3 muki;
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
          muki=p.toCursor*speed;
          rb.velocity=muki;

     }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);
        if(d>maxDistance){
            Destroy(this.gameObject);
        }
        else{
            //rb.MovePosition(transform.position += muki * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(kabe.Contains(other.tag)){
            Destroy(this.gameObject);
        }
    }
}
