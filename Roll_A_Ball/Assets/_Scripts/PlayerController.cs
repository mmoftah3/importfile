using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //accessing functions in input system
using TMPro;
using UnityEngine.SceneManagement; 
public class PlayerController : MonoBehaviour
{
    public float speed = 0; //to make the speed visible in the inspector
    public TextMeshProUGUI countText;
    

    private Rigidbody rb; //value to refference the rigidBody we need to access
    private int score; //to count the score
    private float movementX;
    private float movementY;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0; //to set initial count value to zero

        SetCountText(); //to display the score
        for (int i =0; i < GameObject.FindGameObjectsWithTag("PickupCube").Length; i++)
        {
            MeshRenderer mesh = GameObject.FindGameObjectsWithTag("PickupCube")[i].GetComponent<MeshRenderer>();
            mesh.enabled = true; //to enable back the cube objects when the game starts 
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("PickupCylinder").Length; i++)
        {
            MeshRenderer mesh = GameObject.FindGameObjectsWithTag("PickupCylinder")[i].GetComponent<MeshRenderer>();
            mesh.enabled = true; //to enable back the cylinder objects when the game starts 
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("PickupCapsule").Length; i++)
        {
            MeshRenderer mesh = GameObject.FindGameObjectsWithTag("PickupCapsule")[i].GetComponent<MeshRenderer>();
            mesh.enabled = true; //to enable back the cylinder objects when the game starts 
        }


    }

    //void has nothing to return 
    //movement value stored up, down, right or left
    void OnMove(InputValue movementValue)
    {
        //gets the vector2 value from the movementValue
        Vector2 movementVector = movementValue.Get<Vector2>(); 

        movementX = movementVector.x; //to detect movement horizontally
        movementY = movementVector.y; //to detect movement vertically
    }

    void SetCountText()
    {
        countText.text = "Score: " + score.ToString(); //to display the score of the game
        if (score >= 72 ) //maximum score to obtain and al pickup objects are collected
        {
    
            SceneManager.LoadScene("MyGame");  //to restart the game
          
        }
    }
    
    //apply force to the palyer
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); 

        rb.AddForce(movement * speed); //to modify the speed
    }
    //When player touches the collider deactivate those gameobjects
    private void OnTriggerEnter(Collider other)
    { 
        //if the tag of the object is Pickup to make sure the walls 
        if (other.gameObject.CompareTag("PickupCube"))
        {
            other.gameObject.SetActive(false); //to disable the game object and make it dissapear
            score = score + 5;//cubes are worth 5 points

            SetCountText(); 
        }

        else if (other.gameObject.CompareTag("PickupCylinder"))
        {
            other.gameObject.SetActive(false); //to disable the game object
            score = score + 6; //cylinders are worth 6 points 
            SetCountText();
        }
        else if (other.gameObject.CompareTag("PickupCapsule"))
        {
            other.gameObject.SetActive(false); //to disable the game object
            score = score + 7; //capsues are worth 7 points
            SetCountText();
        }

    }
   

}
