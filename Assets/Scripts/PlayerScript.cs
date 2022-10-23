using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winMessage;
    public Text livesText;
    private int lives = 3;
    private int scoreValue = 0;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool facingRight = true;
    private bool isJumping;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        livesText.text = lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed)); 
        if (facingRight == false && hozMovement > 0)
            {
            Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
             Flip();
            }


        if (Input.GetKeyDown(KeyCode.Escape))
            {
            Application.Quit();
            }
    }
   void Update()
   {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
            
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }  
        
            
   }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if ( scoreValue == 4)
            {
                transform.position = new Vector2(46.5f, 0.0f);
                lives = 3;
                livesText.text = lives.ToString();
            }
            if ( scoreValue == 8)
            {
                winMessage.text = "You win! Game created by Kelsey Powell!";
                musicSource.loop = false;
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = true;
                speed = 0;
            }
        } 
        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            livesText.text = lives.ToString();
            Destroy(collision.collider.gameObject);
            if (lives == 0)
            {
                winMessage.text = "You lose! Game created by Kelsey Powell!";
                Destroy(rd2d.gameObject);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            anim.SetBool("Jump",false);
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
                anim.SetBool("Jump",true);  
                         
            }
            
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
