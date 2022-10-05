using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseesController : MonoBehaviour
{
    // Start is called before the first frame update
    public ballmanager top=null;
    public string parent;
    void Start()
    {
       int index= Int32.Parse(transform.name.Substring(12));
       if(index<15){
           parent="Player1";
       }
       else
       {
           parent="Player2";
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
