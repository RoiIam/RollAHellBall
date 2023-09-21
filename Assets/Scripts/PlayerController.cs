using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public float speed = 0f;


    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject failObject;
    private int count;
    public int pickupsCount = 8;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        failObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO
        /*
         * Na zemi su trhliny ktore vedu do pekla
         * tie trhliny sa obcas otvoria, maju nahodny timer
         * zaroven za tebou ide monstrum, demon, ktore je imunne trhlinam.  ak ta dobehne prehras
         * pridat skybox pekla
         */
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Pickups: " + count.ToString() + " / 8";

        if (count >= pickupsCount) winTextObject.SetActive(true);
    }

    public void Failed()
    {
        failObject.SetActive(true);
        rb.mass = 100000; //prevent movement :D
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}