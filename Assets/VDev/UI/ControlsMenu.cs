

using TMPro;
using UnityEngine.InputSystem;

// Vincent Lee
// 4/26/24

public class ControlsMenu : BaseMenu
{
    public TMP_Text leftText, rightText, crouchText, jumpText, interactText, swapText;

    public override void Awake()
    {
        base.Awake();
        
        leftText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Move.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        rightText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Move.bindings[2].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        crouchText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Crouch.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        jumpText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Jump.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        interactText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Interact.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        swapText.text = InputControlPath.ToHumanReadableString(
            inputReader._gameInput.Gameplay.Switch.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
