
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioMundo : Colisiones
{
    public int TargetScene;

    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerMovement playercomp = col.gameObject.GetComponent<playerMovement>();

            if (TargetScene == 9 && playercomp.boss1 && playercomp.boss2)
            {
                Debug.Log("aaaa");
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                playercomp.ChangeScene(TargetScene, currentSceneIndex);
                SceneManager.LoadScene(TargetScene);
            }
            else if (!(TargetScene == 9))
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                playercomp.ChangeScene(TargetScene, currentSceneIndex);
                SceneManager.LoadScene(TargetScene);
            }

        }
    }
}