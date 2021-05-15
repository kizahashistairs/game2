using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookcheck : MonoBehaviour
{
    public bool isHooked =false;
    public Vector3 hookedposition;
    public GameObject saki;
    private string hookable ="ground";
    // Start is called before the first frame update
    void Start()
    {
        if(saki==null){Debug.Log("先っぽが設定されていません");}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision){
                Debug.Log("c");
                if(collision.tag==hookable){
                    if(!isHooked){
                        saki.SetActive(true);
                        saki.transform.position=this.transform.position;
                        hookedposition=this.transform.position;
                        isHooked =true;
                        }
                    
                }
    }
    public void modosu(){
        saki.SetActive(false);
        isHooked=false;
    }
    /*private void OnTriggerExit2D(Collider2D collision){
                Debug.Log("c");
                if(collision.tag==hookable){
                    isHooked =false;
                    saki.SetActive(false);
                }
    }*/
}
