using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //タッチパッドを押すとメニュー画面に遷移
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            SceneManager.LoadScene("ShouwaGo_Menu");
        }
    }
}
