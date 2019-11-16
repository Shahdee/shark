using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class WinGameplay : WinViewBase
{
    public Button m_BtnUpMove;
    public Button m_BtnUpSense;
    public Button m_BtnDownMove;
    public Button m_BtnDownSense;
    public Button m_BtnLeftMove;
    public Button m_BtnLeftSense;
    public Button m_BtnRightMove;
    public Button m_BtnRightSense;

    protected override void InInit(){

        m_BtnUpMove.onClick.AddListener(MoveUp);
        m_BtnDownMove.onClick.AddListener(MoveDown);
        m_BtnLeftMove.onClick.AddListener(MoveLeft);
        m_BtnRightMove.onClick.AddListener(MoveRight);

        // m_BtnUpSense.onClick.AddListener(SenseUp);
        // m_BtnUpSense.onClick.AddListener(SenseUp);
        // m_BtnUpSense.onClick.AddListener(SenseUp);
        // m_BtnUpSense.onClick.AddListener(SenseUp);
    }

    void MoveUp(){
        (m_Controller as WinGameplayController).SendHeroMoveUp();
    }

    void MoveDown(){
        (m_Controller as WinGameplayController).SendHeroMoveDown();
    }

    void MoveLeft(){
        (m_Controller as WinGameplayController).SendHeroMoveLeft();
    }

    void MoveRight(){
        (m_Controller as WinGameplayController).SendHeroMoveRight();
    }

    // void SenseUp(){

    // }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(m_MainLogic, this);;
    }

    // when the game starts 
    protected override void OnShow(){
        InitUI();
    }

    void LevelStart(int level){
        InitUI();
    }

    void InitUI(){
       
    }


    protected override void OnHide(){

    }

    public override void UpdateMe(float deltaTime){
      
    }

    // void AskForNext(){
    //     (m_Controller as WinGameplayController).SendMoveNext();
    // }
}
