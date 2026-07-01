using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
  public void WalkAndObserve()
  {
    GameData.currentAtmosphere = AtmosphereState.Uneased;
    SceneManager.LoadScene("City");
  }

  public void RunAndSaveYourself()
  {
    GameData.currentAtmosphere = AtmosphereState.Collapsed;
    SceneManager.LoadScene("City");
  }
}
