using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class StageSelect : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private Dropdown dropdown;


    public void SelectStage()
    {
        GameManager.instance.stageNum=dropdown.value+1;
    }
}
