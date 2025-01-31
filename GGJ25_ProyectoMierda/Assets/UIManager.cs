using System.Collections;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject Scoreboard;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject cinematicaFinal;

    public bool continueButton = false;

    public void ActivarMenu()
    {
        Menu.SetActive(true);
    }
    public void DesactivarMenu()
    {
        Menu.SetActive(false);
    }

    public void Continue()
    {
        continueButton = true;
    }

    public void ActivarHUD()
    {
        HUD.SetActive(true);
    }
    public void DesactivarHUD()
    {
        HUD.SetActive(false);
    }

    public void ActivarScoreboard(int updateScore)
    {
        Scoreboard.SetActive(true);
        score.text = updateScore.ToString();
    }
    public void DesactivarScoreboard()
    {
        Scoreboard.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.Instance.enabled = false;

        cinematicaFinal.GetComponent<PlayableDirector>().Play();

        // Esperar 20 segundos antes de recargar la escena
        StartCoroutine(ReloadSceneAfterDelay(18f));
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(SpawnersManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) && continueButton == true)
        {
            StartCoroutine(ReloadSceneAfterDelay(0f));
        }
    }
    // Update is called once per frame

}
