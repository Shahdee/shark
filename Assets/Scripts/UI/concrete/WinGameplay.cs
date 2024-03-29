﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class WinGameplay : WinViewBase
{
    // public Text m_Level;
    public Text m_Scores;
    public Text m_Time;

    protected override void InInit(){
        m_MainLogic.GetLevelLogic().AddScoreChangeListener(SetScores);
        m_MainLogic.GetLevelLogic().AddTimerChangeListener(SetTime);
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(m_MainLogic, this);;
    }

    // when the game starts 
    protected override void OnShow(){
        InitUI();
    }

    void InitUI(){
        int level = m_MainLogic.GetLevelLogic().GetCurrentLevel();
        int currTime = m_MainLogic.GetLevelLogic().GetCurrTime();
        int currScores = m_MainLogic.GetLevelLogic().GetCurrScores();

        SetLevel(level);
        SetScores(currScores);
        SetTime(currTime);
    }

    void SetLevel(int level){
        // m_Level.text = (level + 1).ToString();
    }

    void SetScores(int scores){
        m_Scores.text = (scores).ToString();
    }

    void SetTime(int time){
        m_Time.text = (time).ToString();
    }


    protected override void OnHide(){

    }

    public override void UpdateMe(float deltaTime){
      
    }
}
