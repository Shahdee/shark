using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Object
{
   public BoxCollider2D m_Collider;
   
    void Awake(){
        m_ObjectType = ObjectType.Mine; // test
    }


    // void OnTriggerEnter2D(Collider2D collision)
    // {

    //     Debug.Log("OnTriggerEnter2D mine " + collision.gameObject.name);

    // }
}
