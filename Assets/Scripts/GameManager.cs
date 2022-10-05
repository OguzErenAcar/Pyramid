using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

public class GameManager : IOyunMenu
{
    public Text bilgitext;
    public static GameObject Player_1; 
    public static GameObject Player_2;
    private PlaneManager PlaneM;
    private IPlayer Oynayan_Oyuncu;
    private bool oyun_bitti = false;
    public static IPlayer birinci_Oyuncu;
    public static IPlayer ikinci_Oyuncu;
    public string text;
    public static bool change = false;
    private bool Timer_bool=true; 
    public static float time_count = 30f;
    private Vector3 timer_scale;
    private static bool bitti = true;

     
    void Awake()
    { 
        gc = GameObject.Find("GameController");
        rf = GameObject.Find("Return_frame");

        Player_1 = GameObject.Find("Player1");
        Player_2 = GameObject.Find("Player2");

        time = GameObject.Find("timer_");
        timer_scale= time.GetComponent<RectTransform>().localScale; 
        Buttons.oynayan = "bot";
    }
    void Start()
    {
        time_count = 30f;
        birinci_Oyuncu = Player_1.GetComponent<PlayerManager>();
        birinci_Oyuncu.oynayan = "pl1";

        if (Buttons.oynayan == "pl2")
        {
            Player_2.GetComponent<PlayerManager>().enabled = true;
            ikinci_Oyuncu = Player_2.GetComponent<PlayerManager>();
            ikinci_Oyuncu.oynayan = "pl2";
        }
        else if (Buttons.oynayan == "bot")
        {
            Player_2.GetComponent<BotManager>().enabled = true;
            ikinci_Oyuncu = Player_2.GetComponent<BotManager>();
            ikinci_Oyuncu.oynayan = "bot";
        }



        GameObject plane = GameObject.Find("Planes");
        PlaneM = plane.GetComponent<PlaneManager>();

        StartCoroutine(birinci_Oyuncu.taslari_diz());

        StartCoroutine(ikinci_Oyuncu.taslari_diz());

        StartCoroutine(oyun_baslar());
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);

    }
    public IEnumerator oyun_baslar()
    {
        yield return new WaitForSeconds(1f);
        birinci_Oyuncu.sira = true;
        Oynayan_Oyuncu = birinci_Oyuncu;

        Oynayan_Oyuncu.toplarini_kitle();
        Oynayan_Oyuncu.Kural1_0_kontrol();

        yield return new WaitForSeconds(0.5f);//kuralın belirlenmesi için 
    }

    void Update()
    {
        text = PiecesMoveManager.text;

        StartCoroutine(hamleler());

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public IEnumerator hamleler()
    {
        yield return new WaitForSeconds(1.6f);

        if (!oyun_bitti)
        {

            if (birinci_Oyuncu.sira)//devamlı okumasında sıkıntı yok 
            {
                bilgitext.text = "Sıra 1. Oyuncuda!!!" + text; 
                this.gameObject.transform.GetComponent<CameraManager>().set_camera("Player1");
                time.GetComponent<TextMeshProUGUI>().text = Timer(Timer_bool,time_count)[0]+":"+Timer(Timer_bool,time_count)[1];
                if(!Timer_bool){
                    yield return new WaitForSeconds(1.6f);  
                    Timer_bool=true;         
                } 
                birinci_Oyuncu.hamle();
            }
            else if (ikinci_Oyuncu.sira)
            {
                bilgitext.text = "Sıra 2. Oyuncuda!!!" + text;
                if (ikinci_Oyuncu.oynayan == "pl2")
                {
                    this.gameObject.transform.GetComponent<CameraManager>().set_camera("Player2"); 
                }
                time.GetComponent<TextMeshProUGUI>().text = Timer(Timer_bool,time_count)[0]+":"+Timer(Timer_bool,time_count)[1];
                if(!Timer_bool){
                    yield return new WaitForSeconds(1.6f);  
                    Timer_bool=true;         
                } 
                ikinci_Oyuncu.hamle();
            }

        }
        else
        {
            oyun_biter();
        }

        if (change)
        {
            change = false;///bu ifteki codeların sırasi değişmesi gerekebirl
            this.gameObject.transform.GetComponent<CameraManager>().rotate_bool = true;
            Timer_bool=false;
            time_count=30f;
           // print("changede ");
            if ((birinci_Oyuncu.kalan_top == 0 && ikinci_Oyuncu.kalan_top == 0))
            {
                oyun_bitti = true;

            }
            else if (birinci_Oyuncu.kalan_top == 0 || ikinci_Oyuncu.kalan_top == 0)
            {
                if (Math.Abs(birinci_Oyuncu.kalan_top - ikinci_Oyuncu.kalan_top) > 1)
                {
                    oyun_bitti = true;
                }
                else
                {
                    PlaneM.sifirla(); 
                    Change(Oynayan_Oyuncu);
                    yield return new WaitForSeconds(0.8f);//kamera dönmesi için 
                    Oynayan_Oyuncu.toplarini_kitle();
                    Oynayan_Oyuncu.Kural1_0_kontrol();

                }
            }
            else
            {

                PlaneM.sifirla(); 
                Change(Oynayan_Oyuncu);
                yield return new WaitForSeconds(0.8f);//kamera dönmesi için 

                Oynayan_Oyuncu.toplarini_kitle();
                Oynayan_Oyuncu.Kural1_0_kontrol();

            };


        }

    }

    public void Change(IPlayer Pl)
    {
        Pl.sira = false;
        if (Pl == birinci_Oyuncu)
        {
            ikinci_Oyuncu.sira = true;
            Oynayan_Oyuncu = ikinci_Oyuncu;
        }
        else if (Pl == ikinci_Oyuncu)////////////////////
        {
            birinci_Oyuncu.sira = true;
            Oynayan_Oyuncu = birinci_Oyuncu;
        }
    }

    public string[] Timer(bool b1,float count)
    {
        if(b1){ 
            if (count > 0)
            {
                int int_count=Convert.ToInt32(count);
                count -= Time.deltaTime;
                int int_count2=Convert.ToInt32(count);

                float second =count %(int)count;
                string currentTime = string.Format("{0:00.00}{1:00}", count,second); 
                
                string minutes=currentTime[0].ToString()+currentTime[1].ToString();
                string seconds=currentTime[3].ToString()+currentTime[4].ToString();
                 
                time_count=count;
                if(int_count !=int_count2  ){ 
                    time.GetComponent<RectTransform>().localScale=timer_scale;
                    time.transform.DOScale(timer_scale*(1f/0.6f),0.2f);
                    time.transform.DOScale(timer_scale,0.2f);
                }
                
                string[] time_array={minutes.ToString(),seconds.ToString()};
                return time_array;
            }
            else
            {   change=true;
                oyun_bitti=true;
                return new string[]{"00","00"}; 
            }

        }
        else{
             return new string[]{"30","00"};
        }  

    }
    

    public static void oyun_biter()
    {
        if (bitti)
        {
            returnbutton("!!Oyun bitti!!");
        }
    }
    /*
        private void set_camera(string name)
        {
            float angle = 0.1f;
            if (b1)
            {

                G_c.transform.eulerAngles = new Vector3(0, (int)G_c.transform.eulerAngles.y, 0);


                // bool set = false;
                float rotation_y = G_c.transform.eulerAngles.y;
                float count = 0;
                float abs_rotation = Mathf.Abs(rotation_y - 180);

                if (name == "Player2")
                {

                    // print("player1:");
                    if (180 >= rotation_y)
                    {
                        //    print("180den kücük");
                        count = rotation_y / angle;
                        if (count > angle)
                        {

                            //      print("debug" + count);
                            G_c.transform.Rotate(0, -2f, 0);
                            //        set = true;
                        }
                    }
                    else if (180 < rotation_y)
                    {
                        //  print("180den büyük");
                        count = (rotation_y / angle) - 180;
                        if (count > angle)
                        {
                            //     print("debug" + count);
                            G_c.transform.Rotate(0, 2f, 0);
                            //          set = true;
                        }
                    }
                }
                else if (name == "Player1")
                {

                    //  print("player2:");
                    if (180 >= rotation_y)
                    {
                        //  print("180den kücük");
                        count = abs_rotation / angle;
                        if (count > angle)
                        {
                            //      print("debug" + count);
                            G_c.transform.Rotate(0, 2f, 0);
                            //          set = true;
                        }
                    }
                    else if (180 < rotation_y)
                    {
                        //   print("180den büyük");
                        count = (abs_rotation / angle);
                        if (count > angle)
                        {
                            //    print("debug" + count);
                            G_c.transform.Rotate(0, -2f, 0);
                            //            set = true;
                        }
                    }
                }

            }

        }
    public void set_camera(string name)
    { 
        if (rotate_bool)
        {  
            rotate_bool=false;
            print("rotate_bool true ");
            this.gameObject.transform.eulerAngles = new Vector3(0, (int)G_c.transform.eulerAngles.y, 0);
          
          //  Sequence Seq = DOTween.Sequence(); 
            if (name == "Player2")
            {
                  Vector3 vector_ = new Vector3(0, 0, 0);
                  this.gameObject.transform.DORotate(vector_ , 1.5f);
            }
            else if (name == "Player1")
            {
                Vector3 vector_ = new Vector3(0, 180, 0);
                  this.gameObject.transform.DORotate(vector_ , 1.5f);

            }

        }

    }
*/


   
}
