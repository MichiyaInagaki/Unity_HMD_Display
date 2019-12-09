//適当になんかのオブジェクトにセットする．

namespace UdpReceiverUniRx
{

    using UnityEngine;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using UniRx;

    /// <summary>
    /// UDP通信のアップデートを定義する
    /// </summary>
    public class UdpState : System.IEquatable<UdpState>
    {
        //UDP通信の情報を収める。送受信ともに使える
        public IPEndPoint EndPoint { get; set; }
        public string UdpMsg { get; set; }

        /// <summary>
        /// 値を更新する
        /// </summary>
        /// <param name="ep">エンドポインタ</param>
        /// <param name="udpMsg">受信内容</param>
        public UdpState(IPEndPoint ep, string udpMsg)
        {
            this.EndPoint = ep;
            this.UdpMsg = udpMsg;

            ////受信した文字列の確認(Start関数を有効化しておくこと)
            ////リアルタイムでの表示を確認済み
            //Debug.Log(udpMsg);

        }

        /// <summary>
        /// オーバライド定義
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return EndPoint.Address.GetHashCode();
        }

        /// <summary>
        /// UDPステートの確認
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Equals(UdpState s)
        {
            if (s == null)
            {
                return false;
            }
            return EndPoint.Address.Equals(s.EndPoint.Address);
        }
    }

    /// <summary>
    /// UDP通信の詳細
    /// </summary>
    public class UdpReceiverRx : MonoBehaviour
    {
        //値渡し用
        public float yaw_val;
        public float pitch_val;
        //
        private const int listenPort = 52525;
        private static UdpClient myClient;
        private bool isAppQuitting;
        public UniRx.IObservable<UdpState> _udpSequence;

        //add 2017/06/23
        private GameObject refobj;

        /// <summary>
        /// 起動時にソケットを生成する
        /// </summary>
        void Awake()
        {
            _udpSequence = Observable.Create<UdpState>(observer =>
            {
                //Debug.Log(string.Format("_udpSequence thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
                Debug.Log("Start udpSequence.");
                try
                {
                    myClient = new UdpClient(listenPort);
                }
                catch (SocketException ex)
                {
                    observer.OnError(ex);
                }
                IPEndPoint remoteEP = null;
                myClient.EnableBroadcast = true;
                myClient.Client.ReceiveTimeout = 1000;
                while (!isAppQuitting)
                {
                    try
                    {
                        remoteEP = null;
                        var receivedMsg = System.Text.Encoding.ASCII.GetString(myClient.Receive(ref remoteEP));
                        observer.OnNext(new UdpState(remoteEP, receivedMsg));
                        //値分割（Yaw角：receive_val[0]）
                        string[] receive_val = receivedMsg.Split(',');
                        ////数値に変換
                        yaw_val = float.Parse(receive_val[0]);
                        pitch_val = float.Parse(receive_val[1]);
                        //Debug.Log(yaw_val);
                        //Debug.Log(pitch_val);

                    }
                    catch (SocketException)
                    {
                        //Debug.Log("UDP::Receive timeout");
                    }
                }
                observer.OnCompleted();
                return null;
            })
            .SubscribeOn(Scheduler.ThreadPool)
            .Publish()
            .RefCount();
        }

        //本スクリプトでの受信を指定する
        void Start()
        {
            _udpSequence
            .ObserveOnMainThread()
            .Subscribe(x =>
            {
                ////ここではリアルタイムで反映されない
                //Debug.Log(x.UdpMsg);
            })
            .AddTo(this);
        }

        //終了処理
        void OnApplicationQuit()
        {
            try
            {
                isAppQuitting = true;
                myClient.Client.Blocking = false;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }

        //カメラ制御部
        private void Update()
        {

            //if (Input.GetMouseButtonDown(0))
            //{
            //    // マウスクリック開始(マウスダウン)時にカメラの角度を保持(Z軸には回転させないため).
            //    newAngle = MainCamera.transform.localEulerAngles;
            //    lastMousePosition = Input.mousePosition;
            //}
            //else if (Input.GetMouseButton(0))
            //{
            //    // マウスの移動量分カメラを回転させる.
            //    newAngle.y += (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            //    newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            //    MainCamera.gameObject.transform.localEulerAngles = newAngle;

            //    lastMousePosition = Input.mousePosition;
            //}
        }
    }

}
