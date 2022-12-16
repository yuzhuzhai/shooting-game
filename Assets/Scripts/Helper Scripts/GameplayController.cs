using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ZombieGoal {
    PLAYER,
    FENCE
}

public enum GameGoal {
    KILL_ZOMBIES,
    WALK_TO_GOAL_STEPS,
    DEFEND_FENCE,
    TIMER_COUNTDOWN,
    GAME_OVER
}

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    [HideInInspector]
    public bool bullet_And_BulletFX_Created, rocket_Bullet_Created;

    [HideInInspector]
    public bool playerAlive, fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER;
    public GameGoal gameGoal = GameGoal.DEFEND_FENCE;

    public int zombie_Count = 20;
    public int coin_Count = 0;

    public int timer_Count = 100;

    private Transform playerTarget;
    private Vector3 player_Previous_Position;

    public int step_Count = 100;
    private int initial_Step_Count;

    private Text zombieCounter_Text, timer_Text, stepCounter_Text, coin_Text;
    private Image playerLife;

    [HideInInspector]
    public int coinCount;

    public GameObject pausePanel, gameOverPanel;

	void Awake()
	{
        MakeInstance();
	}

	void Start() {
        playerAlive = true;

        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS) {

            playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            player_Previous_Position = playerTarget.position;

            initial_Step_Count = step_Count;
            stepCounter_Text = GameObject.Find("Step Counter").GetComponent<Text>();

            stepCounter_Text.text = step_Count.ToString();

        }

        if(gameGoal == GameGoal.TIMER_COUNTDOWN || gameGoal == GameGoal.DEFEND_FENCE) {
            
            timer_Text = GameObject.Find("Timer Counter").GetComponent<Text>();
            timer_Text.text = timer_Count.ToString();

            InvokeRepeating("TimerCountdown", 0f, 1f);

        }

        if(gameGoal == GameGoal.KILL_ZOMBIES) {
            zombieCounter_Text = GameObject.Find("Zombie Counter").GetComponent<Text>();
            zombieCounter_Text.text = zombie_Count.ToString();
            coin_Text = GameObject.Find("Coin Counter").GetComponent<Text>();
            coin_Text.text = coin_Count.ToString();

        }
        if(gameGoal == GameGoal.TIMER_COUNTDOWN) {
            coin_Text = GameObject.Find("Coin Counter").GetComponent<Text>();
            coin_Text.text = coin_Count.ToString();
        }

        playerLife = GameObject.Find("Life Full").GetComponent<Image>();

	}

	void OnDisable()
	{
        instance = null;
	}

	void Update() {
		
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS) {
            CountPlayerMovement();
        }

	}

    void CountPlayerMovement() {

        Vector3 playerCurrentMovement = playerTarget.position;

        float dist = Vector3.Distance(new Vector3(playerCurrentMovement.x, 0f, 0f),
                                      new Vector3(player_Previous_Position.x, 0f, 0f));

        // player moving forward
        if(playerCurrentMovement.x > player_Previous_Position.x) {

            if(dist > 1) {

                step_Count--;

                if(step_Count <= 0) {
                    GameOver();
                }

                player_Previous_Position = playerTarget.position;

            }

            // player moving backwards
        } else if(playerCurrentMovement.x < player_Previous_Position.x) {

            if(dist > 0.8f) {

                step_Count++;

                if(step_Count >= initial_Step_Count) {
                    step_Count = initial_Step_Count;
                }

                player_Previous_Position = playerTarget.position;

            }

        }

        stepCounter_Text.text = step_Count.ToString();

    }

	void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void TimerCountdown() {
       
        timer_Count--;

        timer_Text.text = timer_Count.ToString();

        if(timer_Count <= 0) {
            CancelInvoke("TimerCountdown");
            GameOver();
        }

    }

    public void ZombieDied() {
        if(gameGoal == GameGoal.KILL_ZOMBIES) {
        zombie_Count--;
        coin_Count++;
        zombieCounter_Text.text = zombie_Count.ToString();
        coin_Text.text = coin_Count.ToString();

        if(zombie_Count <= 0) {
            GameOver();
        }
        if(coin_Count == 10) {
            GameOver();
        }
        }
         if(gameGoal == GameGoal.TIMER_COUNTDOWN) {
        coin_Count++;
        coin_Text.text = coin_Count.ToString();
         }
    }

    public void PlayerLifeCounter(float fillPercentage) {
        fillPercentage /= 100f;

        playerLife.fillAmount = fillPercentage;
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
    }
	
} // class









































