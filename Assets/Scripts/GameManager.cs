using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Oyunun baþlangýcýnda frame rati 60 fps ve vSync açar.
    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }
}
