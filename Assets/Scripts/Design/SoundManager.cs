using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject[] SoundObject; 

    private void Awake()
    {
          
        SoundObject=GameObject.FindGameObjectsWithTag("GameMusic");
        if(SoundObject.Length>1){
          Destroy(this.gameObject);
        }
          DontDestroyOnLoad(this.gameObject);
    }

}
