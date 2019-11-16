using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimmer : Object
{
    public BoxCollider2D m_Collider;

    void Awake(){
        m_ObjectType = ObjectType.Swimmer; // test
    }

    
    // void OnTriggerEnter2D(Collider2D collision)
    // {

    //     Debug.Log("OnTriggerEnter2D swimmer " + collision.gameObject.name);

    // }

    // swimming behaviour 
}
