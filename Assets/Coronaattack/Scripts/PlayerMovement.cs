using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

	private Rigidbody2D rb;
	private Vector2 mousePos;
	private Vector2 offset;
	private bool clicked;
	public GameObject scoreNotification, gameOverScreen, particles, immunityBar;

	ScoreManager scoreManager;
	AudioManager audioManager;
	ImmunityBarHandler immunityHandler;
	Flash flash;

	[SerializeField]
	private GameObject shield;

	private bool isImmune = false;

	private float dirX, dirY;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		offset = transform.position;

		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		audioManager = GameObject.FindObjectOfType<AudioManager>();
		flash = GameObject.FindObjectOfType<Flash>();

		scoreNotification.SetActive(false);
		gameOverScreen.SetActive(false);
		shield.SetActive(false);
		immunityBar.SetActive(false);
	}

    private void Update()
    {
		dirX = Input.GetAxis("Horizontal") * 7f;
		dirY = Input.GetAxis("Vertical") * 7f;
    }

    private void FixedUpdate()
	{

#if UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL

		if (Input.GetMouseButton(0))
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (!clicked)
			{
				offset = (Vector2)transform.position - mousePos + new Vector2(0, Camera.main.GetComponent<CameraMovement>().cameraSpeed);
				clicked = true;
			}

			Vector2 newPos = new Vector2(
				Mathf.Clamp(mousePos.x + offset.x, GameManager.gm.cameraEdges.w + 0.32f, GameManager.gm.cameraEdges.y - 0.32f),
				Mathf.Clamp(mousePos.y + offset.y, Camera.main.transform.position.y - 5, Camera.main.transform.position.y + 5)
			);

			rb.MovePosition(newPos);
		} //Clicked
		else
		{
			rb.MovePosition(transform.position + new Vector3(0, Camera.main.GetComponent<CameraMovement>().cameraSpeed, 0));
			clicked = false;
		} //Released
#endif

#if UNITY_EDITOR || UNITY_WEBGL
		rb.velocity = new Vector2(dirX, dirY);

#endif
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle")
		{
			if (isImmune)
			{
				StartCoroutine(DestroyParticle(other.gameObject));				
			}

			else
			{
				Destroy(GameObject.FindObjectOfType<SpawnManager>());

				audioManager.bgAudios[0].GetComponent<AudioSource>().Stop();
				audioManager.bgAudios[1].GetComponent<AudioSource>().Stop();
				audioManager.bgAudios[2].GetComponent<AudioSource>().Stop();
				audioManager.gameOverAudio.GetComponent<AudioSource>().Play();

				this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

                #if UNITY_IOS || UNITY_ANDROID
				Handheld.Vibrate();
                #endif

				StartCoroutine(GameOverScreen());
			}
		}
		else if (other.tag == "LevelEnd")
		{
			other.tag = "Untagged"; //Can trigger only once (needs, bcz balloon has 2 colliders)
			GameManager.gm.lm.SpawnLevel();


		}

		else if (other.tag == "Grocery")
		{
			flash.CameraFlash();

			audioManager.groceryAudio.GetComponent<AudioSource>().Play();

			scoreManager.score += 20;

			scoreNotification.SetActive(true);

			Destroy(other.gameObject);
		}

		else if (other.tag == "Immunity")
		{
			flash.CameraFlash();

			audioManager.sanitizerAudio.GetComponent<AudioSource>().Play();

			Destroy(other.gameObject);

			StartCoroutine(EnableShield(other.gameObject));
		}

	}

    IEnumerator DestroyParticle(GameObject other)
    {
		GameObject particle = GameObject.Instantiate(particles, other.transform.position, other.transform.rotation);
		other.gameObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		Destroy(particle);

    }

    IEnumerator GameOverScreen()
    {
		

		PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
		ScoreManager scoreManager = GameObject.FindObjectOfType<ScoreManager>();

		playerMovement.enabled = false;
		scoreManager.enabled = false;


#if UNITY_ANDROID
        
		PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_best_score, (long)(System.Math.Round(scoreManager.score, 0)));
#endif
		yield return null;		

		gameOverScreen.SetActive(true);
		gameOverScreen.GetComponent<Animator>().SetTrigger("isGameOver");

		if (scoreManager.score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(scoreManager.score));
			PlayerPrefs.SetInt("isFirstGame", 1);
		}		
		gameOverScreen.GetComponent<Animator>().enabled = true;
	}

	IEnumerator EnableShield(GameObject immunityType)
	{
		immunityBar.SetActive(true);
		immunityBar.GetComponent<Image>().fillAmount = 1;
		immunityHandler = GameObject.FindObjectOfType<ImmunityBarHandler>();

		if (immunityType.gameObject.name.Contains("Sanitizer"))
		{
			gameObject.GetComponent<CircleCollider2D>().radius = 0.80f;
			shield.SetActive(true);
			isImmune = true;
		
			immunityHandler.immunityTime = 0.20f;

			yield return new WaitForSeconds(5f);

			immunityBar.SetActive(false);

			gameObject.GetComponent<CircleCollider2D>().radius = 0.28f;
			shield.SetActive(false);
			isImmune = false;
		}

		else if (immunityType.gameObject.name.Contains("Mask"))
		{
			gameObject.GetComponent<CircleCollider2D>().radius = 0.80f;
			shield.SetActive(true);
			isImmune = true;

			immunityHandler.immunityTime = 0.12f;

			yield return new WaitForSeconds(8f);

			immunityBar.SetActive(false);

			gameObject.GetComponent<CircleCollider2D>().radius = 0.28f;
			shield.SetActive(false);
			isImmune = false;


		}
	}    
}
