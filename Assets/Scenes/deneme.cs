using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class deneme : MonoBehaviour
{
    public float deger = 0;
    public GameObject box;
    // Start is called before the first frame update
    public float timeRemaining = 10;
    void Start()
    { 
    }
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time has run out!");
        }
    }

     public deneme target;

 





    // Update is called once per frame
    /* void Update()
    {
        float pol1 = deger*deger;
        box.transform.localPosition=(new Vector3(Mathf.Cos(deger),Mathf.Sin(deger),0));
       // Mathf.Sin(deger);
        print(Mathf.Sin(deger));
    }
    public IEnumerator denemef()
    {
        yield return new WaitForSeconds(1f);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(new Vector3(4,4,4),1,1,1)); 
       // seq.Join(transform.DOMoveX(2, 1.5f)); 
    }*/

}


