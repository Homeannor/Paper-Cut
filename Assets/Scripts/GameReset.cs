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
    public TextMeshProUGUI scoreTextFollow;
    public TextMeshProUGUI timeTextFollow;
    public TextMeshProUGUI infoTextFollow;
    public InputActionReference resetActionReference; // Reference to the Reset input action

    public Material blackMaterial;
    public Material whiteMaterial;
    public Material redMaterial;
    public Material greenMaterial;
    public Camera planeCamera;
    public Camera followCamera;
    public Material blackSkybox;
    public Material whiteSkybox;

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

        gameObject.GetComponent<AudioSource>().Play();
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
            timeText.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00") + "." + time.Milliseconds.ToString("000");
            timeTextFollow.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00") + "." + time.Milliseconds.ToString("000");

            if (playerScore <= 10)
            {
                UpdateMaterials("Red");
            }
            else if (playerScore <= 50)
            {
                UpdateMaterials("White");
            }
        }
        else
        {
            UpdateMaterials("Green");
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
                    obj.transform.Find("Void Cube").gameObject.GetComponent<Renderer>().material = whiteMaterial;
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Black Particles").gameObject.SetActive(false);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("White Particles").gameObject.SetActive(true);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Red Particles").gameObject.SetActive(false);
                }
            }

            planeCamera.GetComponent<Camera>().backgroundColor = Color.black;
            followCamera.GetComponent<Camera>().backgroundColor = Color.black;

            RenderSettings.skybox = blackSkybox;
            DynamicGI.UpdateEnvironment();

            player.GetComponent<Renderer>().material = whiteMaterial;
            player.GetComponent<PlaneController>().minSpeed = 60f;
            player.GetComponent<PlaneController>().maxSpeed = 60f;

            planeCamera.GetComponent<Camera>().fieldOfView = 80f;

            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;

            scoreText.color = Color.white;
            timeText.color = Color.white;
            infoText.color = Color.white;
            scoreTextFollow.color = Color.white;
            timeTextFollow.color = Color.white;
            infoTextFollow.color = Color.white;
        }
        else if (materialType == "Black")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<Renderer>().material.color == Color.white)
                {
                    obj.transform.Find("Void Cube").gameObject.GetComponent<Renderer>().material = blackMaterial;
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Black Particles").gameObject.SetActive(true);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("White Particles").gameObject.SetActive(false);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Red Particles").gameObject.SetActive(false);
                }
            }

            planeCamera.GetComponent<Camera>().backgroundColor = Color.white;
            followCamera.GetComponent<Camera>().backgroundColor = Color.white;

            RenderSettings.skybox = whiteSkybox;
            DynamicGI.UpdateEnvironment();

            player.GetComponent<Renderer>().material = blackMaterial;
            player.GetComponent<PlaneController>().minSpeed = 30f;
            player.GetComponent<PlaneController>().maxSpeed = 30f;

            planeCamera.GetComponent<Camera>().fieldOfView = 60f;

            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;

            scoreText.color = Color.black;
            timeText.color = Color.black;
            infoText.color = Color.black;
            scoreTextFollow.color = Color.white;
            timeTextFollow.color = Color.white;
            infoTextFollow.color = Color.white;
        }
        else if (materialType == "Red")
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<Renderer>().material.color == Color.black)
                {
                    obj.transform.Find("Void Cube").gameObject.GetComponent<Renderer>().material = redMaterial;
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Black Particles").gameObject.SetActive(false);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("White Particles").gameObject.SetActive(false);
                    obj.transform.Find("Void Cube").gameObject.transform.Find("Red Particles").gameObject.SetActive(true);
                }
            }

            planeCamera.GetComponent<Camera>().backgroundColor = Color.black;
            followCamera.GetComponent<Camera>().backgroundColor = Color.black;

            player.GetComponent<Renderer>().material = redMaterial;
            player.GetComponent<PlaneController>().minSpeed = 150f;
            player.GetComponent<PlaneController>().maxSpeed = 150f;

            planeCamera.GetComponent<Camera>().fieldOfView = 100f;

            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;

            scoreText.color = Color.red;
            timeText.color = Color.red;
            infoText.color = Color.red;
            scoreTextFollow.color = Color.red;
            timeTextFollow.color = Color.red;
            infoTextFollow.color = Color.red;
        }
        else if (materialType == "Green")
        {
            planeCamera.GetComponent<Camera>().backgroundColor = Color.black;
            followCamera.GetComponent<Camera>().backgroundColor = Color.black;

            player.GetComponent<Renderer>().material = greenMaterial;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Black Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("White Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 0f;
            player.transform.Find("Red Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 0f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().startWidth = 1f;
            player.transform.Find("Green Trail").gameObject.GetComponent<TrailRenderer>().endWidth = 1f;

            scoreText.color = Color.green;
            timeText.color = Color.green;
            infoText.color = Color.green;
            scoreTextFollow.color = Color.green;
            timeTextFollow.color = Color.green;
            infoTextFollow.color = Color.green;
        }
    }

    public void UpdateScore(float scoreChange)
    {
        playerScore += scoreChange;
        scoreText.text = playerScore.ToString() + " REMAIN.";
        scoreTextFollow.text = playerScore.ToString() + " REMAIN.";
    }
}
