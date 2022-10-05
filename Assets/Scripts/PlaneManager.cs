using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;


public class PlaneManager : PiecesMoveManager
{
    [SerializeField]
    private Slider slider;
    public static bool slider_bool = true;


    public override void OnClick(GameObject plane, GameObject player)//plane e tıklanınca
    {

        secili_yer = plane.GetComponent<PlaneController>();
        int yer_index = Int32.Parse(secili_yer.name.Substring(5));
        print ( "plane fonksiyosunda ");

        if (secili_top != null && secili_yer.yerdeki_top == null)//top seçiliyse ve seçilen yer boş ise
        {
            print("plane e yerleştirildi ");
            Close_Suitable_Plane();
            //seçili top la yer biribirin işaret edebilir 
            secili_yer.yerdeki_top = secili_top.GetComponent<ballmanager>();
            secili_top.yer = secili_yer.GetComponent<PlaneController>();
            secili_top.katman = secili_yer.katman;
            Settled_Ball(secili_yer,secili_top);
            places[secili_yer.index].yerdeki_top = secili_top;
            slider_bool = false;
           // slider.normalizedValue = 0.5f;
        //    Buttons.slider_value_changed = false;
            slider_bool = true; 
            player.GetComponent<IPlayer>().hak_--; 
            player.GetComponent<IPlayer>().Kural2_3_kontrol();
            aynıkare_dizi.Clear();
            secili_yer = null;
            secili_top = null;
        }
    }
    private void Close_Suitable_Plane()
    {
        foreach (var item in places)
        {
            item.GetComponent<Renderer>().material.DOFade(0f, 0.5f);
            item.GetComponent<MeshCollider>().enabled = false;
        }
    }



    void Start()
    {
        Add_plane();

    }

    private void Add_plane()
    {
        places= new PlaneController[30]; 
        for (int i = 0; i < transform.childCount; i++)
        {
            
            places[i] = transform.GetChild(i).gameObject.GetComponent<PlaneController>();
        }


    }
    public void sifirla()
    {
        Close_Suitable_Plane();
        kareler.Clear();
        eksen_dizi.Clear();
        kural="kural0";
    }

}