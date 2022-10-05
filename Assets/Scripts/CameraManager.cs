using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    public bool rotate_bool = true;
    public float mause_sensitivity = 1;
    public static float slidr_value = 0.5f;
    public static bool slider_press = false;
    public static bool slider_value_changed = false;
    public int slider_speed = 50;
    float xRotation = 0f;
    Vector3 mousePos;
    private bool b1 = true;
    public GameObject Gameboard;
    public GameObject kamera;
    private float cameray;
    private float cameraz;
    private float rotationx;

    void Start()
    {
        kamera = this.transform.gameObject.transform.GetChild(0).gameObject;
        cameray = kamera.transform.localPosition.y;
        cameraz = kamera.transform.localPosition.z;
        rotationx = kamera.transform.localEulerAngles.x;
        print(rotationx);

    }

    void Update()
    {
        CameraMove();
    }

    public void CameraMove()
    {

        if (Input.touchCount > 0 && b1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);//aldığı (kameranın canvalsındaki) paremetre noktasından kameranın görüş açısına kameradan gelen ışını geçirir
            if (Physics.Raycast(ray, out hit, 100.0f))// ışın ile bir collider olan object yakaladıysa 
            {
                //parentında piecesmove manager olan objeyi al 
                if (Gameboard == hit.collider.gameObject)
                {
                    float mouseX_position = Input.touches[0].deltaPosition.x;
                   // float mouseY_position = Input.GetAxis("Mouse Y") * mause_sensitivity * Time.deltaTime;
                    if(mouseX_position>=.2f){
                        print("if");
                        this.gameObject.transform.Rotate((Vector3.up * mouseX_position/10));
                    }
                    else if(mouseX_position<=-.2f){
                        print("else");
                        this.gameObject.transform.Rotate((Vector3.up * mouseX_position/10));
                    }    
                }
            }
        }
           
    }




    public void set_camera(string name)
    {
        if (rotate_bool)
        {
            rotate_bool = false;
            transform.eulerAngles = new Vector3(0, (int)transform.eulerAngles.y, 0);

            //  Sequence Seq = DOTween.Sequence(); 
            if (name == "Player2")
            {
                Vector3 vector_ = new Vector3(0, 0, 0);
                this.gameObject.transform.DORotate(vector_, 1.5f);
            }
            else if (name == "Player1")
            {
                Vector3 vector_ = new Vector3(0, 180, 0);
                this.gameObject.transform.DORotate(vector_, 1.5f);

            }

        }

    }
    public void Sliderbutton(float value)
    {
        b1=false; 
        float fonk1 = (((float)-30 / 100) * (float)value) + cameray;
        float fonk2 = (((float)25 / 100) * (float)value) + cameraz;
        float rotate = (((float)-22) * (float)value) + rotationx;
        kamera.transform.localEulerAngles = (new Vector3(rotate, 0, 0));
        kamera.transform.localPosition = (new Vector3(0, fonk1, fonk2));
        //   kamera.transform.localPosition=(new Vector3(0,Mathf.Cos(-value),Mathf.Sin(-value)));
    }

    public void slider_pointer_press()
    {
        slider_press = true;
        b1=false;

    }
    public void slider_pointer_up()
    {
        slider_press = false;
        b1=true;

    }


}

/*  if (Input.GetMouseButtonDown(0) && !b1)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//aldığı (kameranın canvalsındaki) paremetre noktasından kameranın görüş açısına kameradan gelen ışını geçirir
        if (Physics.Raycast(ray, out hit, 100.0f))// ışın ile bir collider olan object yakaladıysa 
        {

            //parentında piecesmove manager olan objeyi al   
            if (Gameboard == hit.collider.gameObject)
            {
                b1 = true;
            }

        }
    }
    if (!Input.GetMouseButtonUp(0) && b1)
    {
        float mouseX_position = Input.GetAxis("Mouse X") * mause_sensitivity * Time.deltaTime;
        float mouseY_position = Input.GetAxis("Mouse Y") * mause_sensitivity * Time.deltaTime;


        this.gameObject.transform.Rotate(Vector3.up * mouseX_position);

        // xRotation -= mouseY_position;
        //  xRotation = Mathf.Clamp(xRotation, -30f, 15f);

        //  print(mouseX_position);
        //  this.gameObject.transform.Rotate(Vector3.right * mouseY_position); 

        //    this.gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //    mouseX_position = Input.mousePosition.x;
        //    Vector3 V3= new Vector3(0,  transform.rotation.y, 0);
        //    mouseY_position=Input.mousePosition.y;
        //  yield return new WaitForSeconds(0.2f);
        //  float mouseX_dif = Input.mousePosition.x - mouseX_position;
        //  float mouseY_dif=Input.mousePosition.y-mouseY_position;
        //  this.gameObject.transform.DORotate(new Vector3(0, transform.eulerAngles.x + mouseX_dif, 0), 0.1f);
        //  this.gameObject.transform.Rotate(0*2*Time.deltaTime,(mouseX_dif/2)*Time.deltaTime,0);
    }
    else
    {
        b1 = false;
    }
    */