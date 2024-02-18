using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Player Input Reader")]
public class InputReader : ScriptableObject, PlayerInputScript.IGroundActions
{
    private PlayerInputScript _input;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInputScript();
            _input.Ground.SetCallbacks(this);
        }
        _input.Ground.Enable();
    }

    public event UnityAction<float> MoveEvent = delegate { };


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
        {
            MoveEvent.Invoke(context.ReadValue<float>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}

