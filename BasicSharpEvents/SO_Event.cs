using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Void Event", menuName = "Events/CSharp/Void")]
public class SO_Event : SO_BaseEvent{
    public static SO_Event operator +(SO_Event soEvent, Action listener){
        soEvent.Event += listener;
        return soEvent;
    }

    public static SO_Event operator -(SO_Event soEvent, Action listener){
        soEvent.Event -= listener;
        return soEvent;
    }
}