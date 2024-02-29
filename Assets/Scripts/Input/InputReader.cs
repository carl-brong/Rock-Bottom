using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Player Input Reader")]
public class InputReader : ScriptableObject, PlayerInputScript.IGroundActions, PlayerInputScript.IUIActions
{
    private PlayerInputScript _input;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInputScript();
            _input.Ground.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
        }
        _input.UI.Disable();
        _input.Ground.Enable();

        GameManager.Instance.OnGameStateChanged += SwitchActionMap;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= SwitchActionMap;
    }

    public event UnityAction<float> MoveEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCancelEvent = delegate { };
    public event UnityAction CrouchEvent = delegate { };
    public event UnityAction CrouchCancelEvent = delegate { };
    public event UnityAction<Vector2> AttackEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction ResumeEvent = delegate { }; 
    public event UnityAction SwitchEvent = delegate { };

    private void SwitchActionMap(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Gameplay:
                _input.Ground.Enable();
                _input.UI.Disable();
                break;
            case GameState.Paused:
                _input.Ground.Disable();
                _input.UI.Enable();
                break;
            case GameState.GameOver:
                _input.Ground.Disable();
                _input.UI.Disable();
                break;
        }
    }

    #region IGroundActions

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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PauseEvent.Invoke();
        }
    }
    
    public void OnSwitch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SwitchEvent.Invoke();
        }
    }
    
    #endregion
    
    #region IUIActions
    
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ResumeEvent.Invoke();
        }
    }

    #endregion
    
}

