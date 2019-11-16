using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

// curr level is limited by time 

// score change 
// time change 
// time is up 

public class LevelLogic: IUpdatable
{
    MainLogic m_MainLogic;

    UnityAction<int> m_OnGameStartCallback;

    UnityAction<int> m_OnLevelStartCallback;

    int m_CurrentLevel = 0;

    public int GetCurrentLevel(){
        return m_CurrentLevel;
    }

    int m_CurrentScores = 0;

    public int GetCurrScores(){
        return m_CurrentScores;
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

    // score change 
    UnityAction<int> m_OnScoreChangeCallback;

    public void AddScoreChangeListener(UnityAction<int> listener){
        m_OnScoreChangeCallback += listener;
    }

    public void RemoveScoreChangeListener(UnityAction<int> listener){
        m_OnScoreChangeCallback -= listener;
    }

    void OnScoreChange(){
        if (m_OnScoreChangeCallback != null)
            m_OnScoreChangeCallback(m_CurrentScores);
    }

    // timer change 
    UnityAction<int> m_OnTimerChangeCallback;
    public void AddTimerChangeListener(UnityAction<int> listener){
        m_OnTimerChangeCallback += listener;
    }

    public void RemoveTimerChangeListener(UnityAction<int> listener){
        m_OnTimerChangeCallback -= listener;
    }

    void OnTimerChange(int time){
        if (m_OnTimerChangeCallback != null)
            m_OnTimerChangeCallback(time);
    }

    // time is up
    UnityAction m_OnTimeIsUpCallback;
    public void AddTimeIsUpListener(UnityAction listener){
        m_OnTimeIsUpCallback += listener;
    }

    public void RemoveTimeIsUpListener(UnityAction listener){
        m_OnTimeIsUpCallback -= listener;
    }

    void OnTimerIsUp(){
        if (m_OnTimeIsUpCallback != null)
            m_OnTimeIsUpCallback();
    }

#endregion

    bool m_LevelTimer = false;
    float _CurrLevelTime = 0;

    float m_CurrLevelTime{
        get {return _CurrLevelTime;}

        set{
            _CurrLevelTime = value;

            int val = Mathf.CeilToInt(_CurrLevelTime);
            if (val != m_CurrLevelTimeToShow){
                m_CurrLevelTimeToShow = val;

                OnTimerChange(m_CurrLevelTimeToShow);
            }   
        }
    }

    int m_CurrLevelTimeToShow = 0;
    float c_TimeToAttempt = 30; // sec 

    public int GetCurrTime(){
        return m_CurrLevelTimeToShow;
    }

    void SetLevelTimer(bool start){
        m_LevelTimer = start;
        
        if (m_LevelTimer)
            m_CurrLevelTime = c_TimeToAttempt;
    } 

    public LevelLogic(MainLogic main){

        m_MainLogic = main;

    }

    public void StartGame(int level){
        m_CurrentLevel = level;

        GenerateLevelData();

        ResetScores();

        SetLevelTimer(true);

        OnGameStart(m_CurrentLevel);
    }

    public void RestartCurrLevel(){

        ResetScores();

        SetLevelTimer(true);

        OnLevelStart(m_CurrentLevel);
    }

    public void MoveNext(){
        m_CurrentLevel++;

        GenerateLevelData();

        ResetScores();

        SetLevelTimer(true);

        OnLevelStart(m_CurrentLevel);
    }

    void GenerateLevelData(){

        // Debug.Log("GenerateLevelData " + m_CurrentLevel);

        m_MainLogic.GetLevel().Generate(m_CurrentLevel);
    }

    // tests
    public void AddScore(int score){
        m_CurrentScores += score;
        m_CurrentScores = Mathf.Max(0, m_CurrentScores);

        OnScoreChange();
    }

    void ResetScores(){
        m_CurrentScores = 0;
    }

    public void UpdateMe(float deltaTime){

        UpdateTimers(deltaTime);

    }

   void UpdateTimers(float deltaTime){

        if (m_LevelTimer){
            m_CurrLevelTime -= deltaTime;
            if (m_CurrLevelTime < 0){

                m_LevelTimer = false;

                OnTimerIsUp();
            }
        }
    }
}
