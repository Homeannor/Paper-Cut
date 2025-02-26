using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    public float playerScore = 100f;
    public float timer = 0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI infoText;
    public InputActionReference resetActionReference; // Reference to the Reset input action

    public Material blackMaterial;
    public Material whiteMaterial;
    public Camera planeCamera;
    public Camera followCamera;

    public GameObject ringPrefab;

    public GameObject player;

    private void OnEnable()
    {
        if (resetActionReference != null)
        {
            resetActionReference.action.Enable();
            resetActionReference.action.performed += OnReset;
        }
    }

    private void OnDisable()
    {
        if (resetActionReference != null)
        {
            resetActionReference.action.performed -= OnReset;
            resetActionReference.action.Disable();
        }
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerScore = 100f;
        timer = 0f;

        UpdateScore(0);
        UpdateMaterials("Black");
    }

    private void Update()
    {
        updateTimer();
    }

    public void updateTimer()
    {
        timer += Time.deltaTime;

        if (playerScore > 0)
        {
            TimeSpan time = TimeSpan.FromSeconds(timer);
            timeText.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");

            if (playerScore <= 50)
            {
                UpdateMaterials("White");
            }
        }
        else
        {
            timeText.color = Color.green;
            scoreText.color = Color.green;
        }
    }

    public void UpdateMaterials(string materialType)
    {
        if (materialType == "White")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<Renderer>().material.color == Color.black)
                {
                    obj.GetComponent<Renderer>().material = whiteMaterial;
                    obj.transform.Find("Black Particles").gameObject.SetActive(false);
                    obj.transform.Find("White Particles").gameObject.SetActive(true);
                }
            }

            planeCamera.GetComponent<Camera>().backgroundColor = Color.black;
            followCamera.GetComponent<Camera>().backgroundColor = Color.black;

            player.GetComponent<Renderer>().material = whiteMaterial;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;

            scoreText.color = Color.white;
            timeText.color = Color.white;
            infoText.color = Color.white;
        }
        else if (materialType == "Black")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<Renderer>().material.color == Color.white)
                {
                    obj.GetComponent<Renderer>().material = blackMaterial;
                    obj.transform.Find("Black Particles").gameObject.SetActive(true);
                    obj.transform.Find("White Particles").gameObject.SetActive(false);
                }
            }

            planeCamera.GetComponent<Camera>().backgroundColor = Color.white;
            followCamera.GetComponent<Camera>().backgroundColor = Color.white;

            player.GetComponent<Renderer>().material = blackMaterial;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;

            scoreText.color = Color.black;
            timeText.color = Color.black;
            infoText.color = Color.black;
        }
    }

    public void UpdateScore(float scoreChange)
    {
        playerScore += scoreChange;
        scoreText.text = playerScore.ToString() + " REMAIN.";
    }
}
