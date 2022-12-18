using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitChange : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite noHood;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("StageUnlock", 1) >= 12)
        {
            _image.sprite = noHood;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
