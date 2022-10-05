using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class PlayerManager : IPlayer
{

     
    private void Awake()
    {
        oyuncu_sirasi = Int32.Parse(this.gameObject.name.Substring(6));
    }

    void Start()
    {

        Chosees = GameObject.Find("Chosees");
        ornek_top = GameObject.Find("ornek_top");

    }

    public override void hamle()
    {


        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//aldığı (kameranın canvalsındaki) paremetre noktasından kameranın görüş açısına kameradan gelen ışını geçirir
            if (Physics.Raycast(ray, out hit, 100.0f))// ışın ile bir collider olan object yakaladıysa 
            {
                PiecesMoveManager object_ = hit.collider.GetComponentInParent<PiecesMoveManager>();
                //parentında piecesmove manager olan objeyi al  
                if (object_ != null)
                { 
                    object_.OnClick(hit.transform.gameObject, this.transform.gameObject);

                }
            }
        }

    }
    public override void OnClick(GameObject top, GameObject player)//topa tıklanınca 
    {
        bool tiklanabilir = top.GetComponent<ballmanager>().tiklanabilir;


        if (GameObject.ReferenceEquals(top.transform.parent.gameObject, player) && sira && secili_top == null
         && tiklanabilir && hak_ != 0)//kendi topunu hareket ettirsin, sıra onda olsun 
                                      //top seçilmemiş olsun ,tıklanabilirse

        {
            secili_top = top.GetComponent<ballmanager>();
            if(secili_top.chosee!=null){
                secili_top.chosee.top=null;
            }
            secili_top.chosee=null;
            
            Show_Suitable_Plane(kural);

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
    public override void Show_Suitable_Plane(string kural)//yeşil(update de )+++++
    {
        if (kural == "kural0")
        {
            kalan_top--;
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
            print("kural1");
            //hertürlü karelerin üstü yanacak
            secili_top.transform.DOMoveY(0.9f, 0.5f);
            foreach (var item in kareler)
            {
                if (secili_top.katman <= item.katman)
                {
                    print("secili topun katmanı:" + secili_top.katman + "  , itemin katmanu" + item.katman);

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
            foreach (var item in Chosees_list)
            {   
                if (item.top == null && Player.name == item.parent)
                {  
                    Vector3 chosee_vector = new Vector3(item.transform.position.x, item.transform.position.y + 0.0138f, item.transform.position.z);
                    Sequence Seq = DOTween.Sequence();
                    Seq.Append(secili_top.transform.DOJump(chosee_vector, 0.15f, 1, 1));


                    secili_top.chosee = item;
                    item.top = secili_top;
                     
                    kalan_top++;
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
                    Player.GetComponent<PlayerManager>().hak_--;
                    break;
                }
            }
            if (Player.GetComponent<PlayerManager>().hak_ == 0)
            {
                toplarini_kitle();
                print("burda");
                GameManager.change = true;
            }
        }
    }
    public override void Kural2_3_kontrol()
    {
        kareler.Clear();
        Kare_kontrol();
        if (Eksen_Kontrol(Player))
        {
            rule3(Player.GetComponent<PlayerManager>());
            Player.GetComponent<PlayerManager>().hak_ += 2;

        }
        else if (Ayni_kare_varmi(Player))
        {
            print("kural2");
            //  kural="kural2";
            // 
            rule2(Player.GetComponent<PlayerManager>());
            Player.GetComponent<PlayerManager>().hak_ += 2;

        } 
        else
        {
            print("else");
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

}
