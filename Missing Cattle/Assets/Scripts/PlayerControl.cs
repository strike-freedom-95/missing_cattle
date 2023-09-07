using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    int bombIndex = 3;
    int bombInitCount = 3;
    bool isTargetAchived = false;

    int cattleSaved = 0;
    int turretDestroyed = 0;

    public Image[] bombsAvailable;

    [SerializeField] Rigidbody2D player;
    [SerializeField] GameObject beam;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] AudioClip beamSound;
    [SerializeField] AudioClip collectedCattle;
    [SerializeField] AudioClip shipDrone;
    [SerializeField] AudioClip bombReplenished;
    [SerializeField] AudioClip playerDied;
    [SerializeField] AudioClip playerSplash;
    [SerializeField] float timerLimit = 1f;
    [SerializeField] Rigidbody2D bomb;
    [SerializeField] Transform bombPoint;
    [SerializeField] int bombCount = 3;
    [SerializeField] float loadDelay = 1f;

    void Start()
    {
        beam.SetActive(false);
        soundSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        Cursor.visible = false;
        PlayerPrefs.SetInt("Score", cattleSaved);
        PlayerPrefs.SetInt("TurretDestroyed", turretDestroyed);
        PlayerPrefs.SetString("currentLevel", SceneManager.GetActiveScene().name);
        Debug.Log("Current Scene : " + SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // Debug.Log("Bomb count : " + bombCount.ToString());
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        player.velocity = Vector2.zero;

        if (!isShipDead)
        {
            if (isShipHit)
            {
                FreezeShipControls();
                player.gravityScale = 30;
                BeamActivate(false);
            }

            if (Input.GetMouseButton(0) && !isShipHit)
            {
                FreezeShipControls();
                BeamActivate(true);
                // PlayerSounds(0);
            }

            if (Input.GetMouseButtonUp(0))
            {
                BeamActivate(false);
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
        if (bombCount > 0)
        {
            bombCount--;
            var bombs = Instantiate(bomb, bombPoint.position, bombPoint.rotation);
            bombs.GetComponent<Rigidbody2D>().velocity = -bombPoint.up * 10f;
            bombIndex--;
            bombsAvailable[bombIndex].gameObject.SetActive(false);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bomb")
        {
            // soundSource.PlayOneShot(playerDied);
            AudioSource.PlayClipAtPoint(playerDied, Camera.main.transform.position);
            myAnimator.SetBool("isCrashed", true);
            isShipDead = true;
            player.gravityScale = 0;
            player.velocity = Vector3.zero;
            StartCoroutine(DelayEvent());
            Destroy(gameObject, 5);
        }
        if (collision.gameObject.tag == "Cattle" && isBeamActive)
        {
            cattleSaved++;
            Debug.Log("cattle collected");
            PlayerSounds(1);
            PlayerPrefs.SetInt("Score", cattleSaved);
            if(PlayerPrefs.GetInt("Score", 0) % 10 == 0)
            {
                isTargetAchived = true;
                ReplenishBombs();
            }
            // Action when the player collects the Cattle, maybe some animations...?
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Water")
        {
            // soundSource.PlayOneShot(playerSplash);
            AudioSource.PlayClipAtPoint(playerSplash, Camera.main.transform.position);
            myAnimator.SetBool("isSplashed", true);
            player.gravityScale = 0f;
            isShipDead = true;
            StartCoroutine(DelayEvent());
            Destroy(gameObject, 5f);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            myAnimator.SetBool("isDamaged", true);
            // player.gravityScale = 30;
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
                AudioSource.PlayClipAtPoint(collectedCattle, Camera.main.transform.position);
                // soundSource.PlayOneShot(collectedCattle);
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

    void BeamActivate(bool control)
    {
        beam.SetActive(control);
        isBeamActive = control;
        beam.gameObject.transform.position = transform.position + new Vector3(0, -2.6f, 0);
    }

    void ReplenishBombs()
    {
        if(isTargetAchived)
        {
            // soundSource.PlayOneShot(bombReplenished);
            AudioSource.PlayClipAtPoint(bombReplenished, Camera.main.transform.position);
            for (int i = 0; i < bombInitCount; i++ ){
                bombsAvailable[i].gameObject.SetActive(true);
            }
            bombCount = 3;
            bombIndex = bombCount;
            isTargetAchived = false;
        }
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("Game Over");
    }
}
