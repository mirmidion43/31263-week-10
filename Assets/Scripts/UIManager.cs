using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image health = null;
    private Transform pTrans = null;
    private float hp = 1.0f;
    public Camera camera = null;
    private float speed = 20.0f;
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pTrans!=null){
            hp = Mathf.Clamp(((5.0f - Mathf.Abs((float) pTrans.position.x))/5.0f), 0.0f, 1.0f);
            health.fillAmount = hp;
            if(hp<0.5f && health.color == Color.green)
                health.color = Color.red;
            if(hp>0.5f && health.color == Color.red)
                health.color = Color.green;
        }
    }

    void LateUpdate() {
        if(camera!=null){
            Vector3 rotation = camera.transform.eulerAngles;
            rotation.y += Input.GetAxis("CamMove") * speed * Time.deltaTime;
            camera.transform.eulerAngles = rotation;
        }
        
     
    }

    public void LoadFirstLevel(){
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void QuitGame(){
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Button button = null;

        if(scene.buildIndex == 1){
            button = GameObject.FindWithTag("QuitButton").GetComponent<Button>();
            health = GameObject.FindWithTag("PlayerHealthBar").GetComponent<Image>();
            pTrans = GameObject.FindWithTag("Player").transform;
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if(button!=null)
            button.onClick.AddListener(QuitGame);
    }
}
