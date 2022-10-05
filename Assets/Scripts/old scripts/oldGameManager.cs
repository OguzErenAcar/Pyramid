/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlanesL1;
    private GameObject PlanesL2;
    private GameObject PlanesL3;
    private GameObject secili_top;
    private GameObject secili_plane;
    private GameObject[,] Plane_ball_list = new GameObject[2, 30];
    private int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        for (int i = 0; i < PlanesL1.transform.childCount; i++)
        {
            Plane_ball_list[0, i] = PlanesL1.transform.GetChild(i).gameObject;

        }
    }


    // Update is called once per frame
    void Update()
    {

        if (secili_top != null)
        {
            Show_Suitable_Plane();

            if (secili_plane != null)
            {
                Settled_Ball();
            }
        }
    }


    public void Set_ChoseeBall(GameObject Cb)
    {
        secili_top = Cb;
    }


    public void Set_ChoseePlane(GameObject Cp, string i)
    {
        try
        {
            index = int.Parse(i);
        }
        catch
        {
            index = int.Parse(i.Substring(0, 1));
        }
        Plane_ball_list[1, index] = secili_top;
        secili_plane = Cp;

    }

    private void Show_Suitable_Plane()//yeşil
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject ChildPlane = PlanesL1.transform.GetChild(i).gameObject;
            ChildPlane.GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);
        }



    }
    private void Settled_Ball()//topu yerleştir
    {
        Sequence seq = DOTween.Sequence();
        Vector3 plane_vector = new Vector3(secili_plane.transform.position.x, secili_plane.transform.position.y, secili_plane.transform.position.z);
        seq.Append(secili_top.transform.DOMoveY(0.9f, 0.5f));
        seq.Append(secili_top.transform.DOMove(plane_vector, 1f));
        // hit.transform.GetComponent<Renderer>().material.DOFade(0f, 0.1f);
        //full transparant
        Plane_ball_list[0, index].GetComponent<Renderer>().material.DOFade(0f, 0.5f);

        secili_plane = null;
        secili_top = null;
        PlaneLevelControl();
        //Component Planes_script =PlanesL1.GetComponent<PlaneLevelControl>(); 


    }
    private void PlaneLevelControl()
    {
        int count = 0;
        for (int i = 1; i < 12; i++)
        {
            if (i % 4 != 0)
            {
                if (Plane_ball_list[1, i - 1] != null && Plane_ball_list[1, i] != null && Plane_ball_list[1, i + 3] != null && Plane_ball_list[1, i + 4] != null)
                {
                    GameObject ChildPlane = PlanesL1.transform.GetChild(i + 15 - count).gameObject;
                    ChildPlane.SetActive(true);
                    ChildPlane.GetComponent<Renderer>().material.DOFade(0.45f, 0.5f);

                }

            }
            else
            {

                count++;
            }
        }
        //devamke

    }

}
*/