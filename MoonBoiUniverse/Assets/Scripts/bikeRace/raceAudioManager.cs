using UnityEngine;

public class raceAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _AS;
    [SerializeField] AudioClip _BikeStart;
    [SerializeField] AudioClip _BikeBoost;
    [SerializeField] AudioClip _bikeSound;
    [SerializeField] AudioClip _bikeScrecth;

    [SerializeField] BikeController bikeController; // Reference to BikeController script

    // Max speed for full volume
    [SerializeField] float maxSpeedForFullVolume = 30f;

    public static raceAudioManager Inst;
    private void Awake()
    {
        Inst = this;
    }
    void Update()
    {

    }

    bool isStartBikePlaying = false;

    bool _StopBike = false;

    public void PlayBikeOutside()
    {
        _StopBike = false;
        isStartBikePlaying = false;
    }
    public void PlayBike()
    {
        if (_StopBike)
            return;

        float currentSpeed = bikeController.currentVerticalSpeed;
        float volume = Mathf.Clamp(currentSpeed / maxSpeedForFullVolume, 0f, 1f);
        _AS.volume = volume;
        _AS.clip = _bikeSound;

        if (!isStartBikePlaying)
        {
            isStartBikePlaying = true;
            _AS.loop = true;
            _AS.Play();

        }
    }

    public void StopBike()
    {
        _StopBike = true;
        _AS.volume = 1;
    }

    public void PlayScreech()
    {
        StopBike();
        _AS.volume = 1;
        _AS.clip = _bikeScrecth;
        _AS.loop = false;
        _AS.Play();
        Invoke("StopBikeBoost", 0.2f);
    }

    public void PlayBoost()
    {
        _AS.clip = _BikeBoost;
        _AS.loop = true;
        _AS.Play();
    }

    public void StopBikeBoost()
    {
        PlayBikeOutside();
    }

    public void playerIfDead()
    {
        _AS.clip = null;
        _AS.Play();
    }
}
