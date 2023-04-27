using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;
    [SerializeField] private Animator levelWindowAnim;
    [SerializeField] private Animator settingsWindowAnim;

    [ContextMenu("Удалить все сохранения")]
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Play()
    {
        CheckActivity(levelWindowAnim.gameObject);

        if (settingsWindowAnim.gameObject.activeSelf)
        {
            if (settingsWindowAnim.GetBool("ToStart") != true)
            {
                CloseSettingsMenu();
            }
        }

        levelWindowAnim.SetBool("ToStart", false);

        for (int level = 2; level < levelButtons.Count + 2; level++)
        {
            string key = level.ToString() + " Level";

            if (PlayerPrefs.GetInt(key, 0) != 0)
            {
                levelButtons[level - 2].interactable = true;
            }
        }
    }

    public void Settings()
    {
        if (levelWindowAnim.gameObject.activeSelf)
        {
            if (levelWindowAnim.GetBool("ToStart") != true)
            {
                CloseLevelMenu();
            }
        }

        CheckActivity(settingsWindowAnim.gameObject);

        settingsWindowAnim.SetBool("ToStart", false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CloseLevelMenu()
    {
        levelWindowAnim.SetBool("ToStart", true);
    }

    public void CloseSettingsMenu()
    {
        settingsWindowAnim.SetBool("ToStart", true);
    }

    private void CheckActivity(GameObject gameObject)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
}
