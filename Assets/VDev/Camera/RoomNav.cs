using UnityEngine;

//Source: https://www.youtube.com/watch?v=yaQlRvHgIvE

// Vincent Lee
// 5/2/24

public class RoomNav : MonoBehaviour
{
      [SerializeField] private GameObject _virtualCamera;
      
      private void OnTriggerEnter2D(Collider2D other)
      {
            if (other.CompareTag("Player"))
            {
                  _virtualCamera.SetActive(true);
            }
      }

      private void OnTriggerExit2D(Collider2D other)
      {
            if (other.CompareTag("Player"))
            {
                  _virtualCamera.SetActive(false);
            }
      }
}
