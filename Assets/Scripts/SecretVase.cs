using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretVase : MonoBehaviour
{

    public GameObject SecretDoor;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.SetActive(false);
            SecretDoor.transform.localPosition = new Vector3(-0.766700029f,0.500000119f,2.977f);
            SecretDoor.transform.rotation = Quaternion.Euler(0, -230, 0);
            UIManager.MessageText = "You've Opened the secret door";
            UIManager.isPassingMessage = true;
        }
    }
}
