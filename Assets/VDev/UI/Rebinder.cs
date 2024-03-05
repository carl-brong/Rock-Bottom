

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReBinder : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private int _bindingIndex;
    [SerializeField] private TMP_Text _bindingText;
    [SerializeField] private GameObject _overlay;

    private InputActionRebindingExtensions.RebindingOperation _bindingOperation;

    public void RebindMoveLeft()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Move.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Move))
            .Start();
    }
    
    public void RebindMoveRight()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Move.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(2)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Move))
            .Start();
    }
    
    public void RebindCrouch()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Crouch.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Crouch))
            .Start();
    }
    
    public void RebindJump()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Jump))
            .Start();
    }
    
    public void RebindInteract()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Move.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Move))
            .Start();
    }
    
    public void RebindSwap()
    {
        _overlay.SetActive(true);
        _bindingOperation = _input._gameInput.Gameplay.Switch.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => ReBindComplete(_input._gameInput.Gameplay.Switch))
            .Start();
    }
    
    private void ReBindComplete(InputAction action)
    {
        _bindingText.text = InputControlPath.ToHumanReadableString(
            action.bindings[_bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _bindingOperation.Dispose();
        _overlay.SetActive(false);
        var rebinding = _input._gameInput.asset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("Bindings", rebinding);
    }
}
