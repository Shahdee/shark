using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// model 

public class MainLogic : MonoBehaviour
{
    public GUILogic m_GUILogic;
    public EntityManager m_EntityManager;
    public InputManager m_InputManager;

    // public SoundLogic m_SoundLogic;

    public Level m_Level;

    DataLoader m_DataLoader;
    LevelLogic m_LevelLogic;
    
    // PlayerProfile m_Profile;

    // PlayFabConnect m_PlayfabConnect;

    public DataLoader GetDataLoader(){
        return m_DataLoader;
    }

    public EntityManager GetEntityManager(){
        return m_EntityManager;
    }

    public InputManager GetInputManager(){
        return m_InputManager;
    }

    public LevelLogic GetLevelLogic(){
        return m_LevelLogic;
    }

    public Level GetLevel(){
        return m_Level;
    }

    // public PlayerProfile GetPlayerProfile(){
    //     return m_Profile;
    // }

    static MainLogic m_Logic;

    public static MainLogic GetMainLogic(){
        return m_Logic;
    }

    void Start()
    {
        m_Logic = this; 

        m_DataLoader = new DataLoader(this);
        m_LevelLogic = new LevelLogic(this);
        // m_Profile = new PlayerProfile(this);

        m_InputManager.Init(this);
        m_Level.Init(this);
        m_EntityManager.Init(this);
        m_GUILogic.Init(this);

        // InitBackend();
        OnAuthComplete();
    }

#region Backend 
//     void InitBackend(){

// #if UNITY_EDITOR || UNITY_STANDALONE
//         m_PlayfabConnect = new PlayFabConnect();
// #elif UNITY_IOS
//         m_PlayfabConnect = new PlayFabConnectIOS();
// #elif UNITY_ANDROID
//         m_PlayfabConnect = new PlayFabConnectAndroid();       
// #endif
//         // TODO future - we should init backend with ui 

//         m_PlayfabConnect.AddLoginSuccessListener(OnLoginSuccess);
//         m_PlayfabConnect.AddLoginFailListener(OnLoginFail);
        
//         m_PlayfabConnect.AddCredentialsSuccessListener(OnAddCredentialsSuccess);
//         m_PlayfabConnect.AddCredentialsFailListener(OnAddCredentialsFail);

//         // m_PlayfabConnect.SendLogin();

//         OnAuthComplete(); // temp 
//     }

    // TODO future - show window asking to add credentials 

    // void OnLoginSuccess(string playerId, bool newlyCreated){

    //     Debug.LogError("OnLoginSuccess " + playerId + " / " + newlyCreated);

    //     m_Profile.SetPlayerId(playerId);

    //     if (newlyCreated){
    //         m_PlayfabConnect.SendAddUsernamePassword(); 
    //     }
    //     else{
    //         // TODO future - what if a player didn't add credentials in the prev session? 

    //         OnAuthComplete();
    //     }
    // }

    // void OnLoginFail(){
    //     Debug.LogError("OnLoginFail ");

    //     OnAuthComplete(); // TODO future - should retry several times before giving up 
    // }

    void OnAddCredentialsSuccess(){

        OnAuthComplete();
    }

    void OnAddCredentialsFail(){

        Debug.LogError("OnAddCredentialsFail ");

        OnAuthComplete();

        // TODO future - doesn't mean we can't continue playing the game 
    }
#endregion

    void OnAuthComplete(){
        Debug.Log("OnAuthComplete");

        // TODO future - get player data 

        StartGame();
    }

    public void StartGame(){

        // TODO - set level according to players progress 
        // playfab or player prefs 

        m_LevelLogic.StartGame(0);
    }

    // public void TrackInput(bool track){
    //     m_InputManager.TrackInput(track);
    // }

    public void RestartLevel(){
        m_LevelLogic.RestartCurrLevel();
    }

    public void MoveNext(){
        m_LevelLogic.MoveNext(); 
    }

    float deltaTime = 0;

     void Update(){
        deltaTime = Time.deltaTime;

        m_InputManager.UpdateMe(deltaTime);
        m_LevelLogic.UpdateMe(deltaTime);
        m_GUILogic.UpdateMe(deltaTime);
    }
}
