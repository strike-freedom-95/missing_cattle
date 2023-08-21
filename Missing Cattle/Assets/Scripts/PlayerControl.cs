using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;
    bool isBeamActive = false;
    bool isShipHit = false;
    AudioSource soundSource;
    float sfxDelay = 0;
    Animator myAnimator;
    bool isShipDead = false;
    int exitTimer = 0;

    [SerializeField] Rigidbody2D player;
    [SerializeField] GameObject beam;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] AudioClip beamSound;
    [SerializeField] AudioClip collectedCattle;
    [SerializeField] AudioClip shipDrone;
    [SerializeField] float timerLimit = 1f;
    [SerializeField] Rigidbody2D bomb;
    [SerializeField] Transform bombPoint;
    [SerializeField] int bombCount = 2;

    void Start()
    {
        beam.SetActive(false);
        soundSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (isShipDead)
        {
            exitTimer += 1;
            if (exitTimer >= 48f)
            {
                SceneManager.LoadScene("Game Over");
            }
        }        
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        player.velocity = Vector2.zero;

        if(!isShipDead)
        {
            if (isShipHit)
            {
                FreezeShipControls();
            }


            if (Input.GetKey(KeyCode.E))
            {
                beam.SetActive(true);
                isBeamActive = true;
                FreezeShipControls();
                // PlayerSounds(0);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                beam.SetActive(false);
                isBeamActive = false;
            }

            if (Input.GetMouseButton(0))
            {
                FreezeShipControls();
                beam.SetActive(true);
                isBeamActive = true;
                // PlayerSounds(0);
            }

            if (Input.GetMouseButtonUp(0))
            {
                beam.SetActive(false);
                isBeamActive = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                DropBomb();
            }
            transform.Translate(horizontalMove, verticalMove, 0);
        }
    }

    private void DropBomb()
    {
        bombCount--;
        if (bombCount >= 0)
        {
            var bombs = Instantiate(bomb, bombPoint.position, bombPoint.rotation);
            bombs.GetComponent<Rigidbody2D>().velocity = -bombPoint.up * 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Water")
        {
            Destroy(gameObject, 1f);
            myAnimator.SetBool("isSplashed", true);
            isShipDead = true;
            // groundScript.TriggerExplosion(collision);
            Debug.Log("Game Over");
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bomb")
        {
            Destroy(gameObject, 1f);
            myAnimator.SetBool("isCrashed", true);
            isShipDead = true;
            // groundScript.TriggerExplosion(collision);
            Debug.Log("Game Over");
        }
        if (collision.gameObject.tag == "Cattle" && isBeamActive)
        {
            Debug.Log("cattle collected");
            PlayerSounds(1);
            // Action when the player collects the Cattle, maybe some animations...?
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            // Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            myAnimator.SetBool("isDamaged", true);
            player.gravityScale = 30;
            isShipHit = true;
        }
    }

    void FreezeShipControls()
    {
        horizontalMove = 0;
        verticalMove = 0;
    }

    void PlayerSounds(int index)
    {
        switch (index)
        {
            case 0:soundSource.clip = beamSound;
                SoundDelay(beamSound);
                break;
            case 1:
                Debug.Log("Ding!");
                soundSource.PlayOneShot(collectedCattle);
                break;
            case 2:
                SoundDelay(shipDrone);
                break;
            default:break;
        }
    }

    void SoundDelay(AudioClip clip)
    {
        sfxDelay += 1f;
        if (sfxDelay > timerLimit)
        {
            sfxDelay = 0;
            soundSource.PlayOneShot(clip);
        }
        else if(sfxDelay == 0)
        {
            soundSource.PlayOneShot(clip);
        }
    }
}
