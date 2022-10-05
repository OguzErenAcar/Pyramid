using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;
public class PiecesMoveManager : MonoBehaviour
{
    
    public static string kural = "kural0";
    public static PlaneController[] places;//şu an bütün sıkıntı sınıflarda birbirini işaret ederken bu diziyi unutman 
    //protected static ballmanager[] plane_ball = new ballmanager[30];//plane indexleri ile topları yerleştir.
    protected static ballmanager secili_top;
    protected static PlaneController secili_yer { get; set; }
    public static List<SquareManager> kareler = new List<SquareManager>();
    public static List<ChoseesController> Chosees_list = new List<ChoseesController>();
    public static List<int[]> eksen_dizi = new List<int[]>();
    public static List<int> aynıkare_dizi = new List<int>();
    public virtual void OnClick(GameObject g_o, GameObject player) { }
    public static string text;

    void Start(){
        kareler.Clear();
    }
    public void Settled_Ball(PlaneController sy, ballmanager st)//topu yerleştir
    {
        sy.GetComponent<Renderer>().material.DOFade(0f, 0.5f);
        sy.GetComponent<MeshCollider>().enabled = false;
        Vector3 plane_vector = new Vector3(sy.transform.position.x, sy.transform.position.y+0.005f, sy.transform.position.z);
        st.transform.DOJump(plane_vector, 0.15f, 1, 1);
        st.GetComponent<ballmanager>().oyundami = true;

    }
    public bool Eksen_Kontrol(GameObject Player)
    { //doğru ise 2 hamle hakkı
      //X 
      //  print("eksen kontrol");
        bool renk = false;
        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            try
            {
                renk = GameObject.ReferenceEquals(places[0 + i].yerdeki_top.parent, places[4 + i].yerdeki_top.parent) &&
                       GameObject.ReferenceEquals(places[4 + i].yerdeki_top.parent, places[8 + i].yerdeki_top.parent)
                    && GameObject.ReferenceEquals(places[8 + i].yerdeki_top.parent, places[12 + i].yerdeki_top.parent);

                if (renk)
                {
                    int[] dizi = { 0 + i, 4 + i, 8 + i, 12 + i };
                    int count = 0;
                    foreach (var item in dizi)
                    {
                        if (!places[item].yerdeki_top.ustu_dolu)
                        {
                            count++;
                        }
                    }
                    if (count >= 2)
                    {
                        eksen_dizi.Add(dizi);
                        c++;
                    }

                }
            }
            catch
            {
                renk = false;
            }

        }

        //Y
        for (int i = 0; i < 4; i++)
        {
            try
            {
                renk = GameObject.ReferenceEquals(places[0 + i * 4].yerdeki_top.parent, places[1 + i * 4].yerdeki_top.parent) &&
                      GameObject.ReferenceEquals(places[1 + i * 4].yerdeki_top.parent, places[2 + i * 4].yerdeki_top.parent)
                   && GameObject.ReferenceEquals(places[2 + i * 4].yerdeki_top.parent, places[3 + i * 4].yerdeki_top.parent);

                if (renk)
                {

                    int[] dizi = { 0 + i * 4, 1 + i * 4, 2 + i * 4, 3 + i * 4 };
                    int count = 0;
                    foreach (var item in dizi)
                    {
                        if (!places[item].yerdeki_top.ustu_dolu)
                        {
                            count++;
                        }
                    }
                    if (count >= 2)
                    {
                        eksen_dizi.Add(dizi);
                        c++;
                    }

                }
            }
            catch
            {
                renk = false;
            }
        }
        if (c > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool Ayni_kare_varmi(GameObject Player)
    {
        bool b1 = false;
        print("kareler boyut "+kareler.Count);
        foreach (var item in kareler)
        { 
            GameObject go = places[item.indexler[0]].yerdeki_top.parent;
            if (item.tur&&go==Player)//aynı renktekiler için 
            {
                int count = 0;
                List<int> dizi = new List<int>();
                foreach (var index in item.indexler)
                {

                    dizi.Add(index);
                    if (!places[index].yerdeki_top.ustu_dolu)
                    {
                        count++;

                    }
                }
                if (count >= 2)
                {

                    foreach (var item2 in dizi)
                    {
                        aynıkare_dizi.Add(item2);
                        print("ayni kare diziye eklendi->"+item2);
                    }
                    b1 = true;
                }
            }

        }




        return b1;
    }

    public void Kare_kontrol()
    {

        int katman = 1;
        int k = 0;
        int j = 0;
        int t = 0;
        bool renk = false;

        for (int i = 0; i < 26; i++)
        {

            if (i != 3 && i != 7 && i != 11 && i != 18 && i != 21)//veya eşit değilse ?? 
            {

                bool b1 = places[0 + i].yerdeki_top;
                bool b2 = places[1 + i].yerdeki_top;
                bool b3 = places[4 + i + t].yerdeki_top;
                bool b4 = places[5 + i + t].yerdeki_top;

                if (b1 && b2 && b3 && b4)
                {
                    bool ustu_dolu = false;
                    places[0 + i].yerdeki_top.karede = true;
                    places[1 + i].yerdeki_top.karede = true;
                    places[4 + i + t].yerdeki_top.karede = true;
                    places[5 + i + t].yerdeki_top.karede = true;

                    /*  print(places[0 + i].name);
                      print(places[1 + i].name);
                      print(places[4 + i + t].name);
                      print(places[5 + i + t].name);*/


                    places[0 + i].yerdeki_top.katman = katman;
                    places[1 + i].yerdeki_top.katman = katman;
                    places[4 + i + t].yerdeki_top.katman = katman;
                    places[5 + i + t].yerdeki_top.katman = katman;
                    //ayni kare olayı 
                    try
                    {
                        renk = GameObject.ReferenceEquals(places[0 + i].yerdeki_top.parent, places[1 + i].yerdeki_top.parent) &&
                               GameObject.ReferenceEquals(places[1 + i].yerdeki_top.parent, places[4 + i + t].yerdeki_top.parent)
                            && GameObject.ReferenceEquals(places[4 + i + t].yerdeki_top.parent, places[5 + i + t].yerdeki_top.parent);

                    }
                    catch
                    {
                    }

                    int ust = 0;
                    if (0 <= i && i < 11)
                    {
                        ust = i + 16 - k;
                        if (places[i + 16 - k].yerdeki_top != null)
                        {//3 ve 7 
                            ustu_dolu = true;
                        }

                    }
                    else if (16 <= i && i < 21)
                    {//18
                        ust = i + 9 - j;
                        if (places[i + 9 - j].yerdeki_top != null)
                        {
                            ustu_dolu = true;
                        }
                    }
                    else if (i == 25)
                    {
                        ust = 29;
                        if (places[29].yerdeki_top != null)
                        {
                            ustu_dolu = true;
                        }
                    }

                    if (ustu_dolu)
                    {
                        places[0 + i].yerdeki_top.ustu_dolu = true;
                        places[1 + i].yerdeki_top.ustu_dolu = true;
                        places[4 + i + t].yerdeki_top.ustu_dolu = true;
                        places[5 + i + t].yerdeki_top.ustu_dolu = true;
                    }
                    else
                    {
                        //sadece üstü boş kareler ile işlemler  
                        SquareManager kare = gameObject.AddComponent<SquareManager>();
                        kare.Create(renk, places[0 + i].yerdeki_top.katman, new int[] { 0 + i, 1 + i, 4 + i + t, 5 + i + t }, ust);
                        //  print(places[0 + i].yerdeki_top.katman + "...katman");
                        kareler.Add(kare);

                    }




                }

            }
            else if (i == 3 || i == 7)
            {
                k++;
            }
            else if (i == 11)
            {
                i = 15;
                katman = 2;
                t--;
            }
            else if (i == 18)
            {
                j++;
            }

            else if (i == 21)
            {
                i = 24;
                t--;
                katman = 3;
            }
        }

    }
    public void rule0(IPlayer Pl)
    {
        kural = "kural0";
        for (int i = 0; i < Pl.transform.childCount; i++)
        {
            GameObject top_ = Pl.transform.GetChild(i).gameObject;
            if (!top_.GetComponent<ballmanager>().oyundami)
            {

                top_.GetComponent<ballmanager>().tiklanabilir = true;

            }
            else
            {
                top_.GetComponent<ballmanager>().tiklanabilir = false;
            }

        }

    }
    public void rule1(IPlayer Pl)//burayı kontr et burda sıkıntı yok sanırm 
    {
        kural = "kural1";
       print("rule1_");
        for (int i = 0; i < Pl.transform.childCount; i++)
        {
            GameObject top_ = Pl.transform.GetChild(i).gameObject;
            if (!top_.GetComponent<ballmanager>().oyundami)
            {
                //  print("ifte1");
                top_.GetComponent<ballmanager>().tiklanabilir = true;
            }
            
            else if (!top_.GetComponent<ballmanager>().karede)
            {
                print("rule1 için kareler count "+kareler.Count);
                foreach (var item in kareler)
                {
                    print(item.katman+"asdf");
                    if (top_.GetComponent<ballmanager>().katman <= item.katman)
                    {
                        top_.GetComponent<ballmanager>().tiklanabilir = true;
                        print("rule1 için tıklanır top "+top_.name);
                    }
                }
            }
            else
            {
                top_.GetComponent<ballmanager>().tiklanabilir = false;
            }

        }
    }


    public void rule2(IPlayer Pl)
    {
        kural = "kural2";
        ballmanager top_;
        for (int i = 0; i < Pl.transform.childCount; i++)
        {
            Pl.transform.GetChild(i).gameObject.GetComponent<ballmanager>().tiklanabilir = false;

        }
   //     print("aynıkare dizi count "+aynıkare_dizi.Count);
        foreach (var item in aynıkare_dizi)
        {
       //     print("null hatası->item:"+item);
             top_ = places[item].yerdeki_top;
            if (!places[item].yerdeki_top.ustu_dolu)
            {
               
                top_.tiklanabilir = true;
            }
            top_.karede=false;

        }


    }
    public void rule3(IPlayer Pl)
    {
        kural = "kural3";
        ballmanager top_;
        for (int i = 0; i < Pl.transform.childCount; i++)
        {
            Pl.transform.GetChild(i).gameObject.GetComponent<ballmanager>().tiklanabilir = false;

        }
        foreach (var item in eksen_dizi)
        {
            foreach (var index in item)
            {
                if (!places[index].yerdeki_top.ustu_dolu)
                {
                    top_ = places[index].yerdeki_top;
                    top_.tiklanabilir = true;
                }
            }

        }


    }

}
