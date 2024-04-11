using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class besttimegenerate : MonoBehaviour
{
    [SerializeField]
    private GameObject timetext;
    [SerializeField]
    private int numberofstage;
    // Start is called before the first frame update
    void Start()
    {
 
        for (int i = 0; i <= numberofstage; i++) {
            //if (myStatus.GetItemFlag(item.GetItemName())) {
                //　スロットのインスタンス化
                var instanceSlot = Instantiate<GameObject>(timetext, transform);
                instanceSlot.SetActive(true);
                //　スロットゲームオブジェクトの名前を設定
                instanceSlot.name = "StageTime" + i;
                //　Scaleを設定しないと0になるので設定
                instanceSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                //　アイテム情報をスロットのProcessingSlotに設定する
                instanceSlot.GetComponent<Text>().text = "Stage"+i.ToString() + ": " + PlayerPrefs.GetString ("Stage"+i.ToString(), "タイム無し");
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
