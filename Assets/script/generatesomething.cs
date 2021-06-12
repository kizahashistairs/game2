using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatesomething : MonoBehaviour
{
    [Header("生成オブジェクト")] public GameObject something;
    [Header("生成間隔")]public float interval;
    [Header("画面外でも生成する")] public bool nonVisibleAct; 

    private float timer=0.0f;
    private SpriteRenderer sr = null; 
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sr.isVisible || nonVisibleAct) 
         {
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
