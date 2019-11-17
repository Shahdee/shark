using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class GObject : MonoBehaviour, IInitable
{
    Transform _Transform;
    public Transform m_Transform
    {
        get
        {
            if (_Transform == null)
                _Transform = transform;
            
            return _Transform;
        }
        private set{_Transform = value;}
    }

    public BoxCollider2D m_Collider;

    public enum ObjectType{
        Shark,
        Mine,
        SwimmerStatic,
        Swimmer,
    }

    public ObjectType m_ObjectType;

    public ObjectType GetObjType(){
        return m_ObjectType;
    }

    public virtual void Init(MainLogic logic){

    }

    Vector3Int m_MapPosition = new Vector3Int();
    Vector3 m_Position = new Vector3();

    public void Put(int x, int y){

        m_MapPosition.x = x;
        m_MapPosition.y = y;
        m_MapPosition.z = 0;

        m_Position = MainLogic.GetMainLogic().GetLevel().GetTileMap().GetCellCenterWorld(m_MapPosition);

        Debug.Log("m_MapPosition " + m_MapPosition);
        Debug.Log("m_Position " + m_Position);

        m_Transform.position = m_Position;
    }

    void performSwim(){

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        // Debug.Log("OnTriggerEnter2D mine " + collision.gameObject.name);

    }
}
