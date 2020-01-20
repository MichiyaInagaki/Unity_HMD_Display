using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class WalkController_tobii : MonoBehaviour
{
    public GameObject VReye;
    public Camera mainCamera;
    public float moveSpeed = 0.01f;
    public float moveAngleX = 20.0f;
    public int stride = 15;
    private bool tap_flag = false;
    private int walk_count = 0;
    //private string filePath = @"C:\Users\sens\Desktop\inagaki_exp1_display\CSV\saveData.csv";
    float yOffset;
    //public GameObject score_object = null;

    // Use this for initialization
    void Start()
    {
        yOffset = mainCamera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        // 2-1.カメラの傾きを取得///////////////////////////////////////
        float x = mainCamera.transform.eulerAngles.x;
        // END 2-1. ////////////////////////////////////////////////////

        // 2-2.タッチ離したらフラグ下げる///////////////////////////////
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                tap_flag = false;
            }
        }
        // END 2-2./////////////////////////////////////////////////////



        // 2-1.ある角度範囲であれば前進させる///////////////////////
        //if (moveAngleX < x && x < 90.0f)
        //{
        //    moveFoward();
        //}
        // END 2-1./////////////////////////////////////////////////

        // 2-2.タッチすると連続的に歩く/////////////////////////////
        //if (Input.touchCount > 0)
        //{
        //    // タッチ情報の取得
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        tap_flag = true;
        //    }

        //}
        //if (tap_flag == true)
        //{
        //    moveFoward();
        //}
        //END 2-2.//////////////////////////////////////////////////

        // 2-3.タッチすると離散的に歩く/////////////////////////////
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                moveForward_D();
            }
        }
        //キーボード版
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            walk_count++;
            moveForward_D();
        }
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    walk_back_count++;
        //    moveBackward_D();
        //}
        //END 2-3.//////////////////////////////////////////////////


    }


    //離散移動の関数/////////////////////////////////////////////////////////////
    private void moveForward_D()
    {
        VReye.transform.position = VReye.transform.position + new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z) * Time.deltaTime * stride;
    }

    private void moveBackward_D()
    {
        VReye.transform.position = VReye.transform.position + new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z) * Time.deltaTime * stride * (-1);
    }

}
