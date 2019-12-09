using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1.UIシステムを使うときに必要なライブラリ
using UnityEngine.UI;
// 2.Scene関係の処理を行うときに必要なライブラリ
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    // 3.OnRetry関数が実行されたら、sceneを読み込む
    public void ToScene1()  //アーケード
    {
        SceneManager.LoadScene("ShouwaGo_Scene1");
    }

    public void ToScene2()  //大衆食堂
    {
        SceneManager.LoadScene("ShouwaGo_Scene2");
    }

    public void ToScene3()  //駄菓子屋
    {
        SceneManager.LoadScene("ShouwaGo_Scene3");
    }

    public void ToScene4()  //街角
    {
        SceneManager.LoadScene("ShouwaGo_Scene4");
    }

    public void ToScene5()  //囲炉裏
    {
        SceneManager.LoadScene("ShouwaGo_Scene5");
    }

    public void ToScene6()  //畑
    {
        SceneManager.LoadScene("ShouwaGo_Scene6");
    }
}