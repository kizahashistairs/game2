using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatesomething : MonoBehaviour
{
    [Header("生成オブジェクト")] public GameObject something;
    [Header("生成間隔")]public float interval;
    [Header("画面外でも生成する")] public bool nonVisibleAct; 
    [Header("これが見えていたら生成する1")] public SpriteRenderer hani1;
    [Header("これが見えていたら生成する2")] public SpriteRenderer hani2;


    private float timer=0.0f;
    private SpriteRenderer sr = null; 
    private bool hani1check=true;
    private bool hani2check=true;
    private bool letsgenerate=false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        if(hani1==null){hani1check=false;}
        if(hani2==null){hani2check=false;}
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hani2.isVisible|| hani1.isVisible||sr.isVisible || nonVisibleAct){letsgenerate=true;} 
        else if(hani2check=true){
            if (hani2.isVisible||hani1.isVisible){letsgenerate=true;}
            else{letsgenerate=false;}
            }
        else if(hani1check=true){
            if (hani1.isVisible){letsgenerate=true;}
            else{letsgenerate=false;}
            }
        else{letsgenerate=false;}
        if(letsgenerate) {
        if(timer>interval){
            //anim.SetTrigger("attack");
            timer=0.0f;
            generate();
            }
            else{
                timer+=Time.deltaTime;
            }
         }
    }
    public void generate(){
        GameObject g=Instantiate(something);
        g.transform.SetParent(transform);
        g.transform.position=something.transform.position;
        g.SetActive(true);
        //Debug.Log("attack");
    }
}
