using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private int targetFrameRate = 30;
    GameObject player;
    [SerializeField] private GameObject[] Characters;
    [SerializeField] private Transform PlayerSpawnPoint;
    [SerializeField] CinemachineVirtualCamera vCam;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
        player = Instantiate(Characters[PlayerPrefs.GetInt("player")], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
        vCam.Follow = player.transform;
        vCam.LookAt = player.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if(Input.GetKeyDown(KeyCode.T)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}