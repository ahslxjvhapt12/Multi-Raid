using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject hostBtn;
    [SerializeField] GameObject clientBtn;

    public void HandleHostClick()
    {
        NetworkManager.Singleton.StartHost();
        CloseBtn();
    }

    public void HandleClientClick()
    {
        NetworkManager.Singleton.StartClient();
        CloseBtn();
    }

    public void CloseBtn()
    {
        hostBtn.SetActive(false);
        clientBtn.SetActive(false);
        panel.SetActive(false);
    }
}
