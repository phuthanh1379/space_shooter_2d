using UnityEngine;
using UnityEngine.Playables;

public class PlatformerGameController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject torch;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            director.Play();
        }
    }

    public void LightOn()
    {
        torch.gameObject.SetActive(true);
    }

    public void LightOff()
    {
        torch.gameObject.SetActive(false);
    }
}
