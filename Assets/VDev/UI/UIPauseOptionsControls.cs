
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPauseOptionsControls : MonoBehaviour
{
    [SerializeField] private GameObject _leftObject, _rightObject, _jumpObject, _interactObject, _swapObject;
    
    private Button _leftButton, _rightButton, _jumpButton, _interactButton, _swapButton;

    
    public event UnityAction<string> LeftKeyBindRequested = delegate { };
    public event UnityAction<string> RightKeyBindRequested = delegate { };
    public event UnityAction<string> JumpKeyBindRequested = delegate { };
    public event UnityAction<string> InteractKeyBindRequested = delegate { };
    public event UnityAction<string> SwapKeyBindRequested = delegate { };

    private void Awake()
    {
        _leftButton = _leftObject.GetComponent<Button>();
        _rightButton = _rightObject.GetComponent<Button>();
        _jumpButton = _jumpObject.GetComponent<Button>();
        _interactButton = _interactObject.GetComponent<Button>();
        _swapButton = _swapObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _leftButton.onClick.AddListener(ChangeLeftKeyBind);
        _rightButton.onClick.AddListener(ChangeRightKeyBind);
        _jumpButton.onClick.AddListener(ChangeJumpKeyBind);
        _interactButton.onClick.AddListener(ChangeInteractKeyBind);
        _swapButton.onClick.AddListener(ChangeSwapKeyBind);
        _leftButton.Select();
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveListener(ChangeLeftKeyBind);
        _rightButton.onClick.RemoveListener(ChangeRightKeyBind);
        _jumpButton.onClick.RemoveListener(ChangeJumpKeyBind);
        _interactButton.onClick.RemoveListener(ChangeInteractKeyBind);
        _swapButton.onClick.RemoveListener(ChangeSwapKeyBind);
    }

    private void ChangeLeftKeyBind()
    {
        LeftKeyBindRequested.Invoke("Left");
    }

    private void ChangeRightKeyBind()
    {
        RightKeyBindRequested.Invoke("Right");
    }

    private void ChangeJumpKeyBind()
    {
        JumpKeyBindRequested.Invoke("Jump");
    }

    private void ChangeInteractKeyBind()
    {
        InteractKeyBindRequested.Invoke("Interact");
    }

    private void ChangeSwapKeyBind()
    {
        SwapKeyBindRequested.Invoke("Swap");
    }

}
