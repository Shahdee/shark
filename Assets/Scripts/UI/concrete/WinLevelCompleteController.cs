using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelCompleteController : WinControllerBase
{
    // ? 
    public WinLevelCompleteController(MainLogic logic, WinViewBase view) : base(logic, view){
       
    }

    public void SendStartNext(){
        m_MainLogic.MoveNext();
    }
}
