using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMain : WinViewBase
{
    public Button m_BtnPlay;

    protected override void InInit(){

        m_BtnPlay.onClick.AddListener(PlayClick);
        
    }

    protected override WinControllerBase CreateController(){
        return new WinMainController(m_MainLogic, this);;
    }

    void PlayClick(){
        (m_Controller as WinMainController).SendStart();
    }
}
