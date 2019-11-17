﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// controller 

public class GUILogic : UIObject, IInitable, IUpdatable
{
    public Canvas m_Canvas;
    public CanvasScaler m_Scaler;
    public List<WinViewBase> m_Windows;

    MainLogic m_MainLogic;

    public MainLogic GetMainLogic(){
        return m_MainLogic;
    }

    static float m_Scale;

    public static float GetScale(){
        return m_Scale;
    }

    public void Init(MainLogic main){

        m_MainLogic = main;
        m_MainLogic.GetLevelLogic().AddGameStartListener(GameStart);
        m_MainLogic.GetLevelLogic().AddLevelStartListener(LevelStart);
        m_MainLogic.GetLevelLogic().AddTimeIsUpListener(TimeIsUp);

        InitWindows();

        // Debug.Log("Screen " + Screen.width + " . " + Screen.height);
        // Debug.Log("RTrs " + m_RectTransform.sizeDelta.x + " / " + m_RectTransform.sizeDelta.y);
        // Debug.Log("scale " + m_RectTransform.localScale);

        // m_Scale = m_RectTransform.localScale.x;

        OpenWindow(WinViewBase.WinType.Menu);
    }

    void InitWindows(){
        for (int i=0; i<m_Windows.Count; i++){
            m_Windows[i].Init(m_MainLogic);
        }
    }

    public void UpdateMe(float deltaTime){
        for (int i=0; i<m_Windows.Count; i++){
            m_Windows[i].UpdateMe(deltaTime);
        }
    }

    void OpenWindow(WinViewBase.WinType wType){

        // Debug.Log("OpenWindow " + wType);

        for (int i=0; i<m_Windows.Count; i++){
            if (m_Windows[i].m_WindowType == wType)
                m_Windows[i].Open();
            else
                m_Windows[i].Close();
        }
    }

    void GameStart(int level){
        OpenWindow(WinViewBase.WinType.Gameplay);
    }

    void LevelStart(int level){
        OpenWindow(WinViewBase.WinType.Gameplay);
    }

    void TimeIsUp(){
        // time is up 

        Debug.Log("time is up");

        OpenWindow(WinViewBase.WinType.LevelComplete);
    }
}
