using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour, IInitable
{
    // instantiate objects  

    public GameObject m_Mine;
    public GameObject m_SwimmerStatic;
    public GameObject m_Swimmer;
    public Transform m_TrsPoolParent;

    List<GObject> m_ObjectsInPool = new List<GObject>();

    MainLogic m_Logic;

    public void Init(MainLogic logic){
        m_Logic = logic;
    }

    // pool

    public GObject GetEntity(GObject.ObjectType oType){

        GObject gobj = GetFromPool(oType);

        if (gobj != null)
            return gobj;
        else{
            GameObject gameObject = null;

            switch(oType){
                case GObject.ObjectType.SwimmerStatic:
                    gameObject = GameObject.Instantiate(m_SwimmerStatic, Vector3.zero, Quaternion.identity);
                break;

                 case GObject.ObjectType.Swimmer:
                    gameObject = GameObject.Instantiate(m_Swimmer, Vector3.zero, Quaternion.identity);
                break;

                case GObject.ObjectType.Mine:
                    gameObject = GameObject.Instantiate(m_Mine, Vector3.zero, Quaternion.identity);
                break;
            }
            if (gameObject != null)
                gobj = gameObject.GetComponent<GObject>();
        }
        return gobj;
    }

    public void ReturnToPool(GObject obj){
        m_ObjectsInPool.Add(obj);
        obj.m_Transform.SetParent(m_TrsPoolParent);
    }

    GObject GetFromPool(GObject.ObjectType oType){

        int index = -1;

        for (int i=0; i<m_ObjectsInPool.Count; i++){
            if (m_ObjectsInPool[i].m_ObjectType == oType){
                index = i;
                break;
            }
        }

        if (index >= 0){
            GObject gobj = m_ObjectsInPool[index];
            m_ObjectsInPool.RemoveAt(index);
            return gobj;
        }
        return null;
    }
}
