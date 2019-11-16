using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to get data from json 

[System.Serializable]
public class GameData
{
    [System.Serializable]
    public class Level{
        public List<RoomObject> roomObjects;
        public int[] start;
        public int[] size;

        public void Print(){

            Debug.Log("> Start " + string.Join(" ", start));

            Debug.Log("> Size " + string.Join(" ", size));

            Debug.Log("> roomObjects");

            for (int i=0; i<roomObjects.Count; i++){
                roomObjects[i].Print();
            }
        }
    }

    [System.Serializable]
    public class RoomObject{
        public int type;
        public Location location;

        public void Print(){
            Debug.Log("type " + type + " / " + location.Print());
        }
    }

    [System.Serializable]
    public class Location{
        public int[] st;
        public int[] ed;
        public int or;

        public string Print(){
            // Debug.Log("st:");
            // for (int i=0; i<st.Length; i++)
            //     Debug.Log(" " + st[i]);
 
            // Debug.Log("ed:");
            // for (int i=0; i<ed.Length; i++)
            //     Debug.Log(" " + ed[i]);

            string loc = "st: " + string.Join(" ", st) + " / " + "ed: " + string.Join(" ", ed) + " / " + "or: " + or;
            return loc;

            // Debug.Log("st: " + string.Join(" ", st) + " / " + "ed: " + string.Join(" ", ed) + " / " + "or: " + or);
        }
    }
    
    // has to be public to work 
    public Level levelMap;

    public Level GetLevel(){
        return levelMap;
    }

    public void Print(){

        Debug.Log("print");

        levelMap.Print();
    }

    List<Object> m_Objects;

    public void Map(){

        Debug.Log("map");

        m_Objects = new List<Object>();
        Object obj = null;

        for (int i=0; i<levelMap.roomObjects.Count; i++){
            
            // level.roomObjects[i].

            // obj = n
        }
    }

    public List<Object> GetRoomObjects(){
        return m_Objects;
    }
}


// Example 

// [System.Serializable]
// public class PlayerInfo
// {
//     public List<ActData> data;
//     public int status;
// }

// [System.Serializable]
// public class ActData
// {
//     public int id;
//     public string layoutLabel;
//     public int hasCustomProb;
// }


// {"data":[{"id":141,"layoutLabel":"Sameer","hasCustomProb":1},
// {"id":214,"layoutLabel":"abc","hasCustomProb":0}],"status":200}


// "patternSequence": [
// 		{"patternID": 0, "enemyID": [0,0,0,0]},
// 		{"patternID": 1, "enemyID": [1,1,1,1]}
// ]




          