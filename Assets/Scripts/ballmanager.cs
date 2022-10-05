using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmanager : MonoBehaviour
{
    public bool tiklanabilir = true;
    public bool oyundami = false;
    public int katman = 0;
    public bool karede = false;
   // public bool ayni_karede=false;
    public bool ustu_dolu=false;
    public PlaneController yer = null;
    public string ParentName = "";
    public GameObject parent;
    public ChoseesController chosee;
    public int index;

    private void Start()
    {
        StartCoroutine(Wait());
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        try
        {
            ParentName = transform.parent.name;
            parent=transform.parent.gameObject;


        }
        catch { }
    }

}
