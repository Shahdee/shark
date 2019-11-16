using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour, IInitable
{
    // instantiate objects  

    MainLogic m_Logic;

    public void Init(MainLogic logic){
        m_Logic = logic;
    }

    public Object CreateEntity(Object.ObjectType oType){
        return null;
    }
}
