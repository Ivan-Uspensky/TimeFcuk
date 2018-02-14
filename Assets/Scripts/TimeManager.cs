using UnityEngine;

public class TimeManager : MonoBehaviour {
  public float slowdownFactor = 2f;
  // public float slowdownLength = 2f;

  // void Update() {
  //   Time.timeScale += (1f / slowdownLength) * Time.deltaTime;
  //   Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
  // }
  
  public void DoSlow() {
    Time.timeScale = 1 / slowdownFactor;
    Time.fixedDeltaTime = Time.timeScale * 0.02f;
  }

  public void DoFast() {
    Time.timeScale = 1f;
  }

}
