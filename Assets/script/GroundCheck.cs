using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private string groundTag ="ground";
    private bool onGround=false;
    private bool onGroundEnter=false ,onGroundStay=false,onGroundExit =false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool OnGround(){
        if(onGroundEnter||onGroundStay){
            onGround=true;
        }
        else if(onGroundExit){
            onGround=false;
        }
        onGroundEnter=false;
        onGroundExit=false;
        onGroundStay=false;
        return onGround;
    }
    public void notOnGround(){
        onGroundEnter=false;
        onGroundStay=false;
        onGround=false;
    }
    private void OnTriggerEntre2D(Collider2D collision){
        if(collision.tag ==groundTag){
            onGroundEnter=true;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag ==groundTag){
            //Debug.Log("何かが判定に入っています");
            onGroundStay=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag ==groundTag){
            onGroundExit=true;
        }
    }
}
