using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireshot : MonoBehaviour
{
    [Header("スピード")]public float speed =3.0f;
    [Header("最大距離")]public float maxDistance=100f;
    public float mukix =10.0f;
    public float mukiy =10.0f;

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
     }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);
        if(d>maxDistance){
            Destroy(this.gameObject);
        }
        else{
            rb.MovePosition(transform.position += (mukiy*Vector3.up+mukix* Vector3.right) * Time.deltaTime * speed);
        }
    }
}
