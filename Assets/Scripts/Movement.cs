using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;

    bool canUseRadar = false;
    public int radarBattery = 10;

    public Image myImg;
    bool myBool = false;

    GameObject[] balls;
    Rigidbody BallsGravity;

    public GameObject plant;
    MeshRenderer plantMesh;
    public Material plantMaterial;
    Light plantLight;
    bool colorChange = false;

    float aValue;
    bool up;
    public float mySpeed;

    float health = 100f;
    public Text healthValue;
    public Image HealthBarFront;
    public Image HealthBarRear;

    GameObject[] platforms;

    GameObject door1;

    void Start()
    {
        door1 = GameObject.FindGameObjectWithTag("Door1");
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        balls = GameObject.FindGameObjectsWithTag("Ball");
        plantMesh = plant.GetComponentInChildren<MeshRenderer>();
        plantLight = plant.GetComponentInChildren<Light>();
        healthValue = GetComponentInChildren<Text>();
        plantLight.enabled = false;

        PlatformController();
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveZ = -Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(moveZ, 0, moveX);

        healthValue.text = health.ToString();

        if(aValue<=0)
        {
            up = true;
        }
        else if (aValue >= 1f)
        {
            up = false;
        }
        if(colorChange)
        {
            changeColor();
        }
        else if(!colorChange)
        {
            plantLight.enabled = false;
        }


        StartCoroutine(Radar());
        Debug.Log(aValue);
        
        if(myBool)
        {
            myImg.fillAmount -= Time.deltaTime / 5f;
        }

        BallController();
        
    }

    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Plant")
        {
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Eye")
        {
            SceneManager.LoadScene(0);
        }

        else if (col.gameObject.tag == "Radar")
        {
            Destroy(col.gameObject);
            canUseRadar = true;
        }

        else if(col.gameObject.tag == "Engel")
        {
            health -= 10f;
            HealthBarFront.fillAmount -= .1f;
        }
        else if (col.gameObject.tag == "Ball")
        {
            health -= 10f;
            HealthBarFront.fillAmount -= .1f;
        }
        else if (col.gameObject.tag == "Gate")
        {
            foreach(GameObject plt in platforms)
            {
                Destroy(plt);
            }
            Destroy(door1);
        }
    }

    IEnumerator Radar()
    {
        if (canUseRadar && Input.GetKey(KeyCode.R))
        {
            myBool = true;
            plantMesh.enabled = true;
            colorChange = true;

            yield return new WaitForSeconds(5f);
            Debug.Log("Battery Died");

            plantMesh.enabled = false;
            colorChange = false;
        }
    }

    void BallController()
    {
        for(int k = 0; k < balls.Length; k++)
        {
            if(this.gameObject.transform.position.z - balls[k].transform.position.z <= 15f)
            {
                for (int i = 0; i < balls.Length; i++)
                {
                    BallsGravity = balls[i].GetComponent<Rigidbody>();
                    BallsGravity.useGravity = true;
                }
            }
        }
    }

    void changeColor()
    {
        plantLight.enabled = true;
        Color color = plantMaterial.color;
        color.a = aValue;
        plantLight.intensity = aValue * 5;
        plantMaterial.color = color;

        if (up)
        {
            aValue += mySpeed * Time.deltaTime;
        }
        else
        {
            aValue -= mySpeed * Time.deltaTime;
        }
    }

   void PlatformController()
   {
        GameObject _platform = platforms[(Random.Range(0, platforms.Length))];

        _platform.gameObject.tag = "Gate";

        Vector3 myPlatform = _platform.transform.position;

        Instantiate(plant, myPlatform, Quaternion.identity);
   }
}
