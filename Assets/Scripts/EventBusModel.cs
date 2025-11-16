using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusModel
{
    public static NotifiedVar<(int, int)> playButtonClicked;
    public static NotifiedVar<ApplicationState> applicationStateEntered;
    public static NotifiedVar<ApplicationState> applicationStateExited;
    public static NotifiedVar<(int, int)> gameStart;
    public static NotifiedVar<float> gameOver;
    public static NotifiedVar<float> score;
    public static Notify homeButton;
    public static NotifiedVar<AudioType> playAudio;

    //public static NotifiedVar<bool> moving;
    public void Initialize() {
        playButtonClicked = new NotifiedVar<(int, int)>((0,0));
        applicationStateEntered = new(ApplicationState.None);
        applicationStateExited = new(ApplicationState.None);
        gameStart = new NotifiedVar<(int, int)>((0,0));
        gameOver = new NotifiedVar<float>(0);
        homeButton = new();
        score = new NotifiedVar<float>(0);
        playAudio = new NotifiedVar<AudioType>(AudioType.NONE);
    }
    public void Shutdown() {
        playButtonClicked = null;
        applicationStateEntered = null;
        applicationStateExited = null;
        gameStart = null;
        gameOver = null;
        score = null;
        homeButton = null;
        playAudio = null;
    }
}