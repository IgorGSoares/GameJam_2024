using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    static public UnityEvent<int> OnEntityKilled = new UnityEvent<int>(); //COMMENT: when someone is killed, i think so
}
