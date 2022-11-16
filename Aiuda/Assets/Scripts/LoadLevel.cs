using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private int targetFrameRate = 30;
    GameObject player;
    [SerializeField] private GameObject[] Characters;
    [SerializeField] private Transform PlayerSpawnPoint;
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] Image controls;

    [SerializeField] private bool characterExists;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
        if(PlayerPrefs.GetInt("player") != 3 && !characterExists)
        {
            player = Instantiate(Characters[PlayerPrefs.GetInt("player")], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            vCam.Follow = player.transform;
            vCam.LookAt = player.transform;
            characterExists = true;
        }
        else if(PlayerPrefs.GetInt("player") == 3 && !characterExists)
        {
            player = Instantiate(Characters[3], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            vCam.Follow = player.transform.GetChild(0);
            vCam.LookAt = player.transform.GetChild(0);
            characterExists = true;
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if (Input.GetKeyDown(KeyCode.T)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        else if (Input.GetKeyDown(KeyCode.H)) controls.gameObject.SetActive(!controls.gameObject.activeSelf);
    }
}