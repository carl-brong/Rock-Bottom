


using Cinemachine;
using UnityEngine;

public class FollowSearch : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    public void Start()
    {
        cam.Follow = GameObject.FindWithTag("Player").transform;
    }
}
