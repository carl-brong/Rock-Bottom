
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIPauseOptionsControls : MonoBehaviour
{
    [SerializeField] private GameObject _firstSelected;
    [SerializeField] private InputReader _input;
    [SerializeField] private TMP_Text _leftText, _rightText, _jumpText, _crouchText, _interactText, _swapText;

    private Button _first;

    private void Awake()
    {
        _first = _firstSelected.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _first.Select();
    }

    private void Start()
    {
        _leftText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Move.bindings[1].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        _rightText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Move.bindings[2].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        _jumpText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Jump.bindings[0].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        _crouchText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Crouch.bindings[0].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        _interactText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Interact.bindings[0].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        _swapText.text = InputControlPath.ToHumanReadableString(
            _input._gameInput.Gameplay.Switch.bindings[0].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
