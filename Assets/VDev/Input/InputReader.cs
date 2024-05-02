using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// Vincent Lee
// 5/2/24

[CreateAssetMenu(menuName = "Player Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions
{
    public GameInput _gameInput;

    private void OnEnable()
    {
        var overrides = PlayerPrefs.GetString("Bindings");
        
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            if (!string.IsNullOrEmpty(overrides))
                _gameInput.asset.LoadBindingOverridesFromJson(overrides);
            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Menu.SetCallbacks(this);
        }
        
        EnableGameplayControls();
        
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Menu.Disable();
    }
    
    public void EnableMenuControls()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Menu.Enable();
    }

    public void EnableGameplayControls()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.Menu.Disable();
    }

    public event UnityAction<float> MoveEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction JumpCancelEvent = delegate { };
    public event UnityAction CrouchEvent = delegate { };
    public event UnityAction CrouchCancelEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction PopMenuEvent = delegate { }; 
    public event UnityAction SwitchEvent = delegate { };
    public event UnityAction InteractEvent = delegate { };


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
        if (context.phase == InputActionPhase.Performed)
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent.Invoke();
        }
    }
    
    #endregion
    
    #region IUIActions
    

    public void OnPop(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PopMenuEvent.Invoke();
        }
    }

    #endregion

    
}

