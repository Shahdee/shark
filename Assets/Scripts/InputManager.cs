using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles screen touches  

public class InputManager : MonoBehaviour, IUpdatable, IInitable
{
    UnityAction<Vector2> m_OnTouchBeginCallback;
    UnityAction<Vector2> m_OnTouchMoveCallback;
    UnityAction<Vector2> m_OnTouchEndCallback;

    // touch begin
    public void AddTouchBeginListener(UnityAction<Vector2> listener){
        m_OnTouchBeginCallback += listener;
    }
    
    public void RemoveTouchBeginListener(UnityAction<Vector2> listener){
        m_OnTouchBeginCallback -= listener;
    }

    void OnTouchBegin(Vector2 pos){
        if (m_OnTouchBeginCallback!=null)
            m_OnTouchBeginCallback(pos);
    }

    // touch move 
    public void AddTouchMoveListener(UnityAction<Vector2> listener){
        m_OnTouchMoveCallback += listener;
    }
    
    public void RemoveTouchMoveListener(UnityAction<Vector2> listener){
        m_OnTouchMoveCallback -= listener;
    }

    void OnTouchMove(Vector2 pos){
        if (m_OnTouchMoveCallback!=null)
            m_OnTouchMoveCallback(pos);
    }

    // touch end 
     public void AddTouchEndListener(UnityAction<Vector2> listener){
        m_OnTouchEndCallback += listener;
    }
    
    public void RemoveTouchEndListener(UnityAction<Vector2> listener){
        m_OnTouchEndCallback -= listener;
    }

    void OnTouchEnd(Vector2 pos){
        if (m_OnTouchEndCallback!=null)
            m_OnTouchEndCallback(pos);
    }

    public void Init(MainLogic logic){
        m_JoystickMagnitude = Screen.width * 0.15f; // 15% of the screen width
    }

    Vector2 m_TouchPos;
    Vector2 m_TouchStartPos;
    Touch m_Touch;
    public void UpdateMe(float deltaTime)
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        UpdateAxes();
        // UpdateTouches(deltaTime); // for tests with Unity Remote
#else
        UpdateTouches(deltaTime);
#endif
    }

    public float m_Horizontal = 0;
    public float m_Vertical = 0;

    public float GetHorizontal(){
        return m_Horizontal;
    }

    public float GetVertical(){
        return m_Vertical;
    }

    void UpdateAxes(){
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
    }

    // no need
    // void UpdateMouse(float deltaTime){

    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         // Debug.LogError("DOWN." + Input.mousePosition);

    //         m_TouchStartPos = Input.mousePosition;
    //         OnTouchBegin(m_TouchStartPos);
    //     }

    //     if (Input.GetMouseButton(0))
    //     {
    //         // Debug.LogWarning("held ." + Input.mousePosition); // delta instead of sending every frame 

    //         m_TouchPos = Input.mousePosition;
    //         OnTouchMove(m_TouchPos);
    //     }

    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         // Debug.LogError("up."+ Input.mousePosition);
    //         m_TouchPos = Input.mousePosition;
    //         OnTouchEnd(m_TouchPos);
    //     }
    // }

    Vector2 joyDirection = new Vector2();

    public float m_JoystickMagnitude; 

    void UpdateTouches(float deltaTime){
         // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            m_Touch = Input.GetTouch(0);

            // Debug.Log("p " + m_Touch.phase);

            // Handle finger movements based on touch phase.
            switch (m_Touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:

                    m_Horizontal = 0;
                    m_Vertical = 0;

                    m_TouchStartPos = m_Touch.position;
                    OnTouchBegin(m_TouchStartPos);
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:

                    m_TouchPos = m_Touch.position;

                    joyDirection.x = m_TouchPos.x - m_TouchStartPos.x;
                    joyDirection.y = m_TouchPos.y - m_TouchStartPos.y;

                    // Debug.Log("joyDirection " + joyDirection);
                    // Debug.Log("joyDirection.magnitude " + joyDirection.magnitude);

                    // clamp
                    if (joyDirection.magnitude > m_JoystickMagnitude){
                        joyDirection.Normalize();
                        joyDirection.x *= m_JoystickMagnitude;
                        joyDirection.y *= m_JoystickMagnitude;
                    } 

                    m_Horizontal = joyDirection.x / m_JoystickMagnitude;
                    m_Vertical = joyDirection.y / m_JoystickMagnitude;

                    // Debug.Log("m_Horizontal " + m_Horizontal);
                    // Debug.Log("m_Vertical " + m_Vertical);

                    OnTouchMove(m_TouchPos);
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:

                    m_Horizontal = 0;
                    m_Vertical = 0;

                    m_TouchPos = m_Touch.position;
                    OnTouchEnd(m_TouchPos);
                    break;
            }
        }
    }
}
