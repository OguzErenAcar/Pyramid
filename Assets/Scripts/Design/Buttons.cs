using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Buttons : IOyunMenu
{
    /*
    private GameObject Mainmenu;
    private GameObject ucgenimage;
    private GameObject baslik;
    private GameObject basla_button;
    private GameObject basla_button_childs;
    private GameObject rules_button;
    private GameObject rules_button_childs;
    private GameObject sett_button;
    private GameObject sett_button_childs;
    private GameObject quit_button;
    private bool return_cliked = false;
    public static string oynayan = "";
    [SerializeField]
    private GameObject gc; 
    
*/ 
   
    public void Quitbutton()
    {
        Application.Quit();
    }
    void Start()
    {
        Canvas_= GameObject.Find("Canvas");
        CanvasRect = Canvas_.GetComponent<RectTransform>();
        Mainmenu = GameObject.Find("MainMenu");
        ucgenimage = GameObject.Find("ucgenimage");
        baslik = GameObject.Find("baslik");
        basla_button = GameObject.Find("basla_button");
        basla_button_childs = GameObject.Find("basla_button_childs");
        rules_button = GameObject.Find("rules_button");
        rules_button_childs = GameObject.Find("rules_button_childs");
        sett_button = GameObject.Find("sett_button");
        sett_button_childs = GameObject.Find("sett_button_childs");
        quit_button = GameObject.Find("quit_button");
    
    }
    void Update()
    {


    }
    public void StartButton()
    {
        ucgenimage.SetActive(false);
        rules_button.SetActive(false);
        sett_button.SetActive(false);
        quit_button.SetActive(false);
        sett_button_childs.SetActive(false);
        foreach (Transform item in basla_button_childs.transform)
        {
            item.gameObject.SetActive(true);
        }
        basla_button.SetActive(false);
    }
    public void playerby2()
    {    
        SceneManager.LoadScene(1);
        oynayan = "pl2";
    }
    public void computer()
    {
        SceneManager.LoadScene(1);
        oynayan = "bot";

    }
    public void online()
    {
        SceneManager.LoadScene(1);
    }

    void Reset(){

    }

    /*
    public IEnumerator wait1()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(3f);
        print(GameManager.Player_2.name);
        GameManager.Player_2.GetComponent<PlayerManager>().enabled = true;
        GameManager.ikinci_Oyuncu = GameManager.Player_2.GetComponent<BotManager>();
    }
    public IEnumerator wait2()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(3f);
        GameManager.Player_2.GetComponent<BotManager>().enabled = true;
        GameManager.ikinci_Oyuncu = GameManager.Player_2.GetComponent<BotManager>();
    }*/
    public void Rulesbutton()
    {
        baslik.SetActive(false);
        ucgenimage.SetActive(false);
        basla_button.SetActive(false);
        basla_button_childs.SetActive(false);
        rules_button.SetActive(false);
        foreach (Transform item in rules_button_childs.transform)
        {
            item.gameObject.SetActive(true);
        }
        sett_button.SetActive(false);
        sett_button_childs.SetActive(false);
        quit_button.SetActive(false);

    }

    public void Settingsbutton()
    {
        ucgenimage.SetActive(false);
        rules_button.SetActive(false);
        basla_button.SetActive(false);
        quit_button.SetActive(false);
        basla_button_childs.SetActive(false);
        foreach (Transform item in sett_button_childs.transform)
        {
            item.gameObject.SetActive(true);
        }
        sett_button.SetActive(false);
    }


    public void Exitbutton()
    {
        baslik.SetActive(true);
        foreach (Transform item in Mainmenu.transform)
        {
            item.gameObject.SetActive(true);
        }
        foreach (Transform item in basla_button_childs.transform)
        {
            print(item.name);
            item.gameObject.SetActive(false);
        }
        foreach (Transform item in sett_button_childs.transform)
        {
            item.gameObject.SetActive(false);
        }
        foreach (Transform item in rules_button_childs.transform)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void returnbutton()
    {
        //360 640
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
        text.gameObject.GetComponent<TextMeshProUGUI>().text="Çıkmak istediğinize emin misiniz ?";
    }
    public void nobutton()
    {
        rf.GetComponent<RectTransform>().DOMove(new Vector3(CanvasRect.rect.width/2+180, CanvasRect.rect.height/2+1600, 0), 0.5f);
        rf.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0),0.5f);

        gc.GetComponent<GameManager>().enabled = true;
    }
    public void yesbutton()
    {
        SceneManager.UnloadSceneAsync(1);
        PiecesMoveManager.Chosees_list.Clear();
        PiecesMoveManager.places = null;

        SceneManager.LoadScene(0);
    }


    public void pausebutton()
    {
        rf.GetComponent<RectTransform>().DOMove(new Vector3(CanvasRect.rect.width/2+180, CanvasRect.rect.height/2+300, 0), 0.5f);
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
        text.gameObject.GetComponent<TextMeshProUGUI>().text="Duraklatıldı. Çıkmak istediğinize emin misiniz ?";

    }


    public void musicbutton(GameObject image)
    {

        GameObject gO = GameObject.Find("SoundManager");
        if (gO != null)
        { 
            bool bl = new bool();

            bl = gO.GetComponent<AudioSource>().mute;

            if (bl == true)
            {
                gO.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                gO.GetComponent<AudioSource>().mute = true;
            }
        }

    }

    public void sett_sound_button()
    {
        GameObject gO = GameObject.Find("SoundManager");
        if (gO != null)
        { 
            bool bl = new bool();

            bl = gO.GetComponent<AudioSource>().mute;

            if (bl == true)
            {
                gO.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                gO.GetComponent<AudioSource>().mute = true;
            }
        }
    }

}
