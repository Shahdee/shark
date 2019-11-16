using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using UnityEngine.Tilemaps;

// visual representation of level


public class Level : MonoBehaviour, IInitable
{
    public Shark m_Shark;
    public Tilemap m_Tile;

    public TileBase m_TileFloor; 
    public TileBase m_TileCarpet; 
    public TileBase m_TileWall; 
    public TileBase m_TileDoor; 
    public TileBase m_TileBed; 
    public TileBase m_TileWardrobe; 
    public TileBase m_TileTable; 

    // all objects on level
    List<Object> m_Objects = new List<Object>();
    MainLogic m_MainLogic;

    public void Init(MainLogic logic){
        m_MainLogic = logic;

        m_Shark.Init(m_MainLogic);

        m_Shark.AddMineCollide(MineCollision);
        m_Shark.AddSwimmerCollide(SwimmerCollision);
    }

#region callbacks 
    UnityAction m_OnLevelGenerateCallback;

    public void AddLevelGenerateListener(UnityAction listener){
        m_OnLevelGenerateCallback += listener;
    }
    
    public void RemoveLevelGenerateListener(UnityAction listener){
        m_OnLevelGenerateCallback -= listener;
    }

    void OnLevelGenerate(){
        if (m_OnLevelGenerateCallback!=null)
            m_OnLevelGenerateCallback();
    } 
#endregion

    public void Generate(int level){
        var data = m_MainLogic.GetDataLoader().GetData();
        var entittyMan = m_MainLogic.GetEntityManager();

        PutShark();
        GenerateObstacles();

        OnLevelGenerate();
    }

    void PutShark(){
        // in the center 
        // camera might be here
    }

    void GenerateObstacles(){
        // or init random variables for obstacles
    }

    public Shark GetShark(){
        return m_Shark;
    }

    void MineCollision(Object mine){

        m_MainLogic.GetLevelLogic().AddScore(-1);
        
        // what mine 
        // to remove scores
        // to remove mine from the screen

        // mine should explore 

        // m_Objects
    }

    void SwimmerCollision(Object swimmer){

        m_MainLogic.GetLevelLogic().AddScore(1);
        
        // what swimmer 
        // to add scores
        // to remove swimmer from the screen 

        // m_Objects
    }
}
