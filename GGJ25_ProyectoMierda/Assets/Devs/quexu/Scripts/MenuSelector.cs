using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuSelector : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TMP_Text titulo;
    [SerializeField] private Button jugar;
    [SerializeField] private Button opciones;
    [SerializeField] private Button salir;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        jugar.onClick.AddListener(Jugar);
        opciones.onClick.AddListener(Opciones);
        salir.onClick.AddListener(Salir);
    }

    private void Jugar()
    {
        Debug.Log("Jugar");
        StartCoroutine(FadeOutTexts(() =>
        {
            gameObject.GetComponentInParent<UIManager>().DesactivarMenu();
            gameObject.GetComponentInParent<UIManager>().ActivarHUD();
            gameObject.GetComponentInParent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }));
    }

    private System.Collections.IEnumerator FadeOutTexts(System.Action onFadeComplete)
    {
        TMP_Text jugarText = jugar.GetComponentInChildren<TMP_Text>();
        TMP_Text opcionesText = opciones.GetComponentInChildren<TMP_Text>();
        TMP_Text salirText = salir.GetComponentInChildren<TMP_Text>();

        float elapsedTime = 0f;

        Color tituloOriginalColor = titulo.color;
        Color jugarOriginalColor = jugarText.color;
        Color opcionesOriginalColor = opcionesText.color;
        Color salirOriginalColor = salirText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            titulo.color = new Color(tituloOriginalColor.r, tituloOriginalColor.g, tituloOriginalColor.b, alpha);
            jugarText.color = new Color(jugarOriginalColor.r, jugarOriginalColor.g, jugarOriginalColor.b, alpha);
            opcionesText.color = new Color(opcionesOriginalColor.r, opcionesOriginalColor.g, opcionesOriginalColor.b, alpha);
            salirText.color = new Color(salirOriginalColor.r, salirOriginalColor.g, salirOriginalColor.b, alpha);

            yield return null; 
        }

        onFadeComplete?.Invoke();
    }
    private void Opciones()
    {
        Debug.Log("Abriendo el men� de opciones...");
    }

    private void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void Update()
    {
        
    }
}
