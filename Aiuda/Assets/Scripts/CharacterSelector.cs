using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public void CharacterSelectFunction(int selectedNum)
    {
        PlayerPrefs.SetInt("player", selectedNum);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
