using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingView : MonoBehaviour
{
    [SerializeField] CardGridResizer cardGridResizer;
    [SerializeField] IconHolder iconHolder;
    void Awake()
    {
        EventBusModel.gameStart.Subscribe(InitGameView);
    }
    void OnDestroy()
    {
        EventBusModel.gameStart.Unsubscribe(InitGameView);
    }

    private void InitGameView()
    {
        var (rows,columns) = EventBusModel.gameStart.Value;
        cardGridResizer.Init(rows,columns);
        InitSession(rows,columns);

    }

    private void InitSession(int rows, int columns)
    {
        var iconList = iconHolder.FetchIcons(rows*columns);
        //For Debugging purpose only
        // string s=String.Empty;
        // foreach(var i in iconList)
        // {
        //     s+=i.id+",";
        // }
        // Debug.Log(s);
        
    }
}
