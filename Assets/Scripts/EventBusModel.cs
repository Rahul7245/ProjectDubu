using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusModel
{
    public static NotifiedVar<(int, int)> playButtonClicked;
    public static NotifiedVar<ApplicationState> applicationStateEntered;
    public static NotifiedVar<ApplicationState> applicationStateExited;

    //public static NotifiedVar<bool> moving;
    public void Initialize() {
        playButtonClicked = new NotifiedVar<(int, int)>((0,0));
        applicationStateEntered = new(ApplicationState.None);
        applicationStateExited = new(ApplicationState.None);
    }
    public void Shutdown() {
        playButtonClicked = null;
        applicationStateEntered = null;
        applicationStateExited = null;
    }
}