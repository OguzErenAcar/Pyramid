using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;  
using UnityEngine.SceneManagement;

public abstract   class IOyunMenu : MonoBehaviour
{

    public static RectTransform CanvasRect;
    public static GameObject Canvas_;
    public static  GameObject Mainmenu;
    public static GameObject ucgenimage;
    public static GameObject baslik;
    public static GameObject basla_button;
    public static GameObject basla_button_childs;
    public static GameObject rules_button;
    public static GameObject rules_button_childs;
    public static GameObject sett_button;
    public static GameObject sett_button_childs;
    public static GameObject quit_button; 
    public static bool return_cliked = false;
    public static string oynayan = ""; 
    public static GameObject gc;
    public static GameObject rf ;

    public static GameObject time;
     
    private bool Timer_bool=true; 
    public  static  void returnbutton(string text_)
    {
        rf.GetComponent<RectTransform>().DOMove(new Vector3(CanvasRect.rect.width/2+180, CanvasRect.rect.height/2+320, 0), 0.5f);
        rf.GetComponent<RectTransform>().DOScale(new Vector3(8, 14, 0),0.5f);
        gc.GetComponent<GameManager>().enabled = false;
        Transform text= null;
        foreach (Transform item in rf.transform)
        {
            if(item.name=="dialog_text"){
                text=item;
                break;
            } 
        }
        text.gameObject.GetComponent<TextMeshProUGUI>().text=text_;
    }

}
