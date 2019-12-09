using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FaceController : MonoBehaviour
{

    public GameObject UDP;
    public GameObject VReye;
    //
    private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);
    private float yaw_angle;
    private float pitch_angle;
    //
    private float initial_angle = 0.0f;             //初期角度
    private float initial_angle_pitch = 0.0f;      //初期角度pitch
    private float move_angle = 20.0f;               //追従角度（局所回転を行う角度）
    private float rotation_speed = 0.5f;            //旋回スピード
    private float rotation_angle = 0.0f;
    //ゲイン
    private float rotation_gain = 1.5f;     //局所回転ゲイン
    private float pitch_gain = 1.0f;        //pitchゲイン
    //
    private float temp_angle = 0.0f;
    private float return_angle = 10.0f;
    private float return_face_angle = 0.0f;
    private bool return_face = false;
    private bool return_face_m = false;
    private bool return_face_p = false;
    //
    private bool f1_flag = false;
    private bool f2_flag = false;
    private bool f3_flag = false;
    private bool f4_flag = false;
    private bool f5_flag = false;
    private bool f6_flag = false;


    void Start()
    {
        //角度の初期化
        newAngle.y = initial_angle;
    }

    void Update()
    {
        //UDP通信のスクリプトから頭部角度取得
        yaw_angle = UDP.GetComponent<UdpReceiverUniRx.UdpReceiverRx>().yaw_val;
        pitch_angle = UDP.GetComponent<UdpReceiverUniRx.UdpReceiverRx>().pitch_val;
        //Debug.Log(yaw_angle);


        //動作切り替え
        if (Input.GetKey(KeyCode.F1))
        {
            FlagDown();
            f1_flag = true;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            FlagDown();
            f2_flag = true;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            FlagDown();
            f3_flag = true;
        }
        if (Input.GetKey(KeyCode.F4))
        {
            FlagDown();
            f4_flag = true;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            FlagDown();
            f5_flag = true;
        }
        if (Input.GetKey(KeyCode.F6))
        {
            FlagDown();
            f6_flag = true;
        }



        //各種処理
        if (f1_flag == true)
        {
            //1-1. 局所回転：あり / 大域回転：頭部（顔戻しなし）///////////////////////////////////////
            if (Math.Abs(yaw_angle) > Math.Abs(move_angle))
            {
                if (yaw_angle > 0)
                {
                    rotation_angle -= rotation_speed;
                }
                else
                {
                    rotation_angle += rotation_speed;
                }
            }
            newAngle.y = -yaw_angle - initial_angle + rotation_angle;     //首回転角＋初期調整角度＋旋回角度
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 1-1.////////////////////////////////////////////////////////////////////////////
        }

        if (f2_flag == true)
        {
            //2. 局所回転：なし / 大域回転：頭部///////////////////////////////////////////////////////
            if (Math.Abs(yaw_angle) > Math.Abs(move_angle))
            {
                if (yaw_angle > 0)
                {
                    rotation_angle -= rotation_speed;
                }
                else
                {
                    rotation_angle += rotation_speed;
                }
            }
            newAngle.y = -initial_angle + rotation_angle;     //初期調整角度＋旋回角度
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 2.//////////////////////////////////////////////////////////////////////////////
        }

        if (f3_flag == true)
        {
            //3. 局所回転：あり / 大域回転：デバイス///////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation_angle -= rotation_speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation_angle += rotation_speed;
            }
            newAngle.y = -yaw_angle - initial_angle + rotation_angle;     //首回転角＋初期調整角度＋旋回角度
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 3.////////////////////////////////////////////////////////////////////////////
        }

        if (f4_flag == true)
        {
            //4. 局所回転：なし / 大域回転：デバイス//////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation_angle -= rotation_speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation_angle += rotation_speed;
            }

            newAngle.y = -initial_angle + rotation_angle;     //初期調整角度＋旋回角度
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 4.//////////////////////////////////////////////////////////////////////////////
        }

        if (f5_flag == true)
        {
            //5. 局所回転：あり（ゲイン付き） / 大域回転：デバイス///////////////////////////////
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation_angle -= rotation_speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation_angle += rotation_speed;
            }
            newAngle.y = -yaw_angle * rotation_gain - initial_angle + rotation_angle;     //（首回転角×ゲイン）＋初期調整角度＋旋回角度
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 5./////////////////////////////////////////////////////////////////////////////
        }

        if (f6_flag == true)
        {
            //6. 局所回転：pitchあり / 大域回転：デバイス///////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotation_angle -= rotation_speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotation_angle += rotation_speed;
            }
            newAngle.y = -yaw_angle - initial_angle + rotation_angle;     //首回転角＋初期調整角度＋旋回角度
            //pitchの補正
            if (pitch_angle > -10 && pitch_angle < 10)
            {
                newAngle.x = -pitch_angle * pitch_gain + initial_angle_pitch;              //首回転角pitch
            }
            VReye.gameObject.transform.localEulerAngles = newAngle;
            //END 3.////////////////////////////////////////////////////////////////////////////
        }



        //1-2. 局所回転：あり / 大域回転：頭部（顔戻しあり）//////////////////////////////////////
        //もしmove_angleより大きければ角度取得，画面旋回
        //if (Math.Abs(yaw_val) > Math.Abs(move_angle))
        //{
        //    temp_angle = -yaw_val;
        //    return_face = true;
        //    if (yaw_val > 0)
        //    {
        //        return_face_angle = return_angle;
        //        rotation_angle -= rotation_speed;
        //    }
        //    else
        //    {
        //        return_face_angle = -return_angle;
        //        rotation_angle += rotation_speed;
        //    }
        //}
        //else
        //{
        //    //顔戻しのときは追従しない
        //    if (Math.Abs(yaw_val) < Math.Abs(return_angle))
        //    {
        //        return_face = false;
        //    }
        //}

        //if (return_face == false)
        //{
        //    newAngle.y = -yaw_val - initial_angle + rotation_angle + temp_angle + return_face_angle;     //首回転角＋初期調整角度＋旋回角度
        //    MainCamera.gameObject.transform.localEulerAngles = newAngle;
        //}
        //else
        //{
        //    return_face_angle = 0.0f;
        //    newAngle.y = temp_angle - initial_angle + rotation_angle;     //首回転角＋初期調整角度＋旋回角度
        //    MainCamera.gameObject.transform.localEulerAngles = newAngle;
        //}
        //END 1-2.//////////////////////////////////////////////////////////////////////////
    }

    void FlagDown()
    {
        f1_flag = f2_flag = f3_flag = f4_flag = f5_flag = f6_flag = false;
    }
}
