using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelectionPanel;
    [SerializeField] GameObject playAndSettingsPanel;
    [SerializeField] GameObject redEye;

    private void Start()
    {
        levelSelectionPanel.SetActive(false);
        redEye.SetActive(false);
    }

    public void LevelSelectionPanelOn() 
    {
        levelSelectionPanel.SetActive(true);
        playAndSettingsPanel.SetActive(false);
        redEye.SetActive(true);
    }

    public void PlayAndSettingsPanelOn()
    {
        levelSelectionPanel.SetActive(false);
        playAndSettingsPanel.SetActive(true);
        redEye.SetActive(false);

    }

}
