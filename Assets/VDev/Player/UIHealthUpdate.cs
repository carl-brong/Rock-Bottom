using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

// Vincent Lee
// 5/2/24

public class UIHealthUpdate : MonoBehaviour
{
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        Player.PlayerHealthChangeEvent += HandleHealthChange;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void HandleHealthChange(float cur)
    {
        slider.value = Mathf.Max(cur, 0) / 20;
    }
}
