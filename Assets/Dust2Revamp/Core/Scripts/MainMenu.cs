using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public void Host()
    {
        NetworkingManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }

    public void Join()
    {
        NetworkingManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
}
