using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class Object : MonoBehaviour, IInitable
{
    Transform _Transform;
    protected Transform m_Transform
    {
        get
        {
            if (_Transform == null)
                _Transform = transform;
            
            return _Transform;
        }
        private set{_Transform = value;}
    }

    public enum ObjectType{
        Shark,
        Mine,
        Swimmer,
    }

    protected ObjectType m_ObjectType;

    public ObjectType GetObjType(){
        return m_ObjectType;
    }

    public virtual void Init(MainLogic logic){

    }
}
