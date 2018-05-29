using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneList;

    protected override void OnCollide(Collider2D coll)
    {
        //parent call
        //base.OnCollide(coll);

        if(coll.name == "Player")
        {
            //save on teleport
            GameManager.instance.SaveState();
            //teleport to random dungeon
            string scene = sceneList[Random.Range(0, sceneList.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }

}
