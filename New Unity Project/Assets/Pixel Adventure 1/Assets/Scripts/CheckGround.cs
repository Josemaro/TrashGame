using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;  //USO DE VARIABLES EN DIFERENTE SCRIPT
    
    private void OnTriggerEnter2D(Collider2D other) {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isGrounded = false;
    }

}
