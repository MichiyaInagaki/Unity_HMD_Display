using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class WalkController : MonoBehaviour
{
    public GameObject VReye;
    public Camera mainCamera;
    public float moveSpeed = 0.01f;
    public float moveAngleX = 20.0f;
    public int stride = 15;
    private bool tap_flag = false;
    private int walk_count = 0;
    private string filePath = @"C:\Users\sens\Desktop\inagaki_exp1_display\CSV\saveData.csv";
    float yOffset;
    public GameObject score_object = null;

    // Use this for initialization
    void Start()
    {
        yOffset = mainCamera.transform.position.y;
        // ファイル書き出し
        // ヘッダー出力
        string[] s1 = { "time", "forward" };
        string s2 = string.Join(",", s1);   //s1を一つの文字列として
        //書き込み
        File.AppendAllText(filePath, s2 + "\n");
        //スコア表示
        score_object.SetActive(false);
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

        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        score_text.text = "s" + walk_count;
        //CSV出力（押した回数）/////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.S))
        {
            //スコア表示
            //score_object.SetActive(true);
            // ファイル書き出し
            // データ出力
            string[] str = { DateTime.Now.ToLongTimeString(), walk_count.ToString() };
            string str2 = string.Join(",", str);
            //書き込み
            File.AppendAllText(filePath, str2 + "\n");
            //データの初期化
            walk_count = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // データ出力
            string[] str = { DateTime.Now.ToLongTimeString(), walk_count.ToString() };
            string str2 = string.Join(",", str);
            //書き込み
            File.AppendAllText(filePath, str2 + "\n");
            Application.Quit();
        }
        //END CSV出力///////////////////////////////////////////////

    }

    //private void moveFoward()
    //{
    //    Vector3 direction = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * moveSpeed * Time.deltaTime;
    //    Quaternion rotation = Quaternion.Euler(new Vector3(0, -mainCamera.transform.rotation.eulerAngles.y, 0));
    //    //mainCamera.transform.Translate(rotation * direction);
    //    //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, yOffset, mainCamera.transform.position.z);
    //    VReye.transform.Translate(rotation * direction);
    //    VReye.transform.position = new Vector3(mainCamera.transform.position.x, yOffset, mainCamera.transform.position.z);
    //}

    //private void moveFoward2()
    //{
    //    Vector3 direction = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized * moveSpeed * Time.deltaTime;
    //    Quaternion rotation = Quaternion.Euler(new Vector3(0, -mainCamera.transform.rotation.eulerAngles.y, 0));
    //    for (int i=0; i<stride; i++)
    //    {
    //        //範囲外なら進まない
    //        if (mainCamera.transform.position.x < Xrange && mainCamera.transform.position.x > -Xrange && mainCamera.transform.position.z < Zrange && mainCamera.transform.position.z > -Zrange)
    //        {
    //            VReye.transform.Translate(rotation * direction);
    //            VReye.transform.position = new Vector3(mainCamera.transform.position.x, yOffset, mainCamera.transform.position.z);
    //        }
    //        else
    //        {
    //            //Debug.Log("out of range!");
    //        }
    //    }
    //}

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
