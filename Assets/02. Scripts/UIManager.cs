using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text distance;

    void Update()
    {
        distance.text = "�Ÿ� : " + GameManager.instance.distanceScore.ToString() + "M";
    }
}
