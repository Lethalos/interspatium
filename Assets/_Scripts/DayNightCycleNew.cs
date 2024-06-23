using UnityEngine;

public class DayNightCycleNew : MonoBehaviour
{
    public Light directionalLight; // The sun
    public Gradient ambientColor; // Gradient for ambient light over the day
    public AnimationCurve lightIntensityCurve; // Intensity of the light over the day
    public Material daySkybox; // Skybox for daytime
    public Material nightSkybox; // Skybox for nighttime

    private bool isDay = true; // Boolean to track day/night state

    private void Start()
    {
        // Set initial settings
        SetDay();
    }

    void Update()
    {
        // Check for "K" key press to toggle day/night
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    ToggleDayNight();
        //}
    }

    // Method to toggle between day and night
    void ToggleDayNight()
    {
        isDay = !isDay;

        if (isDay)
        {
            SetDay();
        }
        else
        {
            SetNight();
        }
    }

    void SetDay()
    {
        // Set day settings
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(50f, 30f, 0));
        directionalLight.intensity = lightIntensityCurve.Evaluate(1f);
        RenderSettings.ambientLight = ambientColor.Evaluate(0f);
        RenderSettings.skybox = daySkybox;
    }

    void SetNight()
    {
        // Set night settings
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(-50f, 30f, 0));
        directionalLight.intensity = lightIntensityCurve.Evaluate(0.5f);
        RenderSettings.ambientLight = ambientColor.Evaluate(1f);
        RenderSettings.skybox = nightSkybox;
    }
}
