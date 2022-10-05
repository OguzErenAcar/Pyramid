using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlaneController : MonoBehaviour 
{

    
    //public string den="deneme";
    public int katman = 0;
    private string Plane_idx="";
    public int index=0;
    public ballmanager yerdeki_top=null;

    // Start is called before the first frame update
    void Start()
    {
        
        Plane_idx=this.gameObject.name.Substring(5);
        index=Int32.Parse(Plane_idx);
        katman = setLayer(index);
        
        
    }
     private int setLayer(int i)
    {
        if(i<16){
            return 1;
        }else if(15<i&&i<25){
            return 2;
        }else if(24<i&&i<29){
            return 3;      
        }else{
            return 4;
        }
       
    }
    // Update is called once per frame
    void Update()
    { 
    }
}
