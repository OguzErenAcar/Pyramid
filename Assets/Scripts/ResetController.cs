using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetController : MonoBehaviour
{

    public GameObject Player1  ;
    public GameObject Player2  ;
    public GameObject Planes  ;
    public GameObject Chosees  ;
    public GameObject GameController  ;
    
    /*public GameObject   ;
    public GameObject   ;
    public GameObject   ;
    public GameObject   ;*/

  private void Awake() {
      Player1.GetComponent<PlayerManager>().kalan_top=15;
      Player1.GetComponent<PlayerManager>().Chosees=null;
      Player1.GetComponent<PlayerManager>().sira=false;
      Player1.GetComponent<PlayerManager>().hak_=0;
      Player1.GetComponent<PlayerManager>().oyuncu_sirasi=0;
      Player1.GetComponent<PlayerManager>().Player=null;
      Player1.GetComponent<PlayerManager>().oynayan="";
      
      Player1.GetComponent<BotManager>().kalan_top=15;
      Player1.GetComponent<BotManager>().Chosees=null;
      Player1.GetComponent<BotManager>().sira=false;
      Player1.GetComponent<BotManager>().hak_=0;
      Player1.GetComponent<BotManager>().oyuncu_sirasi=0;
      Player1.GetComponent<BotManager>().Player=null;
      Player1.GetComponent<BotManager>().oynayan="";
      Player1.GetComponent<BotManager>().playerno=1;

      Player2.GetComponent<PlayerManager>().kalan_top=15;
      Player2.GetComponent<PlayerManager>().Chosees=null;
      Player2.GetComponent<PlayerManager>().sira=false;
      Player2.GetComponent<PlayerManager>().hak_=0;
      Player2.GetComponent<PlayerManager>().oyuncu_sirasi=0;
      Player2.GetComponent<PlayerManager>().Player=null;
      Player2.GetComponent<PlayerManager>().oynayan="";
      
      Player2.GetComponent<BotManager>().kalan_top=15;
      Player2.GetComponent<BotManager>().Chosees=null;
      Player2.GetComponent<BotManager>().sira=false;
      Player2.GetComponent<BotManager>().hak_=0;
      Player2.GetComponent<BotManager>().oyuncu_sirasi=0;
      Player2.GetComponent<BotManager>().Player=null;
      Player2.GetComponent<BotManager>().oynayan="";
      Player2.GetComponent<BotManager>().playerno=2;

    //   foreach (GameObject item in Planes.transform)
    //   {
    //       item.GetComponent<PlaneController>().katman=0;
    //       item.GetComponent<PlaneController>().index=0;
    //       item.GetComponent<PlaneController>().yerdeki_top=null;

    //   }
    //   foreach (GameObject item in Chosees.transform)
    //   {
    //       item.GetComponent<ChoseesController>().top=null;
    //       item.GetComponent<ChoseesController>().parent="";
 
    //   }
 

  }
}
