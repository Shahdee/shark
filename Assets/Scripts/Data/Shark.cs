using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// eat swimmer 
// collide with bomb
// move in environment

public class Shark : Object 
{
    public Rigidbody2D m_Rigidbody;

    public float m_MaxVelocity = 3;

    public float m_RotationSpeed = 3;

#region Events

    UnityAction<Object> m_OnSwimmerCollide;

    public void AddSwimmerCollide(UnityAction<Object> listener){
        m_OnSwimmerCollide += listener;
    }

    public void RemoveSwimmerCollide(UnityAction<Object> listener){
        m_OnSwimmerCollide -= listener;
    }

    void OnSwimmerCollide(Object swimmer){

        if (m_OnSwimmerCollide != null)
            m_OnSwimmerCollide(swimmer);
    }

    UnityAction<Object> m_OnMineCollide;

    public void AddMineCollide(UnityAction<Object> listener){
        m_OnMineCollide += listener;
    }

    public void RemoveMineCollide(UnityAction<Object> listener){
        m_OnMineCollide -= listener;
    }

    void OnMineCollide(Object mine){
           if (m_OnMineCollide != null)
            m_OnMineCollide(mine);
    }

#endregion

    float m_yAxis;
    float m_xAxis;
    Vector2 m_Force;
   
    void Update ()
    {
       m_xAxis = Input.GetAxis("Horizontal");
       m_yAxis = Input.GetAxis("Vertical");
       
       ThrustForward(m_yAxis);
       Rotate(m_xAxis * -m_RotationSpeed);
    }

    float m_VelocityX;
    float m_VelocityY;

    Vector2 m_CurrVelocity;

   
    void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject == m_LineHelper.gameObject) return; 

        Debug.Log("enter " + collision.gameObject.name);

        Object obj = collision.transform.GetComponent<Object>();
        if (obj == null)
            return;
        else{
            switch(obj.GetObjType()){
                case Object.ObjectType.Swimmer:
                    OnSwimmerCollide(obj);
                break;

                case Object.ObjectType.Mine:
                    OnMineCollide(obj);
                break;
            }
        }
    }


#region Movements 
    void ClampVelocity(){

        m_VelocityX = Mathf.Clamp(m_Rigidbody.velocity.x, -m_MaxVelocity, m_MaxVelocity);
        m_VelocityY = Mathf.Clamp(m_Rigidbody.velocity.y, -m_MaxVelocity, m_MaxVelocity);

        m_CurrVelocity.x = m_VelocityX;
        m_CurrVelocity.y = m_VelocityY;

        m_Rigidbody.velocity = m_CurrVelocity;
    }

    void ThrustForward(float amount){
        m_Force = m_Transform.up * amount;

        m_Rigidbody.AddForce(m_Force);

        ClampVelocity();
    }

    void Rotate(float amount){
        m_Transform.Rotate(0,0, amount);

    }

#endregion
}