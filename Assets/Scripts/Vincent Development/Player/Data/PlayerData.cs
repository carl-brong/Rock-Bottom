using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Primary Movement Variables")]
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float accelerationTime;
    public float airAccelerationTime;
    [HideInInspector] public float accelerationForce;
    [HideInInspector] public float airAccelerationForce;
    public float decelerationTIme;
    public float airDecelerationTIme;
    [HideInInspector] public float decelerationForce;
    [HideInInspector] public float airDecelerationForce;
    
    [Header("Secondary Movement Variables")]
    public float jumpModifier;
    public float jumpBuffer = 0.2f;
    public float coyoteTime = 0.2f;
    public float fallModifier;

    private void OnValidate()
    {
        accelerationForce = maxHorizontalSpeed / accelerationTime;
        airAccelerationForce = maxHorizontalSpeed / airAccelerationTime;
        decelerationForce = maxHorizontalSpeed / decelerationTIme;
        airDecelerationForce = maxHorizontalSpeed / airDecelerationTIme;
    }
}
