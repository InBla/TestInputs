using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachToLoop : MonoBehaviour
{
    //spawn new Gameobject at contact point with loop
    //parent new Gameobject to the loop
    //parent the player to the new Game object

    // The amount of force to apply when launching the player
    public float launchForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detect contact with the loop
        if (collision.gameObject.CompareTag("loop"))
        {
            //create new game object
            var go = new GameObject();

            //set the new GameObject's parent to be the loop
            go.transform.SetParent(collision.gameObject.transform);

            //set the new Gameobject's position to be the position of contact
            go.transform.position = collision.GetContact(0).point;

            //set the player's parent to be the new Gameobject's transform
            transform.SetParent(go.transform);

            //freeze player's rotation so no rolling about
            GetComponent<Rigidbody2D>().freezeRotation = true;

            //set the player's rigidbod to kinematic to nullify gravity
            GetComponent<Rigidbody2D>().isKinematic = true;

            // apply force to launch the player onto the next ground loop
            Vector2 launchDirection = collision.gameObject.transform.right; // get the right direction of the loop
            GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        }
    }
}
