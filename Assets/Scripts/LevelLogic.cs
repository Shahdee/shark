using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class LevelLogic: IUpdatable
{
    MainLogic m_MainLogic;

    UnityAction<int> m_OnGameStartCallback;

    UnityAction<int> m_OnLevelStartCallback;

    int m_CurrentLevel = 0;

    public int GetCurrentLevel(){
        return m_CurrentLevel;
    }

#region Callbacks

    // game start
    public void AddGameStartListener(UnityAction<int> listener){
        m_OnGameStartCallback += listener;
    }

    public void RemoveGameStartListener(UnityAction<int> listener){
        m_OnGameStartCallback -= listener;
    }

    public void OnGameStart(int level){
        if (m_OnGameStartCallback != null)
            m_OnGameStartCallback(level);
    }

    // level start  
    public void AddLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback += listener;
    }

    public void RemoveLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback -= listener;
    }

    public void OnLevelStart(int level){
        if (m_OnLevelStartCallback != null)
            m_OnLevelStartCallback(level);
    }
#endregion

    public LevelLogic(MainLogic main){

        m_MainLogic = main;

    }

    public void StartGame(int level){
        m_CurrentLevel = level;

        GenerateLevelData();

        OnGameStart(m_CurrentLevel);
    }

    public void RestartCurrLevel(){

        OnLevelStart(m_CurrentLevel);
    }

    public void MoveNext(){
        m_CurrentLevel++;

        GenerateLevelData();

        OnLevelStart(m_CurrentLevel);
    }

    void GenerateLevelData(){

        // Debug.Log("GenerateLevelData " + m_CurrentLevel);

        m_MainLogic.GetLevel().Generate(m_CurrentLevel);
    }

    public void UpdateMe(float deltaTime){

        // UpdateTimers(deltaTime);

    }

    // void UpdateTimers(float deltaTime){

    // }
}
