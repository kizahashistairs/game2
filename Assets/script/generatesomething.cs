using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatesomething : MonoBehaviour
{
    [Header("生成オブジェクト")] public GameObject something;
    [Header("生成間隔")]public float interval;

    private float timer=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    public void generate(){
        GameObject g=Instantiate(something);
        g.transform.SetParent(transform);
        g.transform.position=something.transform.position;
        g.SetActive(true);
        //Debug.Log("attack");
    }
}
