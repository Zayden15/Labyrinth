using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelectionPanel;
    [SerializeField] GameObject playAndSettingsPanel;

    private void Start()
    {
        levelSelectionPanel.SetActive(false);
    }

    public void LevelSelectionPanelOn() 
    {
        levelSelectionPanel.SetActive(true);
        playAndSettingsPanel.SetActive(false);
    }

    public void PlayAndSettingsPanelOn()
    {
        levelSelectionPanel.SetActive(false);
        playAndSettingsPanel.SetActive(true);
    }

}
