using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public float movementPerSecond = 1.0f;
    public Rigidbody rbody;
    GameObject leftPaddle;
    GameObject rightPaddle;
    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public int totalScore = 0;

    // sound variable
    public AudioClip hitSound;

    // audio source
    public AudioSource source;

    // volume ranges for variation
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    public float vol;

    // set pitch adjustments for collisions
    public float pitchLowRange = .75F;
    public float pitchHighRange = 1.25F;

    void awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        leftPaddle = GameObject.Find("LeftPaddle");
        rightPaddle = GameObject.Find("RightPaddle");
        Vector3 force = (Vector3.forward + Vector3.right) * movementPerSecond;

        rbody = GetComponent<Rigidbody>();

        rbody.AddForce(force, ForceMode.Impulse);

        // get audio source for collision
        vol = Random.Range(volLowRange, volHighRange);
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // if it hits the big powerup
        if (collision.gameObject.name == "BigPowerup")
        {
            // make the ball bigger
            transform.localScale += new Vector3(1, 1, 1);
        }

        if (collision.gameObject.name == "SmallPowerup")
        {
            // make the ball smaller
            transform.localScale = new Vector3(.2f, .2f, .2f);
        }

        if (collision.gameObject.name == "LeftPaddle")
        {
            // add sound to collision
            source.PlayOneShot(hitSound,vol);

            source.pitch = Random.Range(pitchLowRange, pitchHighRange);

            // adjust pitch so that it sounds higher on next collision
            pitchLowRange = pitchLowRange + 0.45F;
            pitchHighRange = pitchHighRange + 0.45F;


            movementPerSecond += 1;
            rbody.AddForce(rbody.velocity, ForceMode.Impulse);

            GetComponent<Renderer>().material.color = Color.red;

        }

        if (collision.gameObject.name == "RightPaddle")
        {
            // add sound to collision
            source.PlayOneShot(hitSound, vol);

            source.pitch = Random.Range(pitchLowRange, pitchHighRange);

            // adjust pitch so that it sounds higher on next collision
            pitchLowRange = pitchLowRange + 0.45F;
            pitchHighRange = pitchHighRange + 0.45F;


            movementPerSecond += 1;
            rbody.AddForce(rbody.velocity, ForceMode.Impulse);
            GetComponent<Renderer>().material.color = Color.blue;
        }

        if (collision.gameObject.name == "LeftWall")
        {
            movementPerSecond = 5.0f;
            Vector3 force = (Vector3.forward + Vector3.right) * movementPerSecond;
            rbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rbody.transform.position = new Vector3(-3.5f, 0.2f, 0.0f);
            rbody.AddForce(force, ForceMode.Impulse);
            playerTwoScore++;

            // adjust UI for right player
            RightPlayerDisplay.AddScore();

            // adjust the ball size
            transform.localScale = new Vector3(.5f, .5f, .5f);

            // reset sound
            pitchLowRange = 0.75F;
            pitchHighRange = 1.25F;
            source.pitch = Random.Range(pitchLowRange, pitchHighRange);

            if (playerTwoScore == 11)
            {
                Debug.Log("Game Over!, Right Paddle wins!");
                playerOneScore = 0;
                playerTwoScore = 0;

                // reset score for both UI
                RightPlayerDisplay.ResetScore();
                LeftPlayerDisplay.ResetScore();
            }
            else
            {
                Debug.Log("Right Paddle scores!, Left Paddle: " + playerOneScore + ", Right Paddle: " + playerTwoScore);
            }
        }
        if (collision.gameObject.name == "RightWall")
        {
            movementPerSecond = 5.0f;
            Vector3 force = (Vector3.forward + Vector3.left) * movementPerSecond;
            rbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rbody.transform.position = new Vector3(3.5f, 0.2f, 0.0f);
            rbody.AddForce(force, ForceMode.Impulse);
            playerOneScore++;

            // adjust UI for left player
            LeftPlayerDisplay.AddScore();
            //LeftPlayerDisplay.MakeRed();

            // adjust the ball size
            transform.localScale = new Vector3(.5f, .5f, .5f);

            // reset sound
            pitchLowRange = 0.75F;
            pitchHighRange = 1.25F;
            source.pitch = Random.Range(pitchLowRange, pitchHighRange);

            if (playerOneScore == 11)
            {
                Debug.Log("Game Over!, Left Paddle wins!");
                playerOneScore = 0;
                playerTwoScore = 0;

                // reset score for both UI
                RightPlayerDisplay.ResetScore();
                LeftPlayerDisplay.ResetScore();
            }
            else
            {
                Debug.Log("Left Paddle scores!, Left Paddle: " + playerOneScore + ", Right Paddle: " + playerTwoScore);
            }
        }
    }
}
