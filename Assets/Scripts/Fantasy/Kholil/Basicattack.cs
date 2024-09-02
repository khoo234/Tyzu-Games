using UnityEngine;

public class Basicattack : MonoBehaviour
{

    public Animator anim;  // Referensi ke Animator

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Attack");
        }
    }
}
