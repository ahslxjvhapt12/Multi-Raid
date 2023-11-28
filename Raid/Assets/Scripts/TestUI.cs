using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUI : MonoBehaviour
{
    public void HandleHostClick()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void HandleClientClick()
    {
        NetworkManager.Singleton.StartClient();
    }

}
