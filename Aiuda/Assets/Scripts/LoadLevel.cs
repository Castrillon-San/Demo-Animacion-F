using UnityEngine;
using Cinemachine;

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
}