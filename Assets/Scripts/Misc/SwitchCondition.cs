using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : MonoBehaviour
{
    [SerializeField] private GameObject root;

    private SwitchTimelines _switcher;
    
    private void Awake()
    {
        _switcher = root.GetComponent<SwitchTimelines>();
    }
    
    
}
