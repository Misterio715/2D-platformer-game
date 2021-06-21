using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Text kunaiScore;
    [SerializeField]
    private Image image;

    public static Queue<Image> healthImage;
 
    // Start is called before the first frame update
    void Start()
    {
        int healthAmount = player.GetComponent<PlayerBehaviour>().playerHealth;
        healthImage = new Queue<Image>();
        healthImage.Enqueue(image);
        float prevImagePos = image.transform.position.x;
        for (int i = 1; i < healthAmount; i++)
        {
            Image newImage = Instantiate(image, new Vector3(prevImagePos - 80f, image.transform.position.y), Quaternion.identity);
            newImage.transform.SetParent(canvas.transform);
            prevImagePos = newImage.transform.position.x;
            healthImage.Enqueue(newImage);
        }

        kunaiScore.text = player.GetComponent<PlayerBehaviour>().kunaiAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        kunaiScore.text = player.GetComponent<PlayerBehaviour>().kunaiAmount.ToString();
    }

    public static void LoadSceneFromGame(int index)
    {
        SceneManager.LoadScene(index);
    }
}
