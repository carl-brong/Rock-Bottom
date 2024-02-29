using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartController : MonoBehaviour
{
    public Player player;
    
    public void OnClick()
    {
        player.Rb.velocity = Vector2.zero;
        player.transform.position = player.startpos;
        player.HealHealth(player.MaxHealth);
        player.Anim.SetFloat("CurrentHealth", player.CurrentHealth);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.SetState(GameState.Gameplay);
    }
}