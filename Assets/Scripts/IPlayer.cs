using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPlayer: PiecesMoveManager   
{
    
    public int kalan_top=15;
    public GameObject Chosees;
    public static GameObject ornek_top;
    public bool sira = false;
    public int hak_ = 0;
    public int oyuncu_sirasi;
    public GameObject Player;
    public string oynayan;
    public abstract void Kural2_3_kontrol();
    public abstract void Kural1_0_kontrol();
    public abstract void hamle();
    public abstract void toplarini_kitle();
    public abstract IEnumerator taslari_diz();
     public abstract void Show_Suitable_Plane(string kural);


     
}
