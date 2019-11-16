using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using UnityEngine.Tilemaps;

// or room - it's "visual representation"

// objects 
// floor
// ambient sound 
// start position
// hero 


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

    List<Object> m_Objects = new List<Object>();
    MainLogic m_MainLogic;

    public void Init(MainLogic logic){
        m_MainLogic = logic;

        m_Shark.Init(m_MainLogic);
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

    void PutHero(){
        
        var data = m_MainLogic.GetDataLoader().GetData();
        int[] start = data.GetLevel().start;

        // Debug.Log("put hero " + start[0] + " / " + start[1] + " / " + (Orientation)start[2]);

        // m_Hero.Put(start[0], start[1], (Orientation)start[2]);
    }

    public void Generate(int level){
        var data = m_MainLogic.GetDataLoader().GetData();
        var entittyMan = m_MainLogic.GetEntityManager();

        PutHero();

        GenerateLevelTileMap();

        OnLevelGenerate();
    }

    public Tilemap GetTileMap(){
        return m_Tile;
    }

    // public Hero GetHero(){
    //     return m_Hero;
    // }

    // TileBase GetTile(Object.ObjectType objType){
    //     switch(objType ){

    //         case Object.ObjectType.Floor:
    //             return m_TileFloor;

    //         case Object.ObjectType.Carpet:
    //             return m_TileCarpet;

    //         case Object.ObjectType.Bed:
    //             return m_TileBed;

    //         case Object.ObjectType.Door:
    //             return m_TileDoor;

    //         case Object.ObjectType.Wardrobe:
    //             return m_TileWardrobe;

    //         case Object.ObjectType.Table:
    //             return m_TileTable;

    //         case Object.ObjectType.Wall:
    //             return m_TileWall;

    //         default:
    //             return null;
    //     }
    // }

    Vector3Int m_TilePos;

    void GenerateLevelTileMap(){
        // var data = m_MainLogic.GetDataLoader().GetData();
        // var entittyMan = m_MainLogic.GetEntityManager();

        // m_Objects.Clear();
        // Object obj = null;
        // Object.ObjectType objectType = Object.ObjectType.Wall;

        // GameData.Location location;
        // Orientation ori = Orientation.None;

        // TileBase tile = null;

        // for (int i=0; i<data.GetLevel().roomObjects.Count; i++){

        //     objectType = (Object.ObjectType)(data.GetLevel().roomObjects[i].type);
        //     obj = entittyMan.CreateEntity(objectType);
        //     m_Objects.Add(obj);

        //     // put object in Location
        //     location = data.GetLevel().roomObjects[i].location;
        //     ori = (Orientation)location.or;
        //     obj.Put(location.st, location.ed, ori);

        //     tile = GetTile((Object.ObjectType)data.GetLevel().roomObjects[i].type);
        //     PaintLocation(obj.GetLocation(), tile);
        // } 
        // GenerateFloor();
    }


    // TODO future - set origin to be pe w/2, h/2 of tile map. to put it in the center of the screen 
    void GenerateFloor(){

        // m_Tile.CompressBounds();

        // Debug.Log("origin " + m_Tile.origin);
        // Debug.Log("size " + m_Tile.size);
        // Debug.Log("anchor " + m_Tile.tileAnchor);

        // TileBase tile;
        // Object.ObjectType objectType = Object.ObjectType.Floor;
        // Object obj = null;
        // Orientation ori = Orientation.None;

        // var entittyMan = m_MainLogic.GetEntityManager();

        // int[] start;
        // int[] end;

        // int sizeX = m_Tile.origin.x +  m_Tile.size.x;
        // int sizeY = m_Tile.origin.y +  m_Tile.size.y;

        // for (int i=m_Tile.origin.x; i<sizeX; i++)
        //     for (int j=m_Tile.origin.y; j<sizeY; j++)
        //     {
        //         m_TilePos.x = i;
        //         m_TilePos.y = j;
        //         m_TilePos.z = 0;
                
        //         // Debug.Log("p " + m_TilePos);

        //         tile = m_Tile.GetTile(m_TilePos);

        //         // Debug.Log("tile " + tile);

        //         if (tile == null){

        //             obj = entittyMan.CreateEntity(objectType);
        //             m_Objects.Add(obj);

        //             start = new int[Location.c_LocationSize];
        //             end = new int[Location.c_LocationSize];

        //             start[0] = end[0] = i;
        //             start[1] = end[1] = j;

        //             // put object in Location
        //             obj.Put(start, end, ori);

        //             tile = GetTile(objectType);
        //             PaintLocation(obj.GetLocation(), tile);
        //         }
        //     }
    }

    // void PaintLocation(Location location, TileBase tile){

    //     int[] start = location.GetStart();
    //     int[] end = location.GetEnd();
        
    //     if (location.isOneCell()){

    //         m_TilePos.x = start[0];
    //         m_TilePos.y = start[1];
    //         m_TilePos.z = 0;

    //         m_Tile.SetTile(m_TilePos, tile);
    //     }
    //     else{
    //         for (int i=start[0]; i<=end[0]; i++)
    //             for (int j=start[1]; j<=end[1]; j++){
    //                     m_TilePos.x = i;
    //                     m_TilePos.y = j;
    //                     m_TilePos.z = 0;
    //                     m_Tile.SetTile(m_TilePos, tile);
    //             }
    //     }
    // }

    // Object GetObjectAtPos(int x, int y){

    //     Debug.Log("GetObjectAtPos x=" + x + "/ y=" + y);

    //     for (int i=0; i<m_Objects.Count; i++){
    //         if (m_Objects[i].isHere(x, y)){
    //             Debug.Log("GetObjectAtPos " + m_Objects[i].GetObjType());
    //             return m_Objects[i];
    //         }
    //     }   
    //     return null;
    // }


    // public bool CanMove(int fromX, int fromY, Orientation toOrientation){

    //     int toX = fromX;
    //     int toY = fromY;

    //     LevelHelper.GetNextCellCoords(ref toX, ref toY, toOrientation);

    //     Debug.Log("CanMove fromX=" + fromX + "/ fromY=" + fromY + " / toX=" + toX + " / toY=" + toY);

    //     Object obj = GetObjectAtPos(toX, toY);
    //     if (obj != null){
    //         return obj.isTraversable();
    //     }
    //     else{
    //         // check for out of map 

    //         if (toX < m_Tile.origin.x ||
    //             toX >= (m_Tile.origin.x + m_Tile.size.x) ||
    //             toY < m_Tile.origin.y ||
    //             toY >= (m_Tile.origin.y + m_Tile.size.y))
    //             return false;

    //         // check for out of map 
    //     }
    //     return true;
    // }

    public bool CanMove(int x, int y){
        // object
        // wall 

        return false;
    }
}
