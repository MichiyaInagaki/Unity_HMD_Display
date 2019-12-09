using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixHMD : MonoBehaviour
{
    public GameObject _Camera;
    private bool pauseWA = true;
    private bool _fixed = false;
    private Vector3 basePos;
    private Quaternion baseRot;
    private Vector3 trackingPos;
    private Quaternion trackingRot;


    // Start is called before the first frame update
    void Start()
    {
        trackingPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        trackingRot = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye);
        //_Camera.transform.localEulerAngles = new Vector3(trackingRot.eulerAngles.x+30, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        //トラッキング位置と回転の取得
        trackingPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        trackingRot = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye);

        // 固定にする。
        OVRManager.instance.usePositionTracking = false;
        OVRManager.instance.useRotationTracking = false;
        //XRDevice.DisableAutoXRCameraTracking(_camera, true);
        // 現在位置をベースにする
        basePos = trackingPos;
        baseRot = trackingRot;

        ////トラッキング位置と回転の取得
        //trackingPos = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        //trackingRot = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye);

        ////カメラをアクティブにする
        //if (pauseWA)
        //{
        //    pauseWA = false;
        //    _Camera.SetActive(true);
        //}

        //// Camera Tracking off/on
        ////切り替え中のみカメラを非アクティブにする
        //if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) || Input.GetKeyDown(KeyCode.X))
        //{
        //    //固定されている場合，非固定にする / 非固定の場合，固定にする
        //    if (_fixed)
        //    {
        //        _Camera.SetActive(false);
        //        pauseWA = true;
        //        // 非固定にする。
        //        OVRManager.instance.usePositionTracking = true;
        //        OVRManager.instance.useRotationTracking = true;
        //        //XRDevice.DisableAutoXRCameraTracking(_camera, false);
        //        _fixed = false;
        //    }
        //    else
        //    {
        //        _Camera.SetActive(false);
        //        pauseWA = true;
        //        // 固定にする。
        //        OVRManager.instance.usePositionTracking = false;
        //        OVRManager.instance.useRotationTracking = false;
        //        //XRDevice.DisableAutoXRCameraTracking(_camera, true);
        //        _fixed = true;
        //        // 現在位置をベースにする
        //        basePos = trackingPos;
        //        baseRot = trackingRot;
        //    }
        //}
    }
}
