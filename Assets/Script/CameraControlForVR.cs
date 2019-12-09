using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraControlForVR : MonoBehaviour
{
    [SerializeField] Camera target;
    private bool flag = false;

    // Use this for initialization
    void Start()
    {
        XRDevice.DisableAutoXRCameraTracking(target, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            XRDevice.DisableAutoXRCameraTracking(target, false);
        }
        if (Input.GetKeyDown("s"))
        {
            XRDevice.DisableAutoXRCameraTracking(target, true);
        }

        //タッチパッド押す
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            if (flag == false)
            {
                flag = true;
            }else if (flag == true)
            {
                flag = false;
            }            
        }

        if (flag == true)
        {
            XRDevice.DisableAutoXRCameraTracking(target, true);
        }
        else
        {
            XRDevice.DisableAutoXRCameraTracking(target, false);
        }
    }
}