using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [HideInInspector]public bool isOn=false; 
    [HideInInspector]public bool hajikare=false;

    private string Tag ="canhook";
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag==Tag){
            isOn=true;
        }
        else{
            hajikare=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag==Tag){
            isOn=false;
        }
    }
}
