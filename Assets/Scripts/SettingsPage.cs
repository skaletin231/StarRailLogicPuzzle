using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeDisplay;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    // Start is called before the first frame update
    void Start()
    {
        GlobalSettings.lifeSettingChanged += UpdateLifeDisplay;
        rightButton.onClick.AddListener(GlobalSettings.SetLifeRightOne);
        leftButton.onClick.AddListener(GlobalSettings.SetLifeLeftOne);
        UpdateLifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        rightButton.onClick.RemoveListener(GlobalSettings.SetLifeRightOne);
        leftButton.onClick.RemoveListener(GlobalSettings.SetLifeLeftOne);
    }

    private void UpdateLifeDisplay()
    {
        lifeDisplay.text = GlobalSettings.GetLifeOption();
    }
}
