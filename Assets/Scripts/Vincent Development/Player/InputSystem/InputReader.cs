using UnityEngine;
using UnityEngine.InputSystem;

namespace test
{
    [CreateAssetMenu(menuName = "Player Input Reader")]
    public class InputReader : ScriptableObject, TestInput.ITestActions
    {
        private TestInput _input;

        private void OnEnable()
        {
            if (_input == null)
            {
                _input = new TestInput();
                _input.Test.SetCallbacks(this);
            }
            _input.Test.Enable();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context);
        }
    }
}
