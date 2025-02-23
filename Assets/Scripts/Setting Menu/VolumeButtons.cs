using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeButtons : MonoBehaviour
{
    public GameObject settingPanel;
    public VolumeSettingMenu settingMenu;
    public TextMeshProUGUI messageText;
    void Start()
    {
        //settingMenu = GetComponent<VolumeSettingMenu>();
        //if (settingMenu == null)
        //{
        //    Debug.LogError("VolumeSettingMenu component is not found!");
        //}
    }

    public void OnBack()
    {
        settingPanel.SetActive(false);
        print("Back");
    }

    public void OnSave()
    {
        settingMenu.SaveSettings();
        settingPanel.SetActive(false);

        StartCoroutine(ShowMessage("Saved", 1f));

    }

    public void OnDefault()
    {
        settingMenu.ResetDefault();
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }
}
