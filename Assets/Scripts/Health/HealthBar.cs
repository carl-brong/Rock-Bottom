using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UnityEngine.UI.Image totalHealthBar;
    [SerializeField] private UnityEngine.UI.Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = 1;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.curHealth;
    }
}
