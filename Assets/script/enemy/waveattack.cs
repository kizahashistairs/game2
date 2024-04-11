using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveattack : MonoBehaviour
{
    [Header("攻撃オブジェクト")][SerializeField] private GameObject wave1,wave2;
    private bool firsttime=true;
        [SerializeField]
    [Header("SE")]private AudioClip waveSE;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.tag=="ground"){
        if(firsttime){
            firsttime=false;
        }
        else{
        wave1.transform.SetParent(transform);
        wave1.transform.position=this.transform.position;
        wave1.SetActive(true);
        wave2.transform.SetParent(transform);
        wave2.transform.position=this.transform.position;
        wave2.SetActive(true);
        if(waveSE!=null){
                GameManager.instance.PlaySE(waveSE);
            }
        }
        }

    }
   
}
