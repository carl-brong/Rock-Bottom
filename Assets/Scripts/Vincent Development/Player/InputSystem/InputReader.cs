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
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCancelEvent = delegate { };
    public event UnityAction CrouchEvent = delegate { };
    public event UnityAction CrouchCancelEvent = delegate { };
    public event UnityAction<Vector2> AttackEvent = delegate { };


    public void OnMove(InputAction.CallbackContext context)
    {

        if (context.phase is InputActionPhase.Performed or InputActionPhase.Canceled)
        {
            MoveEvent.Invoke(context.ReadValue<float>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
            JumpEvent.Invoke();
            
        }
        

        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCancelEvent.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            AttackEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CrouchEvent.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            CrouchCancelEvent.Invoke();
        }
    }
}

