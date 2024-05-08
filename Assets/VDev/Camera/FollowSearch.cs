


using Cinemachine;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class FollowSearch : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    
    public void Start()
    {
        cam.Follow = GameObject.FindWithTag("Player").transform;
    }
}
