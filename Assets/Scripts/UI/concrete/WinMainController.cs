using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMainController : WinControllerBase
{

    // ? 
    public WinMainController(MainLogic logic, WinViewBase view) : base(logic, view){
       
    }

    public void SendStart(){
        m_MainLogic.StartGame();
    }
}
