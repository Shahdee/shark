using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// eat swimmer 
// collide with bomb
// move in environment

public class Shark : Object 
{
    public Rigidbody2D m_Rigidbody;

    // public float m_MaxVelocity = 3;
    public float m_RotationSpeed = 3;
    public float m_Velocity = 1;

    public enum MovementType{
        Slide,
        ManualRotation,
        AdaptiveRotation,
    }

    public MovementType m_MovementType = MovementType.Slide;


    InputManager inputManager;

    public override void Init(MainLogic logic){

       inputManager = MainLogic.GetMainLogic().GetInputManager();

    } 

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

    // float m_yAxis;
    // float m_xAxis;
    // Vector2 m_Force;
   
    // void Update ()
    // {
    // //    m_xAxis = Input.GetAxis("Horizontal");
    // //    m_yAxis = Input.GetAxis("Vertical");
       
    // //    ThrustForward(m_yAxis);
    // //    Rotate(m_xAxis * -m_RotationSpeed);
    // }

    // float m_VelocityX;
    // float m_VelocityY;
    // Vector2 m_CurrVelocity;
   
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


#region Movements1 

    // void ClampVelocity(){

    //     m_VelocityX = Mathf.Clamp(m_Rigidbody.velocity.x, -m_MaxVelocity, m_MaxVelocity);
    //     m_VelocityY = Mathf.Clamp(m_Rigidbody.velocity.y, -m_MaxVelocity, m_MaxVelocity);

    //     m_CurrVelocity.x = m_VelocityX;
    //     m_CurrVelocity.y = m_VelocityY;

    //     m_Rigidbody.velocity = m_CurrVelocity;
    // }

    // void ThrustForward(float amount){
    //     m_Force = m_Transform.up * amount;

    //     m_Rigidbody.AddForce(m_Force);

    //     ClampVelocity();
    // }

    // void Rotate(float amount){
    //     m_Transform.Rotate(0,0, amount);

    // }

#endregion


#region Movements2

    void Update(){  
        switch(m_MovementType){
            case MovementType.Slide:
                UpdateSlide();
            break;

            case MovementType.ManualRotation:
                UpdateManualRotation();
            break;

            case MovementType.AdaptiveRotation:
                UpdateAdaptiveRotation();
            break;
        }
    }

    void UpdateSlide(){
        m_Rigidbody.AddForce(inputManager.GetHorizontal() * Vector2.right * m_Velocity * Time.deltaTime); 
        m_Rigidbody.AddForce(inputManager.GetVertical() * Vector2.up * m_Velocity * Time.deltaTime); 
    }

    float rotationAmount;

    void UpdateManualRotation(){
        m_Rigidbody.AddForce(inputManager.GetVertical() * m_Transform.up * m_Velocity * Time.deltaTime);
        rotationAmount  = m_RotationSpeed * Time.deltaTime * inputManager.GetHorizontal();
        m_Rigidbody.AddTorque(-rotationAmount, ForceMode2D.Force);
    }

    Vector2 direction = new Vector2();
    Vector2 normDirection = new Vector2();
    float thrust;
    float rotation;

    void UpdateAdaptiveRotation(){
        direction.x = inputManager.GetHorizontal();
        direction.y = inputManager.GetVertical() ;
        normDirection = direction.normalized;

        thrust = Vector2.Dot(normDirection, m_Transform.up);
        rotation = Vector2.Dot(normDirection, m_Transform.right);

        m_Rigidbody.AddForce(thrust * direction.magnitude * m_Transform.up * m_Velocity * Time.deltaTime);

        rotationAmount  = m_RotationSpeed * Time.deltaTime * rotation;
        m_Rigidbody.AddTorque(-rotationAmount, ForceMode2D.Force);
    }

#endregion 
}