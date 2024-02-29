using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGhost : MonoBehaviour
{
    private SpriteRenderer GhostSprite;
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        GhostSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            toggle = !toggle;
            GhostSprite.enabled = !toggle;

        }
    }
}
