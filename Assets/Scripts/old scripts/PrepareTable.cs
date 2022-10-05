/*using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PrepareTable : MonoBehaviour
{
    [SerializeField]
    private GameObject chosees; 

    [SerializeField]
    public GameObject top;

    private Boolean top_secilimi=false;
    void Start()
    {
         StartCoroutine(taslaridiz());
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsClickObjects(); 
        }
    }

    private void IsClickObjects()
    {    RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//aldığı (kameranın canvalsındaki) paremetre noktasından kameranın görüş açısına kameradan gelen ışını geçirir
        if (Physics.Raycast(ray, out hit, 100.0f))// ışın ile bir collider olan object yakaladıysa 
        {    
            if (hit.transform.name.Substring(0, 3) == "top")
            {
               transform.GetComponent<GameManager>().Set_ChoseeBall(hit.transform.gameObject);
               top_secilimi=true;
            }
            else if(top_secilimi && hit.transform.name.Substring(0, 5) == "Plane"){
            // if(hit.transform.GetComponent<Renderer>().material.color.a!=0f){

               transform.GetComponent<GameManager>().Set_ChoseePlane(hit.transform.gameObject, hit.transform.name.Substring(7, 2)); 
               top_secilimi=false;
              //  }
            }

        }

    } 
    IEnumerator taslaridiz()
    {
        for (int i = 1; i < 30; i++)
        {
            yield return new WaitForSeconds(0.05f);
            float posizyon_x = chosees.transform.GetChild(i).gameObject.transform.position.x;
            float posizyon_y = top.transform.position.y;
            float posizyon_z = chosees.transform.GetChild(i).gameObject.transform.position.z;
            if (i < 15)
            {
                GameObject new_top= Instantiate(top, new Vector3(posizyon_x, posizyon_y, posizyon_z), chosees.transform.GetChild(i).gameObject.transform.rotation);
                new_top.name="top("+i+")";
            }
            else
            {
                GameObject new_top = Instantiate(top, new Vector3(posizyon_x, posizyon_y, posizyon_z), chosees.transform.GetChild(i).gameObject.transform.rotation);
                new_top.name="top("+i+")";
                new_top.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }

    }


}
*/