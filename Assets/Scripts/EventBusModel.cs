using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusModel
{
    public static NotifiedVar<GameStartData> playButtonClicked;
    public static NotifiedVar<ApplicationState> applicationStateEntered;
    public static NotifiedVar<ApplicationState> applicationStateExited;
    public static NotifiedVar<GameStartData> gameStart;
    public static NotifiedVar<float> gameOver;
    public static NotifiedVar<float> score;
    public static Notify homeButton;
    public static NotifiedVar<AudioType> playAudio;

    //public static NotifiedVar<bool> moving;
    public void Initialize() {
        playButtonClicked = new NotifiedVar<GameStartData>(new ());
        applicationStateEntered = new(ApplicationState.None);
        applicationStateExited = new(ApplicationState.None);
        gameStart = new NotifiedVar<GameStartData>(new ());
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