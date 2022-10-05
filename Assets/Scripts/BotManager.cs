using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BotManager : IPlayer
{
    List<int> top_dizi = null;
    List<int> plane_dizi = null;
    private int i = 0;
    public int playerno;
    private void Awake()
    {
        oyuncu_sirasi = Int32.Parse(this.gameObject.name.Substring(6));
    }

    void Start()
    {

        Chosees = GameObject.Find("Chosees");
        ornek_top = GameObject.Find("ornek_top");

    }
    public override void Kural2_3_kontrol()
    {
        kareler.Clear();
        Kare_kontrol();
        //    print("kural kontrol");
        
       // print("boyutu_*_ " +kareler.Count);
        if (Eksen_Kontrol(Player))
        {
            //  print("eksen");
            rule3(Player.GetComponent<BotManager>());
            Player.GetComponent<BotManager>().hak_ += 2;

        }
        else if (Ayni_kare_varmi(Player))
        {
            //    print("kural2");
            //  kural="kural2";
            // 
            rule2(Player.GetComponent<BotManager>());
            Player.GetComponent<BotManager>().hak_ += 2;

        }
        else
        {
            toplarini_kitle();
            GameManager.change = true;

        }
    }

     public override void Kural1_0_kontrol()
    {
        Kare_kontrol();
        if (PiecesMoveManager.kareler.Count != 0)//oyunda ustu bos kare varsa
        {
            //ilk başta sadece kural 1 veya 0 oluşabilir
            print("rule1");
            //   kural="kural1";
            rule1(Player.GetComponent<PlayerManager>());

        }//oyunda kare yok 
        else
        {
            //    kural="kural0";
            print("kural0");
            rule0(Player.GetComponent<PlayerManager>());


        }

        hak_++;
    }

    public override void hamle()
    {
        if (hak_ != 0)
        {
            i++;
            StartCoroutine(Easy_Mode());
        }


    }
    private IEnumerator Easy_Mode()
    {   //tıklanabilri top ve planeler dizide  
        System.Random rastgelesayi = new System.Random();
        int no1 = -1;

        top_dizi = null;
        plane_dizi = null;
        top_dizi = new List<int>();
        plane_dizi = new List<int>(); ;

        for (int i = 0; i < transform.childCount; i++)
        {
            ballmanager child = transform.GetChild(i).gameObject.GetComponent<ballmanager>();
            if (child.tiklanabilir)
            {
                top_dizi.Add(child.index);
            }

        }

        no1 = rastgelesayi.Next(0, top_dizi.Count);


        secili_top = transform.GetChild(top_dizi[no1] - (playerno - 1) * 15).GetComponent<ballmanager>();

        try
        {
             
            secili_top.chosee.top = null;
            secili_top.chosee = null;//___________
           
        }
        catch
        {

        }
        ////*****
        
        print("boyutu_*_ " +kareler.Count);
        Show_Suitable_Plane(kural);//2. döngüde else girmeli ki ordan kural 2 3 çalışsın 
        
         
        

        foreach (var item in places)
        {
            if (item.GetComponent<MeshCollider>().enabled)
            {

                plane_dizi.Add(item.index);

            }
        }


        //print(kural + "asad  " + a);
        if (plane_dizi.Count != 0)
        {
            int no2 = rastgelesayi.Next(0, plane_dizi.Count);

            secili_yer = places[plane_dizi[no2]];
        }

        secili_yer.yerdeki_top = secili_top.GetComponent<ballmanager>();

        secili_top.yer = secili_yer.GetComponent<PlaneController>();
        secili_top.katman = secili_yer.katman;
        Settled_Ball(secili_yer, secili_top);
        places[secili_yer.index].yerdeki_top = secili_top;//___________ 
        hak_--;
        yield return new WaitForSeconds(0.8f);
        Kural2_3_kontrol();//kural 2 3 var mı varsa hak yoksa change 
        aynıkare_dizi.Clear();//___________
        secili_yer = null;
        secili_top = null;


    }
    public override void Show_Suitable_Plane(string Kural)//___________
    {

        if (kural == "kural0")
        {   kalan_top--;
            //print("secili_top " + secili_top.index);
            secili_top.transform.DOMoveY(0.9f, 0.5f);

            foreach (var item in kareler)
            {
                places[item.ust_index].GetComponent<MeshCollider>().enabled = true;
                places[item.ust_index].GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);
            }
            for (int i = 0; i < 16; i++)
            {
                if (places[i].yerdeki_top == null)
                {
                    places[i].GetComponent<MeshCollider>().enabled = true;
                    places[i].GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);
                }

            }
        }
        else if (kural == "kural1")
        {   
            //hertürlü karelerin üstü yanacak
            print("kural1");
            secili_top.transform.DOMoveY(0.9f, 0.5f);
            foreach (var item in kareler)
            {//tur olayı geleblr 
             //kendisinin üstündeki kareler
                if (secili_top.katman <= item.katman)
                {
                 //   print("secili topun katmanı:" + secili_top.katman + "  , itemin karenin üst indexi" + item.ust_index);

                    places[item.ust_index].GetComponent<MeshCollider>().enabled = true;
                    places[item.ust_index].GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);
                }

            }
            if (!secili_top.oyundami)
            {   kalan_top--;
                for (int i = 0; i < 16; i++)
                {
                    if (places[i].yerdeki_top == null)
                    {
                        places[i].GetComponent<MeshCollider>().enabled = true;
                        places[i].GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);
                    }
                }

            }
            try
            {
                places[secili_top.yer.index].yerdeki_top = null;
                secili_top.yer.yerdeki_top = null;
            }
            catch
            {

            }

        }
        else
        {
            foreach (var item in Chosees_list)// try lar gidince halay baslar
            {
                print(item.name+"asda");
                 
                if (item.top == null && Player.name == item.parent)
                {
                    Vector3 chosee_vector = new Vector3(item.transform.position.x, item.transform.position.y + 0.0138f, item.transform.position.z);
                    Sequence Seq = DOTween.Sequence();
                    Seq.Append(secili_top.transform.DOJump(chosee_vector, 0.15f, 1, 1));

                    secili_top.chosee = item;
                    item.top = secili_top;
                    kalan_top++;
                    //chooseun topu 
                    print("....");
                    try
                    {//niye

                        places[secili_top.yer.index].yerdeki_top = null;
                        secili_top.yer.yerdeki_top = null;
                        secili_top.yer = null;
                    }
                    catch
                    {

                    }
                    secili_top.tiklanabilir = false;
                    secili_top.oyundami = false;
                    secili_top.karede = false;
                    secili_top.katman = 0;
                    secili_top = null; 
                    Player.GetComponent<BotManager>().hak_--;
                    break;
                }
            }
            if (Player.GetComponent<BotManager>().hak_ == 0)
            {
                toplarini_kitle();
                GameManager.change = true;
            }
        }
    }



    public override void toplarini_kitle()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ballmanager child = Player.transform.GetChild(i).gameObject.GetComponent<ballmanager>();
            child.tiklanabilir = false;
        }


    }



    public override IEnumerator taslari_diz()
    {
        Player = GameObject.Find("Player" + oyuncu_sirasi.ToString());


        for (int i = 15 * oyuncu_sirasi - 15; i < oyuncu_sirasi * 15; i++)
        {
            yield return new WaitForSeconds(0.1f);
            float posizyon_x = Chosees.transform.GetChild(i).gameObject.transform.position.x;
            float posizyon_y = ornek_top.transform.position.y;
            float posizyon_z = Chosees.transform.GetChild(i).gameObject.transform.position.z;

            GameObject new_top = Instantiate(
            ornek_top,
            new Vector3(posizyon_x, posizyon_y, posizyon_z),
            Chosees.transform.GetChild(i).gameObject.transform.rotation);

            if (i > 14)
            {
                new_top.GetComponent<Renderer>().material.SetColor("_Color", new Color(20f / 255f, 20f / 255f, 20f / 255f));

            }
            new_top.name = "top" + i;

            new_top.transform.parent = Player.transform;
            Chosees_list.Add(Chosees.transform.GetChild(i).GetComponent<ChoseesController>());
            Chosees.transform.GetChild(i).GetComponent<ChoseesController>().top = new_top.GetComponent<ballmanager>();
            new_top.GetComponent<ballmanager>().chosee = Chosees.transform.GetChild(i).GetComponent<ChoseesController>();
            new_top.GetComponent<ballmanager>().index = i;
        }

    }
    
}
