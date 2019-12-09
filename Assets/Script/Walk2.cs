using UnityEngine;
using System.Collections;

public class Walk2 : MonoBehaviour
{
    public GameObject VReye;
    public Camera mainCamera;
    public float moveAngleX = 20.0f;
    public float moveSpeed = 1.5f;
    public int stride = 15;
    private bool tap_flag = false;
    float yOffset;
    //マウスで視点回転用
    public Vector2 mouse_rotationSpeed = new Vector2(0.5f, 0.5f);
    public bool reverse;
    private Vector2 lastMousePosition;
    private Vector2 mouse_newAngle = new Vector2(0, 0);

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
        //if (Input.touchCount > 0)
        //{
        //    // タッチ情報の取得
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        tap_flag = false;
        //    }
        //}
        // END 2-2./////////////////////////////////////////////////////

        // 2-1.ある角度範囲であれば前進させる///////////////////////
        //if (moveAngleX < x && x < 90.0f)
        //{
        //    moveForward_C();
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
        //    moveForward_C();
        //}
        //END 2-2.//////////////////////////////////////////////////

        // 2-3.タッチすると離散的に歩く/////////////////////////////
        //タッチパネル
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                moveForward_D();
            }
        }
        //マウス
        //if (Input.GetMouseButtonDown(0))    //左クリック前進
        //{
        //    moveForward_D();
        //}
        //if (Input.GetMouseButtonDown(1))    //右クリック後退
        //{
        //    moveBackward_D();
        //}
        //END 2-3.//////////////////////////////////////////////////

        //マウスで視点回転//////////////////////////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            // カメラの角度を変数"newAngle"に格納
            mouse_newAngle = VReye.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.mousePosition;
        }
        // 左ドラッグしている間
        else if (Input.GetMouseButton(0))
        {
            //カメラ回転方向の判定フラグが"true"の場合
            if (!reverse)
            {
                // Y軸の回転：マウスドラッグ方向に視点回転
                // マウスの水平移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                mouse_newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * mouse_rotationSpeed.y;
                // X軸の回転：マウスドラッグ方向に視点回転
                // マウスの垂直移動値に変数"rotationSpeed"を掛ける
                //（クリック時の座標とマウス座標の現在値の差分値）
                //mouse_newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * mouse_rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                VReye.transform.localEulerAngles = mouse_newAngle;
                //mainCamera.transform.localEulerAngles = mouse_newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }
            // カメラ回転方向の判定フラグが"reverse"の場合
            else if (reverse)
            {
                // Y軸の回転：マウスドラッグと逆方向に視点回転
                mouse_newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * mouse_rotationSpeed.y;
                // X軸の回転：マウスドラッグと逆方向に視点回転
                //mouse_newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * mouse_rotationSpeed.x;
                // "newAngle"の角度をカメラ角度に格納
                VReye.transform.localEulerAngles = mouse_newAngle;
                //mainCamera.transform.localEulerAngles = mouse_newAngle;
                // マウス座標を変数"lastMousePosition"に格納
                lastMousePosition = Input.mousePosition;
            }
        }
        //END マウスで視点回転//////////////////////////////////////

        //Oculusコントローラによる操作//////////////////////////////////
        //トリガー引くと前進
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            moveForward_D();
        }
        //戻るボタンで後進
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            moveBackward_D();
        }

    }

    //連続移動の関数/////////////////////////////////////////////////////////////
    private void moveForward_C()
    {
        VReye.transform.position = VReye.transform.position + new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z) * Time.deltaTime * moveSpeed;
    }

    private void moveBackward_C()
    {
        VReye.transform.position = VReye.transform.position + new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z) * Time.deltaTime * moveSpeed * (-1);
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

    // マウスドラッグ方向と視点回転方向を反転する処理
    public void DirectionChange()
    {
        // 判定フラグ変数"reverse"が"false"であれば
        if (!reverse)
        {
            // 判定フラグ変数"reverse"に"true"を代入
            reverse = true;
        }
        // でなければ（判定フラグ変数"reverse"が"true"であれば）
        else
        {
            // 判定フラグ変数"reverse"に"false"を代入
            reverse = false;
        }
    }

}
