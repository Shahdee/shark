using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using UnityEngine.Tilemaps;

// visual representation of level


public class Level : MonoBehaviour, IInitable
{
    public Shark m_Shark;
    public List<Tilemap> m_Tilemaps;
    const int c_Tilemap_Border = 0;
    const int c_Tilemap_Water = 1;
    public Transform m_TrsLevelObjects;

    // all objects on level
    List<GObject> m_AllEnemies = new List<GObject>();
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

    int c_Shark_X = 13;
    int c_Shark_Y = 13;

    void PutShark(){
        m_Shark.Put(c_Shark_X, c_Shark_Y);

        // camera might be here
    }

    void GenerateObstacles(){
        // or init random variables for obstacles

        // return objects to buffer 
        // generate objects for level from buffer 
        // put obstacles in free slots 

        for (int i=0; i<m_Tilemaps.Count; i++){
            m_Tilemaps[i].CompressBounds();

            Debug.Log("tilemap "  + m_Tilemaps[i].size);
        }

        GenerateEnemies();
    }

    public float m_EnemyProbability = 0.1f;
    public float m_MineProbability = 0.3f;

    void GenerateEnemies(){

        float enemy = 0;
        float type = 0;

        GObject entity = null;

        ReturnOldEnemiesToPool();

        // TODO exclude shark cell ! 

        for (int i=0; i<m_Tilemaps[c_Tilemap_Water].size.x; i++){
            for (int y=0; y<m_Tilemaps[c_Tilemap_Water].size.y; y++){

                if (i == c_Shark_X && y == c_Shark_Y) continue;
                
                enemy = UnityEngine.Random.Range(0f, 1f);
                if (enemy <= m_EnemyProbability){
                    // put enemy in the cell

                    type = UnityEngine.Random.Range(0f, 1f);
                    if (type <= m_MineProbability)
                    {
                        // mine 
                        entity = m_MainLogic.GetEntityManager().GetEntity(GObject.ObjectType.Mine);
                    }
                    else{
                        // swimmer static
                        entity = m_MainLogic.GetEntityManager().GetEntity(GObject.ObjectType.SwimmerStatic);
                    }

                    if (entity != null){

                        entity.Put(i, y);

                        entity.m_Transform.SetParent(m_TrsLevelObjects);
                        m_AllEnemies.Add(entity);
                    }
                }
            }
        }
    }

    void ReturnOldEnemiesToPool(){
        for (int i=0; i<m_AllEnemies.Count; i++){
            m_MainLogic.GetEntityManager().ReturnToPool(m_AllEnemies[i]);
        }
        m_AllEnemies.Clear();
    }

    void GetRidOfEnemy(GObject enemy){

        int index = GetEnemyIndex(enemy);
        if (index >= 0){
            m_AllEnemies.RemoveAt(index);
        }
        m_MainLogic.GetEntityManager().ReturnToPool(enemy);
    }

    int GetEnemyIndex(GObject enemy){
        for (int i=0; i<m_AllEnemies.Count; i++){
            if (m_AllEnemies[i] == enemy)
                return i;
        }
        return -1;
    }

    public Shark GetShark(){
        return m_Shark;
    }

    public Tilemap GetTileMap(){
        return m_Tilemaps[c_Tilemap_Water];
    }

    void MineCollision(GObject mine){
        m_MainLogic.GetLevelLogic().AddScore(-1);
        GetRidOfEnemy(mine);
    }

    void SwimmerCollision(GObject swimmer){
        m_MainLogic.GetLevelLogic().AddScore(1);
        GetRidOfEnemy(swimmer);
    }
}
