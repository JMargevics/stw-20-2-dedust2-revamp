using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class AutostartHost : NetworkedBehaviour
{
    void Start()
    {
        NetworkingManager.Singleton.StartHost();
    }
}
