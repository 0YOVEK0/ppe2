using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    public int enemiesToKill = 5; // Número de enemigos que el jugador debe matar
    private int enemiesKilled = 0;

    public GameObject areaToUnlock; // Zona que se desbloqueará
    public Text enemyCounterText; // Referencia al texto del contador de enemigos

    void Start()
    {
        // Asegúrate de que la zona esté bloqueada al inicio del juego
        areaToUnlock.SetActive(true);
        UpdateEnemyCounterUI();
        Debug.Log("GameCore initialized. Enemies to kill: " + enemiesToKill);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        Debug.Log("Enemy killed. Total killed: " + enemiesKilled);
        UpdateEnemyCounterUI();
        CheckEnemiesKilled();
    }

    void UpdateEnemyCounterUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = "Enemigos eliminados: " + enemiesKilled + "/" + enemiesToKill;
        }
        Debug.Log("Enemy counter UI updated: " + enemiesKilled + "/" + enemiesToKill);
    }

    void CheckEnemiesKilled()
    {
        if (enemiesKilled >= enemiesToKill)
        {
            UnlockArea();
        }
    }

    void UnlockArea()
    {
        areaToUnlock.SetActive(false);
        Debug.Log("Zona desbloqueada!");
    }
}
