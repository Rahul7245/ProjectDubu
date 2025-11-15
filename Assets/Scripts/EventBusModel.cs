using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusModel
{
    public static NotifiedVar<(int, int)> playButtonClicked;
   // public static NotifiedVar<ApplicationState> newStateEntered;

    //public static NotifiedVar<bool> moving;
    public void Initialize() {
        playButtonClicked = new NotifiedVar<(int, int)>((0,0));
  //      newStateEntered = new(ApplicationState.MENU);
    }
    public void Shutdown() {
        
    }
}