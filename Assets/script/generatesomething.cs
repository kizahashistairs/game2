using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GenerateSomething : MonoBehaviour
{
    [Header("生成オブジェクト")] public GameObject Something;
    [Header("生成間隔")]public float interval;
    [Header("画面外でも生成する")] public bool nonVisibleAct; 
    [Header("これが見えていたら生成する1")] public SpriteRenderer hani1;
    [Header("これが見えていたら生成する2")] public SpriteRenderer hani2;
    [Header("生成タイマーの円(無くてもいい)")][SerializeField] private Image Timerimg;
    [Header("オブジェクトプールが予め生成しておく数")][SerializeField] private int def_num = 2;

    private float timer=0.0f;
    private SpriteRenderer sr = null; 
    private bool hani1check=true;
    private bool hani2check=true;
    private bool letsgenerate=false;
    
    //以下オブジェクトプールまわり
    private Queue<GameObject> SomethingPool = new Queue<GameObject>();

    private GameObject CreateNewObject(GameObject pref, Queue<GameObject> pool)
    {
        var newObj = Instantiate(pref);
        newObj.name = pref.name + (pool.Count + 1);
        pool.Enqueue(newObj);
        var component_Something = newObj.GetComponent<PooledItem>();
        component_Something.PassPool(ref SomethingPool);
        return newObj;
    }

    public Queue<GameObject> CreatePool(GameObject pref, int maxCount)
    {
        Queue<GameObject> pool = new Queue<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject(pref, pool);
            newObj.SetActive(false);
        }
        return pool;
    }

    public GameObject GetObject(GameObject pref, Queue<GameObject> pool)
    {
        // 全て使用中だったら新しく作って返す
        if(pool.Count == 0){
            CreateNewObject(pref, pool);
        }
        
        var obj = pool.Dequeue();
        return obj;
    }
    
    

    void Start()
    {
        var component_Something = Something.GetComponent<PooledItem>();
        component_Something.PassPool(ref SomethingPool);
        SomethingPool = CreatePool(Something, def_num);
        sr = GetComponent<SpriteRenderer>(); 
        if(hani1 == null){
            hani1check = false;
            hani1 = sr;
            }
        if(hani2 == null){
            hani2check = false;
            hani2 = sr;
            }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Timerimg != null){
            Timerimg.fillAmount = timer / interval;
        }

        if (hani2.isVisible || hani1.isVisible || sr.isVisible || nonVisibleAct){
            letsgenerate=true;
            } 
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
        //GameObject g=Instantiate(Something);
        GameObject g = GetObject(Something, SomethingPool);
        g.transform.SetParent(transform);
        g.transform.position=Something.transform.position;
        g.SetActive(true);
    }
}
